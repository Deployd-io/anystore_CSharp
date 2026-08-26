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
    class transactionDAL
    {
        //Create a connection string variable
        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        #region Insert Transaction Method
        public bool Insert_Transaction(transactionsBLL t, out int transactionID)
        {
			System.Diagnostics.Trace.WriteLine(string.Format(">>> Entering Insert_Transaction(t={0})", t));
            //Create a boolean value and set its default value to false
            bool isSuccess = false;
			System.Diagnostics.Trace.WriteLine(string.Format("Insert_Transaction(t={0}): isSuccess → {1}", t, isSuccess));
            //Set the out transactionID value to negative 1 i.e. -1
            transactionID = -1;
			System.Diagnostics.Trace.WriteLine(string.Format("Insert_Transaction(t={0}): transactionID → {1}", t, transactionID));
            //Create a SqlConnection first
            SqlConnection conn = new SqlConnection(myconnstrng);
			System.Diagnostics.Trace.WriteLine(string.Format("Insert_Transaction(t={0}): conn → {1}", t, conn));
            try
            {
                //SQL Query to Insert Transactions
                string sql = "INSERT INTO tbl_transactions (type, dea_cust_id, grandTotal, transaction_date, tax, discount, added_by) VALUES (@type, @dea_cust_id, @grandTotal, @transaction_date, @tax, @discount, @added_by); SELECT @@IDENTITY;";

                //Sql Commandto pass the value in sql query
                SqlCommand cmd = new SqlCommand(sql, conn);
				System.Diagnostics.Trace.WriteLine(string.Format("Insert_Transaction(t={0}): cmd → {1}", t, cmd));

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

				System.Diagnostics.Trace.WriteLine(string.Format("Insert_Transaction(t)={0}: save query executed in {1} ms", t, __logStart.ElapsedMilliseconds));
				System.Diagnostics.Trace.WriteLine(string.Format("Insert_Transaction(t={0}): o → {1}", t, o));
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
				System.Diagnostics.Trace.WriteLine(string.Format("Exception in Insert_Transaction(t={0}): {1}", t, ex.Message));
                MessageBox.Show(ex.Message);
            }
            finally
            {
                //Close the connection 
                conn.Close();
            }

			System.Diagnostics.Trace.WriteLine(string.Format("<<< Exiting Insert_Transaction(t={0})", t));
            return isSuccess;
        }
        #endregion
        #region METHOD TO DISPLAY ALL THE TRANSACTION
        public DataTable DisplayAllTransactions()
        {
			System.Diagnostics.Trace.WriteLine(">>> Entering DisplayAllTransactions()");
            //SQlConnection First
            SqlConnection conn = new SqlConnection(myconnstrng);
			System.Diagnostics.Trace.WriteLine(string.Format("DisplayAllTransactions(): conn → {0}", conn));

            //Create a DAta Table to hold the datafrom database temporarily
            DataTable dt = new DataTable();
			System.Diagnostics.Trace.WriteLine(string.Format("DisplayAllTransactions(): dt → {0}", dt));

            try
            {
                //Write the SQL Query to Display all Transactions
                string sql = "SELECT * FROM tbl_transactions";

                //SqlCommand to Execute Query
                SqlCommand cmd = new SqlCommand(sql, conn);
				System.Diagnostics.Trace.WriteLine(string.Format("DisplayAllTransactions(): cmd → {0}", cmd));

                //SqlDataAdapter to Hold the data from database
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
				System.Diagnostics.Trace.WriteLine(string.Format("DisplayAllTransactions(): adapter → {0}", adapter));

                //Open DAtabase Connection
                conn.Open();

				var __logStart = System.Diagnostics.Stopwatch.StartNew();
				System.Diagnostics.Trace.WriteLine(string.Format("DisplayAllTransactions(): find query executed in {0} ms", __logStart.ElapsedMilliseconds));
                adapter.Fill(dt);
            }
            catch(Exception ex)
            {
				System.Diagnostics.Trace.WriteLine(string.Format("Exception in DisplayAllTransactions(): {0}", ex.Message));
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

			System.Diagnostics.Trace.WriteLine("<<< Exiting DisplayAllTransactions()");
            return dt;
        }
        #endregion
        #region METHOD TO DISPLAY TRANSACTION BASED ON TRANSACTION TYPE
        public DataTable DisplayTransactionByType(string type)
        {
			System.Diagnostics.Trace.WriteLine(string.Format(">>> Entering DisplayTransactionByType(type={0})", type));
            //Create SQL Connection
            SqlConnection conn = new SqlConnection(myconnstrng);
			System.Diagnostics.Trace.WriteLine(string.Format("DisplayTransactionByType(type={0}): conn → {1}", type, conn));

            //Create a DataTable
            DataTable dt = new DataTable();
			System.Diagnostics.Trace.WriteLine(string.Format("DisplayTransactionByType(type={0}): dt → {1}", type, dt));

            try
            {
                //Write SQL Query
                string sql = "SELECT * FROM tbl_transactions WHERE type='"+type+"'";

                //SQL Command to Execute Query
                SqlCommand cmd = new SqlCommand(sql, conn);
				System.Diagnostics.Trace.WriteLine(string.Format("DisplayTransactionByType(type={0}): cmd → {1}", type, cmd));
                //SQlDataAdapter to hold the data from database
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
				System.Diagnostics.Trace.WriteLine(string.Format("DisplayTransactionByType(type={0}): adapter → {1}", type, adapter));

                //Open DAtabase Connection
                conn.Open();
				var __logStart = System.Diagnostics.Stopwatch.StartNew();
				System.Diagnostics.Trace.WriteLine(string.Format("DisplayTransactionByType(type)={0}: find query executed in {1} ms", type, __logStart.ElapsedMilliseconds));
                adapter.Fill(dt);
            }
            catch(Exception ex)
            {
				System.Diagnostics.Trace.WriteLine(string.Format("Exception in DisplayTransactionByType(type={0}): {1}", type, ex.Message));
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

			System.Diagnostics.Trace.WriteLine(string.Format("<<< Exiting DisplayTransactionByType(type={0})", type));
            return dt;
        }
        #endregion
    }
}
