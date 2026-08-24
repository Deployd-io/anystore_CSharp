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
    class productsDAL
    {
		private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
        //Creating STATI String Method for DB Connection
        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        #region Select method for Product Module
        public DataTable Select()
        {
			_logger.LogDebug(">>> Entering Select()");
            //Create Sql Connection to connect Databaes
            SqlConnection conn = new SqlConnection(myconnstrng);
			_logger.LogDebug("Select(): conn → {Conn}", conn);

            //DAtaTable to hold the data from database
            DataTable dt = new DataTable();
			_logger.LogDebug("Select(): dt → {Dt}", dt);

            try
            {
                //Writing the Query to Select all the products from database
                String sql = "SELECT * FROM tbl_products";
				_logger.LogDebug("Select(): sql → {Sql}", sql);

                //Creating SQL Command to Execute Query
                SqlCommand cmd = new SqlCommand(sql, conn);
				_logger.LogDebug("Select(): cmd → {Cmd}", cmd);

                //SQL Data Adapter to hold the value from database temporarily
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
				_logger.LogDebug("Select(): adapter → {Adapter}", adapter);

                //Open DAtabase Connection
                conn.Open();

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
        #region Method to Insert Product in database
        public bool Insert(productsBLL p)
        {
			_logger.LogDebug(">>> Entering Insert(p={P})", p);
            //Creating Boolean Variable and set its default value to false
            bool isSuccess = false;
			_logger.LogDebug("Insert(p={P}): isSuccess → {IsSuccess}", p, isSuccess);

            //Sql Connection for DAtabase
            SqlConnection conn = new SqlConnection(myconnstrng);
			_logger.LogDebug("Insert(p={P}): conn → {Conn}", p, conn);

            try
            {
                //SQL Query to insert product into database
                String sql = "INSERT INTO tbl_products (name, category, description, rate, qty, added_date, added_by) VALUES (@name, @category, @description, @rate, @qty, @added_date, @added_by)";
				_logger.LogDebug("Insert(p={P}): sql → {Sql}", p, sql);

                //Creating SQL Command to pass the values
                SqlCommand cmd = new SqlCommand(sql, conn);
				_logger.LogDebug("Insert(p={P}): cmd → {Cmd}", p, cmd);

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

				_logger.LogInformation("Insert(p)={P}: save query executed in {Arg1} ms", p, __logStart.ElapsedMilliseconds);
				_logger.LogDebug("Insert(p={P}): rows → {Rows}", p, rows);
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
				_logger.LogError("Exception in Insert(p={P}): {Arg1}", p, ex.getMessage());
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

			_logger.LogDebug("<<< Exiting Insert(p={P})", p);
            return isSuccess;
        }
        #endregion
        #region Method to Update Product in Database
        public bool Update(productsBLL p)
        {
			_logger.LogDebug(">>> Entering Update(p={P})", p);
            //create a boolean variable and set its initial value to false
            bool isSuccess = false;
			_logger.LogDebug("Update(p={P}): isSuccess → {IsSuccess}", p, isSuccess);

            //Create SQL Connection for DAtabase
            SqlConnection conn = new SqlConnection(myconnstrng);
			_logger.LogDebug("Update(p={P}): conn → {Conn}", p, conn);

            try
            {
                //SQL Query to Update Data in dAtabase
                String sql = "UPDATE tbl_products SET name=@name, category=@category, description=@description, rate=@rate, added_date=@added_date, added_by=@added_by WHERE id=@id";
				_logger.LogDebug("Update(p={P}): sql → {Sql}", p, sql);

                //Create SQL Cmmand to pass the value to query
                SqlCommand cmd = new SqlCommand(sql, conn);
				_logger.LogDebug("Update(p={P}): cmd → {Cmd}", p, cmd);
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

				_logger.LogInformation("Update(p)={P}: update query executed in {Arg1} ms", p, __logStart.ElapsedMilliseconds);
				_logger.LogDebug("Update(p={P}): rows → {Rows}", p, rows);
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
				_logger.LogError("Exception in Update(p={P}): {Arg1}", p, ex.getMessage());
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

			_logger.LogDebug("<<< Exiting Update(p={P})", p);
            return isSuccess;
        }
        #endregion
        #region Method to Delete Product from Database
        public bool Delete(productsBLL p)
        {
			_logger.LogDebug(">>> Entering Delete(p={P})", p);
            //Create Boolean Variable and Set its default value to false
            bool isSuccess = false;
			_logger.LogDebug("Delete(p={P}): isSuccess → {IsSuccess}", p, isSuccess);

            //SQL Connection for DB connection
            SqlConnection conn = new SqlConnection(myconnstrng);
			_logger.LogDebug("Delete(p={P}): conn → {Conn}", p, conn);

            try
            {
                //Write Query Product from DAtabase
                String sql = "DELETE FROM tbl_products WHERE id=@id";
				_logger.LogDebug("Delete(p={P}): sql → {Sql}", p, sql);

                //Sql Command to Pass the Value
                SqlCommand cmd = new SqlCommand(sql, conn);
				_logger.LogDebug("Delete(p={P}): cmd → {Cmd}", p, cmd);

                //Passing the values using cmd
                cmd.Parameters.AddWithValue("@id", p.id);

                //Open Database Connection
                conn.Open();

				var __logStart = System.Diagnostics.Stopwatch.StartNew();
                int rows = cmd.ExecuteNonQuery();
                //If the query is executed successfullly then the value of rows will be greated than 0 else it will be less than 0
				_logger.LogInformation("Delete(p)={P}: delete query executed in {Arg1} ms", p, __logStart.ElapsedMilliseconds);
				_logger.LogDebug("Delete(p={P}): rows → {Rows}", p, rows);
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
				_logger.LogError("Exception in Delete(p={P}): {Arg1}", p, ex.getMessage());
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

			_logger.LogDebug("<<< Exiting Delete(p={P})", p);
            return isSuccess;
        }
        #endregion
        #region SEARCH Method for Product Module
        public DataTable Search (string keywords)
        {
			_logger.LogDebug(">>> Entering Search(keywords={Keywords})", keywords);
            //SQL Connection fro DB Connection
            SqlConnection conn = new SqlConnection(myconnstrng);
			_logger.LogDebug("Search(keywords={Keywords}): conn → {Conn}", keywords, conn);
            //Creating DAtaTable to hold value from dAtabase
            DataTable dt = new DataTable();
			_logger.LogDebug("Search(keywords={Keywords}): dt → {Dt}", keywords, dt);

            try
            {
                //SQL query to search product
                string sql = "SELECT * FROM tbl_products WHERE id LIKE '%"+keywords+"%' OR name LIKE '%"+keywords+"%' OR category LIKE '%"+keywords+"%'";
				_logger.LogDebug("Search(keywords={Keywords}): sql → {Sql}", keywords, sql);
                //Sql Command to execute Query
                SqlCommand cmd = new SqlCommand(sql, conn);
				_logger.LogDebug("Search(keywords={Keywords}): cmd → {Cmd}", keywords, cmd);

                //SQL Data Adapter to hold the data from database temporarily
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
				_logger.LogDebug("Search(keywords={Keywords}): adapter → {Adapter}", keywords, adapter);

                //Open Database Connection
                conn.Open();

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
        #region METHOD TO SEARCH PRODUCT IN TRANSACTION MODULE
        public productsBLL GetProductsForTransaction(string keyword)
        {
			_logger.LogDebug(">>> Entering GetProductsForTransaction(keyword={Keyword})", keyword);
            //Create an object of productsBLL and return it
            productsBLL p = new productsBLL();
            //SqlConnection
            SqlConnection conn = new SqlConnection(myconnstrng);
			_logger.LogDebug("GetProductsForTransaction(keyword={Keyword}): conn → {Conn}", keyword, conn);
            //Datatable to store data temporarily
            DataTable dt = new DataTable();
			_logger.LogDebug("GetProductsForTransaction(keyword={Keyword}): dt → {Dt}", keyword, dt);

            try
            {
                //Write the Query to Get the detaisl
                string sql = "SELECT name, rate, qty FROM tbl_products WHERE id LIKE '%"+keyword+"%' OR name LIKE '%"+keyword+"%'";
				_logger.LogDebug("GetProductsForTransaction(keyword={Keyword}): sql → {Sql}", keyword, sql);
                //Create Sql Data Adapter to Execute the query
                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
				_logger.LogDebug("GetProductsForTransaction(keyword={Keyword}): adapter → {Adapter}", keyword, adapter);

                //Open DAtabase Connection
                conn.Open();

                //Pass the value from adapter to dt
				var __logStart = System.Diagnostics.Stopwatch.StartNew();
				_logger.LogInformation("GetProductsForTransaction(keyword)={Keyword}: find query executed in {Arg1} ms", keyword, __logStart.ElapsedMilliseconds);
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
				_logger.LogError("Exception in GetProductsForTransaction(keyword={Keyword}): {Arg1}", keyword, ex.getMessage());
                MessageBox.Show(ex.Message);
            }
            finally
            {
                //Close Database Connection
                conn.Close();
            }

			_logger.LogDebug("<<< Exiting GetProductsForTransaction(keyword={Keyword})", keyword);
            return p;
        }
        #endregion
        #region METHOD TO GET PRODUCT ID BASED ON PRODUCT NAME
        public productsBLL GetProductIDFromName(string ProductName)
        {
			_logger.LogDebug(">>> Entering GetProductIDFromName(ProductName={ProductName})", ProductName);
            //First Create an Object of DeaCust BLL and REturn it
            productsBLL p = new productsBLL();

            //SQL Conection here
            SqlConnection conn = new SqlConnection(myconnstrng);
			_logger.LogDebug("GetProductIDFromName(ProductName={ProductName}): conn → {Conn}", ProductName, conn);
            //Data TAble to Holdthe data temporarily
            DataTable dt = new DataTable();
			_logger.LogDebug("GetProductIDFromName(ProductName={ProductName}): dt → {Dt}", ProductName, dt);

            try
            {
                //SQL Query to Get id based on Name
                string sql = "SELECT id FROM tbl_products WHERE name='" + ProductName + "'";
				_logger.LogDebug("GetProductIDFromName(ProductName={ProductName}): sql → {Sql}", ProductName, sql);
                //Create the SQL Data Adapter to Execute the Query
                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
				_logger.LogDebug("GetProductIDFromName(ProductName={ProductName}): adapter → {Adapter}", ProductName, adapter);

                conn.Open();

                //Passing the CAlue from Adapter to DAtatable
				var __logStart = System.Diagnostics.Stopwatch.StartNew();
				_logger.LogInformation("GetProductIDFromName(ProductName)={ProductName}: find query executed in {Arg1} ms", ProductName, __logStart.ElapsedMilliseconds);
                adapter.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    //Pass the value from dt to DeaCustBLL dc
                    p.id = int.Parse(dt.Rows[0]["id"].ToString());
                }
            }
            catch (Exception ex)
            {
				_logger.LogError("Exception in GetProductIDFromName(ProductName={ProductName}): {Arg1}", ProductName, ex.getMessage());
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

			_logger.LogDebug("<<< Exiting GetProductIDFromName(ProductName={ProductName})", ProductName);
            return p;
        }
        #endregion
        #region METHOD TO GET CURRENT QUantity from the Database based on Product ID
        public decimal GetProductQty(int ProductID)
        {
			_logger.LogDebug(">>> Entering GetProductQty(ProductID={ProductID})", ProductID);
            //SQl Connection First
            SqlConnection conn = new SqlConnection(myconnstrng);
			_logger.LogDebug("GetProductQty(ProductID={ProductID}): conn → {Conn}", ProductID, conn);
            //Create a Decimal Variable and set its default value to 0
            decimal qty = 0;
			_logger.LogDebug("GetProductQty(ProductID={ProductID}): qty → {Qty}", ProductID, qty);

            //Create Data Table to save the data from database temporarily
            DataTable dt = new DataTable();
			_logger.LogDebug("GetProductQty(ProductID={ProductID}): dt → {Dt}", ProductID, dt);

            try
            {
                //Write WQL Query to Get Quantity from Database
                string sql = "SELECT qty FROM tbl_products WHERE id = "+ProductID;
				_logger.LogDebug("GetProductQty(ProductID={ProductID}): sql → {Sql}", ProductID, sql);

                //Cerate A SqlCommand
                SqlCommand cmd = new SqlCommand(sql, conn);
				_logger.LogDebug("GetProductQty(ProductID={ProductID}): cmd → {Cmd}", ProductID, cmd);

                //Create a SQL Data Adapter to Execute the query
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
				_logger.LogDebug("GetProductQty(ProductID={ProductID}): adapter → {Adapter}", ProductID, adapter);

                //open DAtabase Connection
                conn.Open();

                //PAss the calue from Data Adapter to DataTable
				var __logStart = System.Diagnostics.Stopwatch.StartNew();
				_logger.LogInformation("GetProductQty(ProductID)={ProductID}: find query executed in {Arg1} ms", ProductID, __logStart.ElapsedMilliseconds);
                adapter.Fill(dt);

                //Lets check if the datatable has value or not
                if(dt.Rows.Count>0)
                {
                    qty = decimal.Parse(dt.Rows[0]["qty"].ToString());
                }
            }
            catch(Exception ex)
            {
				_logger.LogError("Exception in GetProductQty(ProductID={ProductID}): {Arg1}", ProductID, ex.getMessage());
                MessageBox.Show(ex.Message);
            }
            finally
            {
                //Close Database Connection
                conn.Close();
            }

			_logger.LogDebug("<<< Exiting GetProductQty(ProductID={ProductID})", ProductID);
            return qty;
        }
        #endregion
        #region METHOD TO UPDATE QUANTITY
        public bool UpdateQuantity(int ProductID, decimal Qty)
        {
			_logger.LogDebug(">>> Entering UpdateQuantity(ProductID={ProductID},Qty={Qty})", ProductID, Qty);
            //Create a Boolean Variable and Set its value to false
            bool success = false;
			_logger.LogDebug("UpdateQuantity(ProductID={ProductID},Qty={Qty}): success → {Success}", ProductID, Qty, success);

            //SQl Connection to Connect Database
            SqlConnection conn = new SqlConnection(myconnstrng);
			_logger.LogDebug("UpdateQuantity(ProductID={ProductID},Qty={Qty}): conn → {Conn}", ProductID, Qty, conn);

            try
            {
                //Write the SQL Query to Update Qty
                string sql = "UPDATE tbl_products SET qty=@qty WHERE id=@id";
				_logger.LogDebug("UpdateQuantity(ProductID={ProductID},Qty={Qty}): sql → {Sql}", ProductID, Qty, sql);

                //Create SQL Command to Pass the calue into Queyr
                SqlCommand cmd = new SqlCommand(sql, conn);
				_logger.LogDebug("UpdateQuantity(ProductID={ProductID},Qty={Qty}): cmd → {Cmd}", ProductID, Qty, cmd);
                //Passing the VAlue trhough parameters
                cmd.Parameters.AddWithValue("@qty", Qty);
                cmd.Parameters.AddWithValue("@id", ProductID);

                //Open Database Connection
                conn.Open();

                //Create Int Variable and Check whether the query is executed Successfully or not
				var __logStart = System.Diagnostics.Stopwatch.StartNew();
                int rows = cmd.ExecuteNonQuery();
                //Lets check if the query is executed Successfully or not
				_logger.LogInformation("UpdateQuantity(ProductID,Qty)={ProductID},{Qty}: update query executed in {Arg2} ms", ProductID, Qty, __logStart.ElapsedMilliseconds);
				_logger.LogDebug("UpdateQuantity(ProductID={ProductID},Qty={Qty}): rows → {Rows}", ProductID, Qty, rows);
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
				_logger.LogError("Exception in UpdateQuantity(ProductID={ProductID},Qty={Qty}): {Arg2}", ProductID, Qty, ex.getMessage());
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

			_logger.LogDebug("<<< Exiting UpdateQuantity(ProductID={ProductID},Qty={Qty})", ProductID, Qty);
            return success;
        }
        #endregion
        #region METHOD TO INCREASE PRODUCT
        public bool IncreaseProduct(int ProductID, decimal IncreaseQty)
        {
			_logger.LogDebug(">>> Entering IncreaseProduct(ProductID={ProductID},IncreaseQty={IncreaseQty})", ProductID, IncreaseQty);
            //Create a Boolean Variable and SEt its value to False
            bool success = false;
			_logger.LogDebug("IncreaseProduct(ProductID={ProductID},IncreaseQty={IncreaseQty}): success → {Success}", ProductID, IncreaseQty, success);

            //Create SQL Connection To Connect DAtabase
            SqlConnection conn = new SqlConnection(myconnstrng);
			_logger.LogDebug("IncreaseProduct(ProductID={ProductID},IncreaseQty={IncreaseQty}): conn → {Conn}", ProductID, IncreaseQty, conn);

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
				_logger.LogError("Exception in IncreaseProduct(ProductID={ProductID},IncreaseQty={IncreaseQty}): {Arg2}", ProductID, IncreaseQty, ex.getMessage());
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
			_logger.LogDebug("<<< Exiting IncreaseProduct(ProductID={ProductID},IncreaseQty={IncreaseQty})", ProductID, IncreaseQty);
            return success;
        }
        #endregion
        #region METHOD TO DECREASE PRODUCT
        public bool DecreaseProduct(int ProductID, decimal Qty)
        {
			_logger.LogDebug(">>> Entering DecreaseProduct(ProductID={ProductID},Qty={Qty})", ProductID, Qty);
            //Create Boolean Variable and SEt its Value to false
            bool success = false;
			_logger.LogDebug("DecreaseProduct(ProductID={ProductID},Qty={Qty}): success → {Success}", ProductID, Qty, success);

            SqlConnection conn = new SqlConnection(myconnstrng);
			_logger.LogDebug("DecreaseProduct(ProductID={ProductID},Qty={Qty}): conn → {Conn}", ProductID, Qty, conn);

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
				_logger.LogError("Exception in DecreaseProduct(ProductID={ProductID},Qty={Qty}): {Arg2}", ProductID, Qty, ex.getMessage());
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
			_logger.LogDebug("<<< Exiting DecreaseProduct(ProductID={ProductID},Qty={Qty})", ProductID, Qty);
            return success;
        }
        #endregion
        #region DESPLAY PRODUCTS BASED ON CATEGORIES
        public DataTable DisplayProductsByCategory(string category)
        {
			_logger.LogDebug(">>> Entering DisplayProductsByCategory(category={Category})", category);
            //Sql Connection First
            SqlConnection conn = new SqlConnection(myconnstrng);
			_logger.LogDebug("DisplayProductsByCategory(category={Category}): conn → {Conn}", category, conn);

            DataTable dt = new DataTable();
			_logger.LogDebug("DisplayProductsByCategory(category={Category}): dt → {Dt}", category, dt);

            try
            {
                //SQL Query to Display Product Based on CAtegory
                string sql = "SELECT * FROM tbl_products WHERE category='"+category+"'";
				_logger.LogDebug("DisplayProductsByCategory(category={Category}): sql → {Sql}", category, sql);

                SqlCommand cmd = new SqlCommand(sql, conn);
				_logger.LogDebug("DisplayProductsByCategory(category={Category}): cmd → {Cmd}", category, cmd);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
				_logger.LogDebug("DisplayProductsByCategory(category={Category}): adapter → {Adapter}", category, adapter);

                //Open Database Connection Here
                conn.Open();

				var __logStart = System.Diagnostics.Stopwatch.StartNew();
				_logger.LogInformation("DisplayProductsByCategory(category)={Category}: find query executed in {Arg1} ms", category, __logStart.ElapsedMilliseconds);
                adapter.Fill(dt);
            }
            catch(Exception ex)
            {
				_logger.LogError("Exception in DisplayProductsByCategory(category={Category}): {Arg1}", category, ex.getMessage());
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

			_logger.LogDebug("<<< Exiting DisplayProductsByCategory(category={Category})", category);
            return dt;
        }
        #endregion
    }
}
