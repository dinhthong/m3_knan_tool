using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace labproject
{
    public partial class FormConnect : Form
    {
        public FormConnect()
        {
            InitializeComponent();
        }
        string access_file_path, selected_table;
        private void btn_browsefile_Click(object sender, EventArgs e)
        {
            /**/
            OpenFileDialog openFileDialogacdb = new OpenFileDialog();
            openFileDialogacdb.InitialDirectory = System.IO.Directory.GetCurrentDirectory();
            /*
             To be opened Supported files
             */
            openFileDialogacdb.Filter = "Access (*.accdb)|*.accdb";
            openFileDialogacdb.FilterIndex = 2;
            openFileDialogacdb.RestoreDirectory = true;
            /*
             If the file path changes
             */
            if (openFileDialogacdb.ShowDialog() == DialogResult.OK)
            {
                if (openFileDialogacdb.FileName != access_file_path)
                { 
                    access_file_path = openFileDialogacdb.FileName;
                   // txt_filepath.Text = openFileDialogacdb.FileName;
                    Properties.Settings.Default.access_file_path = access_file_path;
                    Properties.Settings.Default.Save();
                    cbb_tables_list.Items.Clear();
                  //  btn_read.Enabled = false;
                }
            }
            /*
             Connect to the database
             */

            //conn = new OleDbConnection(constr);         // establishes connection to the database
            //disconnectToolStripMenuItem.Enabled = true;
            //connectToolStripMenuItem.Enabled = false;
           // FormConnect formconnect = new FormConnect();
          //  formconnect.Show();
        }
        //void connec_to_accdb(string filepath)
        //{

        //}

        private void FormConnect_Shown(object sender, EventArgs e)
        {
            txt_filepath.Text = Properties.Settings.Default.access_file_path;
            access_file_path = Properties.Settings.Default.access_file_path;
            selected_table = Properties.Settings.Default.access_table_name;
            if (access_file_path != null) {
                try
                {
                    myAppUtilities.connec_to_accdb(access_file_path);
                    myAppUtilities.get_connection().Open();
                    /*
                     Show list of tables and bind to combobox
                     */

                    /*
                       Get usertable:
                      https://stackoverflow.com/questions/1699897/retrieve-list-of-tables-in-ms-access-file
                       */
                    // We only want user tables, not system tables
                    string[] restrictions = new string[4];
                    restrictions[3] = "Table";

                    DataTable userTables = myAppUtilities.get_connection().GetSchema("Tables", restrictions);

                    List<string> tableNames = new List<string>();
                    //Console.WriteLine("Print all the tables in the access database:");
                    for (int i = 0; i < userTables.Rows.Count; i++)
                    {
                        tableNames.Add(userTables.Rows[i][2].ToString());
                     //   Console.WriteLine("Table " + i + userTables.Rows[i][2].ToString());
                        cbb_tables_list.Items.Add(userTables.Rows[i][2].ToString());
                    }
                    cbb_tables_list.SelectedIndex = tableNames.IndexOf(selected_table); ;
                }
                catch (Exception ex)
                {

                }
            }


        }

        private void FormConnect_Load(object sender, EventArgs e)
        {

        }

        private void btc_connectdb_Click(object sender, EventArgs e)
        {
            cbb_tables_list.Items.Clear();
            myAppUtilities.connec_to_accdb(access_file_path);
            myAppUtilities.get_connection().Open();
            /*
             Show list of tables and bind to combobox
             */

            /*
               Get usertable:
              https://stackoverflow.com/questions/1699897/retrieve-list-of-tables-in-ms-access-file
               */
            // We only want user tables, not system tables
            string[] restrictions = new string[4];
            restrictions[3] = "Table";

            DataTable userTables = myAppUtilities.get_connection().GetSchema("Tables", restrictions);

            List<string> tableNames = new List<string>();
            Console.WriteLine("Print all the tables in the access database:");
            for (int i = 0; i < userTables.Rows.Count; i++)
            {
                tableNames.Add(userTables.Rows[i][2].ToString());
                Console.WriteLine("Table " + i + userTables.Rows[i][2].ToString());
                cbb_tables_list.Items.Add(userTables.Rows[i][2].ToString());
            }
            cbb_tables_list.SelectedIndex = 0;
        }

        private void btn_select_table_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.access_table_name = cbb_tables_list.GetItemText(this.cbb_tables_list.SelectedItem); ;
        }
    }
}
