using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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

        // Setup software
        private void SetupFileWatcher()
        {
            try
            {
                _watcher = new FileSystemWatcher(_dataPath, "*.DAT");
                _watcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName;
                _watcher.Changed += OnFileChanged;
                _watcher.Created += OnFileChanged;
                _watcher.EnableRaisingEvents = true;
            }
            catch (System.ArgumentException ex)
            {
                SelectFolderForm form = new SelectFolderForm();
                DialogResult result = form.ShowDialog();

                if (result == DialogResult.OK)
                {
                    _dataPath = form.SelectedPath;
                    MessageBox.Show("Definições actualizadas");
                    SetupFileWatcher();
                } 
                else
                {
                    Application.Exit();
                }
            }
        }

        // Event for when any file is added or changed
        private void OnFileChanged(object sender, FileSystemEventArgs e)
        {
            // Wait until file be avalailable
            System.Threading.Thread.Sleep(100);
            Invoke(new Action(() => LoadDataFromFile(e.FullPath)));
        }

        // Load All Data
        private void LoadAllData()
        {
            try
            {
                dataGridView1.Rows.Clear();
                var files = Directory.GetFiles(_dataPath, "*.DAT");

                foreach (var file in files)
                {
                    LoadDataFromFile(file);
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
                    if (values.All(v => v.StartsWith("\"") && v.EndsWith("\""))) 
                    {
                        var cleaned = values.Select(v => v.Trim('"')).ToArray(); 
                        dataGridView1.Rows.Add(cleaned);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"ERRO: {e.Message}");
            }
        }

        public Form1()
        {
            InitializeComponent();
            SetupFileWatcher();
            LoadAllData();
        }

        // Form Load
        private void Form1_Load(object sender, EventArgs e)
        {
            // Inicialização adicional se necessário
        }
    }
}
