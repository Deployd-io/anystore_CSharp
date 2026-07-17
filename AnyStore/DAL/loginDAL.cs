using AnyStore.BLL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnyStore.DAL
{
    class loginDAL
    {
        //Static String to Connect Database
        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        public bool loginCheck(loginBLL l)
        {
			Console.WriteLine(">>> Entering loginCheck(l={0})", l);
			var __logStart = System.Diagnostics.Stopwatch.StartNew();
            //Create a boolean variable and set its value to false and return it
            bool isSuccess = false;

			Console.WriteLine("loginCheck(l={0}): isSuccess → {1}", l, isSuccess);
            //Connecting To DAtabase
            SqlConnection conn = new SqlConnection(myconnstrng);

			Console.WriteLine("loginCheck(l={0}): conn → {1}", l, conn);
            try
            {
                //SQL Query to check login
                string sql = "SELECT * FROM tbl_users WHERE username=@username AND password=@password AND user_type=@user_type";

                //Creating SQL Command to pass value
                SqlCommand cmd = new SqlCommand(sql, conn);

				Console.WriteLine("loginCheck(l={0}): cmd → {1}", l, cmd);
                cmd.Parameters.AddWithValue("@username", l.username);
                cmd.Parameters.AddWithValue("@password", l.password);
                cmd.Parameters.AddWithValue("@user_type", l.user_type);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

				Console.WriteLine("loginCheck(l={0}): adapter → {1}", l, adapter);
                DataTable dt = new DataTable();

				Console.WriteLine("loginCheck(l={0}): dt → {1}", l, dt);
				Console.WriteLine("loginCheck(l)={0}: find query executed in {1} ms", l, __logStart.ElapsedMilliseconds);
                adapter.Fill(dt);

                //Checking The rows in DataTable 
                if(dt.Rows.Count>0)
                {
                    //Login Sucessful
                    isSuccess = true;
                }
                else
                {
                    //Login Failed
                    isSuccess = false;
                }
            }
            catch(Exception ex)
            {
				Console.Error.WriteLine("Exception in loginCheck(l={0}): {1}", l, ex.Message);
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

			Console.WriteLine("<<< Exiting loginCheck(l={0})", l);
            return isSuccess;
        }
    }
}
