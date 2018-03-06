using System.Collections.Generic;
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
            if (isDraging)
            {
                drag_delta = new Point(e.X - down.X, e.Y - down.Y);
                UpdateStyles();
            }

            base.OnMouseMove(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            down = e.Location;
            isDraging = true;
            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            isDraging = false;
            drag = new Point(drag.X + drag_delta.X, drag.Y + drag_delta.Y);
            if (tags != null && tags.Length > 0)
                foreach (var tag in tags)
                {
                    var loc = (e.Location);
                    if (new Rectangle(tag.d_XPosition, tag.d_yPosition, 7, 7).Contains(new Point(loc.X - drag.X, loc.Y - drag.Y)))
                    {
                        var ta = Global.Tags.FirstOrDefault(t => t.Id == tag.TagId);
                        MessageBox.Show($"Isim:{ta.TagFirstName} \nSoyisim:{ta.TagLastName}\nTC:{ta.TagTCNo}\nTelefon:{ta.TagMobilTelephone}", "Çalışan Bilgisi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
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
        private bool isDraging = false;
        private bool isDrawRoute = true;
        private Point drag_delta = new Point(0, 0);
        private Point drag = new Point(0, 0);
        private double scale = 1;

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
                    e.Graphics.DrawEllipse(Pens.OrangeRed, tag.d_XPosition, tag.d_yPosition, 7, 7);
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

    }
}
