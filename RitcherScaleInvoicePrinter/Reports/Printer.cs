using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using Microsoft.Reporting.WinForms;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Collections.Generic;
using System.Drawing;

namespace RitcherScaleInvoicePrinter.Reports
{

    public class ReportPrintDocument : PrintDocument
    {
        private IList<Stream> m_streams;
        private int m_currentPage;

        public ReportPrintDocument(LocalReport report)
        {
            Export(report);
            this.PrintPage += new PrintPageEventHandler(PrintPageHandler);
        }

        private void Export(LocalReport report)
        {
            string deviceInfo =
            @"<DeviceInfo>
            <OutputFormat>EMF</OutputFormat>
            <PageWidth>21cm</PageWidth>
            <PageHeight>29.7cm</PageHeight>
            <MarginTop>0.5cm</MarginTop>
            <MarginLeft>0.5cm</MarginLeft>
            <MarginRight>0.5cm</MarginRight>
            <MarginBottom>0.5cm</MarginBottom>
        </DeviceInfo>";

            Warning[] warnings;
            m_streams = new List<Stream>();
            report.Render("Image", deviceInfo,
                (name, extension, encoding, mimeType, willSeek) =>
                {
                    Stream stream = new MemoryStream();
                    m_streams.Add(stream);
                    return stream;
                }, out warnings);

            foreach (Stream stream in m_streams)
                stream.Position = 0;
        }

        private void PrintPageHandler(object sender, PrintPageEventArgs e)
        {
            Metafile pageImage = new Metafile(m_streams[m_currentPage]);

            e.Graphics.DrawImage(pageImage, e.PageBounds);

            m_currentPage++;
            e.HasMorePages = (m_currentPage < m_streams.Count);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing && m_streams != null)
            {
                foreach (Stream stream in m_streams)
                    stream.Dispose();
                m_streams = null;
            }
        }
    }
}
