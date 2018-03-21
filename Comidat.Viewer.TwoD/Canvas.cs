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
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            comboBox1.DataSource = Global.Database.TBLTags.Local.ToBindingList();
            comboBox1.DisplayMember = "TagFullName";
            _route = route;
            _floor = floor;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.ScaleTransform((float)_scale, (float)_scale);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            if (_isDraging)
                e.Graphics.TranslateTransform(_drag.X + _dragDelta.X, _drag.Y + _dragDelta.Y);
            else
                e.Graphics.TranslateTransform(_drag.X, _drag.Y);
            base.OnPaint(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            _isDraging = _isHold;
            if (_isDraging)
            {
                _dragDelta = new Point((int)((e.X - _down.X) / _scale), (int)((e.Y - _down.Y) / _scale));
                UpdateStyles();
            }
            else
            if (_tags != null && _tags.Length > 0)
            {
                var loc = new Point((int)(e.Location.X / _scale), (int)(e.Location.Y / _scale));
                _shownTag.Clear();
                foreach (var tag in _tags)
                {
                    if (!new Rectangle(tag.d_XPosition + _drag.X - 3, tag.d_yPosition + _drag.Y - 3, 7, 7).Contains(new Point(loc.X, loc.Y))) continue;
                    var ta = Global.Tags.FirstOrDefault(t => t.Id == tag.TagId);
                    if (ta == null) continue;
                    _shownTag.Add(ta);
                }
                if (_shownTag.Count > 0) UpdateStyles();
            }
            base.OnMouseMove(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            _down = e.Location;
            _isHold = true;
            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            _isHold = false;
            if (_isDraging)
            {
                _drag = new Point(_drag.X + _dragDelta.X, _drag.Y + _dragDelta.Y);
                _isDraging = false;
            }
            base.OnMouseUp(e);
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            _scale = e.Delta > 0 ? _scale * 1.1 : _scale / 1.1;
            UpdateStyles();
            base.OnMouseWheel(e);
        }

        private Point _down = Point.Empty;
        private bool _isHold;
        private bool _isDraging;
        private bool _isDrawRoute = false;
        private Point _dragDelta = new Point(0, 0);
        private Point _drag = new Point(0, 0);
        private double _scale = 1;
        private readonly List<TBLTag> _shownTag = new List<TBLTag>();
        private TBLTag _selectedTag;
        private Image _floor;
        private TBLPosition[] _tags;
        private List<List<Vector3d>> _route;

        public void UpdateTags(TBLPosition[] tags)
        {
            _tags = tags;
            UpdateStyles();
        }

        public void UpdateRoute(List<List<Vector3d>> route)
        {
            _route = route;
            UpdateStyles();
        }

        public void UpdateImage(Image image)
        {
            _floor = image;
            UpdateStyles();
        }

        private void Canvas_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImageUnscaled(_floor, Point.Empty);

            if (_isDrawRoute)
                for (int i = 0; i < _route.Count; i++)
                {
                    for (int j = 0; j < _route[i].Count - 1; j++)
                    {
                        e.Graphics.DrawLine(Pens.Red, (float)_route[i][j].X, (float)_route[i][j].Y, (float)_route[i][j + 1].X, (float)_route[i][j + 1].Y);
                    }
                }

            if (_tags != null)
                foreach (var tag in _tags)
                {
                    CalculatePositionFromLine(tag);
                    if (_selectedTag != null && tag.TagId == _selectedTag.Id)
                        e.Graphics.DrawEllipse(new Pen(Color.YellowGreen, 4), tag.d_XPosition - 3, tag.d_yPosition - 3, 8, 8);
                    else
                        e.Graphics.DrawEllipse(Pens.OrangeRed, tag.d_XPosition - 3, tag.d_yPosition - 3, 7, 7);
                }
            if (_shownTag.Count > 0)
                for (var i = 0; i < _shownTag.Count; i++)
                {
                    var tag = _shownTag[i];
                    var tagLoc = _tags?.First(t => t.TagId == tag.Id);
                    if (tagLoc != null)
                        e.Graphics.DrawString($"{tag.TagFullName}", DefaultFont, Brushes.OrangeRed, tagLoc.d_XPosition - 3, tagLoc.d_yPosition - 3 - 12 * (i + 1));
                }
        }

        private void CalculatePositionFromLine(TBLPosition tag)
        {
            Vector3d lastPos = Vector3d.One * int.MaxValue;
            double lastLen = double.MaxValue;
            Vector3d currPos;

            var pos = new Vector3d(tag.d_XPosition, tag.d_yPosition, 0);

            foreach (var rot in _route)
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
            _selectedTag = (TBLTag)comboBox1.SelectedItem;
            UpdateStyles();
        }
    }
}
