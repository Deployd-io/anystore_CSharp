using AnyStore.BLL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Extensions.Logging;

namespace AnyStore.DAL
{
    class transactionDetailDAL
    {
		private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
        //Create Connection String
        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        #region Insert Method for Transaction Detail
        public bool InsertTransactionDetail(transactionDetailBLL td)
        {
			_logger.LogDebug(">>> Entering InsertTransactionDetail(td={Td})", td);
            //Create a boolean value and set its default value to false
            bool isSuccess = false;
			_logger.LogDebug("InsertTransactionDetail(td={Td}): isSuccess → {IsSuccess}", td, isSuccess);

            //Create a database connection here
            SqlConnection conn = new SqlConnection(myconnstrng);
			_logger.LogDebug("InsertTransactionDetail(td={Td}): conn → {Conn}", td, conn);

            try
            {
                //Sql Query to Insert Transaction detais
                string sql = "INSERT INTO tbl_transaction_detail (product_id, rate, qty, total, dea_cust_id, added_date, added_by) VALUES (@product_id, @rate, @qty, @total, @dea_cust_id, @added_date, @added_by)";

                //Passing the value to the SQL Query
                SqlCommand cmd = new SqlCommand(sql, conn);
				_logger.LogDebug("InsertTransactionDetail(td={Td}): cmd → {Cmd}", td, cmd);
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

				_logger.LogDebug("InsertTransactionDetail(td={Td}): rows → {Rows}", td, rows);
				_logger.LogInformation("InsertTransactionDetail(td)={Td}: save query executed in {Arg1} ms", td, __logStart.ElapsedMilliseconds);
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
				_logger.LogError("Exception in InsertTransactionDetail(td={Td}): {Arg1}", td, ex.Message);
                MessageBox.Show(ex.Message);
            }
            finally
            {
                //Close Database Connection
                conn.Close();
            }
			_logger.LogDebug("<<< Exiting InsertTransactionDetail(td={Td})", td);
            return isSuccess;
        }
        #endregion
    }
}
