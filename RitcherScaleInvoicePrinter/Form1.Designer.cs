namespace RitcherScaleInvoicePrinter
{
    partial class Form1
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnImprimir = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.panelOutros = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dateAte = new System.Windows.Forms.DateTimePicker();
            this.dateDe = new System.Windows.Forms.DateTimePicker();
            this.radioOutros = new System.Windows.Forms.RadioButton();
            this.label6 = new System.Windows.Forms.Label();
            this.radioTodos = new System.Windows.Forms.RadioButton();
            this.radioHoje = new System.Windows.Forms.RadioButton();
            this.radioMes = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel4.SuspendLayout();
            this.panelOutros.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(0, 90);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 62;
            this.dataGridView1.Size = new System.Drawing.Size(1035, 312);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // btnImprimir
            // 
            this.btnImprimir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnImprimir.Enabled = false;
            this.btnImprimir.Location = new System.Drawing.Point(860, 408);
            this.btnImprimir.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(170, 30);
            this.btnImprimir.TabIndex = 1;
            this.btnImprimir.Text = "Imprimir";
            this.btnImprimir.UseVisualStyleBackColor = true;
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelar.Location = new System.Drawing.Point(685, 408);
            this.btnCancelar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(170, 30);
            this.btnCancelar.TabIndex = 1;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.label2);
            this.panel4.Controls.Add(this.panelOutros);
            this.panel4.Controls.Add(this.radioOutros);
            this.panel4.Controls.Add(this.label6);
            this.panel4.Controls.Add(this.radioTodos);
            this.panel4.Controls.Add(this.radioHoje);
            this.panel4.Controls.Add(this.radioMes);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Margin = new System.Windows.Forms.Padding(4);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1039, 82);
            this.panel4.TabIndex = 37;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label2.Location = new System.Drawing.Point(395, 9);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(219, 25);
            this.label2.TabIndex = 21;
            this.label2.Text = "Recibos para imprimir";
            // 
            // panelOutros
            // 
            this.panelOutros.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.panelOutros.Controls.Add(this.label4);
            this.panelOutros.Controls.Add(this.label5);
            this.panelOutros.Controls.Add(this.dateAte);
            this.panelOutros.Controls.Add(this.dateDe);
            this.panelOutros.Location = new System.Drawing.Point(524, 50);
            this.panelOutros.Margin = new System.Windows.Forms.Padding(4);
            this.panelOutros.Name = "panelOutros";
            this.panelOutros.Size = new System.Drawing.Size(352, 29);
            this.panelOutros.TabIndex = 20;
            this.panelOutros.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label4.Location = new System.Drawing.Point(177, 5);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(33, 18);
            this.label4.TabIndex = 20;
            this.label4.Text = "Até:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label5.Location = new System.Drawing.Point(7, 5);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(31, 18);
            this.label5.TabIndex = 21;
            this.label5.Text = "De:";
            // 
            // dateAte
            // 
            this.dateAte.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateAte.Location = new System.Drawing.Point(213, 3);
            this.dateAte.Margin = new System.Windows.Forms.Padding(4);
            this.dateAte.Name = "dateAte";
            this.dateAte.Size = new System.Drawing.Size(133, 22);
            this.dateAte.TabIndex = 18;
            this.dateAte.ValueChanged += new System.EventHandler(this.dateAte_ValueChanged);
            // 
            // dateDe
            // 
            this.dateDe.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateDe.Location = new System.Drawing.Point(39, 3);
            this.dateDe.Margin = new System.Windows.Forms.Padding(4);
            this.dateDe.Name = "dateDe";
            this.dateDe.Size = new System.Drawing.Size(133, 22);
            this.dateDe.TabIndex = 19;
            this.dateDe.ValueChanged += new System.EventHandler(this.dateDe_ValueChanged);
            // 
            // radioOutros
            // 
            this.radioOutros.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.radioOutros.AutoSize = true;
            this.radioOutros.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioOutros.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.radioOutros.Location = new System.Drawing.Point(331, 52);
            this.radioOutros.Margin = new System.Windows.Forms.Padding(4);
            this.radioOutros.Name = "radioOutros";
            this.radioOutros.Size = new System.Drawing.Size(180, 22);
            this.radioOutros.TabIndex = 18;
            this.radioOutros.TabStop = true;
            this.radioOutros.Text = "Personalizar (por data)";
            this.radioOutros.UseVisualStyleBackColor = true;
            this.radioOutros.CheckedChanged += new System.EventHandler(this.radioOutros_CheckedChanged);
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label6.Location = new System.Drawing.Point(34, 30);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 18);
            this.label6.TabIndex = 19;
            this.label6.Text = "Filtros:";
            // 
            // radioTodos
            // 
            this.radioTodos.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.radioTodos.AutoSize = true;
            this.radioTodos.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioTodos.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.radioTodos.Location = new System.Drawing.Point(32, 52);
            this.radioTodos.Margin = new System.Windows.Forms.Padding(4);
            this.radioTodos.Name = "radioTodos";
            this.radioTodos.Size = new System.Drawing.Size(93, 22);
            this.radioTodos.TabIndex = 18;
            this.radioTodos.TabStop = true;
            this.radioTodos.Text = "Ver todos";
            this.radioTodos.UseVisualStyleBackColor = true;
            this.radioTodos.CheckedChanged += new System.EventHandler(this.radioTodos_CheckedChanged);
            // 
            // radioHoje
            // 
            this.radioHoje.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.radioHoje.AutoSize = true;
            this.radioHoje.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioHoje.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.radioHoje.Location = new System.Drawing.Point(133, 52);
            this.radioHoje.Margin = new System.Windows.Forms.Padding(4);
            this.radioHoje.Name = "radioHoje";
            this.radioHoje.Size = new System.Drawing.Size(80, 22);
            this.radioHoje.TabIndex = 18;
            this.radioHoje.TabStop = true;
            this.radioHoje.Text = "De hoje";
            this.radioHoje.UseVisualStyleBackColor = true;
            this.radioHoje.CheckedChanged += new System.EventHandler(this.radioHoje_CheckedChanged);
            // 
            // radioMes
            // 
            this.radioMes.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.radioMes.AutoSize = true;
            this.radioMes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioMes.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.radioMes.Location = new System.Drawing.Point(222, 52);
            this.radioMes.Margin = new System.Windows.Forms.Padding(4);
            this.radioMes.Name = "radioMes";
            this.radioMes.Size = new System.Drawing.Size(101, 22);
            this.radioMes.TabIndex = 18;
            this.radioMes.TabStop = true;
            this.radioMes.Text = "Deste Mês";
            this.radioMes.UseVisualStyleBackColor = true;
            this.radioMes.CheckedChanged += new System.EventHandler(this.radioMes_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnCancelar);
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.dataGridView1);
            this.panel1.Controls.Add(this.btnImprimir);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1039, 451);
            this.panel1.TabIndex = 38;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1039, 451);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "WBridge Monitor";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panelOutros.ResumeLayout(false);
            this.panelOutros.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btnImprimir;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panelOutros;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dateAte;
        private System.Windows.Forms.DateTimePicker dateDe;
        private System.Windows.Forms.RadioButton radioOutros;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.RadioButton radioTodos;
        private System.Windows.Forms.RadioButton radioHoje;
        private System.Windows.Forms.RadioButton radioMes;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
    }
}

