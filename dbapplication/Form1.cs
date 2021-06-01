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
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;

namespace labproject
{
    public partial class Form1 : Form
    {
        DataTable vt = new DataTable();
        string constr;
        string ace_file_path = "C:\\Users\\nguye\\Downloads\\m3_knan_tool\\knan_test_data.accdb";
        string wk_table_name = "serial_tong_the";
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
        string logfile_txt_path;
        string logfile_name = "knan_app_log.txt";
        private void Form1_Shown(object sender, EventArgs e)
        {
            constr = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + ace_file_path + ";Persist Security Info=False";

            Console.WriteLine(Properties.Settings.Default.log_file_path);
            logfile_txt_path = Properties.Settings.Default.log_file_path;
            /*
             Create new config file if not exist
            */
            if (!File.Exists(logfile_txt_path))
            {
                Console.WriteLine("File does not exist. Creating a new file");
                /*
                 Scan Drive on computer 
                https://stackoverflow.com/questions/5195653/how-to-get-all-drives-in-pc-with-net-using-c-sharp
                 */
                foreach (var drive in DriveInfo.GetDrives())
                {
                    Console.WriteLine("Drive Type: {0}", drive.Name);
                    Console.WriteLine("Drive Size: {0}", drive.TotalSize);
                    if (drive.Name != @"C:\" && drive.TotalSize > 50000)
                    {
                        logfile_txt_path = drive.Name + logfile_name;
                        Properties.Settings.Default.log_file_path = logfile_txt_path;
                        Properties.Settings.Default.Save();
                        break;
                    }
                }
                Console.WriteLine("Create file {0}", logfile_txt_path);
                File.CreateText(logfile_txt_path);
            }
            else
            {
                Console.WriteLine("File already exists");
                /*
                 Read the first line as excel file path.
                 */
            }
            //txt_filepath.Text = Properties.Settings.Default.excel_file_path;
            //ex_file_path = Properties.Settings.Default.excel_file_path;
            //txt_input_serial.Text = Properties.Settings.Default.char_template;
            //inputCol = Properties.Settings.Default.input_col;
            //outputCol = Properties.Settings.Default.output_col;
            write_new_log_message("New login");

        }

        private void write_new_log_message(string input)
        {
            //FileInfo fi = new FileInfo(logfile_txt_path);
            //while (IsFileLocked(fi))
            //{

            //}
            File.AppendAllText(logfile_txt_path, DateTime.Now.ToString("MM/dd/yyyy h:mm tt: ") + input + Environment.NewLine);
        }

        DataTable pre_GridData;
        DataTable dtContent = new DataTable();
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
                using (var cmd = new OleDbCommand("select * from " + wk_table_name, conn))
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
                
                using (var cmd = new OleDbCommand("select * from "+ wk_table_name, conn))
                {
                    OleDbDataReader reader = cmd.ExecuteReader();
                    dtContent.Load(reader);
                }


               // pre_GridData = dtContent;
                /*
                 get pointer to the DataSource
                 */
                dataGridView1.DataSource = dtContent;
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
            write_new_log_message(string.Format("Save button click by user: {0} with {1} changes", txt_user.Text, data_change_cell_index.Count));
            /*
             Bind data from dataGridView1 to Access Database:
            https://stackoverflow.com/questions/20998803/c-sharp-datatable-update-access-database
            https://stackoverflow.com/questions/6295161/how-to-build-a-datatable-from-a-datagridview

             */
            string query = "SELECT * FROM " + wk_table_name;
            using (OleDbCommand oledbCommand = new OleDbCommand(query, conn))
            {
                using (OleDbDataAdapter oledbDataAdapter = new OleDbDataAdapter(oledbCommand))
                {
                    using (OleDbCommandBuilder oledbCommandBuilder = new OleDbCommandBuilder(oledbDataAdapter))
                    {
                        oledbDataAdapter.DeleteCommand = oledbCommandBuilder.GetDeleteCommand(true);
                        oledbDataAdapter.InsertCommand = oledbCommandBuilder.GetInsertCommand(true);
                        oledbDataAdapter.UpdateCommand = oledbCommandBuilder.GetUpdateCommand(true);
                        oledbDataAdapter.Update(((DataTable)dataGridView1.DataSource));
                    }
                }
            }
            dataGridView1.ResetBindings();
            dataGridView1.Update();
            dataGridView1.Refresh();
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
            //pre_GridData.
            //Console.WriteLine("Cell Previous value is: {0}", pre_GridData.Rows[e.RowIndex].ItemArray[e.ColumnIndex]);

            Console.WriteLine("Cell Value Changed New Value is: {0}", dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);

            // 
            /*
             Perform data update here
             */
            dataGridView1.EndEdit();

            //string sql = "UPDATE CostT SET tFormSent = @selection1,TName = @UserName,FormDate = @FormDate where ReqNum = @ReqNum";
            //OleDbCommand cmd = new OleDbCommand(sql, conn);
            //cmd.Parameters.Add("@selection1", Selection1.Text);
            //cmd.Parameters.Add("@UserName", UserName.Text);
            //cmd.Parameters.Add("@FromDate", FromDate.Text);
            //cmd.Parameters.Add("@ReqNum", ReqNum.Text);
            //cmd.ExecuteNonQuery();
            //con22.Close();
        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string msg = String.Format(
               "Cell at row {0}, column {1} double click",
               e.RowIndex, e.ColumnIndex);
            Console.WriteLine(msg, "Double Click");
        }

        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            Console.WriteLine("new Row is automatically added");
            OleDbCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
      //      cmd.CommandText = "insert into "+ wk_table_name + " values();
        }

