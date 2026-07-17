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
    class userDAL
    {
        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        #region Select Data from Database
        public DataTable Select()
        {
			Console.WriteLine(">>> Entering Select()");
			var __logStart = System.Diagnostics.Stopwatch.StartNew();
            //Static MEthod to connect Database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //TO hold the data from database 
			Console.WriteLine("Select(): conn → {0}", conn);
            DataTable dt = new DataTable();
            try
            {
			Console.WriteLine("Select(): dt → {0}", dt);
                //SQL Query to Get Data From DAtabase
                String sql = "SELECT * FROM tbl_users";
                //For Executing Command
				Console.WriteLine("Select(): sql → {0}", sql);
                SqlCommand cmd = new SqlCommand(sql, conn);
                //Getting DAta from dAtabase
				Console.WriteLine("Select(): cmd → {0}", cmd);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                //Database Connection Open
				Console.WriteLine("Select(): adapter → {0}", adapter);
                conn.Open();
                //Fill Data in our DataTable
                adapter.Fill(dt);
				Console.WriteLine("Select(): find query executed in {0} ms", __logStart.ElapsedMilliseconds);
            }
            catch(Exception ex)
            {
				Console.Error.WriteLine("Exception in Select(): {0}", ex.Message);
                //Throw Message if any error occurs
                MessageBox.Show(ex.Message);
            }
            finally
            {
                //Closing Connection
                conn.Close();
            }
            //Return the value in DataTable
			Console.WriteLine("<<< Exiting Select()");
            return dt;
        }
        #endregion
        #region Insert Data in Database
        public bool Insert(userBLL u)
        {
			Console.WriteLine(">>> Entering Insert(u={0})", u);
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);
			Console.WriteLine("Insert(u={0}): isSuccess → {1}", u, isSuccess);
			Console.WriteLine("Insert(u={0}): conn → {1}", u, conn);
			var __logStart = System.Diagnostics.Stopwatch.StartNew();

            try
            {
                String sql = "INSERT INTO tbl_users (first_name, last_name, email, username, password, contact, address, gender, user_type, added_date, added_by) VALUES (@first_name, @last_name, @email, @username, @password, @contact, @address, @gender, @user_type, @added_date, @added_by)";
                SqlCommand cmd = new SqlCommand(sql, conn);
				Console.WriteLine("Insert(u={0}): sql → {1}", u, sql);
				Console.WriteLine("Insert(u={0}): cmd → {1}", u, cmd);

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

                int rows = cmd.ExecuteNonQuery();

				Console.WriteLine("Insert(u={0}): rows → {1}", u, rows);
				Console.WriteLine("Insert(u)={0}: save query executed in {1} ms", u, __logStart.ElapsedMilliseconds);
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
				Console.Error.WriteLine("Exception in Insert(u={0}): {1}", u, ex.Message);
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
			Console.WriteLine("<<< Exiting Insert(u={0})", u);
            return isSuccess;
        }
        #endregion
        #region Update data in Database
        public bool Update(userBLL u)
        {
			Console.WriteLine(">>> Entering Update(u={0})", u);
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);
			Console.WriteLine("Update(u={0}): isSuccess → {1}", u, isSuccess);
			Console.WriteLine("Update(u={0}): conn → {1}", u, conn);
			var __logStart = System.Diagnostics.Stopwatch.StartNew();
            
            try
            {
                string sql = "UPDATE tbl_users SET first_name=@first_name, last_name=@last_name, email=@email, username=@username, password=@password, contact=@contact, address=@address, gender=@gender, user_type=@user_type, added_date=@added_date, added_by=@added_by WHERE id=@id";
                SqlCommand cmd = new SqlCommand(sql, conn);
				Console.WriteLine("Update(u={0}): sql → {1}", u, sql);
				Console.WriteLine("Update(u={0}): cmd → {1}", u, cmd);

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

                int rows = cmd.ExecuteNonQuery();
                if(rows>0)
                {
				Console.WriteLine("Update(u={0}): rows → {1}", u, rows);
				Console.WriteLine("Update(u)={0}: update query executed in {1} ms", u, __logStart.ElapsedMilliseconds);
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
				Console.Error.WriteLine("Exception in Update(u={0}): {1}", u, ex.Message);
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
			Console.WriteLine("<<< Exiting Update(u={0})", u);
            return isSuccess;
        }
        #endregion
        #region Delete Data from DAtabase
        public bool Delete(userBLL u)
        {
			Console.WriteLine(">>> Entering Delete(u={0})", u);
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);
			Console.WriteLine("Delete(u={0}): isSuccess → {1}", u, isSuccess);
			Console.WriteLine("Delete(u={0}): conn → {1}", u, conn);
			var __logStart = System.Diagnostics.Stopwatch.StartNew();

            try
            {
                string sql = "DELETE FROM tbl_users WHERE id=@id";

				Console.WriteLine("Delete(u={0}): sql → {1}", u, sql);
                SqlCommand cmd = new SqlCommand(sql, conn);

				Console.WriteLine("Delete(u={0}): cmd → {1}", u, cmd);
                cmd.Parameters.AddWithValue("@id", u.id);
                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                if(rows>0)
                {
				Console.WriteLine("Delete(u={0}): rows → {1}", u, rows);
				Console.WriteLine("Delete(u)={0}: delete query executed in {1} ms", u, __logStart.ElapsedMilliseconds);
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
				Console.Error.WriteLine("Exception in Delete(u={0}): {1}", u, ex.Message);
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
			Console.WriteLine("<<< Exiting Delete(u={0})", u);
            return isSuccess;
        }
        #endregion
        #region Search User on Database usingKeywords
        public DataTable Search(string keywords)
        {
			Console.WriteLine(">>> Entering Search(keywords={0})", keywords);
			var __logStart = System.Diagnostics.Stopwatch.StartNew();
            //Static MEthod to connect Database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //TO hold the data from database 
			Console.WriteLine("Search(keywords={0}): conn → {1}", keywords, conn);
            DataTable dt = new DataTable();
            try
            {
			Console.WriteLine("Search(keywords={0}): dt → {1}", keywords, dt);
                //SQL Query to Get Data From DAtabase
                String sql = "SELECT * FROM tbl_users WHERE id LIKE '%"+keywords+"%' OR first_name LIKE '%"+keywords+"%' OR last_name LIKE '%"+keywords+"%' OR username LIKE '%"+keywords+"%'";
                //For Executing Command
				Console.WriteLine("Search(keywords={0}): sql → {1}", keywords, sql);
                SqlCommand cmd = new SqlCommand(sql, conn);
                //Getting DAta from dAtabase
				Console.WriteLine("Search(keywords={0}): cmd → {1}", keywords, cmd);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                //Database Connection Open
				Console.WriteLine("Search(keywords={0}): adapter → {1}", keywords, adapter);
                conn.Open();
                //Fill Data in our DataTable
                adapter.Fill(dt);
				Console.WriteLine("Search(keywords)={0}: find query executed in {1} ms", keywords, __logStart.ElapsedMilliseconds);
            }
            catch (Exception ex)
            {
				Console.Error.WriteLine("Exception in Search(keywords={0}): {1}", keywords, ex.Message);
                //Throw Message if any error occurs
                MessageBox.Show(ex.Message);
            }
            finally
            {
                //Closing Connection
                conn.Close();
            }
            //Return the value in DataTable
			Console.WriteLine("<<< Exiting Search(keywords={0})", keywords);
            return dt;
        }
        #endregion
        #region Getting User ID from Username
        public userBLL GetIDFromUsername (string username)
        {
			Console.WriteLine(">>> Entering GetIDFromUsername(username={0})", username);
            userBLL u = new userBLL();
            SqlConnection conn = new SqlConnection(myconnstrng);
			Console.WriteLine("GetIDFromUsername(username={0}): conn → {1}", username, conn);
			var __logStart = System.Diagnostics.Stopwatch.StartNew();
            DataTable dt = new DataTable();

			Console.WriteLine("GetIDFromUsername(username={0}): dt → {1}", username, dt);
            try
            {
                string sql = "SELECT id FROM tbl_users WHERE username='"+username+"'";

				Console.WriteLine("GetIDFromUsername(username={0}): sql → {1}", username, sql);
                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                conn.Open();
				Console.WriteLine("GetIDFromUsername(username={0}): adapter → {1}", username, adapter);

                adapter.Fill(dt);
				Console.WriteLine("GetIDFromUsername(username)={0}: find query executed in {1} ms", username, __logStart.ElapsedMilliseconds);
                if(dt.Rows.Count>0)
                {
                    u.id = int.Parse(dt.Rows[0]["id"].ToString());
                }
            }
            catch(Exception ex)
            {
				Console.Error.WriteLine("Exception in GetIDFromUsername(username={0}): {1}", username, ex.Message);
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return u;
        }
        #endregion
    }
}
