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
			Console.WriteLine(">>> Entering Insert_Transaction(t={0},transactionID={1})", t, transactionID);
            //Create a boolean value and set its default value to false
            bool isSuccess = false;
            //Set the out transactionID value to negative 1 i.e. -1
			Console.WriteLine("Insert_Transaction(t={0},transactionID={1}): isSuccess → {2}", t, transactionID, isSuccess);
            transactionID = -1;
            //Create a SqlConnection first
			Console.WriteLine("Insert_Transaction(t={0},transactionID={1}): transactionID → {2}", t, transactionID, transactionID);
            SqlConnection conn = new SqlConnection(myconnstrng);
            try
			Console.WriteLine("Insert_Transaction(t={0},transactionID={1}): conn → {2}", t, transactionID, conn);
            {
                //SQL Query to Insert Transactions
                string sql = "INSERT INTO tbl_transactions (type, dea_cust_id, grandTotal, transaction_date, tax, discount, added_by) VALUES (@type, @dea_cust_id, @grandTotal, @transaction_date, @tax, @discount, @added_by); SELECT @@IDENTITY;";

                //Sql Commandto pass the value in sql query
                SqlCommand cmd = new SqlCommand(sql, conn);

				Console.WriteLine("Insert_Transaction(t={0},transactionID={1}): cmd → {2}", t, transactionID, cmd);
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
                object o = cmd.ExecuteScalar();

				Console.WriteLine("Insert_Transaction(t={0},transactionID={1}): o → {2}", t, transactionID, o);
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
				Console.Error.WriteLine("Exception in Insert_Transaction(t={0},transactionID={1}): {2}", t, transactionID, ex.Message);
                MessageBox.Show(ex.Message);
            }
            finally
            {
                //Close the connection 
                conn.Close();
            }

			Console.WriteLine("<<< Exiting Insert_Transaction(t={0},transactionID={1})", t, transactionID);
            return isSuccess;
        }
        #endregion
        #region METHOD TO DISPLAY ALL THE TRANSACTION
        public DataTable DisplayAllTransactions()
        {
			Console.WriteLine(">>> Entering DisplayAllTransactions()");
            //SQlConnection First
            SqlConnection conn = new SqlConnection(myconnstrng);

			Console.WriteLine("DisplayAllTransactions(): conn → {0}", conn);
            //Create a DAta Table to hold the datafrom database temporarily
            DataTable dt = new DataTable();

			Console.WriteLine("DisplayAllTransactions(): dt → {0}", dt);
            try
            {
                //Write the SQL Query to Display all Transactions
                string sql = "SELECT * FROM tbl_transactions";

                //SqlCommand to Execute Query
                SqlCommand cmd = new SqlCommand(sql, conn);

				Console.WriteLine("DisplayAllTransactions(): cmd → {0}", cmd);
                //SqlDataAdapter to Hold the data from database
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

				Console.WriteLine("DisplayAllTransactions(): adapter → {0}", adapter);
                //Open DAtabase Connection
                conn.Open();

                adapter.Fill(dt);
            }
            catch(Exception ex)
            {
				Console.Error.WriteLine("Exception in DisplayAllTransactions(): {0}", ex.Message);
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

			Console.WriteLine("<<< Exiting DisplayAllTransactions()");
            return dt;
        }
        #endregion
        #region METHOD TO DISPLAY TRANSACTION BASED ON TRANSACTION TYPE
        public DataTable DisplayTransactionByType(string type)
        {
			Console.WriteLine(">>> Entering DisplayTransactionByType(type={0})", type);
            //Create SQL Connection
            SqlConnection conn = new SqlConnection(myconnstrng);

			Console.WriteLine("DisplayTransactionByType(type={0}): conn → {1}", type, conn);
            //Create a DataTable
            DataTable dt = new DataTable();

			Console.WriteLine("DisplayTransactionByType(type={0}): dt → {1}", type, dt);
            try
            {
                //Write SQL Query
                string sql = "SELECT * FROM tbl_transactions WHERE type='"+type+"'";

                //SQL Command to Execute Query
                SqlCommand cmd = new SqlCommand(sql, conn);
                //SQlDataAdapter to hold the data from database
				Console.WriteLine("DisplayTransactionByType(type={0}): cmd → {1}", type, cmd);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

				Console.WriteLine("DisplayTransactionByType(type={0}): adapter → {1}", type, adapter);
                //Open DAtabase Connection
                conn.Open();
                adapter.Fill(dt);
            }
            catch(Exception ex)
            {
				Console.Error.WriteLine("Exception in DisplayTransactionByType(type={0}): {1}", type, ex.Message);
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

			Console.WriteLine("<<< Exiting DisplayTransactionByType(type={0})", type);
            return dt;
        }
        #endregion
    }
}
