using RitcherScaleInvoicePrinter.Model;
using RitcherScaleInvoicePrinter.Reports;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RitcherScaleInvoicePrinter
{
    public partial class Form1 : Form
    {
        private FileSystemWatcher _watcher;
        private string _dataPath = @"C:\WBridge\data";
        private List<Measure> travelRecords = new List<Measure>();
        private Measure selectedItem = null;

        // Setup software
        private void SetupFileWatcher()
        {
            try
            {
                _watcher = new FileSystemWatcher(_dataPath, "*.DAT")
                {
                    NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.CreationTime,
                    EnableRaisingEvents = true,
                    IncludeSubdirectories = false
                };

                _watcher.Changed += OnFileChanged;
                _watcher.Created += OnFileChanged;
                _watcher.Renamed += OnFileRenamed;
                _watcher.Deleted += OnFileDeleted;
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show("Pasta inválida, selecione uma nova.");

                var form = new SelectFolderForm();
                if (form.ShowDialog() == DialogResult.OK)
                {
                    _dataPath = form.SelectedPath;
                    MessageBox.Show("Definições atualizadas.");
                    SetupFileWatcher();
                }
                else
                {
                    Application.Exit();
                }
            }
        }

        // Event for when any file is added or changed
        //private void OnFileChanged(object sender, FileSystemEventArgs e)
        //{
        //    // Wait until file be avalailable
        //    System.Threading.Thread.Sleep(100);
        //    Invoke(new Action(() => LoadDataFromFile(e.FullPath)));
        //}
        private void OnFileChanged(object sender, FileSystemEventArgs e)
        {
            // Pequeno delay para evitar erro de IO
            Task.Delay(200).ContinueWith(_ =>
            {
                try
                {
                    // Verifica se o arquivo ainda está sendo usado
                    using (FileStream fs = File.Open(e.FullPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    {
                        Invoke(new Action(() =>
                        {
                            LoadAllData();
                            //LoadDataFromFile(e.FullPath); // Seu método
                        }));
                    }
                }
                catch (IOException)
                {
                    // Arquivo em uso, tenta novamente depois de 500ms
                    Task.Delay(500).ContinueWith(__ => OnFileChanged(sender, e));
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao processar o ficheiro {e.Name}: {ex.Message}");
                }
            });
        }

        private void OnFileRenamed(object sender, RenamedEventArgs e)
        {
            LoadDataFromFile(e.FullPath);
        }

        private void OnFileDeleted(object sender, FileSystemEventArgs e)
        {
            Invoke(new Action(() =>
            {
                LoadAllData(); // Recarrega todos os ficheiros *.DAT do diretório
            }));
        }

        // Load All Data
        private void LoadAllData()
        {
            try
            {
                dataGridView1.DataSource = null;
                var files = Directory.GetFiles(_dataPath, "*.DAT");
                travelRecords = new List<Measure>();

                foreach (var file in files)
                {
                    LoadDataFromFile(file);
                }

                // Configurações recomendadas
                dataGridView1.ScrollBars = ScrollBars.Both;
                dataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

                // Preenche a DataGridView com a lista
                dataGridView1.DataSource = travelRecords
                    ?.OrderByDescending(x => x.EntryDate.Date)  // Ordena pela data (sem hora)
                    ?.ThenByDescending(x => x.EntryTime)  // Depois pela hora do dia
                    ?.ToList();

                btnImprimir.Enabled = false;
                btnVer.Enabled = false;
                selectedItem = null;

                if (radioHoje.Checked) {
                    todayFilter();
                }
            }
            catch (System.IO.DirectoryNotFoundException ex)
            {
                MessageBox.Show("Directório não encontrado", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Environment.Exit(0);
            }
        }

        // Methodos who is reponsible for reading all data from a file
        private void LoadDataFromFile(string filePath)
        {
            try
            {
                var lines = File.ReadAllLines(filePath);

                foreach (var line in lines)
                {
                    var values = line.Split(',');
                    if (values?.Length == 25)
                    {
                        var v = values.Select(val => val.Trim('"')).ToArray();

                        var record = new Measure
                        {
                            EntryDate = DateTime.TryParse(v[0], out var ed) ? ed : DateTime.MinValue,
                            EntryTime = TimeSpan.TryParse(v[1], out var et) ? et : TimeSpan.Zero,
                            ExitDate = DateTime.TryParse(v[2], out var exd) ? exd : DateTime.MinValue,
                            ExitTime = TimeSpan.TryParse(v[3], out var ext) ? ext : TimeSpan.Zero,
                            SlipNumber = v[4],

                            VehicleRegistration = v[5],
                            VehicleNumber = v[6],
                            OperationType = "Deliver", // ou "Collect", se tiver essa info em outro campo
                            DeliveryDistance = null,   // não disponível no CSV original

                            TransporterName = v[8], // se v[7] = código do cliente
                            TransporterNumber = v[7],
                            TariffKmTon = null,

                            MaterialCode = v[9],
                            MaterialDescription = v[10],

                            DriverId = v[11],
                            DriverDocument = v[12],
                            DriverName = v[13],
                            DriverNumber = v[12], // pode usar o mesmo se não houver outro campo

                            CarrierId = v[14],
                            VehicleBrand = v[15],
                            LoadType = v[16],
                            Notes = v[17],

                            TareWeight = int.TryParse(v[19], out var gross) ? gross : 0,
                            GrossWeight = int.TryParse(v[20], out var net) ? net : 0,
                            NetWeight = int.TryParse(v[18], out var tare) ? tare : 0,
                            VerifiedWeight = int.TryParse(v[21], out var verified) ? verified : 0,

                            AdditionalWeight1 = v[22],
                            AdditionalWeight2 = v[23],
                            LoadStatus = v[24],

                            // Customer Details
                            CustomerName = v[8],
                            AccountNumber = v[7],
                            OrderNumber = null,

                            // Product Details
                            ProductName = v[10],
                            ProductNumber = "R502",//v[9],
                            PricePerTon = null,
                            DeliveryPricePerKm = null,

                            // Current Mass (pode ser calculado ou exibido em tempo real)
                            CurrentMass = null
                        };

                        travelRecords.Add(record);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao ler o ficheiro: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        public Form1()
        {
            InitializeComponent();
            radioHoje.Checked = true;
            SetupFileWatcher();
            LoadAllData();
        }

        // Form Load
        private void Form1_Load(object sender, EventArgs e)
        {
            // Inicialização adicional se necessário
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    if (e.RowIndex >= 0)
                    {
                        selectedItem = (Measure)dataGridView1.Rows[e.RowIndex].DataBoundItem;

                        if (selectedItem != null)
                        {
                            btnImprimir.Enabled = true;
                            btnVer.Enabled = true;
                        }
                    }
                    else {
                        btnImprimir.Enabled = false;
                        btnVer.Enabled = false;
                        selectedItem = null; 
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Nao foi possivel selecionar, tente novamente", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void radioHoje_CheckedChanged(object sender, EventArgs e)
        {
            todayFilter();
        }

        private void todayFilter()
        {
            try
            {
                var filtro = travelRecords?.Where(x => x.EntryDate.Date == DateTime.Now.Date)?.ToList();
                if (filtro?.Any() ?? false)
                {
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = filtro
                        ?.OrderByDescending(x => x.EntryDate.Date)  // Ordena pela data (sem hora)
                        ?.ThenByDescending(x => x.EntryTime)  // Depois pela hora do dia
                        ?.ToList();
                }
                else dataGridView1.DataSource = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Tente novamente mais tarde");
            }
        }

        private void radioMes_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                var filtro = travelRecords?.Where(x => x.EntryDate.Month == DateTime.Now.Month)?.ToList();
                if (filtro?.Any() ?? false)
                {
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = filtro
                        ?.OrderByDescending(x => x.EntryDate.Date)  // Ordena pela data (sem hora)
                        ?.ThenByDescending(x => x.EntryTime)  // Depois pela hora do dia
                        ?.ToList();
                }
                else dataGridView1.DataSource = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Tente novamente mais tarde");
            }
        }

        private void radioOutros_CheckedChanged(object sender, EventArgs e)
        {
            if (radioOutros.Checked)
                panelOutros.Visible = true;
            else panelOutros.Visible = false;
            filtraOutros();
        }

        private void filtraOutros()
        {
            try
            {
                if (DateTime.Parse(dateDe.Text) > DateTime.Parse(dateAte.Text))
                    MessageBox.Show("Data inicial não pode ser maior que a data de destino", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                else
                {
                    var filtro = travelRecords?.Where(x => x.EntryDate.Date >= DateTime.Parse(dateDe.Text).Date && x.EntryDate.Date < DateTime.Parse(dateAte.Text).AddDays(1).Date)?.ToList();
                    if (filtro?.Any() ?? false)
                    {
                        dataGridView1.DataSource = null;
                        dataGridView1.DataSource = filtro
                            ?.OrderByDescending(x => x.EntryDate.Date)  // Ordena pela data (sem hora)
                            ?.ThenByDescending(x => x.EntryTime)  // Depois pela hora do dia
                            ?.ToList();
                    }
                    else dataGridView1.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Tente novamente mais tarde");
            }
        }

        private void radioTodos_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (travelRecords?.Any() ?? false)
                {
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = travelRecords
                        ?.OrderByDescending(x => x.EntryDate.Date)  // Ordena pela data (sem hora)
                        ?.ThenByDescending(x => x.EntryTime)  // Depois pela hora do dia
                        ?.ToList();
                }
                else dataGridView1.DataSource = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Tente novamente mais tarde");
            }
        }

        private void dateDe_ValueChanged(object sender, EventArgs e)
        {
            filtraOutros();
        }

        private void dateAte_ValueChanged(object sender, EventArgs e)
        {
            filtraOutros();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            try
            {
                if (selectedItem != null)
                {
                    MessageBox.Show("Pedido enviado para a impressora, aguarde!");
                    ReportService reportService = new ReportService();
                    reportService.PrintInvoice(selectedItem);
                }
                else MessageBox.Show("Selecione algum item na tabela para poder imprimir", "Impressão", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Houve um erro consulte o suporte", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                LoadAllData();
            }
            catch (Exception)
            {
                MessageBox.Show("Houve um erro consulte o suporte", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (selectedItem != null)
                {
                    ReportPreview preview = new ReportPreview(selectedItem);
                    preview.ShowDialog();
                }
                else MessageBox.Show("Selecione algum item na tabela para poder imprimir", "Impressão", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Houve um erro consulte o suporte", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
