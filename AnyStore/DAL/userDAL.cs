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
			System.Diagnostics.Trace.WriteLine(">>> Entering Select()");
            //Static MEthod to connect Database
            SqlConnection conn = new SqlConnection(myconnstrng);
			System.Diagnostics.Trace.WriteLine(string.Format("Select(): conn → {0}", conn));
            //TO hold the data from database 
            DataTable dt = new DataTable();
			System.Diagnostics.Trace.WriteLine(string.Format("Select(): dt → {0}", dt));
            try
            {
                //SQL Query to Get Data From DAtabase
                String sql = "SELECT * FROM tbl_users";
				System.Diagnostics.Trace.WriteLine(string.Format("Select(): sql → {0}", sql));
                //For Executing Command
                SqlCommand cmd = new SqlCommand(sql, conn);
				System.Diagnostics.Trace.WriteLine(string.Format("Select(): cmd → {0}", cmd));
                //Getting DAta from dAtabase
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
				System.Diagnostics.Trace.WriteLine(string.Format("Select(): adapter → {0}", adapter));
                //Database Connection Open
                conn.Open();
                //Fill Data in our DataTable
				var __logStart = System.Diagnostics.Stopwatch.StartNew();
				System.Diagnostics.Trace.WriteLine(string.Format("Select(): find query executed in {0} ms", __logStart.ElapsedMilliseconds));
                adapter.Fill(dt);
            }
            catch(Exception ex)
            {
				System.Diagnostics.Trace.WriteLine(string.Format("Exception in Select(): {0}", ex.Message));
                //Throw Message if any error occurs
                MessageBox.Show(ex.Message);
            }
            finally
            {
                //Closing Connection
                conn.Close();
            }
            //Return the value in DataTable
			System.Diagnostics.Trace.WriteLine("<<< Exiting Select()");
            return dt;
        }
        #endregion
        #region Insert Data in Database
        public bool Insert(userBLL u)
        {
			System.Diagnostics.Trace.WriteLine(string.Format(">>> Entering Insert(u={0})", u));
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);
			System.Diagnostics.Trace.WriteLine(string.Format("Insert(u={0}): conn → {1}", u, conn));
			System.Diagnostics.Trace.WriteLine(string.Format("Insert(u={0}): isSuccess → {1}", u, isSuccess));

            try
            {
                String sql = "INSERT INTO tbl_users (first_name, last_name, email, username, password, contact, address, gender, user_type, added_date, added_by) VALUES (@first_name, @last_name, @email, @username, @password, @contact, @address, @gender, @user_type, @added_date, @added_by)";
				System.Diagnostics.Trace.WriteLine(string.Format("Insert(u={0}): sql → {1}", u, sql));
                SqlCommand cmd = new SqlCommand(sql, conn);
				System.Diagnostics.Trace.WriteLine(string.Format("Insert(u={0}): cmd → {1}", u, cmd));

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

				System.Diagnostics.Trace.WriteLine(string.Format("Insert(u)={0}: save query executed in {1} ms", u, __logStart.ElapsedMilliseconds));
				System.Diagnostics.Trace.WriteLine(string.Format("Insert(u={0}): rows → {1}", u, rows));
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
				System.Diagnostics.Trace.WriteLine(string.Format("Exception in Insert(u={0}): {1}", u, ex.Message));
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
			System.Diagnostics.Trace.WriteLine(string.Format("<<< Exiting Insert(u={0})", u));
            return isSuccess;
        }
        #endregion
        #region Update data in Database
        public bool Update(userBLL u)
        {
			System.Diagnostics.Trace.WriteLine(string.Format(">>> Entering Update(u={0})", u));
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);
			System.Diagnostics.Trace.WriteLine(string.Format("Update(u={0}): conn → {1}", u, conn));
			System.Diagnostics.Trace.WriteLine(string.Format("Update(u={0}): isSuccess → {1}", u, isSuccess));
            
            try
            {
                string sql = "UPDATE tbl_users SET first_name=@first_name, last_name=@last_name, email=@email, username=@username, password=@password, contact=@contact, address=@address, gender=@gender, user_type=@user_type, added_date=@added_date, added_by=@added_by WHERE id=@id";
				System.Diagnostics.Trace.WriteLine(string.Format("Update(u={0}): sql → {1}", u, sql));
                SqlCommand cmd = new SqlCommand(sql, conn);
				System.Diagnostics.Trace.WriteLine(string.Format("Update(u={0}): cmd → {1}", u, cmd));

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
				System.Diagnostics.Trace.WriteLine(string.Format("Exception in Update(u={0}): {1}", u, ex.Message));
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
			System.Diagnostics.Trace.WriteLine(string.Format("<<< Exiting Update(u={0})", u));
            return isSuccess;
        }
        #endregion
        #region Delete Data from DAtabase
        public bool Delete(userBLL u)
        {
			System.Diagnostics.Trace.WriteLine(string.Format(">>> Entering Delete(u={0})", u));
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);
			System.Diagnostics.Trace.WriteLine(string.Format("Delete(u={0}): conn → {1}", u, conn));
			System.Diagnostics.Trace.WriteLine(string.Format("Delete(u={0}): isSuccess → {1}", u, isSuccess));

            try
            {
                string sql = "DELETE FROM tbl_users WHERE id=@id";
				System.Diagnostics.Trace.WriteLine(string.Format("Delete(u={0}): sql → {1}", u, sql));

                SqlCommand cmd = new SqlCommand(sql, conn);
				System.Diagnostics.Trace.WriteLine(string.Format("Delete(u={0}): cmd → {1}", u, cmd));

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
				System.Diagnostics.Trace.WriteLine(string.Format("Exception in Delete(u={0}): {1}", u, ex.Message));
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
			System.Diagnostics.Trace.WriteLine(string.Format("<<< Exiting Delete(u={0})", u));
            return isSuccess;
        }
        #endregion
        #region Search User on Database usingKeywords
        public DataTable Search(string keywords)
        {
			System.Diagnostics.Trace.WriteLine(string.Format(">>> Entering Search(keywords={0})", keywords));
            //Static MEthod to connect Database
            SqlConnection conn = new SqlConnection(myconnstrng);
			System.Diagnostics.Trace.WriteLine(string.Format("Search(keywords={0}): conn → {1}", keywords, conn));
            //TO hold the data from database 
            DataTable dt = new DataTable();
			System.Diagnostics.Trace.WriteLine(string.Format("Search(keywords={0}): dt → {1}", keywords, dt));
            try
            {
                //SQL Query to Get Data From DAtabase
                String sql = "SELECT * FROM tbl_users WHERE id LIKE '%"+keywords+"%' OR first_name LIKE '%"+keywords+"%' OR last_name LIKE '%"+keywords+"%' OR username LIKE '%"+keywords+"%'";
				System.Diagnostics.Trace.WriteLine(string.Format("Search(keywords={0}): sql → {1}", keywords, sql));
                //For Executing Command
                SqlCommand cmd = new SqlCommand(sql, conn);
				System.Diagnostics.Trace.WriteLine(string.Format("Search(keywords={0}): cmd → {1}", keywords, cmd));
                //Getting DAta from dAtabase
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
				System.Diagnostics.Trace.WriteLine(string.Format("Search(keywords={0}): adapter → {1}", keywords, adapter));
                //Database Connection Open
                conn.Open();
                //Fill Data in our DataTable
				var __logStart = System.Diagnostics.Stopwatch.StartNew();
				System.Diagnostics.Trace.WriteLine(string.Format("Search(keywords)={0}: find query executed in {1} ms", keywords, __logStart.ElapsedMilliseconds));
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
				System.Diagnostics.Trace.WriteLine(string.Format("Exception in Search(keywords={0}): {1}", keywords, ex.Message));
                //Throw Message if any error occurs
                MessageBox.Show(ex.Message);
            }
            finally
            {
                //Closing Connection
                conn.Close();
            }
            //Return the value in DataTable
			System.Diagnostics.Trace.WriteLine(string.Format("<<< Exiting Search(keywords={0})", keywords));
            return dt;
        }
        #endregion
        #region Getting User ID from Username
        public userBLL GetIDFromUsername (string username)
        {
			System.Diagnostics.Trace.WriteLine(string.Format(">>> Entering GetIDFromUsername(username={0})", username));
            userBLL u = new userBLL();
            SqlConnection conn = new SqlConnection(myconnstrng);
			System.Diagnostics.Trace.WriteLine(string.Format("GetIDFromUsername(username={0}): conn → {1}", username, conn));
            DataTable dt = new DataTable();
			System.Diagnostics.Trace.WriteLine(string.Format("GetIDFromUsername(username={0}): dt → {1}", username, dt));

            try
            {
                string sql = "SELECT id FROM tbl_users WHERE username='"+username+"'";
				System.Diagnostics.Trace.WriteLine(string.Format("GetIDFromUsername(username={0}): sql → {1}", username, sql));

                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
				System.Diagnostics.Trace.WriteLine(string.Format("GetIDFromUsername(username={0}): adapter → {1}", username, adapter));
                conn.Open();

				var __logStart = System.Diagnostics.Stopwatch.StartNew();
				System.Diagnostics.Trace.WriteLine(string.Format("GetIDFromUsername(username)={0}: find query executed in {1} ms", username, __logStart.ElapsedMilliseconds));
                adapter.Fill(dt);
                if(dt.Rows.Count>0)
                {
                    u.id = int.Parse(dt.Rows[0]["id"].ToString());
                }
            }
            catch(Exception ex)
            {
				System.Diagnostics.Trace.WriteLine(string.Format("Exception in GetIDFromUsername(username={0}): {1}", username, ex.Message));
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
			System.Diagnostics.Trace.WriteLine(string.Format("<<< Exiting GetIDFromUsername(username={0})", username));
            return u;
        }
        #endregion
    }
}
