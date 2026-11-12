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
    class productsDAL
    {
        //Creating STATI String Method for DB Connection
        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        #region Select method for Product Module
        public DataTable Select()
        {
			System.Diagnostics.Trace.WriteLine(">>> Entering Select()");
            //Create Sql Connection to connect Databaes
            SqlConnection conn = new SqlConnection(myconnstrng);
			System.Diagnostics.Trace.WriteLine(string.Format("Select(): conn → {0}", conn));

            //DAtaTable to hold the data from database
            DataTable dt = new DataTable();
			System.Diagnostics.Trace.WriteLine(string.Format("Select(): dt → {0}", dt));

            try
            {
                //Writing the Query to Select all the products from database
                String sql = "SELECT * FROM tbl_products";
				System.Diagnostics.Trace.WriteLine(string.Format("Select(): sql → {0}", sql));

                //Creating SQL Command to Execute Query
                SqlCommand cmd = new SqlCommand(sql, conn);
				System.Diagnostics.Trace.WriteLine(string.Format("Select(): cmd → {0}", cmd));

                //SQL Data Adapter to hold the value from database temporarily
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
				System.Diagnostics.Trace.WriteLine(string.Format("Select(): adapter → {0}", adapter));

                //Open DAtabase Connection
                conn.Open();

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
        #region Method to Insert Product in database
        public bool Insert(productsBLL p)
        {
			System.Diagnostics.Trace.WriteLine(string.Format(">>> Entering Insert(p={0})", p));
            //Creating Boolean Variable and set its default value to false
            bool isSuccess = false;
			System.Diagnostics.Trace.WriteLine(string.Format("Insert(p={0}): isSuccess → {1}", p, isSuccess));

            //Sql Connection for DAtabase
            SqlConnection conn = new SqlConnection(myconnstrng);
			System.Diagnostics.Trace.WriteLine(string.Format("Insert(p={0}): conn → {1}", p, conn));

            try
            {
                //SQL Query to insert product into database
                String sql = "INSERT INTO tbl_products (name, category, description, rate, qty, added_date, added_by) VALUES (@name, @category, @description, @rate, @qty, @added_date, @added_by)";
				System.Diagnostics.Trace.WriteLine(string.Format("Insert(p={0}): sql → {1}", p, sql));

                //Creating SQL Command to pass the values
                SqlCommand cmd = new SqlCommand(sql, conn);
				System.Diagnostics.Trace.WriteLine(string.Format("Insert(p={0}): cmd → {1}", p, cmd));

                //Passign the values through parameters
                cmd.Parameters.AddWithValue("@name", p.name);
                cmd.Parameters.AddWithValue("@category", p.category);
                cmd.Parameters.AddWithValue("@description", p.description);
                cmd.Parameters.AddWithValue("@rate", p.rate);
                cmd.Parameters.AddWithValue("@qty", p.qty);
                cmd.Parameters.AddWithValue("@added_date", p.added_date);
                cmd.Parameters.AddWithValue("@added_by", p.added_by);

                //Opening the Database connection
                conn.Open();

				var __logStart = System.Diagnostics.Stopwatch.StartNew();
                int rows = cmd.ExecuteNonQuery();

				System.Diagnostics.Trace.WriteLine(string.Format("Insert(p)={0}: save query executed in {1} ms", p, __logStart.ElapsedMilliseconds));
				System.Diagnostics.Trace.WriteLine(string.Format("Insert(p={0}): rows → {1}", p, rows));
                //If the query is executed successfully then the value of rows will be greater than 0 else it will be less than 0
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
				System.Diagnostics.Trace.WriteLine(string.Format("Exception in Insert(p={0}): {1}", p, ex.Message));
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

			System.Diagnostics.Trace.WriteLine(string.Format("<<< Exiting Insert(p={0})", p));
            return isSuccess;
        }
        #endregion
        #region Method to Update Product in Database
        public bool Update(productsBLL p)
        {
			System.Diagnostics.Trace.WriteLine(string.Format(">>> Entering Update(p={0})", p));
            //create a boolean variable and set its initial value to false
            bool isSuccess = false;
			System.Diagnostics.Trace.WriteLine(string.Format("Update(p={0}): isSuccess → {1}", p, isSuccess));

            //Create SQL Connection for DAtabase
            SqlConnection conn = new SqlConnection(myconnstrng);
			System.Diagnostics.Trace.WriteLine(string.Format("Update(p={0}): conn → {1}", p, conn));

            try
            {
                //SQL Query to Update Data in dAtabase
                String sql = "UPDATE tbl_products SET name=@name, category=@category, description=@description, rate=@rate, added_date=@added_date, added_by=@added_by WHERE id=@id";
				System.Diagnostics.Trace.WriteLine(string.Format("Update(p={0}): sql → {1}", p, sql));

                //Create SQL Cmmand to pass the value to query
                SqlCommand cmd = new SqlCommand(sql, conn);
				System.Diagnostics.Trace.WriteLine(string.Format("Update(p={0}): cmd → {1}", p, cmd));
                //Passing the values using parameters and cmd
                cmd.Parameters.AddWithValue("@name", p.name);
                cmd.Parameters.AddWithValue("@category", p.category);
                cmd.Parameters.AddWithValue("@description", p.description);
                cmd.Parameters.AddWithValue("@rate", p.rate);
                cmd.Parameters.AddWithValue("@qty", p.qty);
                cmd.Parameters.AddWithValue("@added_date", p.added_date);
                cmd.Parameters.AddWithValue("@added_by", p.added_by);
                cmd.Parameters.AddWithValue("@id", p.id);

                //Open the Database connection
                conn.Open();

                //Create Int Variable to check if the query is executed successfully or not
				var __logStart = System.Diagnostics.Stopwatch.StartNew();
                int rows = cmd.ExecuteNonQuery();

				System.Diagnostics.Trace.WriteLine(string.Format("Update(p)={0}: update query executed in {1} ms", p, __logStart.ElapsedMilliseconds));
				System.Diagnostics.Trace.WriteLine(string.Format("Update(p={0}): rows → {1}", p, rows));
                //if the query is executed successfully then the value of rows will be greater than 0 else it will be less than zero
                if(rows>0)
                {
                    //Query ExecutedSuccessfully
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
				System.Diagnostics.Trace.WriteLine(string.Format("Exception in Update(p={0}): {1}", p, ex.Message));
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

			System.Diagnostics.Trace.WriteLine(string.Format("<<< Exiting Update(p={0})", p));
            return isSuccess;
        }
        #endregion
        #region Method to Delete Product from Database
        public bool Delete(productsBLL p)
        {
			System.Diagnostics.Trace.WriteLine(string.Format(">>> Entering Delete(p={0})", p));
            //Create Boolean Variable and Set its default value to false
            bool isSuccess = false;
			System.Diagnostics.Trace.WriteLine(string.Format("Delete(p={0}): isSuccess → {1}", p, isSuccess));

            //SQL Connection for DB connection
            SqlConnection conn = new SqlConnection(myconnstrng);
			System.Diagnostics.Trace.WriteLine(string.Format("Delete(p={0}): conn → {1}", p, conn));

            try
            {
                //Write Query Product from DAtabase
                String sql = "DELETE FROM tbl_products WHERE id=@id";
				System.Diagnostics.Trace.WriteLine(string.Format("Delete(p={0}): sql → {1}", p, sql));

                //Sql Command to Pass the Value
                SqlCommand cmd = new SqlCommand(sql, conn);
				System.Diagnostics.Trace.WriteLine(string.Format("Delete(p={0}): cmd → {1}", p, cmd));

                //Passing the values using cmd
                cmd.Parameters.AddWithValue("@id", p.id);

                //Open Database Connection
                conn.Open();

				var __logStart = System.Diagnostics.Stopwatch.StartNew();
                int rows = cmd.ExecuteNonQuery();
                //If the query is executed successfullly then the value of rows will be greated than 0 else it will be less than 0
				System.Diagnostics.Trace.WriteLine(string.Format("Delete(p)={0}: delete query executed in {1} ms", p, __logStart.ElapsedMilliseconds));
                if(rows>0)
                {
					System.Diagnostics.Trace.WriteLine(string.Format("Delete(p={0}): rows → {1}", p, rows));
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
				System.Diagnostics.Trace.WriteLine(string.Format("Exception in Delete(p={0}): {1}", p, ex.Message));
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

			System.Diagnostics.Trace.WriteLine(string.Format("<<< Exiting Delete(p={0})", p));
            return isSuccess;
        }
        #endregion
        #region SEARCH Method for Product Module
        public DataTable Search (string keywords)
        {
			System.Diagnostics.Trace.WriteLine(string.Format(">>> Entering Search(keywords={0})", keywords));
            //SQL Connection fro DB Connection
            SqlConnection conn = new SqlConnection(myconnstrng);
			System.Diagnostics.Trace.WriteLine(string.Format("Search(keywords={0}): conn → {1}", keywords, conn));
            //Creating DAtaTable to hold value from dAtabase
            DataTable dt = new DataTable();
			System.Diagnostics.Trace.WriteLine(string.Format("Search(keywords={0}): dt → {1}", keywords, dt));

            try
            {
                //SQL query to search product
                string sql = "SELECT * FROM tbl_products WHERE id LIKE '%"+keywords+"%' OR name LIKE '%"+keywords+"%' OR category LIKE '%"+keywords+"%'";
				System.Diagnostics.Trace.WriteLine(string.Format("Search(keywords={0}): sql → {1}", keywords, sql));
                //Sql Command to execute Query
                SqlCommand cmd = new SqlCommand(sql, conn);
				System.Diagnostics.Trace.WriteLine(string.Format("Search(keywords={0}): cmd → {1}", keywords, cmd));

                //SQL Data Adapter to hold the data from database temporarily
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
				System.Diagnostics.Trace.WriteLine(string.Format("Search(keywords={0}): adapter → {1}", keywords, adapter));

                //Open Database Connection
                conn.Open();

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
        #region METHOD TO SEARCH PRODUCT IN TRANSACTION MODULE
        public productsBLL GetProductsForTransaction(string keyword)
        {
			System.Diagnostics.Trace.WriteLine(string.Format(">>> Entering GetProductsForTransaction(keyword={0})", keyword));
            //Create an object of productsBLL and return it
            productsBLL p = new productsBLL();
            //SqlConnection
            SqlConnection conn = new SqlConnection(myconnstrng);
			System.Diagnostics.Trace.WriteLine(string.Format("GetProductsForTransaction(keyword={0}): conn → {1}", keyword, conn));
            //Datatable to store data temporarily
            DataTable dt = new DataTable();
			System.Diagnostics.Trace.WriteLine(string.Format("GetProductsForTransaction(keyword={0}): dt → {1}", keyword, dt));

            try
            {
                //Write the Query to Get the detaisl
                string sql = "SELECT name, rate, qty FROM tbl_products WHERE id LIKE '%"+keyword+"%' OR name LIKE '%"+keyword+"%'";
				System.Diagnostics.Trace.WriteLine(string.Format("GetProductsForTransaction(keyword={0}): sql → {1}", keyword, sql));
                //Create Sql Data Adapter to Execute the query
                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
				System.Diagnostics.Trace.WriteLine(string.Format("GetProductsForTransaction(keyword={0}): adapter → {1}", keyword, adapter));

                //Open DAtabase Connection
                conn.Open();

                //Pass the value from adapter to dt
				var __logStart = System.Diagnostics.Stopwatch.StartNew();
				System.Diagnostics.Trace.WriteLine(string.Format("GetProductsForTransaction(keyword)={0}: find query executed in {1} ms", keyword, __logStart.ElapsedMilliseconds));
                adapter.Fill(dt);

                //If we have any values on dt then set the values to productsBLL
                if(dt.Rows.Count>0)
                {
                    p.name = dt.Rows[0]["name"].ToString();
                    p.rate = decimal.Parse(dt.Rows[0]["rate"].ToString());
                    p.qty = decimal.Parse(dt.Rows[0]["qty"].ToString());
                }
            }
            catch(Exception ex)
            {
				System.Diagnostics.Trace.WriteLine(string.Format("Exception in GetProductsForTransaction(keyword={0}): {1}", keyword, ex.Message));
                MessageBox.Show(ex.Message);
            }
            finally
            {
                //Close Database Connection
                conn.Close();
            }

			System.Diagnostics.Trace.WriteLine(string.Format("<<< Exiting GetProductsForTransaction(keyword={0})", keyword));
            return p;
        }
        #endregion
        #region METHOD TO GET PRODUCT ID BASED ON PRODUCT NAME
        public productsBLL GetProductIDFromName(string ProductName)
        {
			System.Diagnostics.Trace.WriteLine(string.Format(">>> Entering GetProductIDFromName(ProductName={0})", ProductName));
            //First Create an Object of DeaCust BLL and REturn it
            productsBLL p = new productsBLL();

            //SQL Conection here
            SqlConnection conn = new SqlConnection(myconnstrng);
			System.Diagnostics.Trace.WriteLine(string.Format("GetProductIDFromName(ProductName={0}): conn → {1}", ProductName, conn));
            //Data TAble to Holdthe data temporarily
            DataTable dt = new DataTable();
			System.Diagnostics.Trace.WriteLine(string.Format("GetProductIDFromName(ProductName={0}): dt → {1}", ProductName, dt));

            try
            {
                //SQL Query to Get id based on Name
                string sql = "SELECT id FROM tbl_products WHERE name='" + ProductName + "'";
				System.Diagnostics.Trace.WriteLine(string.Format("GetProductIDFromName(ProductName={0}): sql → {1}", ProductName, sql));
                //Create the SQL Data Adapter to Execute the Query
                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
				System.Diagnostics.Trace.WriteLine(string.Format("GetProductIDFromName(ProductName={0}): adapter → {1}", ProductName, adapter));

                conn.Open();

                //Passing the CAlue from Adapter to DAtatable
				var __logStart = System.Diagnostics.Stopwatch.StartNew();
				System.Diagnostics.Trace.WriteLine(string.Format("GetProductIDFromName(ProductName)={0}: find query executed in {1} ms", ProductName, __logStart.ElapsedMilliseconds));
                adapter.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    //Pass the value from dt to DeaCustBLL dc
                    p.id = int.Parse(dt.Rows[0]["id"].ToString());
                }
            }
            catch (Exception ex)
            {
				System.Diagnostics.Trace.WriteLine(string.Format("Exception in GetProductIDFromName(ProductName={0}): {1}", ProductName, ex.Message));
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

			System.Diagnostics.Trace.WriteLine(string.Format("<<< Exiting GetProductIDFromName(ProductName={0})", ProductName));
            return p;
        }
        #endregion
        #region METHOD TO GET CURRENT QUantity from the Database based on Product ID
        public decimal GetProductQty(int ProductID)
        {
			System.Diagnostics.Trace.WriteLine(string.Format(">>> Entering GetProductQty(ProductID={0})", ProductID));
            //SQl Connection First
            SqlConnection conn = new SqlConnection(myconnstrng);
			System.Diagnostics.Trace.WriteLine(string.Format("GetProductQty(ProductID={0}): conn → {1}", ProductID, conn));
            //Create a Decimal Variable and set its default value to 0
            decimal qty = 0;
			System.Diagnostics.Trace.WriteLine(string.Format("GetProductQty(ProductID={0}): qty → {1}", ProductID, qty));

            //Create Data Table to save the data from database temporarily
            DataTable dt = new DataTable();
			System.Diagnostics.Trace.WriteLine(string.Format("GetProductQty(ProductID={0}): dt → {1}", ProductID, dt));

            try
            {
                //Write WQL Query to Get Quantity from Database
                string sql = "SELECT qty FROM tbl_products WHERE id = "+ProductID;
				System.Diagnostics.Trace.WriteLine(string.Format("GetProductQty(ProductID={0}): sql → {1}", ProductID, sql));

                //Cerate A SqlCommand
                SqlCommand cmd = new SqlCommand(sql, conn);
				System.Diagnostics.Trace.WriteLine(string.Format("GetProductQty(ProductID={0}): cmd → {1}", ProductID, cmd));

                //Create a SQL Data Adapter to Execute the query
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
				System.Diagnostics.Trace.WriteLine(string.Format("GetProductQty(ProductID={0}): adapter → {1}", ProductID, adapter));

                //open DAtabase Connection
                conn.Open();

                //PAss the calue from Data Adapter to DataTable
				var __logStart = System.Diagnostics.Stopwatch.StartNew();
				System.Diagnostics.Trace.WriteLine(string.Format("GetProductQty(ProductID)={0}: find query executed in {1} ms", ProductID, __logStart.ElapsedMilliseconds));
                adapter.Fill(dt);

                //Lets check if the datatable has value or not
                if(dt.Rows.Count>0)
                {
                    qty = decimal.Parse(dt.Rows[0]["qty"].ToString());
                }
            }
            catch(Exception ex)
            {
				System.Diagnostics.Trace.WriteLine(string.Format("Exception in GetProductQty(ProductID={0}): {1}", ProductID, ex.Message));
                MessageBox.Show(ex.Message);
            }
            finally
            {
                //Close Database Connection
                conn.Close();
            }

			System.Diagnostics.Trace.WriteLine(string.Format("<<< Exiting GetProductQty(ProductID={0})", ProductID));
            return qty;
        }
        #endregion
        #region METHOD TO UPDATE QUANTITY
        public bool UpdateQuantity(int ProductID, decimal Qty)
        {
			System.Diagnostics.Trace.WriteLine(string.Format(">>> Entering UpdateQuantity(ProductID={0},Qty={1})", ProductID, Qty));
            //Create a Boolean Variable and Set its value to false
            bool success = false;
			System.Diagnostics.Trace.WriteLine(string.Format("UpdateQuantity(ProductID={0},Qty={1}): success → {2}", ProductID, Qty, success));

            //SQl Connection to Connect Database
            SqlConnection conn = new SqlConnection(myconnstrng);
			System.Diagnostics.Trace.WriteLine(string.Format("UpdateQuantity(ProductID={0},Qty={1}): conn → {2}", ProductID, Qty, conn));

            try
            {
                //Write the SQL Query to Update Qty
                string sql = "UPDATE tbl_products SET qty=@qty WHERE id=@id";
				System.Diagnostics.Trace.WriteLine(string.Format("UpdateQuantity(ProductID={0},Qty={1}): sql → {2}", ProductID, Qty, sql));

                //Create SQL Command to Pass the calue into Queyr
                SqlCommand cmd = new SqlCommand(sql, conn);
				System.Diagnostics.Trace.WriteLine(string.Format("UpdateQuantity(ProductID={0},Qty={1}): cmd → {2}", ProductID, Qty, cmd));
                //Passing the VAlue trhough parameters
                cmd.Parameters.AddWithValue("@qty", Qty);
                cmd.Parameters.AddWithValue("@id", ProductID);

                //Open Database Connection
                conn.Open();

                //Create Int Variable and Check whether the query is executed Successfully or not
				var __logStart = System.Diagnostics.Stopwatch.StartNew();
                int rows = cmd.ExecuteNonQuery();
                //Lets check if the query is executed Successfully or not
				System.Diagnostics.Trace.WriteLine(string.Format("UpdateQuantity(ProductID,Qty)={0},{1}: update query executed in {2} ms", ProductID, Qty, __logStart.ElapsedMilliseconds));
                if(rows>0)
                {
					System.Diagnostics.Trace.WriteLine(string.Format("UpdateQuantity(ProductID={0},Qty={1}): rows → {2}", ProductID, Qty, rows));
                    //Query Executed Successfully
                    success = true;
                }
                else
                {
                    //Failed to Execute Query
                    success = false;
                }
            }
            catch(Exception ex)
            {
				System.Diagnostics.Trace.WriteLine(string.Format("Exception in UpdateQuantity(ProductID={0},Qty={1}): {2}", ProductID, Qty, ex.Message));
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

			System.Diagnostics.Trace.WriteLine(string.Format("<<< Exiting UpdateQuantity(ProductID={0},Qty={1})", ProductID, Qty));
            return success;
        }
        #endregion
        #region METHOD TO INCREASE PRODUCT
        public bool IncreaseProduct(int ProductID, decimal IncreaseQty)
        {
			System.Diagnostics.Trace.WriteLine(string.Format(">>> Entering IncreaseProduct(ProductID={0},IncreaseQty={1})", ProductID, IncreaseQty));
            //Create a Boolean Variable and SEt its value to False
            bool success = false;
			System.Diagnostics.Trace.WriteLine(string.Format("IncreaseProduct(ProductID={0},IncreaseQty={1}): success → {2}", ProductID, IncreaseQty, success));

            //Create SQL Connection To Connect DAtabase
            SqlConnection conn = new SqlConnection(myconnstrng);
			System.Diagnostics.Trace.WriteLine(string.Format("IncreaseProduct(ProductID={0},IncreaseQty={1}): conn → {2}", ProductID, IncreaseQty, conn));

            try
            {
                //Get the Current Qty From dAtabase based on id
                decimal currentQty = GetProductQty(ProductID);

                //Increase the Current Quantity by the qty purchased from Dealer
                decimal NewQty = currentQty + IncreaseQty;

                //Update the Prudcty Quantity Now
                success = UpdateQuantity(ProductID, NewQty);
            }
            catch(Exception ex)
            {
				System.Diagnostics.Trace.WriteLine(string.Format("Exception in IncreaseProduct(ProductID={0},IncreaseQty={1}): {2}", ProductID, IncreaseQty, ex.Message));
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
			System.Diagnostics.Trace.WriteLine(string.Format("<<< Exiting IncreaseProduct(ProductID={0},IncreaseQty={1})", ProductID, IncreaseQty));
            return success;
        }
        #endregion
        #region METHOD TO DECREASE PRODUCT
        public bool DecreaseProduct(int ProductID, decimal Qty)
        {
			System.Diagnostics.Trace.WriteLine(string.Format(">>> Entering DecreaseProduct(ProductID={0},Qty={1})", ProductID, Qty));
            //Create Boolean Variable and SEt its Value to false
            bool success = false;
			System.Diagnostics.Trace.WriteLine(string.Format("DecreaseProduct(ProductID={0},Qty={1}): success → {2}", ProductID, Qty, success));

            SqlConnection conn = new SqlConnection(myconnstrng);
			System.Diagnostics.Trace.WriteLine(string.Format("DecreaseProduct(ProductID={0},Qty={1}): conn → {2}", ProductID, Qty, conn));

            try
            {
                //Get the Current product Quantity
                decimal currentQty = GetProductQty(ProductID);

                //Decrease the Product Quantity based on product sales
                decimal NewQty = currentQty - Qty;

                //Update Product in Database
                success = UpdateQuantity(ProductID, NewQty);
            }
            catch(Exception ex)
            {
				System.Diagnostics.Trace.WriteLine(string.Format("Exception in DecreaseProduct(ProductID={0},Qty={1}): {2}", ProductID, Qty, ex.Message));
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
			System.Diagnostics.Trace.WriteLine(string.Format("<<< Exiting DecreaseProduct(ProductID={0},Qty={1})", ProductID, Qty));
            return success;
        }
        #endregion
        #region DESPLAY PRODUCTS BASED ON CATEGORIES
        public DataTable DisplayProductsByCategory(string category)
        {
			System.Diagnostics.Trace.WriteLine(string.Format(">>> Entering DisplayProductsByCategory(category={0})", category));
            //Sql Connection First
            SqlConnection conn = new SqlConnection(myconnstrng);
			System.Diagnostics.Trace.WriteLine(string.Format("DisplayProductsByCategory(category={0}): conn → {1}", category, conn));

            DataTable dt = new DataTable();
			System.Diagnostics.Trace.WriteLine(string.Format("DisplayProductsByCategory(category={0}): dt → {1}", category, dt));

            try
            {
                //SQL Query to Display Product Based on CAtegory
                string sql = "SELECT * FROM tbl_products WHERE category='"+category+"'";
				System.Diagnostics.Trace.WriteLine(string.Format("DisplayProductsByCategory(category={0}): sql → {1}", category, sql));

                SqlCommand cmd = new SqlCommand(sql, conn);
				System.Diagnostics.Trace.WriteLine(string.Format("DisplayProductsByCategory(category={0}): cmd → {1}", category, cmd));

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
				System.Diagnostics.Trace.WriteLine(string.Format("DisplayProductsByCategory(category={0}): adapter → {1}", category, adapter));

                //Open Database Connection Here
                conn.Open();

				var __logStart = System.Diagnostics.Stopwatch.StartNew();
				System.Diagnostics.Trace.WriteLine(string.Format("DisplayProductsByCategory(category)={0}: find query executed in {1} ms", category, __logStart.ElapsedMilliseconds));
                adapter.Fill(dt);
            }
            catch(Exception ex)
            {
				System.Diagnostics.Trace.WriteLine(string.Format("Exception in DisplayProductsByCategory(category={0}): {1}", category, ex.Message));
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

			System.Diagnostics.Trace.WriteLine(string.Format("<<< Exiting DisplayProductsByCategory(category={0})", category));
            return dt;
        }
        #endregion
    }
}
