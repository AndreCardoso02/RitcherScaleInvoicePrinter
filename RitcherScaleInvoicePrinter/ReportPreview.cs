using RitcherScaleInvoicePrinter.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RitcherScaleInvoicePrinter
{
    public partial class ReportPreview : Form
    {
        private Measure measure;

        public ReportPreview(Measure measure)
        {
            this.measure = measure;
            InitializeComponent();
        }

        private void ReportPreview_Load(object sender, EventArgs e)
        {
            List<Measure> mesures = new List<Measure>
            {
                measure
            };
            this.reportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", mesures));

            this.reportViewer1.RefreshReport();
        }
    }
}
