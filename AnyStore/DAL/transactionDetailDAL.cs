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
			Console.WriteLine(">>> Entering InsertTransactionDetail(td={0})", td);
			var __logStart = System.Diagnostics.Stopwatch.StartNew();
            //Create a boolean value and set its default value to false
            bool isSuccess = false;

			Console.WriteLine("InsertTransactionDetail(td={0}): isSuccess → {1}", td, isSuccess);
            //Create a database connection here
            SqlConnection conn = new SqlConnection(myconnstrng);

			Console.WriteLine("InsertTransactionDetail(td={0}): conn → {1}", td, conn);
            try
            {
                //Sql Query to Insert Transaction detais
                string sql = "INSERT INTO tbl_transaction_detail (product_id, rate, qty, total, dea_cust_id, added_date, added_by) VALUES (@product_id, @rate, @qty, @total, @dea_cust_id, @added_date, @added_by)";

                //Passing the value to the SQL Query
                SqlCommand cmd = new SqlCommand(sql, conn);
                //Passing the values using cmd
				Console.WriteLine("InsertTransactionDetail(td={0}): cmd → {1}", td, cmd);
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
                int rows = cmd.ExecuteNonQuery();

				Console.WriteLine("InsertTransactionDetail(td={0}): rows → {1}", td, rows);
				Console.WriteLine("InsertTransactionDetail(td)={0}: save query executed in {1} ms", td, __logStart.ElapsedMilliseconds);
                if(rows>0)
                {
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
				Console.Error.WriteLine("Exception in InsertTransactionDetail(td={0}): {1}", td, ex.Message);
                MessageBox.Show(ex.Message);
            }
            finally
            {
                //Close Database Connection
                conn.Close();
            }
			Console.WriteLine("<<< Exiting InsertTransactionDetail(td={0})", td);
            return isSuccess;
        }
        #endregion
    }
}
