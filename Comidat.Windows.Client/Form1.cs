using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Comidat.Data;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Comidat.Windows.Client
{
    public partial class Form1 : Form
    {
        private static readonly DatabaseContext Database = new DatabaseContext();
        private readonly BaseFont baseFont;
        public Form1()
        {
            InitializeComponent();
            baseFont = BaseFont.CreateFont(Environment.GetEnvironmentVariable("windir") + @"\fonts\Arial.TTF", BaseFont.IDENTITY_H, true);
            dateTimePicker.MinDate = Database.TBLPositions.Min(p => p.RecordDateTime);
            dateTimePicker.MaxDate = Database.TBLPositions.Min(p => p.RecordDateTime);
            dateTimePicker.Value = DateTime.Now.Date == dateTimePicker.MaxDate ? DateTime.Today : dateTimePicker.MaxDate;
        }

        private void exportButton_Click(object sender, EventArgs e)
        {

            using (SaveFileDialog sfd = new SaveFileDialog())
            using (MemoryStream ms = new MemoryStream())
            using (Document document = new Document(PageSize.A4, 25, 25, 30, 30))
            using (PdfWriter writer = PdfWriter.GetInstance(document, ms))
            {
                sfd.Filter = @"PDF|*.pdf";
                if (sfd.ShowDialog() != DialogResult.OK) return;

                var test = Database.TBLPositions
                    .Where(p => p.RecordDateTime.Date == dateTimePicker.Value.Date)
                    .OrderBy(p => p.TagId)
                    .ThenByDescending(p => p.RecordDateTime)
                    .Join(Database.TBLMaps, p => p.MapId, m => m.Id,
                        (p, m) => new { Position = p, Map = m }) //Join Map
                    .Join(Database.TBLTags, p => p.Position.TagId, t => t.Id,
                        (p, t) => new { p.Position, p.Map, Tag = t }); //Join Tags


                document.Open();
                writer.DirectContent.SetFontAndSize(baseFont, 12);
                PdfPTable table = new PdfPTable(6);
                table.AddCell("isim");
                table.AddCell("Soyisim");
                table.AddCell("Harita");
                table.AddCell("X");
                table.AddCell("Y");
                table.AddCell("Saat");
                foreach (var obj in test)
                {
                    table.AddCell(obj.Tag.TagFirstName);
                    table.AddCell(obj.Tag.TagLastName);
                    table.AddCell(obj.Map.MapName);
                    table.AddCell(obj.Position.d_XPosition.ToString());
                    table.AddCell(obj.Position.d_yPosition.ToString());
                    table.AddCell(obj.Position.RecordDateTime.TimeOfDay.ToString(@"hh\:mm\:ss"));
                }

                document.Add(table);
                document.Close();
                writer.Close();
                ms.Close();

                File.WriteAllBytes(sfd.FileName, ms.GetBuffer());
            }
            MessageBox.Show("Dışarı Aktarma Başarılı Birşekilde Tamamlandı.", "Dışarı Aktarma", MessageBoxButtons.OK);
        }
    }
}