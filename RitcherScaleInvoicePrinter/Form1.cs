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
        private readonly string _dataPath = @"C:\WBridge\data";

        // Setup software
        private void SetupFileWatcher()
        {
            _watcher = new FileSystemWatcher(_dataPath, "*.DAT");
            _watcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName;
            _watcher.Changed += OnFileChanged;
            _watcher.Created += OnFileChanged;
            _watcher.EnableRaisingEvents = true;
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
            dataGridView1.Rows.Clear();
            var files = Directory.GetFiles(_dataPath, "*.DAT");

            foreach (var file in files)
            {
                LoadDataFromFile(file);
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
    }
}
