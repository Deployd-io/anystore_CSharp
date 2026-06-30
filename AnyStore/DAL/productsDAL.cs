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
			Console.WriteLine(">>> Entering Select()");
            //Create Sql Connection to connect Databaes
            SqlConnection conn = new SqlConnection(myconnstrng);

			Console.WriteLine("Select(): conn → {0}", conn);
            //DAtaTable to hold the data from database
            DataTable dt = new DataTable();

			Console.WriteLine("Select(): dt → {0}", dt);
            try
            {
                //Writing the Query to Select all the products from database
                String sql = "SELECT * FROM tbl_products";

				Console.WriteLine("Select(): sql → {0}", sql);
                //Creating SQL Command to Execute Query
                SqlCommand cmd = new SqlCommand(sql, conn);

				Console.WriteLine("Select(): cmd → {0}", cmd);
                //SQL Data Adapter to hold the value from database temporarily
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

				Console.WriteLine("Select(): adapter → {0}", adapter);
                //Open DAtabase Connection
                conn.Open();

                adapter.Fill(dt);
            }
            catch(Exception ex)
            {
				Console.Error.WriteLine("Exception in Select(): {0}", ex.Message);
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

			Console.WriteLine("<<< Exiting Select()");
            return dt;
        }
        #endregion
        #region Method to Insert Product in database
        public bool Insert(productsBLL p)
        {
			Console.WriteLine(">>> Entering Insert(p={0})", p);
            //Creating Boolean Variable and set its default value to false
            bool isSuccess = false;

			Console.WriteLine("Insert(p={0}): isSuccess → {1}", p, isSuccess);
            //Sql Connection for DAtabase
            SqlConnection conn = new SqlConnection(myconnstrng);

			Console.WriteLine("Insert(p={0}): conn → {1}", p, conn);
            try
            {
                //SQL Query to insert product into database
                String sql = "INSERT INTO tbl_products (name, category, description, rate, qty, added_date, added_by) VALUES (@name, @category, @description, @rate, @qty, @added_date, @added_by)";

				Console.WriteLine("Insert(p={0}): sql → {1}", p, sql);
                //Creating SQL Command to pass the values
                SqlCommand cmd = new SqlCommand(sql, conn);

				Console.WriteLine("Insert(p={0}): cmd → {1}", p, cmd);
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

                int rows = cmd.ExecuteNonQuery();

				Console.WriteLine("Insert(p={0}): rows → {1}", p, rows);
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
				Console.Error.WriteLine("Exception in Insert(p={0}): {1}", p, ex.Message);
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

			Console.WriteLine("<<< Exiting Insert(p={0})", p);
            return isSuccess;
        }
        #endregion
        #region Method to Update Product in Database
        public bool Update(productsBLL p)
        {
			Console.WriteLine(">>> Entering Update(p={0})", p);
            //create a boolean variable and set its initial value to false
            bool isSuccess = false;

			Console.WriteLine("Update(p={0}): isSuccess → {1}", p, isSuccess);
            //Create SQL Connection for DAtabase
            SqlConnection conn = new SqlConnection(myconnstrng);

			Console.WriteLine("Update(p={0}): conn → {1}", p, conn);
            try
            {
                //SQL Query to Update Data in dAtabase
                String sql = "UPDATE tbl_products SET name=@name, category=@category, description=@description, rate=@rate, added_date=@added_date, added_by=@added_by WHERE id=@id";

				Console.WriteLine("Update(p={0}): sql → {1}", p, sql);
                //Create SQL Cmmand to pass the value to query
                SqlCommand cmd = new SqlCommand(sql, conn);
                //Passing the values using parameters and cmd
				Console.WriteLine("Update(p={0}): cmd → {1}", p, cmd);
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
                int rows = cmd.ExecuteNonQuery();

				Console.WriteLine("Update(p={0}): rows → {1}", p, rows);
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
				Console.Error.WriteLine("Exception in Update(p={0}): {1}", p, ex.Message);
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

			Console.WriteLine("<<< Exiting Update(p={0})", p);
            return isSuccess;
        }
        #endregion
        #region Method to Delete Product from Database
        public bool Delete(productsBLL p)
        {
			Console.WriteLine(">>> Entering Delete(p={0})", p);
            //Create Boolean Variable and Set its default value to false
            bool isSuccess = false;

			Console.WriteLine("Delete(p={0}): isSuccess → {1}", p, isSuccess);
            //SQL Connection for DB connection
            SqlConnection conn = new SqlConnection(myconnstrng);

			Console.WriteLine("Delete(p={0}): conn → {1}", p, conn);
            try
            {
                //Write Query Product from DAtabase
                String sql = "DELETE FROM tbl_products WHERE id=@id";

				Console.WriteLine("Delete(p={0}): sql → {1}", p, sql);
                //Sql Command to Pass the Value
                SqlCommand cmd = new SqlCommand(sql, conn);

				Console.WriteLine("Delete(p={0}): cmd → {1}", p, cmd);
                //Passing the values using cmd
                cmd.Parameters.AddWithValue("@id", p.id);

                //Open Database Connection
                conn.Open();

                int rows = cmd.ExecuteNonQuery();
                //If the query is executed successfullly then the value of rows will be greated than 0 else it will be less than 0
				Console.WriteLine("Delete(p={0}): rows → {1}", p, rows);
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
				Console.Error.WriteLine("Exception in Delete(p={0}): {1}", p, ex.Message);
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

			Console.WriteLine("<<< Exiting Delete(p={0})", p);
            return isSuccess;
        }
        #endregion
        #region SEARCH Method for Product Module
        public DataTable Search (string keywords)
        {
			Console.WriteLine(">>> Entering Search(keywords={0})", keywords);
            //SQL Connection fro DB Connection
            SqlConnection conn = new SqlConnection(myconnstrng);
            //Creating DAtaTable to hold value from dAtabase
			Console.WriteLine("Search(keywords={0}): conn → {1}", keywords, conn);
            DataTable dt = new DataTable();

			Console.WriteLine("Search(keywords={0}): dt → {1}", keywords, dt);
            try
            {
                //SQL query to search product
                string sql = "SELECT * FROM tbl_products WHERE id LIKE '%"+keywords+"%' OR name LIKE '%"+keywords+"%' OR category LIKE '%"+keywords+"%'";
                //Sql Command to execute Query
				Console.WriteLine("Search(keywords={0}): sql → {1}", keywords, sql);
                SqlCommand cmd = new SqlCommand(sql, conn);

				Console.WriteLine("Search(keywords={0}): cmd → {1}", keywords, cmd);
                //SQL Data Adapter to hold the data from database temporarily
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

				Console.WriteLine("Search(keywords={0}): adapter → {1}", keywords, adapter);
                //Open Database Connection
                conn.Open();

                adapter.Fill(dt);
            }
            catch(Exception ex)
            {
				Console.Error.WriteLine("Exception in Search(keywords={0}): {1}", keywords, ex.Message);
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return dt;
        }
        #endregion
        #region METHOD TO SEARCH PRODUCT IN TRANSACTION MODULE
        public productsBLL GetProductsForTransaction(string keyword)
        {
			Console.WriteLine(">>> Entering GetProductsForTransaction(keyword={0})", keyword);
            //Create an object of productsBLL and return it
            productsBLL p = new productsBLL();
            //SqlConnection
            SqlConnection conn = new SqlConnection(myconnstrng);
            //Datatable to store data temporarily
			Console.WriteLine("GetProductsForTransaction(keyword={0}): conn → {1}", keyword, conn);
            DataTable dt = new DataTable();

			Console.WriteLine("GetProductsForTransaction(keyword={0}): dt → {1}", keyword, dt);
            try
            {
                //Write the Query to Get the detaisl
                string sql = "SELECT name, rate, qty FROM tbl_products WHERE id LIKE '%"+keyword+"%' OR name LIKE '%"+keyword+"%'";
                //Create Sql Data Adapter to Execute the query
				Console.WriteLine("GetProductsForTransaction(keyword={0}): sql → {1}", keyword, sql);
                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);

				Console.WriteLine("GetProductsForTransaction(keyword={0}): adapter → {1}", keyword, adapter);
                //Open DAtabase Connection
                conn.Open();

                //Pass the value from adapter to dt
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
				Console.Error.WriteLine("Exception in GetProductsForTransaction(keyword={0}): {1}", keyword, ex.Message);
                MessageBox.Show(ex.Message);
            }
            finally
            {
                //Close Database Connection
                conn.Close();
            }

			Console.WriteLine("<<< Exiting GetProductsForTransaction(keyword={0})", keyword);
            return p;
        }
        #endregion
        #region METHOD TO GET PRODUCT ID BASED ON PRODUCT NAME
        public productsBLL GetProductIDFromName(string ProductName)
        {
			Console.WriteLine(">>> Entering GetProductIDFromName(ProductName={0})", ProductName);
            //First Create an Object of DeaCust BLL and REturn it
            productsBLL p = new productsBLL();

            //SQL Conection here
            SqlConnection conn = new SqlConnection(myconnstrng);
            //Data TAble to Holdthe data temporarily
			Console.WriteLine("GetProductIDFromName(ProductName={0}): conn → {1}", ProductName, conn);
            DataTable dt = new DataTable();

			Console.WriteLine("GetProductIDFromName(ProductName={0}): dt → {1}", ProductName, dt);
            try
            {
                //SQL Query to Get id based on Name
                string sql = "SELECT id FROM tbl_products WHERE name='" + ProductName + "'";
                //Create the SQL Data Adapter to Execute the Query
				Console.WriteLine("GetProductIDFromName(ProductName={0}): sql → {1}", ProductName, sql);
                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);

				Console.WriteLine("GetProductIDFromName(ProductName={0}): adapter → {1}", ProductName, adapter);
                conn.Open();

                //Passing the CAlue from Adapter to DAtatable
                adapter.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    //Pass the value from dt to DeaCustBLL dc
                    p.id = int.Parse(dt.Rows[0]["id"].ToString());
                }
            }
            catch (Exception ex)
            {
				Console.Error.WriteLine("Exception in GetProductIDFromName(ProductName={0}): {1}", ProductName, ex.Message);
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

			Console.WriteLine("<<< Exiting GetProductIDFromName(ProductName={0})", ProductName);
            return p;
        }
        #endregion
        #region METHOD TO GET CURRENT QUantity from the Database based on Product ID
        public decimal GetProductQty(int ProductID)
        {
			Console.WriteLine(">>> Entering GetProductQty(ProductID={0})", ProductID);
            //SQl Connection First
            SqlConnection conn = new SqlConnection(myconnstrng);
            //Create a Decimal Variable and set its default value to 0
			Console.WriteLine("GetProductQty(ProductID={0}): conn → {1}", ProductID, conn);
            decimal qty = 0;

			Console.WriteLine("GetProductQty(ProductID={0}): qty → {1}", ProductID, qty);
            //Create Data Table to save the data from database temporarily
            DataTable dt = new DataTable();

			Console.WriteLine("GetProductQty(ProductID={0}): dt → {1}", ProductID, dt);
            try
            {
                //Write WQL Query to Get Quantity from Database
                string sql = "SELECT qty FROM tbl_products WHERE id = "+ProductID;

				Console.WriteLine("GetProductQty(ProductID={0}): sql → {1}", ProductID, sql);
                //Cerate A SqlCommand
                SqlCommand cmd = new SqlCommand(sql, conn);

				Console.WriteLine("GetProductQty(ProductID={0}): cmd → {1}", ProductID, cmd);
                //Create a SQL Data Adapter to Execute the query
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

				Console.WriteLine("GetProductQty(ProductID={0}): adapter → {1}", ProductID, adapter);
                //open DAtabase Connection
                conn.Open();

                //PAss the calue from Data Adapter to DataTable
                adapter.Fill(dt);

                //Lets check if the datatable has value or not
                if(dt.Rows.Count>0)
                {
                    qty = decimal.Parse(dt.Rows[0]["qty"].ToString());
                }
            }
            catch(Exception ex)
            {
				Console.Error.WriteLine("Exception in GetProductQty(ProductID={0}): {1}", ProductID, ex.Message);
                MessageBox.Show(ex.Message);
            }
            finally
            {
                //Close Database Connection
                conn.Close();
            }

			Console.WriteLine("<<< Exiting GetProductQty(ProductID={0})", ProductID);
            return qty;
        }
        #endregion
        #region METHOD TO UPDATE QUANTITY
        public bool UpdateQuantity(int ProductID, decimal Qty)
        {
			Console.WriteLine(">>> Entering UpdateQuantity(ProductID={0},Qty={1})", ProductID, Qty);
            //Create a Boolean Variable and Set its value to false
            bool success = false;

			Console.WriteLine("UpdateQuantity(ProductID={0},Qty={1}): success → {2}", ProductID, Qty, success);
            //SQl Connection to Connect Database
            SqlConnection conn = new SqlConnection(myconnstrng);

			Console.WriteLine("UpdateQuantity(ProductID={0},Qty={1}): conn → {2}", ProductID, Qty, conn);
            try
            {
                //Write the SQL Query to Update Qty
                string sql = "UPDATE tbl_products SET qty=@qty WHERE id=@id";

				Console.WriteLine("UpdateQuantity(ProductID={0},Qty={1}): sql → {2}", ProductID, Qty, sql);
                //Create SQL Command to Pass the calue into Queyr
                SqlCommand cmd = new SqlCommand(sql, conn);
                //Passing the VAlue trhough parameters
				Console.WriteLine("UpdateQuantity(ProductID={0},Qty={1}): cmd → {2}", ProductID, Qty, cmd);
                cmd.Parameters.AddWithValue("@qty", Qty);
                cmd.Parameters.AddWithValue("@id", ProductID);

                //Open Database Connection
                conn.Open();

                //Create Int Variable and Check whether the query is executed Successfully or not
                int rows = cmd.ExecuteNonQuery();
                //Lets check if the query is executed Successfully or not
				Console.WriteLine("UpdateQuantity(ProductID={0},Qty={1}): rows → {2}", ProductID, Qty, rows);
                if(rows>0)
                {
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
				Console.Error.WriteLine("Exception in UpdateQuantity(ProductID={0},Qty={1}): {2}", ProductID, Qty, ex.Message);
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

			Console.WriteLine("<<< Exiting UpdateQuantity(ProductID={0},Qty={1})", ProductID, Qty);
            return success;
        }
        #endregion
        #region METHOD TO INCREASE PRODUCT
        public bool IncreaseProduct(int ProductID, decimal IncreaseQty)
        {
			Console.WriteLine(">>> Entering IncreaseProduct(ProductID={0},IncreaseQty={1})", ProductID, IncreaseQty);
            //Create a Boolean Variable and SEt its value to False
            bool success = false;

			Console.WriteLine("IncreaseProduct(ProductID={0},IncreaseQty={1}): success → {2}", ProductID, IncreaseQty, success);
            //Create SQL Connection To Connect DAtabase
            SqlConnection conn = new SqlConnection(myconnstrng);

			Console.WriteLine("IncreaseProduct(ProductID={0},IncreaseQty={1}): conn → {2}", ProductID, IncreaseQty, conn);
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
				Console.Error.WriteLine("Exception in IncreaseProduct(ProductID={0},IncreaseQty={1}): {2}", ProductID, IncreaseQty, ex.Message);
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
			Console.WriteLine("<<< Exiting IncreaseProduct(ProductID={0},IncreaseQty={1})", ProductID, IncreaseQty);
            return success;
        }
        #endregion
        #region METHOD TO DECREASE PRODUCT
        public bool DecreaseProduct(int ProductID, decimal Qty)
        {
			Console.WriteLine(">>> Entering DecreaseProduct(ProductID={0},Qty={1})", ProductID, Qty);
            //Create Boolean Variable and SEt its Value to false
            bool success = false;

			Console.WriteLine("DecreaseProduct(ProductID={0},Qty={1}): success → {2}", ProductID, Qty, success);
            SqlConnection conn = new SqlConnection(myconnstrng);

			Console.WriteLine("DecreaseProduct(ProductID={0},Qty={1}): conn → {2}", ProductID, Qty, conn);
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
				Console.Error.WriteLine("Exception in DecreaseProduct(ProductID={0},Qty={1}): {2}", ProductID, Qty, ex.Message);
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
			Console.WriteLine("<<< Exiting DecreaseProduct(ProductID={0},Qty={1})", ProductID, Qty);
            return success;
        }
        #endregion
        #region DESPLAY PRODUCTS BASED ON CATEGORIES
        public DataTable DisplayProductsByCategory(string category)
        {
			Console.WriteLine(">>> Entering DisplayProductsByCategory(category={0})", category);
            //Sql Connection First
            SqlConnection conn = new SqlConnection(myconnstrng);

			Console.WriteLine("DisplayProductsByCategory(category={0}): conn → {1}", category, conn);
            DataTable dt = new DataTable();

			Console.WriteLine("DisplayProductsByCategory(category={0}): dt → {1}", category, dt);
            try
            {
                //SQL Query to Display Product Based on CAtegory
                string sql = "SELECT * FROM tbl_products WHERE category='"+category+"'";

				Console.WriteLine("DisplayProductsByCategory(category={0}): sql → {1}", category, sql);
                SqlCommand cmd = new SqlCommand(sql, conn);

				Console.WriteLine("DisplayProductsByCategory(category={0}): cmd → {1}", category, cmd);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

				Console.WriteLine("DisplayProductsByCategory(category={0}): adapter → {1}", category, adapter);
                //Open Database Connection Here
                conn.Open();

                adapter.Fill(dt);
            }
            catch(Exception ex)
            {
				Console.Error.WriteLine("Exception in DisplayProductsByCategory(category={0}): {1}", category, ex.Message);
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

			Console.WriteLine("<<< Exiting DisplayProductsByCategory(category={0})", category);
            return dt;
        }
        #endregion
    }
}
