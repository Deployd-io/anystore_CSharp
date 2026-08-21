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
    class transactionDAL
    {
		private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
        //Create a connection string variable
        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        #region Insert Transaction Method
        public bool Insert_Transaction(transactionsBLL t, out int transactionID)
        {
			_logger.LogDebug(">>> Entering Insert_Transaction(t={T})", t);
            //Create a boolean value and set its default value to false
            bool isSuccess = false;
			_logger.LogDebug("Insert_Transaction(t={T}): isSuccess → {IsSuccess}", t, isSuccess);
            //Set the out transactionID value to negative 1 i.e. -1
            transactionID = -1;
            //Create a SqlConnection first
			_logger.LogDebug("Insert_Transaction(t={T}): transactionID → {TransactionID}", t, transactionID);
            SqlConnection conn = new SqlConnection(myconnstrng);
			_logger.LogDebug("Insert_Transaction(t={T}): conn → {Conn}", t, conn);
            try
            {
                //SQL Query to Insert Transactions
                string sql = "INSERT INTO tbl_transactions (type, dea_cust_id, grandTotal, transaction_date, tax, discount, added_by) VALUES (@type, @dea_cust_id, @grandTotal, @transaction_date, @tax, @discount, @added_by); SELECT @@IDENTITY;";

                //Sql Commandto pass the value in sql query
                SqlCommand cmd = new SqlCommand(sql, conn);
				_logger.LogDebug("Insert_Transaction(t={T}): cmd → {Cmd}", t, cmd);

                //Passing the value to sql query using cmd
                cmd.Parameters.AddWithValue("@type", t.type);
                cmd.Parameters.AddWithValue("@dea_cust_id", t.dea_cust_id);
                cmd.Parameters.AddWithValue("@grandTotal", t.grandTotal);
                cmd.Parameters.AddWithValue("@transaction_date", t.transaction_date);
                cmd.Parameters.AddWithValue("@tax", t.tax);
                cmd.Parameters.AddWithValue("@discount", t.discount);
                cmd.Parameters.AddWithValue("@added_by", t.added_by);

                //Open Database Connection
                conn.Open();

                //Execute the Query
				var __logStart = System.Diagnostics.Stopwatch.StartNew();
                object o = cmd.ExecuteScalar();

				_logger.LogInformation("Insert_Transaction(t)={T}: save query executed in {Arg1} ms", t, __logStart.ElapsedMilliseconds);
				_logger.LogDebug("Insert_Transaction(t={T}): o → {O}", t, o);
                //If the query is executed successfully then the value will not be null else it will be null
                if(o!=null)
                {
                    //Query Executed Successfully
                    transactionID = int.Parse(o.ToString());
                    isSuccess = true;
                }
                else
                {
                    //failed to execute query
                    isSuccess = false;
                }
            }
            catch(Exception ex)
            {
				_logger.LogError("Exception in Insert_Transaction(t={T}): {Arg1}", t, ex.getMessage());
                MessageBox.Show(ex.Message);
            }
            finally
            {
                //Close the connection 
                conn.Close();
            }

			_logger.LogDebug("<<< Exiting Insert_Transaction(t={T})", t);
            return isSuccess;
        }
        #endregion
        #region METHOD TO DISPLAY ALL THE TRANSACTION
        public DataTable DisplayAllTransactions()
        {
			_logger.LogDebug(">>> Entering DisplayAllTransactions()");
            //SQlConnection First
            SqlConnection conn = new SqlConnection(myconnstrng);
			_logger.LogDebug("DisplayAllTransactions(): conn → {Conn}", conn);

            //Create a DAta Table to hold the datafrom database temporarily
            DataTable dt = new DataTable();
			_logger.LogDebug("DisplayAllTransactions(): dt → {Dt}", dt);

            try
            {
                //Write the SQL Query to Display all Transactions
                string sql = "SELECT * FROM tbl_transactions";

                //SqlCommand to Execute Query
                SqlCommand cmd = new SqlCommand(sql, conn);
				_logger.LogDebug("DisplayAllTransactions(): cmd → {Cmd}", cmd);

                //SqlDataAdapter to Hold the data from database
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
				_logger.LogDebug("DisplayAllTransactions(): adapter → {Adapter}", adapter);

                //Open DAtabase Connection
                conn.Open();

				var __logStart = System.Diagnostics.Stopwatch.StartNew();
				_logger.LogInformation("DisplayAllTransactions(): find query executed in {Arg0} ms", __logStart.ElapsedMilliseconds);
                adapter.Fill(dt);
            }
            catch(Exception ex)
            {
				_logger.LogError("Exception in DisplayAllTransactions(): {Arg0}", ex.getMessage());
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

			_logger.LogDebug("<<< Exiting DisplayAllTransactions()");
            return dt;
        }
        #endregion
        #region METHOD TO DISPLAY TRANSACTION BASED ON TRANSACTION TYPE
        public DataTable DisplayTransactionByType(string type)
        {
			_logger.LogDebug(">>> Entering DisplayTransactionByType(type={Type})", type);
            //Create SQL Connection
            SqlConnection conn = new SqlConnection(myconnstrng);
			_logger.LogDebug("DisplayTransactionByType(type={Type}): conn → {Conn}", type, conn);

            //Create a DataTable
            DataTable dt = new DataTable();
			_logger.LogDebug("DisplayTransactionByType(type={Type}): dt → {Dt}", type, dt);

            try
            {
                //Write SQL Query
                string sql = "SELECT * FROM tbl_transactions WHERE type='"+type+"'";

                //SQL Command to Execute Query
                SqlCommand cmd = new SqlCommand(sql, conn);
				_logger.LogDebug("DisplayTransactionByType(type={Type}): cmd → {Cmd}", type, cmd);
                //SQlDataAdapter to hold the data from database
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
				_logger.LogDebug("DisplayTransactionByType(type={Type}): adapter → {Adapter}", type, adapter);

                //Open DAtabase Connection
                conn.Open();
				var __logStart = System.Diagnostics.Stopwatch.StartNew();
				_logger.LogInformation("DisplayTransactionByType(type)={Type}: find query executed in {Arg1} ms", type, __logStart.ElapsedMilliseconds);
                adapter.Fill(dt);
            }
            catch(Exception ex)
            {
				_logger.LogError("Exception in DisplayTransactionByType(type={Type}): {Arg1}", type, ex.getMessage());
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

			_logger.LogDebug("<<< Exiting DisplayTransactionByType(type={Type})", type);
            return dt;
        }
        #endregion
    }
}
