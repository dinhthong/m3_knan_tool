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
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;
using System.Media;

namespace labproject
{
    public partial class Form1 : Form
    {
        DataTable vt = new DataTable();
        string constr;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        string error_table_name = "loi_tb_tongthe";
        private void formconnectForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            //txt_input_serial.Text = Properties.Settings.Default.char_template;
            /*
             @todos: check if the file path changes -> create new connection
                       if the table_name changes -> reload GridView
             */
            Console.WriteLine("Event: Form closed in the parent");
            load_DataTable_to_GridView(Properties.Settings.Default.access_table_name);
            load_DataTable_to_GridView_err(error_table_name);
            get_Columns_list_from_con_table(Properties.Settings.Default.access_table_name);
            create_input_row_boxes();
            lb_tablename.Text = Properties.Settings.Default.access_table_name;
        }


        private void dataGridViewQuery_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        
        private void Form1_Shown(object sender, EventArgs e)
        { 
            constr = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + Properties.Settings.Default.access_file_path + ";Persist Security Info=False";
            check_and_create_logfile_atshown();
            myAppUtilities.connec_to_accdb(Properties.Settings.Default.access_file_path);
            load_database();
            create_input_row_boxes();
            lb_tablename.Text = Properties.Settings.Default.access_table_name;
        }

        DataTable dtContent = new DataTable();
        List<TextBox> myTextboxList = new List<TextBox>();

