namespace NSHW
{
    partial class GUI
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GUI));
            this.adapters_label = new System.Windows.Forms.Label();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.start_button = new System.Windows.Forms.Button();
            this.adapters_list = new System.Windows.Forms.ComboBox();
            this.stop_button = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // adapters_label
            // 
            this.adapters_label.AutoSize = true;
            this.adapters_label.BackColor = System.Drawing.Color.Transparent;
            this.adapters_label.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.adapters_label.ForeColor = System.Drawing.Color.Black;
            this.adapters_label.Location = new System.Drawing.Point(13, 12);
            this.adapters_label.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.adapters_label.Name = "adapters_label";
            this.adapters_label.Size = new System.Drawing.Size(252, 19);
            this.adapters_label.TabIndex = 15;
            this.adapters_label.Text = "Selecciona Network Adeptar :";
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader6});
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.Location = new System.Drawing.Point(13, 102);
            this.listView1.Margin = new System.Windows.Forms.Padding(4);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(564, 483);
            this.listView1.TabIndex = 14;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.DoubleClick += new System.EventHandler(this.listView1_DoubleClick);
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Datos Capturados";
            this.columnHeader6.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader6.Width = 1000;
            // 
            // start_button
            // 
            this.start_button.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.start_button.ForeColor = System.Drawing.Color.Black;
            this.start_button.Location = new System.Drawing.Point(13, 53);
            this.start_button.Margin = new System.Windows.Forms.Padding(4);
            this.start_button.Name = "start_button";
            this.start_button.Size = new System.Drawing.Size(176, 32);
            this.start_button.TabIndex = 13;
            this.start_button.Text = "Capturar Datos";
            this.start_button.UseVisualStyleBackColor = true;
            this.start_button.Click += new System.EventHandler(this.start_button_Click);
            // 
            // adapters_list
            // 
            this.adapters_list.FormattingEnabled = true;
            this.adapters_list.Location = new System.Drawing.Point(273, 12);
            this.adapters_list.Margin = new System.Windows.Forms.Padding(4);
            this.adapters_list.Name = "adapters_list";
            this.adapters_list.Size = new System.Drawing.Size(304, 21);
            this.adapters_list.TabIndex = 12;
            // 
            // stop_button
            // 
            this.stop_button.BackColor = System.Drawing.Color.Transparent;
            this.stop_button.Enabled = false;
            this.stop_button.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.stop_button.ForeColor = System.Drawing.Color.Black;
            this.stop_button.Location = new System.Drawing.Point(196, 53);
            this.stop_button.Name = "stop_button";
            this.stop_button.Size = new System.Drawing.Size(186, 32);
            this.stop_button.TabIndex = 23;
            this.stop_button.Text = "Detener";
            this.stop_button.UseVisualStyleBackColor = false;
            this.stop_button.Click += new System.EventHandler(this.stop_button_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Transparent;
            this.button1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.button1.ForeColor = System.Drawing.Color.Black;
            this.button1.Location = new System.Drawing.Point(388, 53);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(189, 32);
            this.button1.TabIndex = 24;
            this.button1.Text = "Abrir manualmente";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // GUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(590, 598);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.stop_button);
            this.Controls.Add(this.adapters_label);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.start_button);
            this.Controls.Add(this.adapters_list);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "GUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sniffer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label adapters_label;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.Button start_button;
        private System.Windows.Forms.ComboBox adapters_list;
        private System.Windows.Forms.Button stop_button;
        private System.Windows.Forms.Timer timer1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Button button1;
    }
}

