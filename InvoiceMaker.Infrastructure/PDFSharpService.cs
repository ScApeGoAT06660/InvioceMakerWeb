using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using InvoiceMaker.Domain;
using InvoiceMaker.Application.Services;


namespace InvoiceMaker.Infrastructure
{
    public class PDFSharpService : IPDFSharpService
    {
        Invoice _invoice;
        Seller _seller;
        Buyer _buyer;

        public byte[] GenerateInvoicePdf(Invoice invoice, Seller seller, Buyer buyer)
        {
            _invoice = invoice;
            _seller = seller;
            _buyer = buyer;

            PdfDocument document = new PdfDocument();
            document.Info.Title = $"Faktura {_invoice.Number}";
            PdfPage page = document.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);
            XFont font = new XFont("Verdana", 10);

            int y = 40;
            y = DrawHeader(gfx, font, y);
            y = DrawSellerAndBuyer(gfx, font, y);
            y = DrawItemsTable(gfx, font, y);
            y = DrawSummary(gfx, font, y);
            y = DrawFooter(gfx, font, y);

            using (MemoryStream stream = new MemoryStream())
            {
                document.Save(stream, false);
                return stream.ToArray();
            }
        }

        private int DrawHeader(XGraphics gfx, XFont font, int y)
        {
            gfx.DrawString($"Faktura VAT: {_invoice.Number}", font, XBrushes.Black, new XPoint(40, y));
            y += 20;
            gfx.DrawString($"Data wystawienia: {_invoice.IssueDate:yyyy-MM-dd}", font, XBrushes.Black, new XPoint(40, y));
            y += 20;
            gfx.DrawString($"Data sprzedaży: {_invoice.SaleDate:yyyy-MM-dd}", font, XBrushes.Black, new XPoint(40, y));
            y += 20;
            gfx.DrawString($"Miejsce wystawienia: {_invoice.Place}", font, XBrushes.Black, new XPoint(40, y));
            y += 30;

            double logoSize = 150;
            double margin = 40;

            double x = gfx.PageSize.Width - logoSize - margin;
            double yLogo = margin;

            string logoPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                "Faktury",
                _seller.Id.ToString(),
                "logo.jpg"
            );

