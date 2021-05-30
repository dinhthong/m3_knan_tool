﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace labproject
{
    public partial class Form1 : Form
    {
        DataTable vt = new DataTable();
        string constr;
        string ace_file_path = "C:\\Users\\nguye\\Downloads\\m3_knan_tool\\knan_test_data.accdb";
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            disconnectToolStripMenuItem.Enabled = false;
            runQueryToolStripMenuItem.ShortcutKeys = Keys.F5;
            insertRecordToolStripMenuItem.ShortcutKeys = Keys.F6;
            updateRecordToolStripMenuItem.ShortcutKeys = Keys.F7;
            deleteRecordToolStripMenuItem.ShortcutKeys = Keys.F8;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        OleDbConnection conn;
        private void connectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            conn = new OleDbConnection(constr);         // establishes connection to the database
            disconnectToolStripMenuItem.Enabled = true;
            connectToolStripMenuItem.Enabled = false;
        }

        private void disconnectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OleDbConnection conn = new OleDbConnection(constr);
            conn.Close();
            disconnectToolStripMenuItem.Enabled = false;
            connectToolStripMenuItem.Enabled = true;
        }

        private void runQueryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string sqlstr = textBoxQuery.Text;
            if (connectToolStripMenuItem.Enabled == false)
            {
                try
                {
                    OleDbDataAdapter da = new OleDbDataAdapter(sqlstr, constr);
                    da.Fill(vt);
                    da.Dispose();
                    dataGridViewQuery.DataSource = null;
                    dataGridViewQuery.Refresh();
                    dataGridViewQuery.Rows.Clear();
                    dataGridViewQuery.DataSource = vt;
                }
                catch (Exception ar)
                {
                    MessageBox.Show(ar.Message);
                }
            }
            else
            {
                MessageBox.Show("Please connect to the database first!");
            }
        }

        private void updateRecordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (connectToolStripMenuItem.Enabled == false)
            {
                FormUpdate formupdate = new FormUpdate();
                formupdate.Show();
                this.Visible = false;
            }
            else
            {
                MessageBox.Show("Please connect to the database first!");
            }
        }

        private void insertRecordToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (connectToolStripMenuItem.Enabled == false)
            {
                FormInsert forminsert = new FormInsert();
                forminsert.Show();
                this.Visible = false;
            }
            else
            {
                MessageBox.Show("Please connect to the database first!");
            }
        }

        private void deleteRecordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (connectToolStripMenuItem.Enabled == false)
            {
                FormDelete formdelete = new FormDelete();
                formdelete.Show();
                this.Visible = false;
            }
            else
            {
                MessageBox.Show("Please connect to the database first!");
            }
        }

        private void dataGridViewQuery_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            constr = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + ace_file_path + ";Persist Security Info=False";
        }

        private void btn_test_Click(object sender, EventArgs e)
        {
            string strSQL = "SELECT * FROM serial_tong_the";
            OleDbCommand command = new OleDbCommand(strSQL, conn);
            // Open the connection and execute the select command.    
            try
            {
                // Open connecton    
                conn.Open();
                /*
                 Get all the tables and column fields in the access database

                https://stackoverflow.com/questions/15978225/how-to-get-all-table-names-and-also-column-name-using-c-sharp-in-ms-access
                 */
                DataTable schema = conn.GetSchema("Columns");
                foreach (DataRow row in schema.Rows)
                    Console.WriteLine("TABLE:" + row.Field<string>("TABLE_NAME") +
                                      " COLUMN:" + row.Field<string>("COLUMN_NAME"));
                // Execute command    
                //using (OleDbDataReader reader = command.ExecuteReader())
                //{
                //    Console.WriteLine("------------Original data----------------");
                //    while (reader.Read())
                //    {
                //        Console.WriteLine("{0} {1}", reader["Name"].ToString(), reader["Address"].ToString());
                //    }
                //}
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
