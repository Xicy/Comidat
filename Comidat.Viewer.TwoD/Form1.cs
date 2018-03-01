using System;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using Comidat.IO;
using Comidat.Model;

namespace Comidat
{
    public partial class Form1 : Form
    {
        private FileReader fr;

        public Form1()
        {
            InitializeComponent();
            string[ /*Kat*/] map = new string[10];
            Vector3d[/*Kat*/][/*Ayırım*/][/*Konum*/] lines = (Vector3d[][][])Array.CreateInstance(typeof(Vector3d), 10, 100, 100);
            int floor = 0,lineSub = 0;
            fr = new FileReader(Path.Combine(Environment.CurrentDirectory, "map.data"));
            foreach (var line in fr)
            {
                /*
                 * Map0.jpg
                 * Line0Route0X Line0Route0Y Line0Route1X Line0Route1Y ...
                 * Line1Route0X Line1Route0Y Line1Route1X Line1Route1Y ...
                 */
                if (line.Value.StartsWith("map", true, CultureInfo.InvariantCulture))
                {
                    map[floor++] = line.Value;
                    lineSub = 0;
                }
                else
                {

                    lineSub++;
                }
            }
        }
    }
}
