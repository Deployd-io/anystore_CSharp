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
    class categoriesDAL
    {
		private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
        //Static String Method for Database Connection String
        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        #region Select Method
        public DataTable Select()
        {
			_logger.LogDebug(">>> Entering Select()");
            //Creating Database Connection
            SqlConnection conn = new SqlConnection(myconnstrng);
			_logger.LogDebug("Select(): conn → {Conn}", conn);

            DataTable dt = new DataTable();
			_logger.LogDebug("Select(): dt → {Dt}", dt);

            try
            {
                //Wrting SQL Query to get all the data from DAtabase
                string sql = "SELECT * FROM tbl_categories";
				_logger.LogDebug("Select(): sql → {Sql}", sql);

                SqlCommand cmd = new SqlCommand(sql, conn);
				_logger.LogDebug("Select(): cmd → {Cmd}", cmd);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
				_logger.LogDebug("Select(): adapter → {Adapter}", adapter);
                //Open DAtabase Connection
                conn.Open();
                //Adding the value from adapter to Data TAble dt
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
        #region Insert New CAtegory
        public bool Insert(categoriesBLL c)
        {
			_logger.LogDebug(">>> Entering Insert(c={C})", c);
            //Creating A Boolean VAriable and set its default value to false
            bool isSucces = false;
			_logger.LogDebug("Insert(c={C}): isSucces → {IsSucces}", c, isSucces);

            //Connecting to Database
            SqlConnection conn = new SqlConnection(myconnstrng);
			_logger.LogDebug("Insert(c={C}): conn → {Conn}", c, conn);

            try
            {
                //Writing Query to Add New Category
                string sql = "INSERT INTO tbl_categories (title, description, added_date, added_by) VALUES (@title, @description, @added_date, @added_by)";
				_logger.LogDebug("Insert(c={C}): sql → {Sql}", c, sql);

                //Creating SQL Command to pass values in our query
                SqlCommand cmd = new SqlCommand(sql, conn);
				_logger.LogDebug("Insert(c={C}): cmd → {Cmd}", c, cmd);
                //Passing Values through parameter
                cmd.Parameters.AddWithValue("@title", c.title);
                cmd.Parameters.AddWithValue("@description", c.description);
                cmd.Parameters.AddWithValue("@added_date", c.added_date);
                cmd.Parameters.AddWithValue("@added_by", c.added_by);

                //Open Database Connection
                conn.Open();

                //Creating the int variable to execute query
				var __logStart = System.Diagnostics.Stopwatch.StartNew();
                int rows = cmd.ExecuteNonQuery();

				_logger.LogInformation("Insert(c)={C}: save query executed in {Arg1} ms", c, __logStart.ElapsedMilliseconds);
				_logger.LogDebug("Insert(c={C}): rows → {Rows}", c, rows);
                //If the query is executed successfully then its value will be greater than 0 else it will be less than 0

                if(rows>0)
                {
                    //Query Executed Succesfully
                    isSucces = true;
                }
                else
                {
                    //Failed to Execute Query
                    isSucces = false;
                }
            }
            catch(Exception ex)
            {
				_logger.LogError("Exception in Insert(c={C}): {Arg1}", c, ex.getMessage());
                MessageBox.Show(ex.Message);
            }
            finally
            {
                //Closing Database Connection
                conn.Close();
            }

			_logger.LogDebug("<<< Exiting Insert(c={C})", c);
            return isSucces;
        }
        #endregion
        #region Update Method
        public bool Update(categoriesBLL c)
        {
			_logger.LogDebug(">>> Entering Update(c={C})", c);
            //Creating Boolean variable and set its default value to false
            bool isSuccess = false;
			_logger.LogDebug("Update(c={C}): isSuccess → {IsSuccess}", c, isSuccess);

            //Creating SQL Connection
            SqlConnection conn = new SqlConnection(myconnstrng);
			_logger.LogDebug("Update(c={C}): conn → {Conn}", c, conn);

            try
            {
                //Query to Update Category
                string sql = "UPDATE tbl_categories SET title=@title, description=@description, added_date=@added_date, added_by=@added_by WHERE id=@id";
				_logger.LogDebug("Update(c={C}): sql → {Sql}", c, sql);

                //SQl Command to Pass the Value on Sql Query
                SqlCommand cmd = new SqlCommand(sql, conn);
				_logger.LogDebug("Update(c={C}): cmd → {Cmd}", c, cmd);

                //Passing Value using cmd
                cmd.Parameters.AddWithValue("@title", c.title);
                cmd.Parameters.AddWithValue("@description", c.description);
                cmd.Parameters.AddWithValue("@added_date", c.added_date);
                cmd.Parameters.AddWithValue("@added_by", c.added_by);
                cmd.Parameters.AddWithValue("@id", c.id);

                //Open DAtabase Connection
                conn.Open();

                //Create Int Variable to execute query
				var __logStart = System.Diagnostics.Stopwatch.StartNew();
                int rows = cmd.ExecuteNonQuery();

				_logger.LogInformation("Update(c)={C}: update query executed in {Arg1} ms", c, __logStart.ElapsedMilliseconds);
				_logger.LogDebug("Update(c={C}): rows → {Rows}", c, rows);
                //if the query is successfully executed then the value will be grater than zero 
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
				_logger.LogError("Exception in Update(c={C}): {Arg1}", c, ex.getMessage());
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

			_logger.LogDebug("<<< Exiting Update(c={C})", c);
            return isSuccess;
        }
        #endregion
        #region Delete Category Method
        public bool Delete(categoriesBLL c)
        {
			_logger.LogDebug(">>> Entering Delete(c={C})", c);
            //Create a Boolean variable and set its value to false
            bool isSuccess = false;
			_logger.LogDebug("Delete(c={C}): isSuccess → {IsSuccess}", c, isSuccess);

            SqlConnection conn = new SqlConnection(myconnstrng);
			_logger.LogDebug("Delete(c={C}): conn → {Conn}", c, conn);

            try
            {
                //SQL Query to Delete from Database
                string sql = "DELETE FROM tbl_categories WHERE id=@id";
				_logger.LogDebug("Delete(c={C}): sql → {Sql}", c, sql);

                SqlCommand cmd = new SqlCommand(sql, conn);
				_logger.LogDebug("Delete(c={C}): cmd → {Cmd}", c, cmd);
                //Passing the value using cmd
                cmd.Parameters.AddWithValue("@id", c.id);

                //Open SqlConnection
                conn.Open();

				var __logStart = System.Diagnostics.Stopwatch.StartNew();
                int rows = cmd.ExecuteNonQuery();

				_logger.LogInformation("Delete(c)={C}: delete query executed in {Arg1} ms", c, __logStart.ElapsedMilliseconds);
				_logger.LogDebug("Delete(c={C}): rows → {Rows}", c, rows);
                //If the query is executd successfully then the value of rows will be greater than zero else it will be less than 0
                if(rows>0)
                {
                    //Query Executed Successfully
                    isSuccess = true;
                }
                else
                {
                    //Faied to Execute Query
                    isSuccess = false;
                }

            }
            catch(Exception ex)
            {
				_logger.LogError("Exception in Delete(c={C}): {Arg1}", c, ex.getMessage());
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

			_logger.LogDebug("<<< Exiting Delete(c={C})", c);
            return isSuccess;
        }
        #endregion
        #region Method for Searh Funtionality
        public DataTable Search(string keywords)
        {
			_logger.LogDebug(">>> Entering Search(keywords={Keywords})", keywords);
            //SQL Connection For Database Connection
            SqlConnection conn = new SqlConnection(myconnstrng);
			_logger.LogDebug("Search(keywords={Keywords}): conn → {Conn}", keywords, conn);

            //Creating Data TAble to hold the data from database temporarily
            DataTable dt = new DataTable();
			_logger.LogDebug("Search(keywords={Keywords}): dt → {Dt}", keywords, dt);

            try
            {
                //SQL Query To Search Categories from DAtabase
                String sql = "SELECT * FROM tbl_categories WHERE id LIKE '%"+keywords+"%' OR title LIKE '%"+keywords+"%' OR description LIKE '%"+keywords+"%'";
				_logger.LogDebug("Search(keywords={Keywords}): sql → {Sql}", keywords, sql);
                //Creating SQL Command to Execute the Query
                SqlCommand cmd = new SqlCommand(sql, conn);
				_logger.LogDebug("Search(keywords={Keywords}): cmd → {Cmd}", keywords, cmd);

                //Getting DAta From DAtabase
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
				_logger.LogDebug("Search(keywords={Keywords}): adapter → {Adapter}", keywords, adapter);

                //Open DatabaseConnection
                conn.Open();
                //Passing values from adapter to Data Table dt
				var __logStart = System.Diagnostics.Stopwatch.StartNew();
				_logger.LogInformation("Search(keywords)={Keywords}: find query executed in {Arg1} ms", keywords, __logStart.ElapsedMilliseconds);
                adapter.Fill(dt);
            }
            catch(Exception ex)
            {
				_logger.LogError("Exception in Search(keywords={Keywords}): {Arg1}", keywords, ex.getMessage());
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

			_logger.LogDebug("<<< Exiting Search(keywords={Keywords})", keywords);
            return dt;
        }
        #endregion
    }
}
