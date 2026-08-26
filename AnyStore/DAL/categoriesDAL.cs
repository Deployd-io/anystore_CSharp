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
    class categoriesDAL
    {
        //Static String Method for Database Connection String
        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        #region Select Method
        public DataTable Select()
        {
			System.Diagnostics.Trace.WriteLine(">>> Entering Select()");
            //Creating Database Connection
            SqlConnection conn = new SqlConnection(myconnstrng);
			System.Diagnostics.Trace.WriteLine(string.Format("Select(): conn → {0}", conn));

            DataTable dt = new DataTable();
			System.Diagnostics.Trace.WriteLine(string.Format("Select(): dt → {0}", dt));

            try
            {
                //Wrting SQL Query to get all the data from DAtabase
                string sql = "SELECT * FROM tbl_categories";
				System.Diagnostics.Trace.WriteLine(string.Format("Select(): sql → {0}", sql));

                SqlCommand cmd = new SqlCommand(sql, conn);
				System.Diagnostics.Trace.WriteLine(string.Format("Select(): cmd → {0}", cmd));

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
				System.Diagnostics.Trace.WriteLine(string.Format("Select(): adapter → {0}", adapter));
                //Open DAtabase Connection
                conn.Open();
                //Adding the value from adapter to Data TAble dt
				var __logStart = System.Diagnostics.Stopwatch.StartNew();
				System.Diagnostics.Trace.WriteLine(string.Format("Select(): find query executed in {0} ms", __logStart.ElapsedMilliseconds));
                adapter.Fill(dt);
            }
            catch(Exception ex)
            {
				System.Diagnostics.Trace.WriteLine(string.Format("Exception in Select(): {0}", ex.Message));
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

			System.Diagnostics.Trace.WriteLine("<<< Exiting Select()");
            return dt;
        }
        #endregion
        #region Insert New CAtegory
        public bool Insert(categoriesBLL c)
        {
			System.Diagnostics.Trace.WriteLine(string.Format(">>> Entering Insert(c={0})", c));
            //Creating A Boolean VAriable and set its default value to false
            bool isSucces = false;
			System.Diagnostics.Trace.WriteLine(string.Format("Insert(c={0}): isSucces → {1}", c, isSucces));

            //Connecting to Database
            SqlConnection conn = new SqlConnection(myconnstrng);
			System.Diagnostics.Trace.WriteLine(string.Format("Insert(c={0}): conn → {1}", c, conn));

            try
            {
                //Writing Query to Add New Category
                string sql = "INSERT INTO tbl_categories (title, description, added_date, added_by) VALUES (@title, @description, @added_date, @added_by)";
				System.Diagnostics.Trace.WriteLine(string.Format("Insert(c={0}): sql → {1}", c, sql));

                //Creating SQL Command to pass values in our query
                SqlCommand cmd = new SqlCommand(sql, conn);
				System.Diagnostics.Trace.WriteLine(string.Format("Insert(c={0}): cmd → {1}", c, cmd));
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

				System.Diagnostics.Trace.WriteLine(string.Format("Insert(c)={0}: save query executed in {1} ms", c, __logStart.ElapsedMilliseconds));
				System.Diagnostics.Trace.WriteLine(string.Format("Insert(c={0}): rows → {1}", c, rows));
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
				System.Diagnostics.Trace.WriteLine(string.Format("Exception in Insert(c={0}): {1}", c, ex.Message));
                MessageBox.Show(ex.Message);
            }
            finally
            {
                //Closing Database Connection
                conn.Close();
            }

			System.Diagnostics.Trace.WriteLine(string.Format("<<< Exiting Insert(c={0})", c));
            return isSucces;
        }
        #endregion
        #region Update Method
        public bool Update(categoriesBLL c)
        {
			System.Diagnostics.Trace.WriteLine(string.Format(">>> Entering Update(c={0})", c));
            //Creating Boolean variable and set its default value to false
            bool isSuccess = false;
			System.Diagnostics.Trace.WriteLine(string.Format("Update(c={0}): isSuccess → {1}", c, isSuccess));

            //Creating SQL Connection
            SqlConnection conn = new SqlConnection(myconnstrng);
			System.Diagnostics.Trace.WriteLine(string.Format("Update(c={0}): conn → {1}", c, conn));

            try
            {
                //Query to Update Category
                string sql = "UPDATE tbl_categories SET title=@title, description=@description, added_date=@added_date, added_by=@added_by WHERE id=@id";
				System.Diagnostics.Trace.WriteLine(string.Format("Update(c={0}): sql → {1}", c, sql));

                //SQl Command to Pass the Value on Sql Query
                SqlCommand cmd = new SqlCommand(sql, conn);
				System.Diagnostics.Trace.WriteLine(string.Format("Update(c={0}): cmd → {1}", c, cmd));

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

				System.Diagnostics.Trace.WriteLine(string.Format("Update(c)={0}: update query executed in {1} ms", c, __logStart.ElapsedMilliseconds));
				System.Diagnostics.Trace.WriteLine(string.Format("Update(c={0}): rows → {1}", c, rows));
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
				System.Diagnostics.Trace.WriteLine(string.Format("Exception in Update(c={0}): {1}", c, ex.Message));
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

			System.Diagnostics.Trace.WriteLine(string.Format("<<< Exiting Update(c={0})", c));
            return isSuccess;
        }
        #endregion
        #region Delete Category Method
        public bool Delete(categoriesBLL c)
        {
			System.Diagnostics.Trace.WriteLine(string.Format(">>> Entering Delete(c={0})", c));
            //Create a Boolean variable and set its value to false
            bool isSuccess = false;
			System.Diagnostics.Trace.WriteLine(string.Format("Delete(c={0}): isSuccess → {1}", c, isSuccess));

            SqlConnection conn = new SqlConnection(myconnstrng);
			System.Diagnostics.Trace.WriteLine(string.Format("Delete(c={0}): conn → {1}", c, conn));

            try
            {
                //SQL Query to Delete from Database
                string sql = "DELETE FROM tbl_categories WHERE id=@id";
				System.Diagnostics.Trace.WriteLine(string.Format("Delete(c={0}): sql → {1}", c, sql));

                SqlCommand cmd = new SqlCommand(sql, conn);
				System.Diagnostics.Trace.WriteLine(string.Format("Delete(c={0}): cmd → {1}", c, cmd));
                //Passing the value using cmd
                cmd.Parameters.AddWithValue("@id", c.id);

                //Open SqlConnection
                conn.Open();

				var __logStart = System.Diagnostics.Stopwatch.StartNew();
                int rows = cmd.ExecuteNonQuery();

				System.Diagnostics.Trace.WriteLine(string.Format("Delete(c)={0}: delete query executed in {1} ms", c, __logStart.ElapsedMilliseconds));
				System.Diagnostics.Trace.WriteLine(string.Format("Delete(c={0}): rows → {1}", c, rows));
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
				System.Diagnostics.Trace.WriteLine(string.Format("Exception in Delete(c={0}): {1}", c, ex.Message));
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

			System.Diagnostics.Trace.WriteLine(string.Format("<<< Exiting Delete(c={0})", c));
            return isSuccess;
        }
        #endregion
        #region Method for Searh Funtionality
        public DataTable Search(string keywords)
        {
			System.Diagnostics.Trace.WriteLine(string.Format(">>> Entering Search(keywords={0})", keywords));
            //SQL Connection For Database Connection
            SqlConnection conn = new SqlConnection(myconnstrng);
			System.Diagnostics.Trace.WriteLine(string.Format("Search(keywords={0}): conn → {1}", keywords, conn));

            //Creating Data TAble to hold the data from database temporarily
            DataTable dt = new DataTable();
			System.Diagnostics.Trace.WriteLine(string.Format("Search(keywords={0}): dt → {1}", keywords, dt));

            try
            {
                //SQL Query To Search Categories from DAtabase
                String sql = "SELECT * FROM tbl_categories WHERE id LIKE '%"+keywords+"%' OR title LIKE '%"+keywords+"%' OR description LIKE '%"+keywords+"%'";
				System.Diagnostics.Trace.WriteLine(string.Format("Search(keywords={0}): sql → {1}", keywords, sql));
                //Creating SQL Command to Execute the Query
                SqlCommand cmd = new SqlCommand(sql, conn);
				System.Diagnostics.Trace.WriteLine(string.Format("Search(keywords={0}): cmd → {1}", keywords, cmd));

                //Getting DAta From DAtabase
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
				System.Diagnostics.Trace.WriteLine(string.Format("Search(keywords={0}): adapter → {1}", keywords, adapter));

                //Open DatabaseConnection
                conn.Open();
                //Passing values from adapter to Data Table dt
				var __logStart = System.Diagnostics.Stopwatch.StartNew();
				System.Diagnostics.Trace.WriteLine(string.Format("Search(keywords)={0}: find query executed in {1} ms", keywords, __logStart.ElapsedMilliseconds));
                adapter.Fill(dt);
            }
            catch(Exception ex)
            {
				System.Diagnostics.Trace.WriteLine(string.Format("Exception in Search(keywords={0}): {1}", keywords, ex.Message));
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

			System.Diagnostics.Trace.WriteLine(string.Format("<<< Exiting Search(keywords={0})", keywords));
            return dt;
        }
        #endregion
    }
}
