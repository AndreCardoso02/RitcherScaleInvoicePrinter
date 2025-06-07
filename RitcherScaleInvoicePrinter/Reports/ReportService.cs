using Microsoft.Reporting.WinForms;
using RitcherScaleInvoicePrinter.Model;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RitcherScaleInvoicePrinter.Reports
{
    public class ReportService
    {
        public void PrintInvoice(Measure measure)
        {
            List<Measure> mesures = new List<Measure>
            {
                measure
            };

            LocalReport invoice = new LocalReport();
            invoice.ReportPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Reports", "Invoice.rdlc");

            invoice.DataSources.Clear();
            invoice.DataSources.Add(new ReportDataSource("DataSet1", mesures));

            var streams = ExportToEMF(invoice);
            Print(streams);
        }

        private IList<Stream> ExportToEMF(LocalReport report)
        {
            string deviceInfo = @"
                <DeviceInfo>
                    <OutputFormat>EMF</OutputFormat>
                    <PageWidth>21cm</PageWidth>
                    <PageHeight>29.7cm</PageHeight>
                    <MarginTop>1cm</MarginTop>
                    <MarginLeft>1cm</MarginLeft>
                    <MarginRight>1cm</MarginRight>
                    <MarginBottom>1cm</MarginBottom>
                </DeviceInfo>
            ";

            IList<Stream> streams = new List<Stream>();
            Warning[] warnings;

            report.Render("Image", deviceInfo, (name, fileNameExtension, encoding, mimeType, willSeek) => {
                MemoryStream stream = new MemoryStream();
                streams.Add(stream);
                return stream;
            }, out warnings);

            foreach (Stream stream in streams)
            {
                stream.Position = 0;
            }

            return streams;
        }

        private void Print(IList<Stream> pages)
        {
            try
            {
                PrintDocument printDoc = new PrintDocument();
                printDoc.PrintPage += (sender, e) =>
                {
                    Metafile pageImage = new Metafile(pages[0]);

                    e.Graphics.DrawImage(pageImage, e.PageBounds);
                    pages.RemoveAt(0);
                    e.HasMorePages = pages.Count > 0;
                };

                printDoc.Print();
            }
            catch (System.Drawing.Printing.InvalidPrinterException ex)
            {
                SaveAsPdfWithDialog(pages);
                return;
            }
        }

        private void SaveAsPdf(IList<Stream> pages)
        {
            PrintDocument pdfDoc = new PrintDocument();
            pdfDoc.PrinterSettings.PrinterName = "Microsoft Print to PDF";
            pdfDoc.PrinterSettings.PrintToFile = true;
            pdfDoc.PrinterSettings.PrintFileName = "output.pdf";

            pdfDoc.PrintPage += (sender, e) =>
            {
                Metafile pageImage = new Metafile(pages[0]);
                e.Graphics.DrawImage(pageImage, e.PageBounds);
                pages.RemoveAt(0);
                e.HasMorePages = pages.Count > 0;
            };

            try
            {
                pdfDoc.Print();
                MessageBox.Show("PDF salvo como 'output.pdf'");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao salvar PDF: " + ex.Message);
            }
        }


        private void SaveAsPdfWithDialog(IList<Stream> pages)
        {
            PrintDocument pdfDoc = new PrintDocument();
            pdfDoc.PrinterSettings.PrinterName = "Microsoft Print to PDF";

            PrintDialog printDialog = new PrintDialog();
            printDialog.Document = pdfDoc;
            printDialog.UseEXDialog = true;

            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                pdfDoc.PrintPage += (sender, e) =>
                {
                    using (Metafile pageImage = new Metafile(pages[0]))
                    {
                        e.Graphics.DrawImage(pageImage, e.PageBounds);
                    }
                    pages[0].Dispose();
                    pages.RemoveAt(0);
                    e.HasMorePages = pages.Count > 0;
                };

                pdfDoc.Print();
            }
        }

    }
}
