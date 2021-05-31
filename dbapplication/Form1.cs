using System;
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

            try
            {
                conn.Open();
                //string strSQL = "SELECT * FROM serial_tong_the";
                //OleDbCommand command = new OleDbCommand(strSQL, conn);
                // Open the connection and execute the select command.    
                /*
                 Get all the tables and column fields in the access database

                https://stackoverflow.com/questions/15978225/how-to-get-all-table-names-and-also-column-name-using-c-sharp-in-ms-access
                 */
                DataTable schema = conn.GetSchema("Columns");
                foreach (DataRow row in schema.Rows)
                    Console.WriteLine("TABLE:" + row.Field<string>("TABLE_NAME") +
                                      " COLUMN:" + row.Field<string>("COLUMN_NAME"));

                /*
                 Get usertable:
                https://stackoverflow.com/questions/1699897/retrieve-list-of-tables-in-ms-access-file
                 */
                // We only want user tables, not system tables
                string[] restrictions = new string[4];
                restrictions[3] = "Table";

                DataTable userTables = conn.GetSchema("Tables", restrictions);

                List<string> tableNames = new List<string>();
                Console.WriteLine("Print all the tables in the access database:");
                for (int i = 0; i < userTables.Rows.Count; i++)
                {
                    tableNames.Add(userTables.Rows[i][2].ToString());
                    Console.WriteLine("Table "+i+ userTables.Rows[i][2].ToString());
                }
                /*
                 Fetch all columns (data name) in an specific Access Table:
                    https://stackoverflow.com/questions/3775047/fetch-column-names-for-specific-table
                 */
                using (var cmd = new OleDbCommand("select * from serial_tong_the", conn))
                using (var reader = cmd.ExecuteReader(CommandBehavior.SchemaOnly))
                {
                    var table = reader.GetSchemaTable();
                    var nameCol = table.Columns["ColumnName"];
                    foreach (DataRow row in table.Rows)
                    {
                        Console.WriteLine(row[nameCol]);
                    }
                }
                /*
                 Load Data (Table Content) Into DataGridView From Access Database.
                    https://www.youtube.com/watch?v=Uc8BuvMQIfI&ab_channel=Tech%26TravelTV
                 */
                DataTable dtContent = new DataTable();
                using (var cmd = new OleDbCommand("select * from serial_tong_the", conn))
                {
                    OleDbDataReader reader = cmd.ExecuteReader();
                    dtContent.Load(reader);

                }
                dataGridView1.DataSource = dtContent;
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
        private DataTable GetTableContent()
        {
            DataTable dtContent = new DataTable();
            return dtContent;
        }
        List<CellPosition> data_change_cell_index = new List<CellPosition>();
        private void btn_save_Click(object sender, EventArgs e)
        {

            /*
             Save last DataGrid Content
             */
            // dataGridView1
            Console.WriteLine("There are total of {0} data change", data_change_cell_index.Count);
            for (int j=0; j< data_change_cell_index.Count; j++)
            {
                Console.WriteLine("Change number {0}: Row {1} Column {2}", j, data_change_cell_index[j].row, data_change_cell_index[j].col);
            }
            data_change_cell_index.Clear();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        /*
         Create a proper 2D int list in C#
         https://stackoverflow.com/questions/665299/are-2-dimensional-lists-possible-in-c
         */
        public class CellPosition
        {
            public int row { get; set; }
            public int col { get; set; }
        }

        
       // uint data_change_cell_index;
        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            Console.WriteLine("Data change detected");
            string msg = String.Format(
               "Cell at row {0}, column {1} value changed",
               e.RowIndex, e.ColumnIndex);
            Console.WriteLine(msg, "Cell Value Changed");

            data_change_cell_index.Add(new CellPosition
            {
                row = e.RowIndex,
                col = e.ColumnIndex
            });

            //  if ()
        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string msg = String.Format(
               "Cell at row {0}, column {1} double click",
               e.RowIndex, e.ColumnIndex);
            Console.WriteLine(msg, "Double Click");
        }
    }
}
