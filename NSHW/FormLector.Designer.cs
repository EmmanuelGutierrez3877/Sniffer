namespace NSHW
{
    partial class FormLector
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormLector));
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.listViewInformacion = new System.Windows.Forms.ListView();
            this.textBoxDatos = new System.Windows.Forms.TextBox();
            this.textBoxNombre = new System.Windows.Forms.TextBox();
            this.buttonAbrir = new System.Windows.Forms.Button();
            this.buttonGuardar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(4, 488);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(243, 96);
            this.label2.TabIndex = 14;
            this.label2.Text = "Practica 10\r\nEquipo #4\r\nGutierrez Santillan Emmanuel Alejandro\r\nHurtado Valle Her" +
    "iberto\r\nLópez casillas Juan Carlos\r\nRodriguez Barrientos Antonio ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(18, 398);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 16);
            this.label1.TabIndex = 13;
            this.label1.Text = "Datos:";
            // 
            // listViewInformacion
            // 
            this.listViewInformacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listViewInformacion.Location = new System.Drawing.Point(10, 66);
            this.listViewInformacion.Name = "listViewInformacion";
            this.listViewInformacion.Size = new System.Drawing.Size(591, 330);
            this.listViewInformacion.TabIndex = 12;
            this.listViewInformacion.UseCompatibleStateImageBehavior = false;
            // 
            // textBoxDatos
            // 
            this.textBoxDatos.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxDatos.ForeColor = System.Drawing.SystemColors.WindowText;
            this.textBoxDatos.Location = new System.Drawing.Point(8, 417);
            this.textBoxDatos.Multiline = true;
            this.textBoxDatos.Name = "textBoxDatos";
            this.textBoxDatos.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxDatos.Size = new System.Drawing.Size(593, 59);
            this.textBoxDatos.TabIndex = 11;
            // 
            // textBoxNombre
            // 
            this.textBoxNombre.Enabled = false;
            this.textBoxNombre.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxNombre.Location = new System.Drawing.Point(118, 11);
            this.textBoxNombre.Multiline = true;
            this.textBoxNombre.Name = "textBoxNombre";
            this.textBoxNombre.Size = new System.Drawing.Size(482, 39);
            this.textBoxNombre.TabIndex = 10;
            // 
            // buttonAbrir
            // 
            this.buttonAbrir.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonAbrir.Location = new System.Drawing.Point(10, 11);
            this.buttonAbrir.Name = "buttonAbrir";
            this.buttonAbrir.Size = new System.Drawing.Size(93, 39);
            this.buttonAbrir.TabIndex = 9;
            this.buttonAbrir.Text = "Abrir archivo";
            this.buttonAbrir.UseVisualStyleBackColor = true;
            this.buttonAbrir.Click += new System.EventHandler(this.buttonAbrir_Click);
            // 
            // buttonGuardar
            // 
            this.buttonGuardar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonGuardar.Location = new System.Drawing.Point(479, 488);
            this.buttonGuardar.Name = "buttonGuardar";
            this.buttonGuardar.Size = new System.Drawing.Size(121, 39);
            this.buttonGuardar.TabIndex = 15;
            this.buttonGuardar.Text = "Guardar archivo";
            this.buttonGuardar.UseVisualStyleBackColor = true;
            this.buttonGuardar.Click += new System.EventHandler(this.button1_Click);
            // 
            // FormLector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(616, 593);
            this.Controls.Add(this.buttonGuardar);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listViewInformacion);
            this.Controls.Add(this.textBoxDatos);
            this.Controls.Add(this.textBoxNombre);
            this.Controls.Add(this.buttonAbrir);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "FormLector";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sniffer";
            this.Load += new System.EventHandler(this.FormLector_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView listViewInformacion;
        private System.Windows.Forms.TextBox textBoxDatos;
        private System.Windows.Forms.TextBox textBoxNombre;
        private System.Windows.Forms.Button buttonAbrir;
        private System.Windows.Forms.Button buttonGuardar;
    }
}