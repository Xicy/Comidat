using System;
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
        private static DatabaseContext _database;
        private readonly BaseFont _baseFont;
        public Form1()
        {
            InitializeComponent();

            _database = new DatabaseContext();
            _baseFont = BaseFont.CreateFont(Environment.GetEnvironmentVariable("windir") + @"\fonts\Arial.TTF", BaseFont.IDENTITY_H, true);

            dateTimePickerFirst.MinDate = _database.TBLPositions.Min(p => p.RecordDateTime);
            dateTimePickerFirst.MaxDate = _database.TBLPositions.Min(p => p.RecordDateTime);
            dateTimePickerFirst.Value = DateTime.Now.Date == dateTimePickerFirst.MinDate ? DateTime.Today : dateTimePickerFirst.MinDate;

            dateTimePickerLast.MinDate = _database.TBLPositions.Min(p => p.RecordDateTime);
            dateTimePickerLast.MaxDate = _database.TBLPositions.Min(p => p.RecordDateTime);
            dateTimePickerLast.Value = DateTime.Now.Date == dateTimePickerLast.MaxDate ? DateTime.Today : dateTimePickerLast.MaxDate;
        }

        private void ExportButton_Click(object sender, EventArgs e)
        {

            using (SaveFileDialog sfd = new SaveFileDialog())
            using (MemoryStream ms = new MemoryStream())
            using (Document document = new Document(PageSize.A4, 25, 25, 30, 30))
            using (PdfWriter writer = PdfWriter.GetInstance(document, ms))
            {
                sfd.Filter = @"PDF|*.pdf";
                if (sfd.ShowDialog() != DialogResult.OK) return;

                var test = _database.TBLPositions
                    .Where(p => p.RecordDateTime.Date >= dateTimePickerFirst.Value.Date && p.RecordDateTime.Date <= dateTimePickerLast.Value.Date)
                    .OrderBy(p => p.TagId)
                    .ThenByDescending(p => p.RecordDateTime)
                    .Join(_database.TBLMaps, p => p.MapId, m => m.Id,
                        (p, m) => new { Position = p, Map = m }) //Join Map
                    .Join(_database.TBLTags, p => p.Position.TagId, t => t.Id,
                        (p, t) => new { p.Position, p.Map, Tag = t }); //Join Tags


                document.Open();
                writer.DirectContent.SetFontAndSize(_baseFont, 12);
                PdfPTable table = new PdfPTable(6);
                table.AddCell("isim");
                table.AddCell("Soyisim");
                table.AddCell("Harita");
                table.AddCell("X");
                table.AddCell("Y");
                table.AddCell("Tarih");
                foreach (var obj in test)
                {
                    table.AddCell(obj.Tag.TagFirstName);
                    table.AddCell(obj.Tag.TagLastName);
                    table.AddCell(obj.Map.MapName);
                    table.AddCell(obj.Position.d_XPosition.ToString());
                    table.AddCell(obj.Position.d_yPosition.ToString());
                    table.AddCell(obj.Position.RecordDateTime.ToString("g"));
                }

                document.Add(table);
                //document.Close();
                //writer.Close();
                //ms.Close();

                File.WriteAllBytes(sfd.FileName, ms.GetBuffer());
            }
            MessageBox.Show(@"Dışarı Aktarma Başarılı Birşekilde Tamamlandı.", @"Dışarı Aktarma", MessageBoxButtons.OK);
        }
    }
}