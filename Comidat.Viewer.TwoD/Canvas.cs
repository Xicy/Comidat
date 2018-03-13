using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using Comidat.Data.Model;
using Comidat.Model;

namespace Comidat
{
    public partial class Canvas : UserControl
    {
        public Canvas(Image floor, List<List<Vector3d>> route)
        {
            InitializeComponent();
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            comboBox1.DataSource = Global.Database.TBLTags.Local.ToBindingList();
            comboBox1.DisplayMember = "TagFullName";
            this.route = route;
            this.floor = floor;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.ScaleTransform((float)scale, (float)scale);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            if (isDraging)
                e.Graphics.TranslateTransform(drag.X + drag_delta.X, drag.Y + drag_delta.Y);
            else
                e.Graphics.TranslateTransform(drag.X, drag.Y);
            base.OnPaint(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            isDraging = isHold;
            if (isDraging)
            {
                drag_delta = new Point((int)((e.X - down.X) / scale), (int)((e.Y - down.Y) / scale));
                UpdateStyles();
            }
            else
            if (tags != null && tags.Length > 0)
            {
                var loc = new Point((int)(e.Location.X / scale), (int)(e.Location.Y / scale));
                shownTag.Clear();
                foreach (var tag in tags)
                {
                    if (!new Rectangle(tag.d_XPosition + drag.X - 3, tag.d_yPosition + drag.Y - 3, 7, 7).Contains(new Point(loc.X, loc.Y))) continue;
                    var ta = Global.Tags.FirstOrDefault(t => t.Id == tag.TagId);
                    if (ta == null) continue;
                    shownTag.Add(ta);
                }
                if (shownTag.Count > 0) UpdateStyles();
            }
            base.OnMouseMove(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            down = e.Location;
            isHold = true;
            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            isHold = false;
            if (isDraging)
            {
                drag = new Point(drag.X + drag_delta.X, drag.Y + drag_delta.Y);
                isDraging = false;
            }
            base.OnMouseUp(e);
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            scale = e.Delta > 0 ? scale * 1.1 : scale / 1.1;
            UpdateStyles();
            base.OnMouseWheel(e);
        }

        private Point down = Point.Empty;
        private bool isHold = false;
        private bool isDraging = false;
        private bool isDrawRoute = false;
        private Point drag_delta = new Point(0, 0);
        private Point drag = new Point(0, 0);
        private double scale = 1;
        private List<TBLTag> shownTag = new List<TBLTag>();
        private TBLTag selectedTag = null;
        private Image floor;
        private TBLPosition[] tags;
        private List<List<Vector3d>> route;

        public void UpdateTags(TBLPosition[] tags)
        {
            this.tags = tags;
            UpdateStyles();
        }

        public void UpdateRoute(List<List<Vector3d>> route)
        {
            this.route = route;
            UpdateStyles();
        }

        public void UpdateImage(Image image)
        {
            this.floor = image;
            UpdateStyles();
        }

        private void Canvas_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImageUnscaled(floor, Point.Empty);

            if (isDrawRoute)
                for (int i = 0; i < route.Count; i++)
                {
                    for (int j = 0; j < route[i].Count - 1; j++)
                    {
                        e.Graphics.DrawLine(Pens.Red, (float)route[i][j].X, (float)route[i][j].Y, (float)route[i][j + 1].X, (float)route[i][j + 1].Y);
                    }
                }

            if (tags != null)
                foreach (var tag in tags)
                {
                    CalculatePositionFromLine(tag);
                    if (selectedTag != null && tag.TagId == selectedTag.Id)
                        e.Graphics.DrawEllipse(new Pen(Color.YellowGreen,4), tag.d_XPosition - 3, tag.d_yPosition - 3, 8, 8);
                    else
                        e.Graphics.DrawEllipse(Pens.OrangeRed, tag.d_XPosition - 3, tag.d_yPosition - 3, 7, 7);
                }
            if (shownTag.Count > 0)
                for (var i = 0; i < shownTag.Count; i++)
                {
                    var tag = shownTag[i];
                    var tagLoc = tags.First(t => t.TagId == tag.Id);
                    e.Graphics.DrawString($"{tag.TagFullName}", DefaultFont, Brushes.OrangeRed, tagLoc.d_XPosition - 3, tagLoc.d_yPosition - 3 - 12 * (i + 1));
                }
        }

        private void CalculatePositionFromLine(TBLPosition tag)
        {
            Vector3d lastPos = Vector3d.One * int.MaxValue;
            double lastLen = double.MaxValue;
            Vector3d currPos;

            var pos = new Vector3d(tag.d_XPosition, tag.d_yPosition, 0);

            foreach (var rot in route)
            {
                currPos = Runtime.Helper.NearestPointOnLine(rot.ToArray(), pos);
                if (lastLen > (lastLen = Vector3d.Subtract(currPos, pos).Length))
                    lastPos = currPos;
            }
            tag.d_XPosition = (int)lastPos.X;
            tag.d_yPosition = (int)lastPos.Y;
        }

        private void comboBox1_SelectedValueChanged(object sender, System.EventArgs e)
        {
            selectedTag = (TBLTag)comboBox1.SelectedItem;
            UpdateStyles();
        }
    }
}
