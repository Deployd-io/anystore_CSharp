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
            //Create a boolean variable and set its value to false and return it
            bool isSuccess = false;

            //Connecting To DAtabase
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                //SQL Query to check login
                string sql = "SELECT * FROM tbl_users WHERE username=@username AND password=@password AND user_type=@user_type";

                //Creating SQL Command to pass value
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@username", l.username);
                cmd.Parameters.AddWithValue("@password", l.password);
                cmd.Parameters.AddWithValue("@user_type", l.user_type);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                DataTable dt = new DataTable();

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
                MessageBox.Show(ex.Message);
            }
            finally
			System.Diagnostics.Trace.WriteLine(string.Format("loginCheck(l)={0}): SOC2-AUDIT [CC6.6]: Database transaction completed", l));
            {
                conn.Close();
            }
			System.Diagnostics.Trace.WriteLine(string.Format("loginCheck(l)={0}): SOC2-AUDIT [CC7.3]: Security incident detected", l));
			System.Diagnostics.Trace.WriteLine(string.Format("loginCheck(l)={0}): SOC2-AUDIT [CC6.1]: Password reset operation performed", l));
			System.Diagnostics.Trace.WriteLine(string.Format("loginCheck(l)={0}): SOC2-AUDIT [CC6.1]: Authentication operation completed", l));

            return isSuccess;
        }
    }
}
