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
    class userDAL
    {
		private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        #region Select Data from Database
        public DataTable Select()
        {
			_logger.LogDebug(">>> Entering Select()");
            //Static MEthod to connect Database
            SqlConnection conn = new SqlConnection(myconnstrng);
			_logger.LogDebug("Select(): conn → {Conn}", conn);
            //TO hold the data from database 
            DataTable dt = new DataTable();
			_logger.LogDebug("Select(): dt → {Dt}", dt);
            try
            {
                //SQL Query to Get Data From DAtabase
                String sql = "SELECT * FROM tbl_users";
				_logger.LogDebug("Select(): sql → {Sql}", sql);
                //For Executing Command
                SqlCommand cmd = new SqlCommand(sql, conn);
				_logger.LogDebug("Select(): cmd → {Cmd}", cmd);
                //Getting DAta from dAtabase
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
				_logger.LogDebug("Select(): adapter → {Adapter}", adapter);
                //Database Connection Open
                conn.Open();
                //Fill Data in our DataTable
				var __logStart = System.Diagnostics.Stopwatch.StartNew();
				_logger.LogInformation("Select(): find query executed in {Arg0} ms", __logStart.ElapsedMilliseconds);
                adapter.Fill(dt);
            }
            catch(Exception ex)
            {
				_logger.LogError("Exception in Select(): {Arg0}", ex.getMessage());
                //Throw Message if any error occurs
                MessageBox.Show(ex.Message);
            }
            finally
            {
                //Closing Connection
                conn.Close();
            }
            //Return the value in DataTable
			_logger.LogDebug("<<< Exiting Select()");
            return dt;
        }
        #endregion
        #region Insert Data in Database
        public bool Insert(userBLL u)
        {
			_logger.LogDebug(">>> Entering Insert(u={U})", u);
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);
			_logger.LogDebug("Insert(u={U}): conn → {Conn}", u, conn);
			_logger.LogDebug("Insert(u={U}): isSuccess → {IsSuccess}", u, isSuccess);

            try
            {
                String sql = "INSERT INTO tbl_users (first_name, last_name, email, username, password, contact, address, gender, user_type, added_date, added_by) VALUES (@first_name, @last_name, @email, @username, @password, @contact, @address, @gender, @user_type, @added_date, @added_by)";
				_logger.LogDebug("Insert(u={U}): sql → {Sql}", u, sql);
                SqlCommand cmd = new SqlCommand(sql, conn);
				_logger.LogDebug("Insert(u={U}): cmd → {Cmd}", u, cmd);

                cmd.Parameters.AddWithValue("@first_name", u.first_name);
                cmd.Parameters.AddWithValue("@last_name", u.last_name);
                cmd.Parameters.AddWithValue("@email", u.email);
                cmd.Parameters.AddWithValue("@username", u.username);
                cmd.Parameters.AddWithValue("@password", u.password);
                cmd.Parameters.AddWithValue("@contact", u.contact);
                cmd.Parameters.AddWithValue("@address", u.address);
                cmd.Parameters.AddWithValue("@gender", u.gender);
                cmd.Parameters.AddWithValue("@user_type", u.user_type);
                cmd.Parameters.AddWithValue("@added_date", u.added_date);
                cmd.Parameters.AddWithValue("@added_by", u.added_by);

                conn.Open();

				var __logStart = System.Diagnostics.Stopwatch.StartNew();
                int rows = cmd.ExecuteNonQuery();

				_logger.LogInformation("Insert(u)={U}: save query executed in {Arg1} ms", u, __logStart.ElapsedMilliseconds);
				_logger.LogDebug("Insert(u={U}): rows → {Rows}", u, rows);
                //If the query is executed Successfully then the value to rows will be greater than 0 else it will be less than 0
                if(rows>0)
                {
                    //Query Sucessfull
                    isSuccess = true;
                }
                else
                {
                    //Query Failed
                    isSuccess = false;
                }
            }
            catch(Exception ex)
            {
				_logger.LogError("Exception in Insert(u={U}): {Arg1}", u, ex.getMessage());
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
			_logger.LogDebug("<<< Exiting Insert(u={U})", u);
            return isSuccess;
        }
        #endregion
        #region Update data in Database
        public bool Update(userBLL u)
        {
			_logger.LogDebug(">>> Entering Update(u={U})", u);
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);
			_logger.LogDebug("Update(u={U}): conn → {Conn}", u, conn);
			_logger.LogDebug("Update(u={U}): isSuccess → {IsSuccess}", u, isSuccess);
            
            try
            {
                string sql = "UPDATE tbl_users SET first_name=@first_name, last_name=@last_name, email=@email, username=@username, password=@password, contact=@contact, address=@address, gender=@gender, user_type=@user_type, added_date=@added_date, added_by=@added_by WHERE id=@id";
				_logger.LogDebug("Update(u={U}): sql → {Sql}", u, sql);
                SqlCommand cmd = new SqlCommand(sql, conn);
				_logger.LogDebug("Update(u={U}): cmd → {Cmd}", u, cmd);

                cmd.Parameters.AddWithValue("@first_name", u.first_name);
                cmd.Parameters.AddWithValue("@last_name", u.last_name);
                cmd.Parameters.AddWithValue("@email", u.email);
                cmd.Parameters.AddWithValue("@username", u.username);
                cmd.Parameters.AddWithValue("@password", u.password);
                cmd.Parameters.AddWithValue("@contact", u.contact);
                cmd.Parameters.AddWithValue("@address", u.address);
                cmd.Parameters.AddWithValue("@gender", u.gender);
                cmd.Parameters.AddWithValue("@user_type", u.user_type);
                cmd.Parameters.AddWithValue("@added_date", u.added_date);
                cmd.Parameters.AddWithValue("@added_by", u.added_by);
                cmd.Parameters.AddWithValue("@id", u.id);

                conn.Open();

				var __logStart = System.Diagnostics.Stopwatch.StartNew();
                int rows = cmd.ExecuteNonQuery();
                if(rows>0)
                {
                    //Query Successfull
                    isSuccess = true;
                }
                else
                {
                    //Query Failed
                    isSuccess = false;
                }

            }
            catch(Exception ex)
            {
				_logger.LogError("Exception in Update(u={U}): {Arg1}", u, ex.getMessage());
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
			_logger.LogDebug("<<< Exiting Update(u={U})", u);
            return isSuccess;
        }
        #endregion
        #region Delete Data from DAtabase
        public bool Delete(userBLL u)
        {
			_logger.LogDebug(">>> Entering Delete(u={U})", u);
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);
			_logger.LogDebug("Delete(u={U}): conn → {Conn}", u, conn);
			_logger.LogDebug("Delete(u={U}): isSuccess → {IsSuccess}", u, isSuccess);

            try
            {
                string sql = "DELETE FROM tbl_users WHERE id=@id";
				_logger.LogDebug("Delete(u={U}): sql → {Sql}", u, sql);

                SqlCommand cmd = new SqlCommand(sql, conn);
				_logger.LogDebug("Delete(u={U}): cmd → {Cmd}", u, cmd);

                cmd.Parameters.AddWithValue("@id", u.id);
                conn.Open();
				var __logStart = System.Diagnostics.Stopwatch.StartNew();
                int rows = cmd.ExecuteNonQuery();
                if(rows>0)
                {
                    //Query Successfull
                    isSuccess = true;
                }
                else
                {
                    //Query Failed
                    isSuccess = false;
                }
            }
            catch(Exception ex)
            {
				_logger.LogError("Exception in Delete(u={U}): {Arg1}", u, ex.getMessage());
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
			_logger.LogDebug("<<< Exiting Delete(u={U})", u);
            return isSuccess;
        }
        #endregion
        #region Search User on Database usingKeywords
        public DataTable Search(string keywords)
        {
			_logger.LogDebug(">>> Entering Search(keywords={Keywords})", keywords);
            //Static MEthod to connect Database
            SqlConnection conn = new SqlConnection(myconnstrng);
			_logger.LogDebug("Search(keywords={Keywords}): conn → {Conn}", keywords, conn);
            //TO hold the data from database 
            DataTable dt = new DataTable();
			_logger.LogDebug("Search(keywords={Keywords}): dt → {Dt}", keywords, dt);
            try
            {
                //SQL Query to Get Data From DAtabase
                String sql = "SELECT * FROM tbl_users WHERE id LIKE '%"+keywords+"%' OR first_name LIKE '%"+keywords+"%' OR last_name LIKE '%"+keywords+"%' OR username LIKE '%"+keywords+"%'";
				_logger.LogDebug("Search(keywords={Keywords}): sql → {Sql}", keywords, sql);
                //For Executing Command
                SqlCommand cmd = new SqlCommand(sql, conn);
				_logger.LogDebug("Search(keywords={Keywords}): cmd → {Cmd}", keywords, cmd);
                //Getting DAta from dAtabase
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
				_logger.LogDebug("Search(keywords={Keywords}): adapter → {Adapter}", keywords, adapter);
                //Database Connection Open
                conn.Open();
                //Fill Data in our DataTable
				var __logStart = System.Diagnostics.Stopwatch.StartNew();
				_logger.LogInformation("Search(keywords)={Keywords}: find query executed in {Arg1} ms", keywords, __logStart.ElapsedMilliseconds);
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
				_logger.LogError("Exception in Search(keywords={Keywords}): {Arg1}", keywords, ex.getMessage());
                //Throw Message if any error occurs
                MessageBox.Show(ex.Message);
            }
            finally
            {
                //Closing Connection
                conn.Close();
            }
            //Return the value in DataTable
			_logger.LogDebug("<<< Exiting Search(keywords={Keywords})", keywords);
            return dt;
        }
        #endregion
        #region Getting User ID from Username
        public userBLL GetIDFromUsername (string username)
        {
			_logger.LogDebug(">>> Entering GetIDFromUsername(username={Username})", username);
            userBLL u = new userBLL();
            SqlConnection conn = new SqlConnection(myconnstrng);
			_logger.LogDebug("GetIDFromUsername(username={Username}): conn → {Conn}", username, conn);
            DataTable dt = new DataTable();
			_logger.LogDebug("GetIDFromUsername(username={Username}): dt → {Dt}", username, dt);

            try
            {
                string sql = "SELECT id FROM tbl_users WHERE username='"+username+"'";
				_logger.LogDebug("GetIDFromUsername(username={Username}): sql → {Sql}", username, sql);

                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
				_logger.LogDebug("GetIDFromUsername(username={Username}): adapter → {Adapter}", username, adapter);
                conn.Open();

				var __logStart = System.Diagnostics.Stopwatch.StartNew();
				_logger.LogInformation("GetIDFromUsername(username)={Username}: find query executed in {Arg1} ms", username, __logStart.ElapsedMilliseconds);
                adapter.Fill(dt);
                if(dt.Rows.Count>0)
                {
                    u.id = int.Parse(dt.Rows[0]["id"].ToString());
                }
            }
            catch(Exception ex)
            {
				_logger.LogError("Exception in GetIDFromUsername(username={Username}): {Arg1}", username, ex.getMessage());
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
			_logger.LogDebug("<<< Exiting GetIDFromUsername(username={Username})", username);
            return u;
        }
        #endregion
    }
}
