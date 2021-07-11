using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace labproject
{
    public class myAppUtilities
    {
        static OleDbConnection my_conn;
        static private int connect_status;
        public static void connec_to_accdb(string ac_filepath)
        {
            try
            {
                string constr = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + ac_filepath + ";Persist Security Info=False";
                my_conn = new OleDbConnection(constr);         // establishes connection to the database
                Console.WriteLine("New connection established");
                connect_status = 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                //return ex;
            }
        }

        public static void disconnec_to_accdb(string ac_filepath)
        {
            try { 
            if (connect_status == 1) { 
                string constr = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + ac_filepath + ";Persist Security Info=False";
                my_conn = new OleDbConnection(constr);         // establishes connection to the database
                Console.WriteLine("New connection established");
            }
            }
            catch (Exception ex)
            {
                //return ex;
            }
        }
        public static int get_connection_status()
        {
            return connect_status;
        }

        public static OleDbConnection get_connection()
        {
            return my_conn;
        }

        public static void save_settings()
        {
            Properties.Settings.Default.Save();
        }

        public static int check_newrow_data_is_all_empty(DataRow dr)
        {
            if (dr == null)
            {
                return 1;
            }
            else
            {
                foreach (var value in dr.ItemArray)
                {
                    if (value.ToString() != "" && value != null)
                    {
                        return 0;
                    }
                }
                return 1;
            }
        }

        public static int not_has_duplicate_in_column(DataRow newRowDat, DataTable dataTabl, List<string> columnName_list)
        {
            //Console.WriteLine("---not_has_duplicate_in_column");
            int duplicate_cnt = 0;
            for (int i = 1; i < dataTabl.Columns.Count; i++)
            {
                if (newRowDat[i] == DBNull.Value)
                {
                    //Console.WriteLine("---Null detected");
                    continue;
                }
                for (int j = 0; j < dataTabl.Rows.Count; j++)
                {
                    //Console.WriteLine("Value of newRowDat[{0}]={1},  dataTabl.Rows[{2}].ItemArray[{0}]={3}", i, newRowDat[i], j, dataTabl.Rows[j].ItemArray[i]);
                    //Console.WriteLine("Type 1: {0}, type 2 {1}", newRowDat[i].GetType().Name, dataTabl.Rows[j].ItemArray[i].GetType().Name);
                    /*
                     @note: newRowDat[i] == dataTabl.Rows[j].ItemArray[i] doesn't work as they're different type
                        So I have to use ToString() method as a workaround
                     */
                    if (newRowDat[i].ToString() == dataTabl.Rows[j].ItemArray[i].ToString())
                    {
                        Form1.inform_user_error("Error: Duplicate at [column name; row index] = [{0}; {1}]", columnName_list[i], j);
                        duplicate_cnt++;
                    }
                }
            }
            Console.WriteLine("Duplicate_cnt value =  {0}", duplicate_cnt);
            if (duplicate_cnt > 0)
            {
                Form1.inform_user_error("Please correct input textboxes before adding");
                return 0;
            }
            return 1;
        }
    }
}
