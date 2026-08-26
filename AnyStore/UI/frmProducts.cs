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

namespace AnyStore.UI
{
    public partial class frmProducts : Form
    {
        public frmProducts()
        {
			System.Diagnostics.Trace.WriteLine(">>> Entering frmProducts()");
            InitializeComponent();
			System.Diagnostics.Trace.WriteLine("<<< Exiting frmProducts()");
        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
			System.Diagnostics.Trace.WriteLine(string.Format(">>> Entering pictureBoxClose_Click(sender={0},e={1})", sender, e));
            //Add code to hide this form
            this.Hide();
			System.Diagnostics.Trace.WriteLine(string.Format("<<< Exiting pictureBoxClose_Click(sender={0},e={1})", sender, e));
        }

        categoriesDAL cdal = new categoriesDAL();
        productsBLL p = new productsBLL();
        productsDAL pdal = new productsDAL();
        userDAL udal = new userDAL();
        private void frmProducts_Load(object sender, EventArgs e)
        {
			System.Diagnostics.Trace.WriteLine(string.Format(">>> Entering frmProducts_Load(sender={0},e={1})", sender, e));
            //Creating DAta Table to hold the categories from Database
            DataTable categoriesDT = cdal.Select();
			System.Diagnostics.Trace.WriteLine(string.Format("frmProducts_Load(sender={0},e={1}): categoriesDT → {2}", sender, e, categoriesDT));
            //Specify DataSource for Category ComboBox
            cmbCategory.DataSource = categoriesDT;
			System.Diagnostics.Trace.WriteLine(string.Format("frmProducts_Load(sender={0},e={1}): cmbCategory.DataSource → {2}", sender, e, cmbCategory.DataSource));
            //Specify Display Member and Value Member for Combobox
            cmbCategory.DisplayMember = "title";
			System.Diagnostics.Trace.WriteLine(string.Format("frmProducts_Load(sender={0},e={1}): cmbCategory.DisplayMember → {2}", sender, e, cmbCategory.DisplayMember));
			System.Diagnostics.Trace.WriteLine(string.Format("frmProducts_Load(sender={0},e={1}): cmbCategory.ValueMember → {2}", sender, e, cmbCategory.ValueMember));
            cmbCategory.ValueMember = "title";

            //Load all the Products in Data Grid View
			System.Diagnostics.Trace.WriteLine(string.Format("<<< Exiting frmProducts_Load(sender={0},e={1})", sender, e));
			System.Diagnostics.Trace.WriteLine(string.Format("frmProducts_Load(sender={0},e={1}): dgvProducts.DataSource → {2}", sender, e, dgvProducts.DataSource));
            DataTable dt = pdal.Select();
			System.Diagnostics.Trace.WriteLine(string.Format("frmProducts_Load(sender={0},e={1}): dt → {2}", sender, e, dt));
            dgvProducts.DataSource = dt;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
			System.Diagnostics.Trace.WriteLine(string.Format(">>> Entering btnAdd_Click(sender={0},e={1})", sender, e));
            //Get All the Values from Product Form
            p.name = txtName.Text;
			System.Diagnostics.Trace.WriteLine(string.Format("btnAdd_Click(sender={0},e={1}): p.name → {2}", sender, e, p.name));
			System.Diagnostics.Trace.WriteLine(string.Format("btnAdd_Click(sender={0},e={1}): p.category → {2}", sender, e, p.category));
			System.Diagnostics.Trace.WriteLine(string.Format("btnAdd_Click(sender={0},e={1}): p.description → {2}", sender, e, p.description));
            p.category = cmbCategory.Text;
            p.description = txtDescription.Text;
            p.rate = decimal.Parse(txtRate.Text);
            p.qty = 0;
			System.Diagnostics.Trace.WriteLine(string.Format("btnAdd_Click(sender={0},e={1}): p.qty → {2}", sender, e, p.qty));
			System.Diagnostics.Trace.WriteLine(string.Format("btnAdd_Click(sender={0},e={1}): p.added_date → {2}", sender, e, p.added_date));
            p.added_date = DateTime.Now;
            //Getting username of logged in user
            String loggedUsr = frmLogin.loggedIn;
            userBLL usr = udal.GetIDFromUsername(loggedUsr);

            p.added_by = usr.id;
			System.Diagnostics.Trace.WriteLine(string.Format("btnAdd_Click(sender={0},e={1}): p.added_by → {2}", sender, e, p.added_by));

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
				System.Diagnostics.Trace.WriteLine(string.Format("btnAdd_Click(sender={0},e={1}): dt → {2}", sender, e, dt));
				System.Diagnostics.Trace.WriteLine(string.Format("btnAdd_Click(sender={0},e={1}): dgvProducts.DataSource → {2}", sender, e, dgvProducts.DataSource));
                dgvProducts.DataSource = dt;
            }
            else
            {
                //Failed to Add New product
                MessageBox.Show("Failed to Add New Product");
            }
			System.Diagnostics.Trace.WriteLine(string.Format("<<< Exiting btnAdd_Click(sender={0},e={1})", sender, e));
        }
        public void Clear()
        {
			System.Diagnostics.Trace.WriteLine(">>> Entering Clear()");
			System.Diagnostics.Trace.WriteLine(string.Format("Clear(): txtSearch.Text → {0}", txtSearch.Text));
			System.Diagnostics.Trace.WriteLine(string.Format("Clear(): txtName.Text → {0}", txtName.Text));
			System.Diagnostics.Trace.WriteLine(string.Format("Clear(): txtDescription.Text → {0}", txtDescription.Text));
			System.Diagnostics.Trace.WriteLine(string.Format("Clear(): txtRate.Text → {0}", txtRate.Text));
			System.Diagnostics.Trace.WriteLine(string.Format("Clear(): txtID.Text → {0}", txtID.Text));
            txtID.Text = "";
            txtName.Text = "";
            txtDescription.Text = "";
            txtRate.Text = "";
            txtSearch.Text = "";
        }

