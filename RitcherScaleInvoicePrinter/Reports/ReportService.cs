using Microsoft.Reporting.WebForms;
using RitcherScaleInvoicePrinter.Model;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RitcherScaleInvoicePrinter.Reports
{
    public class ReportService
    {
        public void PrintInvoice()
        {
            List<Measure> mesures = new List<Measure>();
            //mesures.Add(new Measure { Driver = "Andre", Mass="20.0"});

            LocalReport invoice = new LocalReport();
            invoice.ReportPath = "Invoice.rdlc";

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
    }
}