            if (File.Exists(logoPath))
            {
                try
                {
                    using (XImage logo = XImage.FromFile(logoPath))
                    {
                        gfx.DrawImage(logo, x, yLogo, logoSize, logoSize);
                    }
                }
                catch (Exception ex)
                {
                    // MessageBox.Show("Błąd podczas rysowania logo: " + ex.Message);
                }
            }
            return y;
        }

        private int DrawItemsTable(XGraphics gfx, XFont font, int y)
        {
            XFont boldFont = new XFont("Verdana", 11);
            XPen pen = new XPen(XColors.Black, 0.5);
            XBrush headerBackground = new XSolidBrush(XColors.LightGray);

            int colLp = 30;
            int colName = 200;
            int colQty = 50;
            int colNet = 80;
            int colVat = 50;
            int colGross = 80;

            int[] colWidths = { colLp, colName, colQty, colNet, colVat, colGross };
            string[] headers = { "Lp", "Nazwa", "Ilość", "Cena netto", "VAT %", "Brutto" };

            int rowHeight = 20;
            int maxY = 800;

            int totalTableWidth = colWidths.Sum();
            int startX = (int)((gfx.PageSize.Width - totalTableWidth) / 2);
            if (startX < 40) startX = 40;

            void DrawTableHeader()
            {
                int x = startX;
                for (int j = 0; j < headers.Length; j++)
                {
                    gfx.DrawRectangle(headerBackground, x, y, colWidths[j], rowHeight);
                    gfx.DrawRectangle(pen, x, y, colWidths[j], rowHeight);
                    gfx.DrawString(headers[j], boldFont, XBrushes.Black, new XRect(x, y, colWidths[j], rowHeight), XStringFormats.Center);
                    x += colWidths[j];
                }
                y += rowHeight;
            }

            DrawTableHeader();

            int i = 1;
            foreach (var item in _invoice.Items)
            {
                if (y + rowHeight > maxY)
                {
                    PdfPage newPage = gfx.PdfPage.Owner.AddPage();
                    gfx = XGraphics.FromPdfPage(newPage);
                    y = 40;
                    DrawTableHeader();
                }

                int x = startX;

                string trimmedName = item.Name.Length > 33 ? item.Name.Substring(0, 33) : item.Name;

                string[] rowValues =
                {
                    i.ToString(),
                    trimmedName,
                    item.Quantity.ToString(),
                    item.Netto.ToString("0.00"),
                    item.VAT.ToString(),
                    item.Brutto.ToString("0.00")
                };

                for (int j = 0; j < rowValues.Length; j++)
                {
                    gfx.DrawRectangle(pen, x, y, colWidths[j], rowHeight);
                    gfx.DrawString(rowValues[j], font, XBrushes.Black, new XRect(x, y, colWidths[j], rowHeight), XStringFormats.Center);
                    x += colWidths[j];
                }

                y += rowHeight;
                i++;
            }

            return y + 10;
        }

        private int DrawSummary(XGraphics gfx, XFont font, int y)
        {
            y += 15;

            decimal totalNet = _invoice.Items.Sum(i => i.Netto * i.Quantity);
            decimal totalVat = _invoice.Items.Sum(i => (i.Netto * i.Quantity) * (Convert.ToDecimal(i.VAT) / 100));
            decimal totalGross = totalNet + totalVat;

            gfx.DrawString($"Razem netto: {totalNet:C}", font, XBrushes.Black, new XPoint(40, y));
            y += 15;
            gfx.DrawString($"VAT: {totalVat:C}", font, XBrushes.Black, new XPoint(40, y));
            y += 15;
            gfx.DrawString($"Razem brutto: {totalGross:C}", font, XBrushes.Black, new XPoint(40, y));

            return y + 30;
        }

        private int DrawFooter(XGraphics gfx, XFont font, int y)
        {
            gfx.DrawString("Podpis wystawiającego:", font, XBrushes.Black, new XPoint(40, y));
            gfx.DrawString("Podpis nabywcy:", font, XBrushes.Black, new XPoint(300, y));
            y += 15;
            gfx.DrawString(_invoice.SellerSignature, font, XBrushes.Black, new XPoint(40, y));
            gfx.DrawString(_invoice.BuyerSignature, font, XBrushes.Black, new XPoint(300, y));
            y += 30;

            gfx.DrawString("Uwagi:", font, XBrushes.Black, new XPoint(40, y));
            y += 15;
            gfx.DrawString(_invoice.Notes, font, XBrushes.Black, new XRect(40, y, 500, 100), XStringFormats.TopLeft);

            return y + 100;
        }

        private int DrawSellerAndBuyer(XGraphics gfx, XFont font, int y)
        {

            gfx.DrawString("Sprzedawca:", font, XBrushes.Black, new XPoint(40, y));
            gfx.DrawString("Nabywca:", font, XBrushes.Black, new XPoint(300, y));
            y += 15;

            string trimmedSeller = _seller.Name.Length > 40 ? _seller.Name.Substring(0, 40) : _seller.Name;
            string trimmedBuyer = _buyer.Name.Length > 40 ? _buyer.Name.Substring(0, 40) : _buyer.Name;

            gfx.DrawString(trimmedSeller, font, XBrushes.Black, new XPoint(40, y));
            gfx.DrawString(trimmedBuyer, font, XBrushes.Black, new XPoint(300, y));
            y += 15;

            if (!string.IsNullOrWhiteSpace(_seller.VATID))
                gfx.DrawString("NIP: " + _seller.VATID, font, XBrushes.Black, new XPoint(40, y));
            if (!string.IsNullOrWhiteSpace(_buyer.VATID))
                gfx.DrawString("NIP: " + _buyer.VATID, font, XBrushes.Black, new XPoint(300, y));
            y += 15;

            gfx.DrawString($"{_seller.StreetAndNo}, {_seller.Postcode} {_seller.City}", font, XBrushes.Black, new XPoint(40, y));
            gfx.DrawString($"{_buyer.StreetAndNo}, {_buyer.Postcode} {_buyer.City}", font, XBrushes.Black, new XPoint(300, y));
            y += 20;

            if (!string.IsNullOrWhiteSpace(_seller.Bank))
                gfx.DrawString("Bank: " + _seller.Bank, font, XBrushes.Black, new XPoint(40, y));
            y += 15;
            if (!string.IsNullOrWhiteSpace(_seller.BankAccount))
                gfx.DrawString("Konto: " + _seller.BankAccount, font, XBrushes.Black, new XPoint(40, y));
            y += 15;
            if (!string.IsNullOrWhiteSpace(_seller.SWIFT))
                gfx.DrawString("SWIFT: " + _seller.SWIFT, font, XBrushes.Black, new XPoint(40, y));
            y += 15;

            return y + 15;
        }
    }
}
