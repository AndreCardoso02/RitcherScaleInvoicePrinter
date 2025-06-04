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
    public partial class SelectFolderForm : Form
    {
        public string SelectedPath { get; private set; }

        public SelectFolderForm()
        {
            InitializeComponent();
        }

        private void btn_browser_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                folderDialog.Description = "Selecione a pasta da aplicação Ritcher Scale";
                folderDialog.ShowNewFolderButton = true;

                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    string folderPath = folderDialog.SelectedPath;
                    MessageBox.Show("Directório selecionado: " + folderPath);

                    input_rs_path.Text = folderPath;
                }
            }
        }

        private void btn_salvar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(input_rs_path.Text))
            {
                SelectedPath = input_rs_path.Text;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Por favor, selecione um directório.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btn_cancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
