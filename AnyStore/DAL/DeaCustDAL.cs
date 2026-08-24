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
    class DeaCustDAL
    {
		private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
        //Static String Method for Database Connection
        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        #region SELECT MEthod for Dealer and Customer
        public DataTable Select()
        {
			_logger.LogDebug(">>> Entering Select()");
            //SQL Connection for Database Connection
            SqlConnection conn = new SqlConnection(myconnstrng);
			_logger.LogDebug("Select(): conn → {Conn}", conn);

            //DataTble to hold the value from database and return it
            DataTable dt = new DataTable();
			_logger.LogDebug("Select(): dt → {Dt}", dt);

            try
            {
                //Write SQL Query t Select all the DAta from dAtabase
                string sql = "SELECT * FROM tbl_dea_cust";

                //Creating SQL Command to execute Query
                SqlCommand cmd = new SqlCommand(sql, conn);
				_logger.LogDebug("Select(): cmd → {Cmd}", cmd);

                //Creting SQL Data Adapter to Store Data From Database Temporarily
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
				_logger.LogDebug("Select(): adapter → {Adapter}", adapter);

                //Open Database Connection
                conn.Open();
                //Passign the value from SQL Data Adapter to DAta table
				var __logStart = System.Diagnostics.Stopwatch.StartNew();
				_logger.LogInformation("Select(): find query executed in {Arg0} ms", __logStart.ElapsedMilliseconds);
                adapter.Fill(dt);
            }
            catch(Exception ex)
            {
				_logger.LogError("Exception in Select(): {Arg0}", ex.getMessage());
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

			_logger.LogDebug("<<< Exiting Select()");
            return dt;
        }
        #endregion
        #region INSERT Method to Add details fo Dealer or Customer
        public bool Insert(DeaCustBLL dc)
        {
			_logger.LogDebug(">>> Entering Insert(dc={Dc})", dc);
            //Creting SQL Connection First
            SqlConnection conn = new SqlConnection(myconnstrng);
			_logger.LogDebug("Insert(dc={Dc}): conn → {Conn}", dc, conn);

            //Create and Boolean value and set its default value to false
            bool isSuccess = false;
			_logger.LogDebug("Insert(dc={Dc}): isSuccess → {IsSuccess}", dc, isSuccess);

            try
            {
                //Write SQL Query to Insert Details of Dealer or Customer
                string sql = "INSERT INTO tbl_dea_cust (type, name, email, contact, address, added_date, added_by) VALUES (@type, @name, @email, @contact, @address, @added_date, @added_by)";

                //SQl Command to Pass the values to query and execute
                SqlCommand cmd = new SqlCommand(sql, conn);
				_logger.LogDebug("Insert(dc={Dc}): cmd → {Cmd}", dc, cmd);
                //Passing the calues using Parameters
                cmd.Parameters.AddWithValue("@type", dc.type);
                cmd.Parameters.AddWithValue("@name", dc.name);
                cmd.Parameters.AddWithValue("@email", dc.email);
                cmd.Parameters.AddWithValue("@contact", dc.contact);
                cmd.Parameters.AddWithValue("@address", dc.address);
                cmd.Parameters.AddWithValue("@added_date", dc.added_date);
                cmd.Parameters.AddWithValue("@added_by", dc.added_by);

                //Open DAtabaseConnection
                conn.Open();

                //Int variable to check whether the query is executed successfully or not
				var __logStart = System.Diagnostics.Stopwatch.StartNew();
                int rows = cmd.ExecuteNonQuery();

				_logger.LogInformation("Insert(dc)={Dc}: save query executed in {Arg1} ms", dc, __logStart.ElapsedMilliseconds);
				_logger.LogDebug("Insert(dc={Dc}): rows → {Rows}", dc, rows);
                //If the query is executed successfully then the value of rows will be greater than 0 else it will be less than 0
                if(rows>0)
                {
                    //Query Executed Successfully
                    isSuccess = true;
                }
                else
                {
                    //Failed to Execute Query
                    isSuccess = false;
                }
            }
            catch(Exception ex)
            {
				_logger.LogError("Exception in Insert(dc={Dc}): {Arg1}", dc, ex.getMessage());
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

			_logger.LogDebug("<<< Exiting Insert(dc={Dc})", dc);
            return isSuccess;
        }
        #endregion
        #region UPDATE method for Dealer and Customer Module
        public bool Update(DeaCustBLL dc)
        {
			_logger.LogDebug(">>> Entering Update(dc={Dc})", dc);
            //SQL Connection for Database Connection
            SqlConnection conn = new SqlConnection(myconnstrng);
			_logger.LogDebug("Update(dc={Dc}): conn → {Conn}", dc, conn);
            //Create Boolean variable and set its default value to false
            bool isSuccess = false;
			_logger.LogDebug("Update(dc={Dc}): isSuccess → {IsSuccess}", dc, isSuccess);

            try
            {
                //SQL Query to update data in database
                string sql = "UPDATE tbl_dea_cust SET type=@type, name=@name, email=@email, contact=@contact, address=@address, added_date=@added_date, added_by=@added_by WHERE id=@id";
                //Create SQL Command to pass the value in sql
                SqlCommand cmd = new SqlCommand(sql, conn);
				_logger.LogDebug("Update(dc={Dc}): cmd → {Cmd}", dc, cmd);

                //Passing the values through parameters
                cmd.Parameters.AddWithValue("@type", dc.type);
                cmd.Parameters.AddWithValue("@name", dc.name);
                cmd.Parameters.AddWithValue("@email", dc.email);
                cmd.Parameters.AddWithValue("@contact", dc.contact);
                cmd.Parameters.AddWithValue("@address", dc.address);
                cmd.Parameters.AddWithValue("@added_date", dc.added_date);
                cmd.Parameters.AddWithValue("@added_by", dc.added_by);
                cmd.Parameters.AddWithValue("@id", dc.id);

                //open the Database Connection
                conn.Open();

                //Int varialbe to check if the query executed successfully or not
				var __logStart = System.Diagnostics.Stopwatch.StartNew();
                int rows = cmd.ExecuteNonQuery();
                if(rows>0)
                {
                    //Query Executed Successfully 
                    isSuccess = true;
                }
                else
                {
                    //Failed to Execute Query
                    isSuccess = false;
                }
            }
            catch(Exception ex)
            {
				_logger.LogError("Exception in Update(dc={Dc}): {Arg1}", dc, ex.getMessage());
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

			_logger.LogDebug("<<< Exiting Update(dc={Dc})", dc);
            return isSuccess;
        }
        #endregion
        #region DELETE Method for Dealer and Customer Module
        public bool Delete(DeaCustBLL dc)
        {
			_logger.LogDebug(">>> Entering Delete(dc={Dc})", dc);
            //SQlConnection for Database Connection
            SqlConnection conn = new SqlConnection(myconnstrng);
			_logger.LogDebug("Delete(dc={Dc}): conn → {Conn}", dc, conn);

            //Create a Boolean Variable and set its default value to false
            bool isSuccess = false;
			_logger.LogDebug("Delete(dc={Dc}): isSuccess → {IsSuccess}", dc, isSuccess);

            try
            {
                //SQL Query to Delete Data from dAtabase
                string sql = "DELETE FROM tbl_dea_cust WHERE id=@id";

                //SQL command to pass the value
                SqlCommand cmd = new SqlCommand(sql, conn);
				_logger.LogDebug("Delete(dc={Dc}): cmd → {Cmd}", dc, cmd);
                //Passing the value
                cmd.Parameters.AddWithValue("@id", dc.id);

                //Open DB Connection
                conn.Open();
                //integer variable
				var __logStart = System.Diagnostics.Stopwatch.StartNew();
                int rows = cmd.ExecuteNonQuery();
                if(rows>0)
                {
                    //Query Executed Successfully 
                    isSuccess = true;
                }
                else
                {
                    //Failed to Execute Query
                    isSuccess = false;
                }
            }
            catch(Exception ex)
            {
				_logger.LogError("Exception in Delete(dc={Dc}): {Arg1}", dc, ex.getMessage());
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

			_logger.LogDebug("<<< Exiting Delete(dc={Dc})", dc);
            return isSuccess;
        }
        #endregion
        #region SEARCH METHOD for Dealer and Customer Module
        public DataTable Search(string keyword)
        {
			_logger.LogDebug(">>> Entering Search(keyword={Keyword})", keyword);
            //Create a Sql Connection
            SqlConnection conn = new SqlConnection(myconnstrng);
			_logger.LogDebug("Search(keyword={Keyword}): conn → {Conn}", keyword, conn);

            //Creating a Data TAble and returnign its value
            DataTable dt = new DataTable();
			_logger.LogDebug("Search(keyword={Keyword}): dt → {Dt}", keyword, dt);

            try
            {
                //Write the Query to Search Dealer or Customer Based in id, type and name
                string sql = "SELECT * FROM tbl_dea_cust WHERE id LIKE '%"+keyword+"%' OR type LIKE '%"+keyword+"%' OR name LIKE '%"+keyword+"%'";

                //Sql Command to Execute the Query
                SqlCommand cmd = new SqlCommand(sql, conn);
				_logger.LogDebug("Search(keyword={Keyword}): cmd → {Cmd}", keyword, cmd);
                //Sql Dat Adapeter to hold tthe data from dataase temporarily
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
				_logger.LogDebug("Search(keyword={Keyword}): adapter → {Adapter}", keyword, adapter);

                //Open DAta Base Connection
                conn.Open();
                //Pass the value from adapter to data table
				var __logStart = System.Diagnostics.Stopwatch.StartNew();
				_logger.LogInformation("Search(keyword)={Keyword}: find query executed in {Arg1} ms", keyword, __logStart.ElapsedMilliseconds);
                adapter.Fill(dt);
            }
            catch(Exception ex)
            {
				_logger.LogError("Exception in Search(keyword={Keyword}): {Arg1}", keyword, ex.getMessage());
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

			_logger.LogDebug("<<< Exiting Search(keyword={Keyword})", keyword);
            return dt;
        }
        #endregion
        #region METHOD TO SAERCH DEALER Or CUSTOMER FOR TRANSACTON MODULE
        public DeaCustBLL SearchDealerCustomerForTransaction(string keyword)
        {
			_logger.LogDebug(">>> Entering SearchDealerCustomerForTransaction(keyword={Keyword})", keyword);
            //Create an object for DeaCustBLL class
            DeaCustBLL dc = new DeaCustBLL();
			_logger.LogDebug("SearchDealerCustomerForTransaction(keyword={Keyword}): dc → {Dc}", keyword, dc);

            //Create a DAtabase Connection
            SqlConnection conn = new SqlConnection(myconnstrng);
			_logger.LogDebug("SearchDealerCustomerForTransaction(keyword={Keyword}): conn → {Conn}", keyword, conn);

            //Create a DAta Table to hold the value temporarily
            DataTable dt = new DataTable();
			_logger.LogDebug("SearchDealerCustomerForTransaction(keyword={Keyword}): dt → {Dt}", keyword, dt);

            try
            {
                //Write a SQL Query to Search Dealer or Customer Based on Keywords
                string sql = "SELECT name, email, contact, address from tbl_dea_cust WHERE id LIKE '%"+keyword+"%' OR name LIKE '%"+keyword+"%'";

                //Create a Sql Data Adapter to Execute the Query
                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
				_logger.LogDebug("SearchDealerCustomerForTransaction(keyword={Keyword}): adapter → {Adapter}", keyword, adapter);

                //Open the DAtabase Connection
                conn.Open();

                //Transfer the data from SqlData Adapter to DAta Table
				var __logStart = System.Diagnostics.Stopwatch.StartNew();
				_logger.LogInformation("SearchDealerCustomerForTransaction(keyword)={Keyword}: find query executed in {Arg1} ms", keyword, __logStart.ElapsedMilliseconds);
                adapter.Fill(dt);

                //If we have values on dt we need to save it in dealerCustomer BLL
                if(dt.Rows.Count>0)
                {
                    dc.name = dt.Rows[0]["name"].ToString();
                    dc.email = dt.Rows[0]["email"].ToString();
                    dc.contact = dt.Rows[0]["contact"].ToString();
                    dc.address = dt.Rows[0]["address"].ToString();
                }

            }
            catch(Exception ex)
            {
				_logger.LogError("Exception in SearchDealerCustomerForTransaction(keyword={Keyword}): {Arg1}", keyword, ex.getMessage());
                MessageBox.Show(ex.Message);
            }
            finally
            {
                //Close Database connection
                conn.Close();
            }

			_logger.LogDebug("<<< Exiting SearchDealerCustomerForTransaction(keyword={Keyword})", keyword);
            return dc;
        }
        #endregion
        #region METHOD TO GET ID OF THE DEALER OR CUSTOMER BASED ON NAME
        public DeaCustBLL GetDeaCustIDFromName(string Name)
        {
			_logger.LogDebug(">>> Entering GetDeaCustIDFromName(Name={Name})", Name);
            //First Create an Object of DeaCust BLL and REturn it
            DeaCustBLL dc = new DeaCustBLL();
			_logger.LogDebug("GetDeaCustIDFromName(Name={Name}): dc → {Dc}", Name, dc);

            //SQL Conection here
            SqlConnection conn = new SqlConnection(myconnstrng);
			_logger.LogDebug("GetDeaCustIDFromName(Name={Name}): conn → {Conn}", Name, conn);
            //Data TAble to Holdthe data temporarily
            DataTable dt = new DataTable();
			_logger.LogDebug("GetDeaCustIDFromName(Name={Name}): dt → {Dt}", Name, dt);

            try
            {
                //SQL Query to Get id based on Name
                string sql = "SELECT id FROM tbl_dea_cust WHERE name='"+Name+"'";
                //Create the SQL Data Adapter to Execute the Query
                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
				_logger.LogDebug("GetDeaCustIDFromName(Name={Name}): adapter → {Adapter}", Name, adapter);

                conn.Open();

                //Passing the CAlue from Adapter to DAtatable
				var __logStart = System.Diagnostics.Stopwatch.StartNew();
				_logger.LogInformation("GetDeaCustIDFromName(Name)={Name}: find query executed in {Arg1} ms", Name, __logStart.ElapsedMilliseconds);
                adapter.Fill(dt);
                if(dt.Rows.Count>0)
                {
                    //Pass the value from dt to DeaCustBLL dc
                    dc.id = int.Parse(dt.Rows[0]["id"].ToString());
                }
            }
            catch(Exception ex)
            {
				_logger.LogError("Exception in GetDeaCustIDFromName(Name={Name}): {Arg1}", Name, ex.getMessage());
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

			_logger.LogDebug("<<< Exiting GetDeaCustIDFromName(Name={Name})", Name);
            return dc;
        }
        #endregion

    }
}
