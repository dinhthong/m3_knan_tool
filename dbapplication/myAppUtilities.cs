using System;
using System.Collections.Generic;
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
    }
}
