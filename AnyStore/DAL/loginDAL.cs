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
using Microsoft.Extensions.Logging;

namespace AnyStore.DAL
{
    class loginDAL
    {
		private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
        //Static String to Connect Database
        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        public bool loginCheck(loginBLL l)
        {
			_logger.LogDebug(">>> Entering loginCheck(l={L})", l);
            //Create a boolean variable and set its value to false and return it
            bool isSuccess = false;
			_logger.LogDebug("loginCheck(l={L}): isSuccess → {IsSuccess}", l, isSuccess);

            //Connecting To DAtabase
            SqlConnection conn = new SqlConnection(myconnstrng);
			_logger.LogDebug("loginCheck(l={L}): conn → {Conn}", l, conn);

            try
            {
                //SQL Query to check login
                string sql = "SELECT * FROM tbl_users WHERE username=@username AND password=@password AND user_type=@user_type";

                //Creating SQL Command to pass value
                SqlCommand cmd = new SqlCommand(sql, conn);
				_logger.LogDebug("loginCheck(l={L}): cmd → {Cmd}", l, cmd);

                cmd.Parameters.AddWithValue("@username", l.username);
                cmd.Parameters.AddWithValue("@password", l.password);
                cmd.Parameters.AddWithValue("@user_type", l.user_type);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
				_logger.LogDebug("loginCheck(l={L}): adapter → {Adapter}", l, adapter);

                DataTable dt = new DataTable();
				_logger.LogDebug("loginCheck(l={L}): dt → {Dt}", l, dt);
				var __logStart = System.Diagnostics.Stopwatch.StartNew();

                adapter.Fill(dt);
				_logger.LogInformation("loginCheck(l)={L}: find query executed in {Arg1} ms", l, __logStart.ElapsedMilliseconds);

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
				_logger.LogError("Exception in loginCheck(l={L}): {Arg1}", l, ex.Message);
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

			_logger.LogDebug("<<< Exiting loginCheck(l={L})", l);
            return isSuccess;
        }
    }
}