        private void dgvProducts_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
			System.Diagnostics.Trace.WriteLine(string.Format(">>> Entering dgvProducts_RowHeaderMouseClick(sender={0},e={1})", sender, e));
            //Create integer variable to know which product was clicked
            int rowIndex = e.RowIndex;
            //Display the Value on Respective TextBoxes
            txtID.Text = dgvProducts.Rows[rowIndex].Cells[0].Value.ToString();
            txtName.Text = dgvProducts.Rows[rowIndex].Cells[1].Value.ToString();
            cmbCategory.Text = dgvProducts.Rows[rowIndex].Cells[2].Value.ToString();
            txtDescription.Text = dgvProducts.Rows[rowIndex].Cells[3].Value.ToString();
			System.Diagnostics.Trace.WriteLine(string.Format("<<< Exiting dgvProducts_RowHeaderMouseClick(sender={0},e={1})", sender, e));
            txtRate.Text = dgvProducts.Rows[rowIndex].Cells[4].Value.ToString();

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
			System.Diagnostics.Trace.WriteLine(string.Format(">>> Entering btnUpdate_Click(sender={0},e={1})", sender, e));
            //Get the Values from UI or Product Form
            p.id = int.Parse(txtID.Text);
            p.name = txtName.Text;
			System.Diagnostics.Trace.WriteLine(string.Format("btnUpdate_Click(sender={0},e={1}): p.name → {2}", sender, e, p.name));
			System.Diagnostics.Trace.WriteLine(string.Format("btnUpdate_Click(sender={0},e={1}): p.category → {2}", sender, e, p.category));
			System.Diagnostics.Trace.WriteLine(string.Format("btnUpdate_Click(sender={0},e={1}): p.description → {2}", sender, e, p.description));
            p.category = cmbCategory.Text;
            p.description = txtDescription.Text;
            p.rate = decimal.Parse(txtRate.Text);
            p.added_date = DateTime.Now;
			System.Diagnostics.Trace.WriteLine(string.Format("btnUpdate_Click(sender={0},e={1}): p.added_date → {2}", sender, e, p.added_date));
            //Getting Username of logged in user for added by
            String loggedUsr = frmLogin.loggedIn;
            userBLL usr = udal.GetIDFromUsername(loggedUsr);

            p.added_by = usr.id;
			System.Diagnostics.Trace.WriteLine(string.Format("btnUpdate_Click(sender={0},e={1}): p.added_by → {2}", sender, e, p.added_by));

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
				System.Diagnostics.Trace.WriteLine(string.Format("btnUpdate_Click(sender={0},e={1}): dt → {2}", sender, e, dt));
				System.Diagnostics.Trace.WriteLine(string.Format("btnUpdate_Click(sender={0},e={1}): dgvProducts.DataSource → {2}", sender, e, dgvProducts.DataSource));
                dgvProducts.DataSource = dt;
            }
            else
            {
                //Failed to Update Product
                MessageBox.Show("Failed to Update Product");
            }
			System.Diagnostics.Trace.WriteLine(string.Format("<<< Exiting btnUpdate_Click(sender={0},e={1})", sender, e));
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
			System.Diagnostics.Trace.WriteLine(string.Format(">>> Entering btnDelete_Click(sender={0},e={1})", sender, e));
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
				System.Diagnostics.Trace.WriteLine(string.Format("btnDelete_Click(sender={0},e={1}): dt → {2}", sender, e, dt));
				System.Diagnostics.Trace.WriteLine(string.Format("btnDelete_Click(sender={0},e={1}): dgvProducts.DataSource → {2}", sender, e, dgvProducts.DataSource));
                dgvProducts.DataSource = dt;
            }
            else
            {
                //Failed to Delete Product
                MessageBox.Show("Failed to Delete Product.");
            }
			System.Diagnostics.Trace.WriteLine(string.Format("<<< Exiting btnDelete_Click(sender={0},e={1})", sender, e));
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
			System.Diagnostics.Trace.WriteLine(string.Format(">>> Entering txtSearch_TextChanged(sender={0},e={1})", sender, e));
            //Get the Keywordss from Form
            string keywords = txtSearch.Text;

            if(keywords!=null)
            {
                //Search the products
                DataTable dt = pdal.Search(keywords);
				System.Diagnostics.Trace.WriteLine(string.Format("txtSearch_TextChanged(sender={0},e={1}): dt → {2}", sender, e, dt));
				System.Diagnostics.Trace.WriteLine(string.Format("txtSearch_TextChanged(sender={0},e={1}): dgvProducts.DataSource → {2}", sender, e, dgvProducts.DataSource));
                dgvProducts.DataSource = dt;
            }
            else
            {
                //Display All the products
                DataTable dt = pdal.Select();
                dgvProducts.DataSource = dt;
            }
			System.Diagnostics.Trace.WriteLine(string.Format("<<< Exiting txtSearch_TextChanged(sender={0},e={1})", sender, e));
        }
    }
}
