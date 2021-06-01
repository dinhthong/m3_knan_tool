
namespace labproject
{
    partial class FormConnect
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
            this.txt_filepath = new System.Windows.Forms.TextBox();
            this.btn_browsefile = new System.Windows.Forms.Button();
            this.openFileDialogacdb = new System.Windows.Forms.OpenFileDialog();
            this.cbb_tables_list = new System.Windows.Forms.ComboBox();
            this.btn_select_table = new System.Windows.Forms.Button();
            this.btc_connectdb = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txt_filepath
            // 
            this.txt_filepath.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_filepath.Location = new System.Drawing.Point(37, 39);
            this.txt_filepath.Name = "txt_filepath";
            this.txt_filepath.Size = new System.Drawing.Size(511, 26);
            this.txt_filepath.TabIndex = 0;
            // 
            // btn_browsefile
            // 
            this.btn_browsefile.Location = new System.Drawing.Point(572, 42);
            this.btn_browsefile.Name = "btn_browsefile";
            this.btn_browsefile.Size = new System.Drawing.Size(75, 23);
            this.btn_browsefile.TabIndex = 1;
            this.btn_browsefile.Text = "...";
            this.btn_browsefile.UseVisualStyleBackColor = true;
            this.btn_browsefile.Click += new System.EventHandler(this.btn_browsefile_Click);
            // 
            // openFileDialogacdb
            // 
            this.openFileDialogacdb.FileName = "openFileDialog1";
            // 
            // cbb_tables_list
            // 
            this.cbb_tables_list.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbb_tables_list.FormattingEnabled = true;
            this.cbb_tables_list.Location = new System.Drawing.Point(83, 137);
            this.cbb_tables_list.Name = "cbb_tables_list";
            this.cbb_tables_list.Size = new System.Drawing.Size(219, 24);
            this.cbb_tables_list.TabIndex = 2;
            // 
            // btn_select_table
            // 
            this.btn_select_table.Location = new System.Drawing.Point(333, 137);
            this.btn_select_table.Name = "btn_select_table";
            this.btn_select_table.Size = new System.Drawing.Size(75, 23);
            this.btn_select_table.TabIndex = 3;
            this.btn_select_table.Text = "button1";
            this.btn_select_table.UseVisualStyleBackColor = true;
            this.btn_select_table.Click += new System.EventHandler(this.btn_select_table_Click);
            // 
            // btc_connectdb
            // 
            this.btc_connectdb.Location = new System.Drawing.Point(572, 82);
            this.btc_connectdb.Name = "btc_connectdb";
            this.btc_connectdb.Size = new System.Drawing.Size(75, 23);
            this.btc_connectdb.TabIndex = 4;
            this.btc_connectdb.Text = "Connect DB";
            this.btc_connectdb.UseVisualStyleBackColor = true;
            this.btc_connectdb.Click += new System.EventHandler(this.btc_connectdb_Click);
            // 
            // FormConnect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(764, 497);
            this.Controls.Add(this.btc_connectdb);
            this.Controls.Add(this.btn_select_table);
            this.Controls.Add(this.cbb_tables_list);
            this.Controls.Add(this.btn_browsefile);
            this.Controls.Add(this.txt_filepath);
            this.Name = "FormConnect";
            this.Text = "FormConnect";
            this.Load += new System.EventHandler(this.FormConnect_Load);
            this.Shown += new System.EventHandler(this.FormConnect_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_filepath;
        private System.Windows.Forms.Button btn_browsefile;
        private System.Windows.Forms.OpenFileDialog openFileDialogacdb;
        private System.Windows.Forms.ComboBox cbb_tables_list;
        private System.Windows.Forms.Button btn_select_table;
        private System.Windows.Forms.Button btc_connectdb;
    }
}