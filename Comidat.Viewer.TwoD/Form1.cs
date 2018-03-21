using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Comidat.IO;
using Comidat.Model;
using Microsoft.EntityFrameworkCore;

namespace Comidat
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            string[ /*Kat*/] map = new string[10];
            List<List<List<Vector3d>>> lines = new List<List<List<Vector3d>>>();

            int floor = -1, lineSub = 0;
            var fr = new FileReader(Path.Combine(Environment.CurrentDirectory, "map.data"));
            foreach (var line in fr)
            {
                /*
                 * Map0.jpg
                 * Line0Route0X Line0Route0Y Line0Route1X Line0Route1Y ...
                 * Line1Route0X Line1Route0Y Line1Route1X Line1Route1Y ...
                 */
                if (line.Value.StartsWith("map", true, CultureInfo.InvariantCulture))
                {
                    floor++;
                    lines.Add(new List<List<Vector3d>>());
                    map[floor] = line.Value;
                    lineSub = 0;
                }
                else
                {
                    var a = line.Value.Split(' ');
                    lines[floor].Add(new List<Vector3d>());
                    for (int i = 0; i < a.Length - 1; i += 2)
                        lines[floor][lineSub].Add(new Vector3d(double.Parse(a[i]), double.Parse(a[i + 1]), 0));
                    lineSub++;
                }
            }

            var canvas = new Canvas(Image.FromFile(Path.Combine(Environment.CurrentDirectory, map[0])), lines[0]) { Dock = DockStyle.Fill };
            Controls.Add(canvas);

            Task.Run(async () =>
            {
                while (true)
                {
                    var tblPositions = Global.Database.TBLPositions.AsNoTracking()
                        .Where(position => position.RecordDateTime > DateTime.Now.AddMinutes(-10))
                        .OrderByDescending(position => position.RecordDateTime).GroupBy(position => position.TagId)
                        .Select(grouping => grouping.First());
                    var a = tblPositions.ToArray();
                    canvas.BeginInvoke((Action)(() => { canvas.UpdateTags(a); }));
                    await Task.Delay(5000);
                }
            });


        }
    }
}
