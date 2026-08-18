using AnyStore.BLL;
using AnyStore.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Extensions.Logging;

namespace AnyStore.UI
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    public partial class frmProducts : Form
    {
        public frmProducts()
        {
			_logger.LogDebug(">>> Entering frmProducts()");
            InitializeComponent();
			_logger.LogDebug("<<< Exiting frmProducts()");
        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
			_logger.LogDebug(">>> Entering pictureBoxClose_Click(sender={Sender},e={E})", sender, e);
            //Add code to hide this form
            this.Hide();
			_logger.LogDebug("<<< Exiting pictureBoxClose_Click(sender={Sender},e={E})", sender, e);
        }

        categoriesDAL cdal = new categoriesDAL();
        productsBLL p = new productsBLL();
        productsDAL pdal = new productsDAL();
        userDAL udal = new userDAL();
        private void frmProducts_Load(object sender, EventArgs e)
        {
			_logger.LogDebug(">>> Entering frmProducts_Load(sender={Sender},e={E})", sender, e);
            //Creating DAta Table to hold the categories from Database
            DataTable categoriesDT = cdal.Select();
			_logger.LogDebug("frmProducts_Load(sender={Sender},e={E}): categoriesDT → {CategoriesDT}", sender, e, categoriesDT);
            //Specify DataSource for Category ComboBox
            cmbCategory.DataSource = categoriesDT;
			_logger.LogDebug("frmProducts_Load(sender={Sender},e={E}): cmbCategory.DataSource → {Arg2}", sender, e, cmbCategory.DataSource);
            //Specify Display Member and Value Member for Combobox
            cmbCategory.DisplayMember = "title";
			_logger.LogDebug("frmProducts_Load(sender={Sender},e={E}): cmbCategory.DisplayMember → {Arg2}", sender, e, cmbCategory.DisplayMember);
			_logger.LogDebug("frmProducts_Load(sender={Sender},e={E}): cmbCategory.ValueMember → {Arg2}", sender, e, cmbCategory.ValueMember);
            cmbCategory.ValueMember = "title";

            //Load all the Products in Data Grid View
            DataTable dt = pdal.Select();
			_logger.LogDebug("frmProducts_Load(sender={Sender},e={E}): dt → {Dt}", sender, e, dt);
			_logger.LogDebug("frmProducts_Load(sender={Sender},e={E}): dgvProducts.DataSource → {Arg2}", sender, e, dgvProducts.DataSource);
            dgvProducts.DataSource = dt;
			_logger.LogDebug("<<< Exiting frmProducts_Load(sender={Sender},e={E})", sender, e);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
			_logger.LogDebug(">>> Entering btnAdd_Click(sender={Sender},e={E})", sender, e);
            //Get All the Values from Product Form
            p.name = txtName.Text;
			_logger.LogDebug("btnAdd_Click(sender={Sender},e={E}): p.name → {Arg2}", sender, e, p.name);
			_logger.LogDebug("btnAdd_Click(sender={Sender},e={E}): p.category → {Arg2}", sender, e, p.category);
			_logger.LogDebug("btnAdd_Click(sender={Sender},e={E}): p.description → {Arg2}", sender, e, p.description);
            p.category = cmbCategory.Text;
            p.description = txtDescription.Text;
            p.rate = decimal.Parse(txtRate.Text);
            p.qty = 0;
			_logger.LogDebug("btnAdd_Click(sender={Sender},e={E}): p.qty → {Arg2}", sender, e, p.qty);
			_logger.LogDebug("btnAdd_Click(sender={Sender},e={E}): p.added_date → {Arg2}", sender, e, p.added_date);
            p.added_date = DateTime.Now;
            //Getting username of logged in user
            String loggedUsr = frmLogin.loggedIn;
            userBLL usr = udal.GetIDFromUsername(loggedUsr);

            p.added_by = usr.id;
			_logger.LogDebug("btnAdd_Click(sender={Sender},e={E}): p.added_by → {Arg2}", sender, e, p.added_by);

            //Create Boolean variable to check if the product is added successfully or not
            bool success = pdal.Insert(p);
            //if the product is added successfully then the value of success will be true else it will be false
            if(success==true)
            {
                //Product Inserted Successfully
                MessageBox.Show("Product Added Successfully");
                //Calling the Clear Method
                Clear();
                //Refreshing DAta Grid View
                DataTable dt = pdal.Select();
				_logger.LogDebug("btnAdd_Click(sender={Sender},e={E}): dt → {Dt}", sender, e, dt);
				_logger.LogDebug("btnAdd_Click(sender={Sender},e={E}): dgvProducts.DataSource → {Arg2}", sender, e, dgvProducts.DataSource);
                dgvProducts.DataSource = dt;
            }
            else
            {
                //Failed to Add New product
                MessageBox.Show("Failed to Add New Product");
            }
			_logger.LogDebug("<<< Exiting btnAdd_Click(sender={Sender},e={E})", sender, e);
        }
        public void Clear()
        {
			_logger.LogDebug(">>> Entering Clear()");
            txtID.Text = "";
            txtName.Text = "";
			_logger.LogDebug("Clear(): txtName.Text → {Arg0}", txtName.Text);
			_logger.LogDebug("Clear(): txtDescription.Text → {Arg0}", txtDescription.Text);
			_logger.LogDebug("Clear(): txtRate.Text → {Arg0}", txtRate.Text);
			_logger.LogDebug("Clear(): txtSearch.Text → {Arg0}", txtSearch.Text);
			_logger.LogDebug("Clear(): txtID.Text → {Arg0}", txtID.Text);
            txtDescription.Text = "";
            txtRate.Text = "";
            txtSearch.Text = "";
			_logger.LogDebug("<<< Exiting Clear()");
        }

        private void dgvProducts_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
			_logger.LogDebug(">>> Entering dgvProducts_RowHeaderMouseClick(sender={Sender},e={E})", sender, e);
            //Create integer variable to know which product was clicked
            int rowIndex = e.RowIndex;
            //Display the Value on Respective TextBoxes
            txtID.Text = dgvProducts.Rows[rowIndex].Cells[0].Value.ToString();
            txtName.Text = dgvProducts.Rows[rowIndex].Cells[1].Value.ToString();
            cmbCategory.Text = dgvProducts.Rows[rowIndex].Cells[2].Value.ToString();
            txtDescription.Text = dgvProducts.Rows[rowIndex].Cells[3].Value.ToString();
			_logger.LogDebug("<<< Exiting dgvProducts_RowHeaderMouseClick(sender={Sender},e={E})", sender, e);
            txtRate.Text = dgvProducts.Rows[rowIndex].Cells[4].Value.ToString();

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
			_logger.LogDebug(">>> Entering btnUpdate_Click(sender={Sender},e={E})", sender, e);
            //Get the Values from UI or Product Form
            p.id = int.Parse(txtID.Text);
            p.name = txtName.Text;
			_logger.LogDebug("btnUpdate_Click(sender={Sender},e={E}): p.name → {Arg2}", sender, e, p.name);
			_logger.LogDebug("btnUpdate_Click(sender={Sender},e={E}): p.category → {Arg2}", sender, e, p.category);
			_logger.LogDebug("btnUpdate_Click(sender={Sender},e={E}): p.description → {Arg2}", sender, e, p.description);
            p.category = cmbCategory.Text;
            p.description = txtDescription.Text;
            p.rate = decimal.Parse(txtRate.Text);
            p.added_date = DateTime.Now;
			_logger.LogDebug("btnUpdate_Click(sender={Sender},e={E}): p.added_date → {Arg2}", sender, e, p.added_date);
            //Getting Username of logged in user for added by
            String loggedUsr = frmLogin.loggedIn;
            userBLL usr = udal.GetIDFromUsername(loggedUsr);

            p.added_by = usr.id;
			_logger.LogDebug("btnUpdate_Click(sender={Sender},e={E}): p.added_by → {Arg2}", sender, e, p.added_by);

            //Create a boolean variable to check if the product is updated or not
            bool success = pdal.Update(p);
            //If the prouct is updated successfully then the value of success will be true else it will be false
            if(success==true)
            {
                //Product updated Successfully
                MessageBox.Show("Product Successfully Updated");
                Clear();
                //REfresh the Data Grid View
                DataTable dt = pdal.Select();
				_logger.LogDebug("btnUpdate_Click(sender={Sender},e={E}): dt → {Dt}", sender, e, dt);
				_logger.LogDebug("btnUpdate_Click(sender={Sender},e={E}): dgvProducts.DataSource → {Arg2}", sender, e, dgvProducts.DataSource);
                dgvProducts.DataSource = dt;
            }
            else
            {
                //Failed to Update Product
                MessageBox.Show("Failed to Update Product");
            }
			_logger.LogDebug("<<< Exiting btnUpdate_Click(sender={Sender},e={E})", sender, e);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
			_logger.LogDebug(">>> Entering btnDelete_Click(sender={Sender},e={E})", sender, e);
            //GEt the ID of the product to be deleted
            p.id = int.Parse(txtID.Text);

            //Create Bool VAriable to Check if the product is deleted or not
            bool success = pdal.Delete(p);

            //If prouct is deleted successfully then the value of success will true else it will be false
            if(success==true)
            {
                //Product Successfuly Deleted
                MessageBox.Show("Product successfully deleted.");
                Clear();
                //Refresh DAta Grid View
                DataTable dt = pdal.Select();
				_logger.LogDebug("btnDelete_Click(sender={Sender},e={E}): dt → {Dt}", sender, e, dt);
				_logger.LogDebug("btnDelete_Click(sender={Sender},e={E}): dgvProducts.DataSource → {Arg2}", sender, e, dgvProducts.DataSource);
                dgvProducts.DataSource = dt;
            }
            else
            {
                //Failed to Delete Product
                MessageBox.Show("Failed to Delete Product.");
            }
			_logger.LogDebug("<<< Exiting btnDelete_Click(sender={Sender},e={E})", sender, e);
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
			_logger.LogDebug(">>> Entering txtSearch_TextChanged(sender={Sender},e={E})", sender, e);
            //Get the Keywordss from Form
            string keywords = txtSearch.Text;

            if(keywords!=null)
            {
                //Search the products
                DataTable dt = pdal.Search(keywords);
				_logger.LogDebug("txtSearch_TextChanged(sender={Sender},e={E}): dt → {Dt}", sender, e, dt);
				_logger.LogDebug("txtSearch_TextChanged(sender={Sender},e={E}): dgvProducts.DataSource → {Arg2}", sender, e, dgvProducts.DataSource);
                dgvProducts.DataSource = dt;
            }
            else
            {
                //Display All the products
                DataTable dt = pdal.Select();
                dgvProducts.DataSource = dt;
            }
			_logger.LogDebug("<<< Exiting txtSearch_TextChanged(sender={Sender},e={E})", sender, e);
        }
    }
}
