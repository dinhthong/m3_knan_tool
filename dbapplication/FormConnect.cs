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
        string ex_file_path;
        private void btn_browsefile_Click(object sender, EventArgs e)
        {
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
                if (openFileDialogacdb.FileName != ex_file_path)
                { 
                    ex_file_path = openFileDialogacdb.FileName;
                    txt_filepath.Text = openFileDialogacdb.FileName;
                    Properties.Settings.Default.access_file_path = ex_file_path;
                    Properties.Settings.Default.Save();
                  //  btn_read.Enabled = false;
                }
            }
        }

        private void FormConnect_Shown(object sender, EventArgs e)
        {
            txt_filepath.Text = Properties.Settings.Default.access_file_path;
            ex_file_path = Properties.Settings.Default.access_file_path;
        }
    }
}
