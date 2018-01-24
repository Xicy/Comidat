using System;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Comidat.Data;
using Comidat.Windows.Reporter;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Comidat.Windows.Client
{
    public partial class Form1 : Form
    {
        private static DatabaseContext _database;
        private readonly Font _baseFont;
        private readonly Font _baseFontBold;
        private readonly Font _headerFont;
        public Form1()
        {
            InitializeComponent();

            _database = new DatabaseContext();

            _baseFont = FontFactory.GetFont(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "arial.ttf"), "Cp1254", true, 12, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            _baseFontBold = FontFactory.GetFont(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "arial.ttf"), "Cp1254", true, 12, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            _headerFont = FontFactory.GetFont(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "arial.ttf"), "Cp1254", true, 14, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);

            DateTime minDate = _database.TBLPositions.Min(p => p.RecordDateTime);
            DateTime maxDate = _database.TBLPositions.Max(p => p.RecordDateTime);

            dateTimePickerFirst.MinDate = minDate;
            dateTimePickerFirst.MaxDate = maxDate;
            dateTimePickerFirst.Value = DateTime.Now.Date == minDate ? DateTime.Today : minDate;

            dateTimePickerLast.MinDate = minDate;
            dateTimePickerLast.MaxDate = maxDate;
            dateTimePickerLast.Value = maxDate;
        }

        private IElement HeaderOfCompany(string name)
        {
            //Write Header
            PdfPTable table = new PdfPTable(2) { WidthPercentage = 100 };
            table.SetWidths(new[] { 45, 550 });
            var img = Image.GetInstance(Embed.Logo, ImageFormat.Bmp);
            img.ScaleAbsolute(45, 45);
            table.AddCell(new PdfPCell(img, false) { BorderWidth = 0 });
            table.AddCell(new PdfPCell(new Phrase(name, _headerFont)) { VerticalAlignment = Element.ALIGN_MIDDLE, HorizontalAlignment = Element.ALIGN_CENTER, BorderWidth = 0 });
            table.SpacingAfter = 16;
            return table;
        }

        private void ExportButton_Click(object sender, EventArgs e)
        {

            using (SaveFileDialog sfd = new SaveFileDialog())
            using (MemoryStream ms = new MemoryStream())
            using (Document document = new Document(PageSize.A4, 36F, 36F, 36F, 36F))
            using (PdfWriter writer = PdfWriter.GetInstance(document, ms))
            {
                sfd.Filter = @"PDF|*.pdf";
                if (sfd.ShowDialog() != DialogResult.OK) return;

                //Open PDF Document
                document.Open();
                document.Add(HeaderOfCompany("Comidat Personal Takip Raporu"));

                PdfPTable table = new PdfPTable(2) { WidthPercentage = 100 };
                table.AddCell(new PdfPCell(new Phrase { new Chunk("Tarih Aralığı", _baseFontBold), new Chunk($":{dateTimePickerFirst.Value:d} - {dateTimePickerLast.Value:d}", _baseFont) }) { BorderWidth = 0 });
                table.AddCell(new PdfPCell(new Phrase { new Chunk("Rapor Tarihi", _baseFontBold), new Chunk($":{DateTime.Now:d}", _baseFont) }) { BorderWidth = 0, HorizontalAlignment = Element.ALIGN_RIGHT });
                table.SpacingAfter = 16;
                document.Add(table);

                //Get Table Data
                var data = _database.TBLPositions
                    .Where(p => p.RecordDateTime.Date >= dateTimePickerFirst.Value.Date && p.RecordDateTime.Date <= dateTimePickerLast.Value.Date)
                    .OrderBy(p => p.TagId)
                    .ThenByDescending(p => p.RecordDateTime)
                    .Join(_database.TBLMaps, p => p.MapId, m => m.Id,
                        (p, m) => new { Position = p, Map = m }) //Join Map
                    .Join(_database.TBLTags, p => p.Position.TagId, t => t.Id,
                        (p, t) => new { p.Position, p.Map, Tag = t }); //Join Tags

                //Write Table
                table = new PdfPTable(new[] { 3F, 3F, 2F, 1F, 1F, 4F }) { HeaderRows = 1, WidthPercentage = 100 };
                table.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                table.AddCell(new PdfPCell(new Phrase("İsim", _baseFontBold)) { HorizontalAlignment = Element.ALIGN_CENTER, GrayFill = 0.85F });
                table.AddCell(new PdfPCell(new Phrase("Soyisim", _baseFontBold)) { HorizontalAlignment = Element.ALIGN_CENTER, GrayFill = 0.85F });
                table.AddCell(new PdfPCell(new Phrase("Harita", _baseFontBold)) { HorizontalAlignment = Element.ALIGN_CENTER, GrayFill = 0.85F });
                table.AddCell(new PdfPCell(new Phrase("X", _baseFontBold)) { HorizontalAlignment = Element.ALIGN_CENTER, GrayFill = 0.85F });
                table.AddCell(new PdfPCell(new Phrase("Y", _baseFontBold)) { HorizontalAlignment = Element.ALIGN_CENTER, GrayFill = 0.85F });
                table.AddCell(new PdfPCell(new Phrase("Tarih", _baseFontBold)) { HorizontalAlignment = Element.ALIGN_CENTER, GrayFill = 0.85F });
                foreach (var obj in data)
                {
                    table.AddCell(new PdfPCell(new Phrase(obj.Tag.TagFirstName, _baseFont)) { HorizontalAlignment = Element.ALIGN_LEFT });
                    table.AddCell(new PdfPCell(new Phrase(obj.Tag.TagLastName, _baseFont)) { HorizontalAlignment = Element.ALIGN_LEFT });
                    table.AddCell(new Phrase(obj.Map.MapName, _baseFont));
                    table.AddCell(new Phrase(obj.Position.d_XPosition.ToString(), _baseFont));
                    table.AddCell(new Phrase(obj.Position.d_yPosition.ToString(), _baseFont));
                    table.AddCell(new Phrase(obj.Position.RecordDateTime.ToString("g"), _baseFont));
                }
                document.Add(table);

                document.Close();
                writer.SetFullCompression();
                writer.Close();
                File.WriteAllBytes(sfd.FileName, ms.ToArray());
                ms.Close();
            }

            MessageBox.Show(@"Dışarı Aktarma Başarılı Birşekilde Tamamlandı.", @"Dışarı Aktarma", MessageBoxButtons.OK);
        }
    }
}