        void load_database()
        {
            try
            {
                myAppUtilities.get_connection().Open();
                get_Tables_list_from_conn();
                get_Columns_list_from_con_table(Properties.Settings.Default.access_table_name);
                load_DataTable_to_GridView(Properties.Settings.Default.access_table_name);
                load_DataTable_to_GridView_err(error_table_name);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void btn_test_Click(object sender, EventArgs e)
        {
            SystemSounds.Hand.Play();
        }

        List<CellPosition> data_change_cell_index = new List<CellPosition>();
        private void btn_save_Click(object sender, EventArgs e)
        {
            /*
             Save last DataGrid Content
             */
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

            //   string query = "SELECT * FROM " + Properties.Settings.Default.access_table_name;
            string query = string.Format("SELECT * FROM {0}", Properties.Settings.Default.access_table_name);
            Console.WriteLine(query);
            using (OleDbCommand oledbCommand = new OleDbCommand(query, myAppUtilities.get_connection()))
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

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            Console.WriteLine("Data change detected");
            cell_value_change_action(e.RowIndex, e.ColumnIndex);
        }

        private void cell_value_change_action(int row_index, int col_index)
        {
            string msg = String.Format(
            "Cell at row {0}, column {1} value changed", row_index, col_index);
            Console.WriteLine(msg, "Cell Value Changed");
            data_change_cell_index.Add(new CellPosition
            {
                row = row_index,
                col = col_index
            });
            Console.WriteLine("Cell Value Changed New Value is: {0}", dataGridView1.Rows[row_index].Cells[col_index].Value);
            dataGridView1.EndEdit();
        }
        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string msg = String.Format(
               "Cell at row {0}, column {1} double click",
               e.RowIndex, e.ColumnIndex);
            Console.WriteLine(msg, "Double Click");
            if (e.ColumnIndex == -1)
            {
                /*
                 Fill text box with the values in this row
                 */
                fill_input_textboxes(ref inputTextboxList, dataGridView1, dataGridView1.CurrentCell.RowIndex);
                //dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
            }
            //
        }
        void fill_input_textboxes(ref List<TextBox> source_TextboxList, DataGridView inputDataGridView, int row_index)
        {
            for (int i=0; i< source_TextboxList.Count; i++)
            {
               source_TextboxList[i].Text = inputDataGridView[i, row_index].Value.ToString();
            }
        }
        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            Console.WriteLine("new Row is automatically added");
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
            string search_column = conn_info.columnNames[ccb_column_list.SelectedIndex];
            /*
             * https://stackoverflow.com/questions/31809201/how-to-use-textbox-to-search-data-in-data-grid-view
             * https://stackoverflow.com/questions/25392682/error-cannot-perform-like-operation-on-system-int32-and-system-string-sett
             */
            (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = string.Format("convert("+ search_column + ", 'System.String') LIKE '%{0}%'", txt_search.Text);

        }
        string excel_file_path;
        /*
         https://stackoverflow.com/questions/8207869/how-to-export-datatable-to-excel
        */
        private void btn_excel_Click(object sender, EventArgs e)
        {
            excel_file_path = Application.StartupPath + @"\..\.." + @"\Database1.xlsx";
            dtContent.ExportToExcel(excel_file_path);
        }
        connected_table conn_info = new connected_table();
        public class connected_table
        {
            public List<string> tableNames = new List<string>();
            public List<string> columnNames = new List<string>();
          //  public string selected_table;
        }

        private void get_Tables_list_from_conn()
        {
            conn_info.tableNames.Clear();
            string[] restrictions = new string[4];
            restrictions[3] = "Table";

            DataTable userTables = myAppUtilities.get_connection().GetSchema("Tables", restrictions);
            Console.WriteLine("**Print all the tables in the access database:");
            for (int i = 0; i < userTables.Rows.Count; i++)
            {
                conn_info.tableNames.Add(userTables.Rows[i][2].ToString());
                Console.WriteLine("Table " + i + ": " + userTables.Rows[i][2].ToString());
            }
        }

        private void get_Columns_list_from_con_table(string table_name)
        {
            ccb_column_list.Items.Clear();
            conn_info.columnNames.Clear();
            Console.WriteLine("**Print all the columns in table {0}", table_name);
            /*
                get Column schema of the table
                check if the table is correct (or handle exception)
            */
            using (var cmd = new OleDbCommand("select * from " + table_name, myAppUtilities.get_connection()))
            using (var reader = cmd.ExecuteReader(CommandBehavior.SchemaOnly))
            {
                var table = reader.GetSchemaTable();
                var nameCol = table.Columns["ColumnName"];
                foreach (DataRow row in table.Rows)
                {
                    Console.WriteLine(row[nameCol]);
                    conn_info.columnNames.Add(row[nameCol].ToString());
                    ccb_column_list.Items.Add(row[nameCol].ToString());
                }
            }
            ccb_column_list.SelectedIndex = 1;
        }
            /*
                This function must be tied to a Table, by user's selection
             */
            private void load_DataTable_to_GridView_err(string table_name)
            {
                /*
                    Method 2
                    https://stackoverflow.com/questions/15149491/how-to-display-data-in-datagridview-from-access-database/34288085
                 */
                try
            {
                string query = "SELECT * From " + table_name;
                //ds.Clear();
                using (OleDbDataAdapter table_Adapter = new OleDbDataAdapter(query, myAppUtilities.get_connection()))
                {
                    DataSet ds = new DataSet();
                    table_Adapter.Fill(ds);
                    dataGridView_err.DataSource = ds.Tables[0];
                }
            }   
            catch (Exception ex)
            {

            }

            }

        private void load_DataTable_to_GridView(string table_name)
        {
            //dataGridView1.DataSource = null;
            /*
             Method 1
             */
            //using (var cmd = new OleDbCommand("select * from " + table_name, myAppUtilities.get_connection()))
            //{
            //    OleDbDataReader reader = cmd.ExecuteReader();
            //    dtContent.Load(reader);
            //}

            /*
             Method 2
            https://stackoverflow.com/questions/15149491/how-to-display-data-in-datagridview-from-access-database/34288085
             */
            string query = "SELECT * From " + table_name;
            //ds.Clear();
            using (OleDbDataAdapter table_Adapter = new OleDbDataAdapter(query, myAppUtilities.get_connection()))
            {
                DataSet ds = new DataSet();
                table_Adapter.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
            }
        }

        private void check_and_create_logfile_atshown()
        {
            /*
                Create new config file if not exist
            */
            string logfile_txt_path;
            string logfile_name = "knan_app_log.txt";
            logfile_txt_path = Properties.Settings.Default.log_file_path;
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
                        myAppUtilities.save_settings();
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
            Console.WriteLine("Log txt file path is: {0}", Properties.Settings.Default.log_file_path);
            write_new_log_message("New login");
        }
        private void write_new_log_message(string input)
        {
            File.AppendAllText(Properties.Settings.Default.log_file_path, DateTime.Now.ToString("MM/dd/yyyy h:mm tt: ") + input + Environment.NewLine);
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

        private void btn_Connect_Click(object sender, EventArgs e)
        {
            myAppUtilities.connec_to_accdb(Properties.Settings.Default.access_file_path);
            FormConnect formconnect = new FormConnect();
            formconnect.Show();
            formconnect.FormClosed += new FormClosedEventHandler(formconnectForm_FormClosed);
        }

        private void btn_adderr_Click(object sender, EventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }
        /*
         * Dynamically create array of textboxes in C#
         * https://stackoverflow.com/questions/9368748/dynamically-create-multiple-textboxes-c-sharp
         https://stackoverflow.com/questions/18092711/textbox-not-showing-in-winforms-form
           It always solve my previous error (object reference not set to an instance of an object.') when trying to loop:
         */
        
        List<TextBox> inputTextboxList = new List<TextBox>();
        List<Label> inputLabelList = new List<Label>();
        private void create_input_row_boxes()
        {
            if (inputTextboxList.Count>0)
            {
                for (int i = 0; i < inputTextboxList.Count; i++)
                {
                    this.tabPage1.Controls.Remove(inputTextboxList[i]);
                    inputTextboxList[i].Dispose();
                    this.tabPage1.Controls.Remove(inputLabelList[i]);
                    inputLabelList[i].Dispose();
                }
            }
            inputTextboxList.Clear();
            inputLabelList.Clear();
            TextBox[] txtTeamNames = new TextBox[conn_info.columnNames.Count];
            //int txtbox_y_location = 60;
            int x_loc=0, y_loc=0;
            for (int i = 0; i < conn_info.columnNames.Count; i++)
            {
                var txt = new TextBox();
                txtTeamNames[i] = txt;
                txt.Name = "txtbox" + Convert.ToString(i);
                get_x_y_location(ref x_loc, ref y_loc, i, 65, 1100, 75);
                txt.Location = new Point(x_loc, y_loc);
                txt.Visible = true;
                this.tabPage1.Controls.Add(txt);
                Console.WriteLine("Create text box {0}", i);
                txt.BringToFront();
                inputTextboxList.Add(txt);
                if (i == 0)
                {
                    inputTextboxList[0].Enabled = false;
                }
            }
            
            Label[] lb_names = new Label[conn_info.columnNames.Count];
            
            for (int i = 0; i < conn_info.columnNames.Count; i++)
            {
                var lb = new Label();
                lb_names[i] = lb;
                lb.Name = "lb" + Convert.ToString(i);
                lb.Text = conn_info.columnNames[i];
                lb.Location = new Point(10 + inputTextboxList[i].Location.X, inputTextboxList[i].Location.Y -25);
                lb.Visible = true;
                this.tabPage1.Controls.Add(lb);
                Console.WriteLine("Create Label {0}", i);
                lb.BringToFront();
                inputLabelList.Add(lb);
            }
        }
        /*
         Calculate the x, y location for a element based on allowed location range.
         */
        private void get_x_y_location(ref int x_location, ref int y_location, int index, int min_x_location, int max_x_location, int min_y_location)
        {
            int element_x_width = 110, element_y_width = 60;

            int step = 0;
            int x_width = max_x_location - min_x_location, n = x_width / element_x_width;

            x_location = min_x_location + element_x_width * index;
            
            while (x_location + element_x_width > max_x_location)
            {
                step++;
                x_location = x_location - n*element_x_width;
            }        
            y_location = min_y_location + element_y_width * step;
        }

        private void btn_insert_Click(object sender, EventArgs e)
        {
            /*
             * https://stackoverflow.com/questions/10771048/how-to-define-an-array-of-textboxes-in-c/10771284
             loop through column count
                * https://stackoverflow.com/questions/337797/adding-new-row-to-datatables-top
                * note that the current datagridview is not bound to a dataset so u can't programmatically create and add new row to this
             */

            DataTable dataTable = dataGridView1.DataSource as DataTable;
            DataRow newRow = dataTable.NewRow();
            
            int healthy_rowdata_flag = check_and_verify_input_data(ref newRow, inputTextboxList);
            /*
                Check and validate input value before actually inserting new row to dataTable
            */
            int newrow_data_valid = 0;
            if (healthy_rowdata_flag<0)
            {
                MessageBox.Show("Input data is not in correct type, abort inserting");
            }
            else
            {
                /*
                    Get number of row and insert at the end of the dataTable (new Row)
                 */
                if (myAppUtilities.not_has_duplicate_in_column(newRow, dataTable, conn_info.columnNames) != 0)
                {
                    newrow_data_valid = 1;
                }
            }
            
            if (newrow_data_valid == 1)
               dataTable.Rows.InsertAt(newRow, dataTable.Rows.Count);
        }
        /*
         * check duplication in newRow data in each column
         * fill good values to newRowdata
        */
        int check_and_verify_input_data(ref DataRow newRowData, List<TextBox> source_TextboxList)
        {
            char[] charsToTrim = {' ', '\'' };
            string trimmed_text;
            int error_cnt = 0;
            /*
             First check: legal input as field definition
             */
            for (int i = 1; i < conn_info.columnNames.Count; i++)
            {
                trimmed_text = source_TextboxList[i].Text.Trim(charsToTrim);
                //Console.WriteLine(newRowData[i].GetType());
                /*
                    ensure input TextboxList data is all in correct format (pre-defined by setting)
                    by try catch
                 */
                try
                {
                    if (trimmed_text == "")
                    {
                        /*
                         https://stackoverflow.com/questions/5120914/setting-a-datarow-item-to-null
                         */
                        newRowData[i] = DBNull.Value;
                    }
                    else
                    {
                        newRowData[i] = trimmed_text;
                    }
                    
                }
                catch (Exception e)
                {
                    inform_user_error("Input data is illegal!");
                    inform_user_error(e.Message);
                    error_cnt++;
                }
            }
            /*
             Second check: At least 1 column isn't empty
             */
            if (myAppUtilities.check_newrow_data_is_all_empty(newRowData) == 1)
            {
                return -1;

            }
            if (error_cnt > 0)
            {
                return -2;
            }
                
            return 1;
        }

        public static void inform_user_error(string mes, params object[] args)
        {
            Console.WriteLine("****User box:");
            Console.WriteLine(mes, args);   
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            Console.WriteLine("Selection changed event test");
            //Console.WriteLine("Row: {0}, Column: {1}", dataGridView1.CurrentCell.RowIndex, dataGridView1.CurrentCell.ColumnIndex);
            fill_input_textboxes(ref inputTextboxList, dataGridView1, dataGridView1.CurrentCell.RowIndex);
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            //Console.WriteLine("button update clicked {0}", dataGridView1.CurrentCell.RowIndex);
            int insert_row_index = dataGridView1.CurrentCell.RowIndex;

            DataTable dataTable = dataGridView1.DataSource as DataTable;
            //DataRow newRow = dataTable.NewRow();
            /*
             Reference assignment
             */
            DataRow currentRow = dataTable.Rows[insert_row_index] as DataRow;
            
            for (int k = 0; k < dataTable.Columns.Count; k++)
                Console.WriteLine("Column: {0}, Content = {1}", k, currentRow[k]);
            //dataTable.Rows.RemoveAt(insert_row_index);
            /*
              @Todos: Get collection of [modify update, blank update, no update] indexes
                - Modify update: Specify the reason to update
                - Blank update: Accept and log
                - No update: Do nothing
             */
            int healthy_rowdata_flag = check_and_verify_input_data(ref currentRow, inputTextboxList);
            /*
                Check and validate input value before actually inserting new row to dataTable
            */
            int newrow_data_valid = 0;
            if (healthy_rowdata_flag < 0)
            {
                MessageBox.Show("Input data is not in correct type, abort inserting");
            }
            else
            {
                /*
                 * @todos: check duplicate in modify and blank update columns
                    Get number of row and insert at the end of the dataTable (new Row)
                 */
                //if (not_has_duplicate_in_column(currentRow, dataTable, conn_info.columnNames) != 0)
                //{
                //    newrow_data_valid = 1;
                //}
            }

            //if (newrow_data_valid == 1)
            //{
            //    //dataTable.Rows.RemoveAt(insert_row_index);
            //    dataTable.Rows.InsertAt(newRow, insert_row_index);
            //}
            //else
            //{
            //    for (int k = 0; k < dataTable.Columns.Count; k++)
            //        Console.WriteLine("Column: {0}, Content = {1}", k, oldRow[k]);
            //    MessageBox.Show("InsertAt(oldRow, insert_row_index)");
            //    dataTable.Rows.InsertAt(oldRow, insert_row_index);
            //}
                

            /*
             Get the row index
             */
        }
    }
}
