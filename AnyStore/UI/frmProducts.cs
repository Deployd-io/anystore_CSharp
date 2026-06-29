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
			Console.WriteLine(">>> Entering frmProducts()");
            InitializeComponent();
			Console.WriteLine("<<< Exiting frmProducts()");
        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
			Console.WriteLine(">>> Entering pictureBoxClose_Click(sender={0},e={1})", sender, e);
            //Add code to hide this form
            this.Hide();
			Console.WriteLine("<<< Exiting pictureBoxClose_Click(sender={0},e={1})", sender, e);
        }

        categoriesDAL cdal = new categoriesDAL();
        productsBLL p = new productsBLL();
        productsDAL pdal = new productsDAL();
        userDAL udal = new userDAL();
        private void frmProducts_Load(object sender, EventArgs e)
        {
			Console.WriteLine(">>> Entering frmProducts_Load(sender={0},e={1})", sender, e);
            //Creating DAta Table to hold the categories from Database
            DataTable categoriesDT = cdal.Select();
            //Specify DataSource for Category ComboBox
			Console.WriteLine("frmProducts_Load(sender={0},e={1}): categoriesDT → {2}", sender, e, categoriesDT);
			Console.WriteLine("frmProducts_Load(sender={0},e={1}): cmbCategory.DataSource → {2}", sender, e, cmbCategory.DataSource);
            cmbCategory.DataSource = categoriesDT;
            //Specify Display Member and Value Member for Combobox
            cmbCategory.DisplayMember = "title";
			Console.WriteLine("frmProducts_Load(sender={0},e={1}): cmbCategory.DisplayMember → {2}", sender, e, cmbCategory.DisplayMember);
			Console.WriteLine("frmProducts_Load(sender={0},e={1}): cmbCategory.ValueMember → {2}", sender, e, cmbCategory.ValueMember);
            cmbCategory.ValueMember = "title";

            //Load all the Products in Data Grid View
            DataTable dt = pdal.Select();
            dgvProducts.DataSource = dt;
			Console.WriteLine("<<< Exiting frmProducts_Load(sender={0},e={1})", sender, e);
			Console.WriteLine("frmProducts_Load(sender={0},e={1}): dt → {2}", sender, e, dt);
			Console.WriteLine("frmProducts_Load(sender={0},e={1}): dgvProducts.DataSource → {2}", sender, e, dgvProducts.DataSource);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
			Console.WriteLine(">>> Entering btnAdd_Click(sender={0},e={1})", sender, e);
            //Get All the Values from Product Form
            p.name = txtName.Text;
			Console.WriteLine("btnAdd_Click(sender={0},e={1}): p.name → {2}", sender, e, p.name);
			Console.WriteLine("btnAdd_Click(sender={0},e={1}): p.category → {2}", sender, e, p.category);
			Console.WriteLine("btnAdd_Click(sender={0},e={1}): p.description → {2}", sender, e, p.description);
            p.category = cmbCategory.Text;
            p.description = txtDescription.Text;
            p.rate = decimal.Parse(txtRate.Text);
            p.qty = 0;
			Console.WriteLine("btnAdd_Click(sender={0},e={1}): p.qty → {2}", sender, e, p.qty);
			Console.WriteLine("btnAdd_Click(sender={0},e={1}): p.added_date → {2}", sender, e, p.added_date);
            p.added_date = DateTime.Now;
            //Getting username of logged in user
            String loggedUsr = frmLogin.loggedIn;
            userBLL usr = udal.GetIDFromUsername(loggedUsr);

			Console.WriteLine("btnAdd_Click(sender={0},e={1}): p.added_by → {2}", sender, e, p.added_by);
            p.added_by = usr.id;

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
                dgvProducts.DataSource = dt;
				Console.WriteLine("btnAdd_Click(sender={0},e={1}): dt → {2}", sender, e, dt);
				Console.WriteLine("btnAdd_Click(sender={0},e={1}): dgvProducts.DataSource → {2}", sender, e, dgvProducts.DataSource);
            }
            else
            {
                //Failed to Add New product
                MessageBox.Show("Failed to Add New Product");
            }
			Console.WriteLine("<<< Exiting btnAdd_Click(sender={0},e={1})", sender, e);
        }
        public void Clear()
        {
			Console.WriteLine(">>> Entering Clear()");
            txtID.Text = "";
            txtName.Text = "";
			Console.WriteLine("Clear(): txtName.Text → {0}", txtName.Text);
			Console.WriteLine("Clear(): txtDescription.Text → {0}", txtDescription.Text);
			Console.WriteLine("Clear(): txtRate.Text → {0}", txtRate.Text);
			Console.WriteLine("Clear(): txtSearch.Text → {0}", txtSearch.Text);
			Console.WriteLine("Clear(): txtID.Text → {0}", txtID.Text);
            txtDescription.Text = "";
            txtRate.Text = "";
            txtSearch.Text = "";
			Console.WriteLine("<<< Exiting Clear()");
        }

        private void dgvProducts_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
			Console.WriteLine(">>> Entering dgvProducts_RowHeaderMouseClick(sender={0},e={1})", sender, e);
            //Create integer variable to know which product was clicked
            int rowIndex = e.RowIndex;
            //Display the Value on Respective TextBoxes
            txtID.Text = dgvProducts.Rows[rowIndex].Cells[0].Value.ToString();
            txtName.Text = dgvProducts.Rows[rowIndex].Cells[1].Value.ToString();
            cmbCategory.Text = dgvProducts.Rows[rowIndex].Cells[2].Value.ToString();
            txtDescription.Text = dgvProducts.Rows[rowIndex].Cells[3].Value.ToString();
            txtRate.Text = dgvProducts.Rows[rowIndex].Cells[4].Value.ToString();

		Console.WriteLine("<<< Exiting dgvProducts_RowHeaderMouseClick(sender={0},e={1})", sender, e);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
			Console.WriteLine(">>> Entering btnUpdate_Click(sender={0},e={1})", sender, e);
            //Get the Values from UI or Product Form
            p.id = int.Parse(txtID.Text);
            p.name = txtName.Text;
			Console.WriteLine("btnUpdate_Click(sender={0},e={1}): p.name → {2}", sender, e, p.name);
			Console.WriteLine("btnUpdate_Click(sender={0},e={1}): p.category → {2}", sender, e, p.category);
			Console.WriteLine("btnUpdate_Click(sender={0},e={1}): p.description → {2}", sender, e, p.description);
            p.category = cmbCategory.Text;
            p.description = txtDescription.Text;
            p.rate = decimal.Parse(txtRate.Text);
            p.added_date = DateTime.Now;
			Console.WriteLine("btnUpdate_Click(sender={0},e={1}): p.added_date → {2}", sender, e, p.added_date);
            //Getting Username of logged in user for added by
            String loggedUsr = frmLogin.loggedIn;
            userBLL usr = udal.GetIDFromUsername(loggedUsr);

			Console.WriteLine("btnUpdate_Click(sender={0},e={1}): p.added_by → {2}", sender, e, p.added_by);
            p.added_by = usr.id;

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
                dgvProducts.DataSource = dt;
				Console.WriteLine("btnUpdate_Click(sender={0},e={1}): dt → {2}", sender, e, dt);
				Console.WriteLine("btnUpdate_Click(sender={0},e={1}): dgvProducts.DataSource → {2}", sender, e, dgvProducts.DataSource);
            }
            else
            {
                //Failed to Update Product
                MessageBox.Show("Failed to Update Product");
            }
			Console.WriteLine("<<< Exiting btnUpdate_Click(sender={0},e={1})", sender, e);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
			Console.WriteLine(">>> Entering btnDelete_Click(sender={0},e={1})", sender, e);
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
                dgvProducts.DataSource = dt;
				Console.WriteLine("btnDelete_Click(sender={0},e={1}): dt → {2}", sender, e, dt);
				Console.WriteLine("btnDelete_Click(sender={0},e={1}): dgvProducts.DataSource → {2}", sender, e, dgvProducts.DataSource);
            }
            else
            {
                //Failed to Delete Product
                MessageBox.Show("Failed to Delete Product.");
            }
			Console.WriteLine("<<< Exiting btnDelete_Click(sender={0},e={1})", sender, e);
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
			Console.WriteLine(">>> Entering txtSearch_TextChanged(sender={0},e={1})", sender, e);
            //Get the Keywordss from Form
            string keywords = txtSearch.Text;

            if(keywords!=null)
            {
                //Search the products
                DataTable dt = pdal.Search(keywords);
                dgvProducts.DataSource = dt;
				Console.WriteLine("txtSearch_TextChanged(sender={0},e={1}): dt → {2}", sender, e, dt);
				Console.WriteLine("txtSearch_TextChanged(sender={0},e={1}): dgvProducts.DataSource → {2}", sender, e, dgvProducts.DataSource);
            }
            else
            {
                //Display All the products
                DataTable dt = pdal.Select();
                dgvProducts.DataSource = dt;
            }
			Console.WriteLine("<<< Exiting txtSearch_TextChanged(sender={0},e={1})", sender, e);
        }
    }
}