        private void dataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            var oldValue = dataGridView1[e.ColumnIndex, e.RowIndex].Value;
            Console.WriteLine("Cell Previous value is: {0}", oldValue);
            var newValue = e.FormattedValue;

        }
        /*
         https://stackoverflow.com/questions/2084346/how-to-delete-a-selected-datagridviewrow-and-update-a-connected-database-table
         */
        private void btn_del_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow item in this.dataGridView1.SelectedRows)
            {
                dataGridView1.Rows.RemoveAt(item.Index);
            }
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            //{
            //    if (string.IsNullOrEmpty(txt_search.Text))
            //        dataGridView1.DataSource = dtContent;
            //    else
            //    {
            //        var query = from o in dtContent.AsEnumerable()
            //                    where o.Field<double>("STT_TB") == Convert.ToInt32(txt_search.Text)
            //                    select o;
            //        dataGridView1.DataSource = query.ToList();
            //    }
            //}
        }
        string excel_file_path;
        /*
         https://stackoverflow.com/questions/8207869/how-to-export-datatable-to-excel
        */
        private void btn_excel_Click(object sender, EventArgs e)
        {
            excel_file_path = Application.StartupPath + @"\..\.." + @"\Database1.xlsx";
            // toExcelFile();
            dtContent.ExportToExcel(excel_file_path);
        }
       
        public void toExcelFile()
        {
            //Create COM Objects. Create a COM object for everything that is referenced
            Excel.Application xlApp = new Excel.Application();
            excel_file_path = Application.StartupPath + @"\..\.." + @"\Database.xlsx";
            Console.WriteLine(excel_file_path);
            Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(excel_file_path);
            //Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(@"E:\Cuahang_ap\Database.xlsx");
            //Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
            //Excel.Range xlRange = xlWorksheet.UsedRange;
            //decode = new string[rowCount, colCount];
            //rowCount = xlRange.Rows.Count;
            //colCount = xlRange.Columns.Count;
            //iterate over the rows and columns and print to the console as it appears in the file
            //excel is not zero based!!
            //for (int i = rowCount + 1; i <= rowCount + productList.Count; i++)
            //{
            //    xlRange.Cells[i, 1].Value2 = productList[i - rowCount - 1].Name;
            //    xlRange.Cells[i, 2].Value2 = productList[i - rowCount - 1].Image_url;
            //    xlRange.Cells[i, 3].Value2 = productList[i - rowCount - 1].Product_url;
            //    xlRange.Cells[i, 4].Value2 = productList[i - rowCount - 1].Price;
            //}
            xlWorkbook.Worksheets.Add(dtContent, "Sheet1");

            //cleanup
            GC.Collect();
            GC.WaitForPendingFinalizers();
            //rule of thumb for releasing com objects:
            //  never use two dots, all COM objects must be referenced and released individually
            //  ex: [somthing].[something].[something] is bad
            //  Console.WriteLine(decode[0, 0] + decode[1, 1]);
            //close and release
            xlWorkbook.Close();
            //quit and release
            xlApp.Quit();
        }
      

    }
}
