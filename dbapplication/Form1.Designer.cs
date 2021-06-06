namespace labproject
{
    partial class Form1
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
            this.dataGridViewQuery = new System.Windows.Forms.DataGridView();
            this.btn_test = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btn_save = new System.Windows.Forms.Button();
            this.btn_del = new System.Windows.Forms.Button();
            this.txt_user = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_search = new System.Windows.Forms.TextBox();
            this.btn_search = new System.Windows.Forms.Button();
            this.btn_excel = new System.Windows.Forms.Button();
            this.btn_insert = new System.Windows.Forms.Button();
            this.ccb_column_list = new System.Windows.Forms.ComboBox();
            this.btn_Connect = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dataGridView_err = new System.Windows.Forms.DataGridView();
            this.btn_adderr = new System.Windows.Forms.Button();
            this.txt_newerror = new System.Windows.Forms.TextBox();
            this.btn_report = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewQuery)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_err)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewQuery
            // 
            this.dataGridViewQuery.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewQuery.Location = new System.Drawing.Point(0, 101);
            this.dataGridViewQuery.Name = "dataGridViewQuery";
            this.dataGridViewQuery.Size = new System.Drawing.Size(455, 96);
            this.dataGridViewQuery.TabIndex = 2;
            this.dataGridViewQuery.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewQuery_CellContentClick);
            // 
            // btn_test
            // 
            this.btn_test.Location = new System.Drawing.Point(965, 116);
            this.btn_test.Name = "btn_test";
            this.btn_test.Size = new System.Drawing.Size(75, 23);
            this.btn_test.TabIndex = 4;
            this.btn_test.Text = "btn_test";
            this.btn_test.UseVisualStyleBackColor = true;
            this.btn_test.Click += new System.EventHandler(this.btn_test_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(22, 116);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(882, 361);
            this.dataGridView1.TabIndex = 5;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dataGridView1.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseDoubleClick);
            this.dataGridView1.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dataGridView1_CellValidating);
            this.dataGridView1.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellValueChanged);
            this.dataGridView1.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dataGridView1_RowsAdded);
            // 
            // btn_save
            // 
            this.btn_save.Location = new System.Drawing.Point(981, 183);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(75, 23);
            this.btn_save.TabIndex = 6;
            this.btn_save.Text = "SAVE";
            this.btn_save.UseVisualStyleBackColor = true;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // btn_del
            // 
            this.btn_del.Location = new System.Drawing.Point(981, 229);
            this.btn_del.Name = "btn_del";
            this.btn_del.Size = new System.Drawing.Size(75, 23);
            this.btn_del.TabIndex = 7;
            this.btn_del.Text = "DELETE";
            this.btn_del.UseVisualStyleBackColor = true;
            this.btn_del.Click += new System.EventHandler(this.btn_del_Click);
            // 
            // txt_user
            // 
            this.txt_user.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_user.Location = new System.Drawing.Point(870, 14);
            this.txt_user.Name = "txt_user";
            this.txt_user.Size = new System.Drawing.Size(227, 29);
            this.txt_user.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(708, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(144, 24);
            this.label1.TabIndex = 9;
            this.label1.Text = "Nguoi thuc hien";
            // 
            // txt_search
            // 
            this.txt_search.Location = new System.Drawing.Point(22, 500);
            this.txt_search.Name = "txt_search";
            this.txt_search.Size = new System.Drawing.Size(313, 20);
            this.txt_search.TabIndex = 10;
            // 
            // btn_search
            // 
            this.btn_search.Location = new System.Drawing.Point(376, 497);
            this.btn_search.Name = "btn_search";
            this.btn_search.Size = new System.Drawing.Size(75, 23);
            this.btn_search.TabIndex = 11;
            this.btn_search.Text = "search";
            this.btn_search.UseVisualStyleBackColor = true;
            this.btn_search.Click += new System.EventHandler(this.btn_search_Click);
            // 
            // btn_excel
            // 
            this.btn_excel.Location = new System.Drawing.Point(981, 321);
            this.btn_excel.Name = "btn_excel";
            this.btn_excel.Size = new System.Drawing.Size(75, 23);
            this.btn_excel.TabIndex = 12;
            this.btn_excel.Text = "Excel";
            this.btn_excel.UseVisualStyleBackColor = true;
            this.btn_excel.Click += new System.EventHandler(this.btn_excel_Click);
            // 
            // btn_insert
            // 
            this.btn_insert.Location = new System.Drawing.Point(866, 41);
            this.btn_insert.Name = "btn_insert";
            this.btn_insert.Size = new System.Drawing.Size(75, 31);
            this.btn_insert.TabIndex = 13;
            this.btn_insert.Text = "insert";
            this.btn_insert.UseVisualStyleBackColor = true;
            this.btn_insert.Click += new System.EventHandler(this.btn_insert_Click);
            // 
            // ccb_column_list
            // 
            this.ccb_column_list.FormattingEnabled = true;
            this.ccb_column_list.Location = new System.Drawing.Point(488, 500);
            this.ccb_column_list.Name = "ccb_column_list";
            this.ccb_column_list.Size = new System.Drawing.Size(186, 21);
            this.ccb_column_list.TabIndex = 14;
            // 
            // btn_Connect
            // 
            this.btn_Connect.Location = new System.Drawing.Point(981, 26);
            this.btn_Connect.Name = "btn_Connect";
            this.btn_Connect.Size = new System.Drawing.Size(149, 31);
            this.btn_Connect.TabIndex = 15;
            this.btn_Connect.Text = "NEW CONNECT";
            this.btn_Connect.UseVisualStyleBackColor = true;
            this.btn_Connect.Click += new System.EventHandler(this.btn_Connect_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(0, 53);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1152, 566);
            this.tabControl1.TabIndex = 16;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.txt_search);
            this.tabPage1.Controls.Add(this.btn_insert);
            this.tabPage1.Controls.Add(this.ccb_column_list);
            this.tabPage1.Controls.Add(this.btn_test);
            this.tabPage1.Controls.Add(this.btn_excel);
            this.tabPage1.Controls.Add(this.btn_Connect);
            this.tabPage1.Controls.Add(this.btn_del);
            this.tabPage1.Controls.Add(this.btn_save);
            this.tabPage1.Controls.Add(this.btn_search);
            this.tabPage1.Controls.Add(this.dataGridView1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1144, 540);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dataGridView_err);
            this.tabPage2.Controls.Add(this.btn_adderr);
            this.tabPage2.Controls.Add(this.txt_newerror);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1144, 540);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            this.tabPage2.Click += new System.EventHandler(this.tabPage2_Click);
            // 
            // dataGridView_err
            // 
            this.dataGridView_err.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_err.Location = new System.Drawing.Point(8, 83);
            this.dataGridView_err.Name = "dataGridView_err";
            this.dataGridView_err.Size = new System.Drawing.Size(742, 384);
            this.dataGridView_err.TabIndex = 7;
            // 
            // btn_adderr
            // 
            this.btn_adderr.Location = new System.Drawing.Point(920, 279);
            this.btn_adderr.Name = "btn_adderr";
            this.btn_adderr.Size = new System.Drawing.Size(155, 23);
            this.btn_adderr.TabIndex = 6;
            this.btn_adderr.Text = "ADD NEW ERROR";
            this.btn_adderr.UseVisualStyleBackColor = true;
            this.btn_adderr.Click += new System.EventHandler(this.btn_adderr_Click);
            // 
            // txt_newerror
            // 
            this.txt_newerror.Location = new System.Drawing.Point(772, 238);
            this.txt_newerror.Name = "txt_newerror";
            this.txt_newerror.Size = new System.Drawing.Size(303, 20);
            this.txt_newerror.TabIndex = 5;
            // 
            // btn_report
            // 
            this.btn_report.Location = new System.Drawing.Point(589, 13);
            this.btn_report.Name = "btn_report";
            this.btn_report.Size = new System.Drawing.Size(77, 32);
            this.btn_report.TabIndex = 17;
            this.btn_report.Text = "REPORT";
            this.btn_report.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1154, 626);
            this.Controls.Add(this.btn_report);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.dataGridViewQuery);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt_user);
            this.Name = "Form1";
            this.Text = "DatabaseApp";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewQuery)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_err)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.DataGridView dataGridViewQuery;
        private System.Windows.Forms.Button btn_test;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btn_save;
        private System.Windows.Forms.Button btn_del;
        private System.Windows.Forms.TextBox txt_user;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_search;
        private System.Windows.Forms.Button btn_search;
        private System.Windows.Forms.Button btn_excel;
        private System.Windows.Forms.Button btn_insert;
        private System.Windows.Forms.ComboBox ccb_column_list;
        private System.Windows.Forms.Button btn_Connect;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button btn_report;
        private System.Windows.Forms.Button btn_adderr;
        private System.Windows.Forms.TextBox txt_newerror;
        private System.Windows.Forms.DataGridView dataGridView_err;
    }
}

