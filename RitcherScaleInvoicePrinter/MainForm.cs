using Microsoft.Reporting.WinForms;
using RitcherScaleInvoicePrinter.Model;
using RitcherScaleInvoicePrinter.Reports;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace RitcherScaleInvoicePrinter
{
    public partial class MainForm : Form
    {
        private Measure adMeasure = null;
        private string _dataPath = @"C:\WBridge\data";

        public static List<Measure> measures = new List<Measure>();

        private Measure selectedTruck = null;

        public MainForm()
        {
            InitializeComponent();
        }

        private void rdFuel_CheckedChanged(object sender, EventArgs e)
        {
            if (rdAsfalto.Checked == true)
                rdAsfalto.Checked = false;
        }

        private void rdAsfalto_CheckedChanged(object sender, EventArgs e)
        {
            if (rdFuel.Checked == true)
                rdFuel.Checked = false;
        }

        private void txtTara_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int val = Int32.Parse(txtTara.Text);
                lblLiquid.Text = val.ToString();
            }
            catch (Exception) { }
        }

        private void txtWeight_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int tara = Int32.Parse(txtTara.Text);
                int bruto = Int32.Parse(txtWeight.Text);

                lblLiquid.Text = (bruto - tara).ToString();
            }
            catch (Exception) { }
        }

        private void txtTara_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Bloqueia o caractere
            }
        }

        private void txtWeight_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Bloqueia o caractere
            }
        }

        private void txtNOrder_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Bloqueia o caractere
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            Measure measure = GetData();

            if (selectedTruck == null)
            {
                measures.Add(measure);
            }
            else
            {
                selectedTruck.ExitDate = DateTime.Now.Date;
                selectedTruck.ExitTime = DateTime.Now.TimeOfDay;
                selectedTruck.GrossWeight = !string.IsNullOrEmpty(txtWeight.Text) ? Int32.Parse(txtWeight.Text) : 0;
                selectedTruck.VerifiedWeight = selectedTruck.GrossWeight - selectedTruck.TareWeight;
                selectedTruck.LoadStatus = (string.IsNullOrEmpty(txtWeight.Text) ? "EMPTY" : "FULL");
                EscreverNoArquivo(selectedTruck);
                Print(selectedTruck);
                measures.Remove(selectedTruck);
                selectedTruck = null;
            }

            Limpar();
            ActualizarLista();
        }

        #region Get and Set
        private Measure GetData()
        {
            adMeasure = new Measure
            {
                // Datas e horas
                EntryDate = DateTime.Now.Date,
                EntryTime = DateTime.Now.TimeOfDay,

                // Outros dados conforme CSV
                SlipNumber = "8091",

                VehicleRegistration = txtMatr.Text,
                VehicleNumber = txtMatr.Text,

                OperationType = "CAT", // Supondo que CAT representa o tipo de operação
                DeliveryDistance = 0,  // Do CSV: "0"

                TransporterName = txtCompany.Text, // CAT de novo
                TransporterNumber = "1234",

                TariffKmTon = 1234, // Como está no CSV, supondo decimal (exemplo)

                DriverName = txtDriver.Text,
                DriverNumber = "1234",

                CustomerName = txtCompany.Text,
                AccountNumber = txtCompany.Text,
                OrderNumber = txtNOrder.Text, // CSV está vazio aqui

                ProductName = (rdAsfalto.Checked ? "ASFALTO" : "FUELL"), // Do CSV: "0" (confuso, mas é o valor dado)
                ProductNumber = "0",
                PricePerTon = 0, // Do CSV: 0
                DeliveryPricePerKm = 0,

                TareWeight = !string.IsNullOrEmpty(txtTara.Text) ? Int32.Parse(txtTara.Text) : 0,
                GrossWeight = !string.IsNullOrEmpty(txtWeight.Text) ? Int32.Parse(txtWeight.Text) : 0,
                NetWeight = !string.IsNullOrEmpty(txtWeight.Text) ? Int32.Parse(txtWeight.Text) : 0,
                CurrentMass = !string.IsNullOrEmpty(lblLiquid.Text) ? Int32.Parse(lblLiquid.Text) : 0, // double?

                MaterialCode = "0",
                MaterialDescription = "0",
                DriverId = "FULL",
                DriverDocument = "",    // Não veio valor
                CarrierId = "",         // Não veio valor
                VehicleBrand = txtTruckModel.Text,      // Não veio valor
                LoadType = (rdAsfalto.Checked ? "ASFALTO" : "FUELL"),         // Não veio valor
                Notes = "",            // Não veio valor
                VerifiedWeight = !string.IsNullOrEmpty(lblLiquid.Text) ? Int32.Parse(lblLiquid.Text) : 0,
                AdditionalWeight1 = "0",
                AdditionalWeight2 = "0",
                LoadStatus = (string.IsNullOrEmpty(txtWeight.Text) ? "EMPTY" : "FULL")
            };

            return adMeasure;
        }

        private void SetData(Measure measure)
        {
            txtNOrder.Text = measure.OrderNumber;
            txtMatr.Text = measure.VehicleNumber;
            txtTruckModel.Text = measure.VehicleBrand;
            txtDriver.Text = measure.DriverName;
            txtCompany.Text = measure.CustomerName;
            if (measure.ProductName == "ASFALTO") rdAsfalto.Checked = true;
            else rdFuel.Checked = true;
            txtTara.Text = measure.TareWeight.ToString();
            txtWeight.Text = measure.NetWeight.ToString();
            lblLiquid.Text = measure.VerifiedWeight.ToString();
        }

        private void Limpar()
        {
            txtNOrder.Clear();
            txtMatr.Clear();
            txtTruckModel.Clear();
            txtDriver.Clear();
            txtCompany.Clear();
            rdFuel.Checked = true;
            txtTara.Clear();
            txtWeight.Clear();
            lblLiquid.Text = "0";
            txtWeight.Enabled = false;
        }
        #endregion

        private void MainForm_Load(object sender, EventArgs e)
        {
            ActualizarLista();
        }

        private void ActualizarLista()
        {
            dgView.Rows.Clear(); // Limpa as linhas existentes
            if (measures.Count > 0)
            {
                foreach (var measure in measures)
                {
                    AdicionarNovoItem(measure);
                }
            }
        }

        private void AdicionarNovoItem(Measure measure)
        {
            // Adiciona uma nova linha com os valores de Matricula e Motorista
            int rowIndex = dgView.Rows.Add(measure.VehicleNumber, measure.DriverName);

            // Armazena o objeto 'measure' na propriedade Tag da linha, se quiser acessá-lo depois
            dgView.Rows[rowIndex].Tag = measure;
        }

        private void EscreverNoArquivo(Measure measure)
        {
            // Nome do ficheiro no formato ddMMyyyy.DAT
            string fileName = DateTime.Now.ToString("ddMMyyyy") + ".DAT";
            string filePath = Path.Combine(_dataPath, fileName);

            // Constrói a linha no formato desejado
            string linha = string.Format("\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\",\"{6}\",\"{7}\",\"{8}\",\"{9}\",\"{10}\",\"{11}\",\"{12}\",\"{13}\",\"{14}\",\"{15}\",\"{16}\",\"{17}\",\"{18}\",\"{19}\",\"{20}\",\"{21}\",\"{22}\",\"{23}\",\"{24}\"",
                measure.EntryDate.ToString("d/M/yyyy"),
                measure.EntryTime.ToString(@"hh\:mm\:ss"),
                measure.ExitDate.ToString("d/M/yyyy"),
                measure.ExitTime.ToString(@"hh\:mm\:ss"),
                measure.SlipNumber,
                measure.VehicleRegistration,
                measure.VehicleNumber,
                measure.DeliveryDistance?.ToString() ?? "0",
                measure.OperationType,
                "",
                measure.OperationType,
                measure.TransporterNumber,
                measure.TransporterNumber,
                measure.DriverName,
                measure.DriverNumber,
                measure.VehicleBrand,
                measure.ProductName,
                "",
                "0",
                measure.TareWeight.ToString(),
                measure.GrossWeight.ToString(),
                measure.NetWeight.ToString(),
                "0",
                "0",
                measure.LoadStatus
            );

            // Escreve no final do ficheiro
            using (StreamWriter sw = new StreamWriter(filePath, true))
            {
                sw.WriteLine(linha);
            }
        }

        private void Print(Measure measure)
        {
            try
            {
                if (measure != null)
                {
                    ReportPreview preview = new ReportPreview(measure);
                    preview.ShowDialog();
                }
                else MessageBox.Show("Selecione algum item na tabela para poder imprimir", "Impressão", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Houve um erro consulte o suporte", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnReimprimir_Click(object sender, EventArgs e)
        {
            try
            {
                Form1 preview = new Form1();

                preview.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Houve um erro consulte o suporte", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dgView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // Verifica se clicou numa linha válida
                if (e.RowIndex >= 0 && e.RowIndex < dgView.Rows.Count)
                {
                    // Remove da lista de dados
                    selectedTruck = measures[e.RowIndex];
                    selectedTruck.ExitDate = DateTime.Now;
                    selectedTruck.ExitTime = DateTime.Now.TimeOfDay;
                    measures.RemoveAt(e.RowIndex);

                    // Remove do DataGridView
                    dgView.Rows.RemoveAt(e.RowIndex);

                    // Limpa dados relacionados
                    txtWeight.Enabled = true;
                    SetData(selectedTruck);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Não foi possível remover, tente novamente.", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

    }
}
