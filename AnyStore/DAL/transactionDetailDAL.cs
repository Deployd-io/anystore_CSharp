using AnyStore.BLL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnyStore.DAL
{
    class transactionDetailDAL
    {
        //Create Connection String
        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        #region Insert Method for Transaction Detail
        public bool InsertTransactionDetail(transactionDetailBLL td)
        {
			System.Diagnostics.Trace.WriteLine(string.Format(">>> Entering InsertTransactionDetail(td={0})", td));
            //Create a boolean value and set its default value to false
            bool isSuccess = false;
			System.Diagnostics.Trace.WriteLine(string.Format("InsertTransactionDetail(td={0}): isSuccess → {1}", td, isSuccess));

            //Create a database connection here
            SqlConnection conn = new SqlConnection(myconnstrng);
			System.Diagnostics.Trace.WriteLine(string.Format("InsertTransactionDetail(td={0}): conn → {1}", td, conn));

            try
            {
                //Sql Query to Insert Transaction detais
                string sql = "INSERT INTO tbl_transaction_detail (product_id, rate, qty, total, dea_cust_id, added_date, added_by) VALUES (@product_id, @rate, @qty, @total, @dea_cust_id, @added_date, @added_by)";

                //Passing the value to the SQL Query
                SqlCommand cmd = new SqlCommand(sql, conn);
				System.Diagnostics.Trace.WriteLine(string.Format("InsertTransactionDetail(td={0}): cmd → {1}", td, cmd));
                //Passing the values using cmd
                cmd.Parameters.AddWithValue("@product_id", td.product_id);
                cmd.Parameters.AddWithValue("@rate", td.rate);
                cmd.Parameters.AddWithValue("@qty", td.qty);
                cmd.Parameters.AddWithValue("@total", td.total);
                cmd.Parameters.AddWithValue("@dea_cust_id", td.dea_cust_id);
                cmd.Parameters.AddWithValue("@added_date", td.added_date);
                cmd.Parameters.AddWithValue("@added_by", td.added_by);

                //Open Database connection
                conn.Open();

                //declare the int variable and execute the query
				var __logStart = System.Diagnostics.Stopwatch.StartNew();
                int rows = cmd.ExecuteNonQuery();

				System.Diagnostics.Trace.WriteLine(string.Format("InsertTransactionDetail(td)={0}: save query executed in {1} ms", td, __logStart.ElapsedMilliseconds));
                if(rows>0)
                {
					System.Diagnostics.Trace.WriteLine(string.Format("InsertTransactionDetail(td={0}): rows → {1}", td, rows));
                    //Query Executed Successfully
                    isSuccess = true;
                }
                else
                {
                    //FAiled to Execute Query
                    isSuccess = false;
                }
            }
            catch(Exception ex)
            {
				System.Diagnostics.Trace.WriteLine(string.Format("Exception in InsertTransactionDetail(td={0}): {1}", td, ex.Message));
                MessageBox.Show(ex.Message);
            }
            finally
            {
                //Close Database Connection
                conn.Close();
            }
			System.Diagnostics.Trace.WriteLine(string.Format("<<< Exiting InsertTransactionDetail(td={0})", td));
            return isSuccess;
        }
        #endregion
    }
}
