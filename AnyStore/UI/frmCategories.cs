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
    public partial class frmCategories : Form
    {
        public frmCategories()
        {
			System.Diagnostics.Trace.WriteLine(">>> Entering frmCategories()");
            InitializeComponent();
			System.Diagnostics.Trace.WriteLine("<<< Exiting frmCategories()");
        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
			System.Diagnostics.Trace.WriteLine(string.Format(">>> Entering pictureBoxClose_Click(sender={0},e={1})", sender, e));
            this.Hide();
			System.Diagnostics.Trace.WriteLine(string.Format("<<< Exiting pictureBoxClose_Click(sender={0},e={1})", sender, e));
        }

        categoriesBLL c = new categoriesBLL();
        categoriesDAL dal = new categoriesDAL();
        userDAL udal = new userDAL();

        private void btnADD_Click(object sender, EventArgs e)
        {
			System.Diagnostics.Trace.WriteLine(string.Format(">>> Entering btnADD_Click(sender={0},e={1})", sender, e));
            //Get the values from Categroy Form
            c.title = txtTitle.Text;
			System.Diagnostics.Trace.WriteLine(string.Format("btnADD_Click(sender={0},e={1}): c.title → {2}", sender, e, c.title));
			System.Diagnostics.Trace.WriteLine(string.Format("btnADD_Click(sender={0},e={1}): c.description → {2}", sender, e, c.description));
			System.Diagnostics.Trace.WriteLine(string.Format("btnADD_Click(sender={0},e={1}): c.added_date → {2}", sender, e, c.added_date));
            c.description = txtDescription.Text;
            c.added_date = DateTime.Now;

            //Getting ID in Added by field
            string loggedUser = frmLogin.loggedIn;
            userBLL usr = udal.GetIDFromUsername(loggedUser);
            //Passign the id of Logged in User in added by field
            c.added_by = usr.id;
			System.Diagnostics.Trace.WriteLine(string.Format("btnADD_Click(sender={0},e={1}): c.added_by → {2}", sender, e, c.added_by));

            //Creating Boolean Method To insert data into database
            bool success = dal.Insert(c);

            //If the category is inserted successfully then the value of the success will be true else it will be false
            if(success==true)
            {
                //NewCAtegory Inserted Successfully
                MessageBox.Show("New Category Inserted Successfully.");
                Clear();
                //Refresh Data Grid View
                DataTable dt = dal.Select();
				System.Diagnostics.Trace.WriteLine(string.Format("btnADD_Click(sender={0},e={1}): dt → {2}", sender, e, dt));
				System.Diagnostics.Trace.WriteLine(string.Format("btnADD_Click(sender={0},e={1}): dgvCategories.DataSource → {2}", sender, e, dgvCategories.DataSource));
                dgvCategories.DataSource = dt;
            }
            else
            {
                //FAiled to Insert New Category
                MessageBox.Show("Failed to Insert New CAtegory.");
            }
			System.Diagnostics.Trace.WriteLine(string.Format("<<< Exiting btnADD_Click(sender={0},e={1})", sender, e));
        }
        public void Clear()
        {
			System.Diagnostics.Trace.WriteLine(">>> Entering Clear()");
			System.Diagnostics.Trace.WriteLine(string.Format("Clear(): txtSearch.Text → {0}", txtSearch.Text));
			System.Diagnostics.Trace.WriteLine(string.Format("Clear(): txtTitle.Text → {0}", txtTitle.Text));
			System.Diagnostics.Trace.WriteLine(string.Format("Clear(): txtDescription.Text → {0}", txtDescription.Text));
			System.Diagnostics.Trace.WriteLine(string.Format("Clear(): txtCategoryID.Text → {0}", txtCategoryID.Text));
            txtCategoryID.Text = "";
            txtTitle.Text = "";
            txtDescription.Text = "";
            txtSearch.Text = "";
        }

        private void frmCategories_Load(object sender, EventArgs e)
        {
			System.Diagnostics.Trace.WriteLine(string.Format(">>> Entering frmCategories_Load(sender={0},e={1})", sender, e));
			System.Diagnostics.Trace.WriteLine(string.Format("frmCategories_Load(sender={0},e={1}): dgvCategories.DataSource → {2}", sender, e, dgvCategories.DataSource));
            //Here write the code to display all the categries in DAta Grid View
			System.Diagnostics.Trace.WriteLine(string.Format("<<< Exiting frmCategories_Load(sender={0},e={1})", sender, e));
            DataTable dt = dal.Select();
			System.Diagnostics.Trace.WriteLine(string.Format("frmCategories_Load(sender={0},e={1}): dt → {2}", sender, e, dt));
            dgvCategories.DataSource = dt;
        }

        private void dgvCategories_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
			System.Diagnostics.Trace.WriteLine(string.Format(">>> Entering dgvCategories_RowHeaderMouseClick(sender={0},e={1})", sender, e));
            //Finding the Row Index of the Row Clicked on Data Grid View
            int RowIndex = e.RowIndex;
            txtCategoryID.Text = dgvCategories.Rows[RowIndex].Cells[0].Value.ToString();
            txtTitle.Text = dgvCategories.Rows[RowIndex].Cells[1].Value.ToString();
            txtDescription.Text = dgvCategories.Rows[RowIndex].Cells[2].Value.ToString();
			System.Diagnostics.Trace.WriteLine(string.Format("<<< Exiting dgvCategories_RowHeaderMouseClick(sender={0},e={1})", sender, e));
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
			System.Diagnostics.Trace.WriteLine(string.Format(">>> Entering btnUpdate_Click(sender={0},e={1})", sender, e));
            //Get the Values from the CAtegory form
            c.id = int.Parse(txtCategoryID.Text);
            c.title = txtTitle.Text;
			System.Diagnostics.Trace.WriteLine(string.Format("btnUpdate_Click(sender={0},e={1}): c.title → {2}", sender, e, c.title));
			System.Diagnostics.Trace.WriteLine(string.Format("btnUpdate_Click(sender={0},e={1}): c.description → {2}", sender, e, c.description));
			System.Diagnostics.Trace.WriteLine(string.Format("btnUpdate_Click(sender={0},e={1}): c.added_date → {2}", sender, e, c.added_date));
            c.description = txtDescription.Text;
            c.added_date = DateTime.Now;
            //Getting ID in Added by field
            string loggedUser = frmLogin.loggedIn;
            userBLL usr = udal.GetIDFromUsername(loggedUser);
            //Passign the id of Logged in User in added by field
            c.added_by = usr.id;
			System.Diagnostics.Trace.WriteLine(string.Format("btnUpdate_Click(sender={0},e={1}): c.added_by → {2}", sender, e, c.added_by));

            //Creating Boolean variable to update categories and check 
            bool success = dal.Update(c);
            //If the cateory is updated successfully then the value of success will be true else it will be false
            if(success==true)
            {
                //CAtegory updated Successfully 
                MessageBox.Show("Category Updated Successfully");
                Clear();
                //Refresh Data Gid View
                DataTable dt = dal.Select();
				System.Diagnostics.Trace.WriteLine(string.Format("btnUpdate_Click(sender={0},e={1}): dt → {2}", sender, e, dt));
				System.Diagnostics.Trace.WriteLine(string.Format("btnUpdate_Click(sender={0},e={1}): dgvCategories.DataSource → {2}", sender, e, dgvCategories.DataSource));
                dgvCategories.DataSource = dt;
            }
            else
            {
                //FAiled to Update Category
                MessageBox.Show("Failed to Update Category");
            }
			System.Diagnostics.Trace.WriteLine(string.Format("<<< Exiting btnUpdate_Click(sender={0},e={1})", sender, e));
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
			System.Diagnostics.Trace.WriteLine(string.Format(">>> Entering btnDelete_Click(sender={0},e={1})", sender, e));
            //Get te ID of the Category Which we want to Delete
            c.id = int.Parse(txtCategoryID.Text);

            //Creating Boolean Variable to Delete The CAtegory
            bool success = dal.Delete(c);

            //If the CAtegory id Deleted Successfully then the vaue of success will be true else it will be false
            if(success==true)
            {
                //Category Deleted Successfully
                MessageBox.Show("Category Deleted Successfully");
                Clear();
                //REfreshing DAta Grid View
                DataTable dt = dal.Select();
				System.Diagnostics.Trace.WriteLine(string.Format("btnDelete_Click(sender={0},e={1}): dt → {2}", sender, e, dt));
				System.Diagnostics.Trace.WriteLine(string.Format("btnDelete_Click(sender={0},e={1}): dgvCategories.DataSource → {2}", sender, e, dgvCategories.DataSource));
                dgvCategories.DataSource = dt;
            }
            else
            {
                //FAiled to Delete CAtegory 
                MessageBox.Show("Failed to Delete CAtegory");
            }
			System.Diagnostics.Trace.WriteLine(string.Format("<<< Exiting btnDelete_Click(sender={0},e={1})", sender, e));
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
			System.Diagnostics.Trace.WriteLine(string.Format(">>> Entering txtSearch_TextChanged(sender={0},e={1})", sender, e));
            //Get the Keywords
            string keywords = txtSearch.Text;

            //Filte the categories based on keywords
            if(keywords!=null)
            {
                //Use Searh Method To Display Categoreis
                DataTable dt = dal.Search(keywords);
				System.Diagnostics.Trace.WriteLine(string.Format("txtSearch_TextChanged(sender={0},e={1}): dt → {2}", sender, e, dt));
				System.Diagnostics.Trace.WriteLine(string.Format("txtSearch_TextChanged(sender={0},e={1}): dgvCategories.DataSource → {2}", sender, e, dgvCategories.DataSource));
                dgvCategories.DataSource = dt;
            }
            else
            {
                //Use Select Method to Display All Categories
                DataTable dt = dal.Select();
                dgvCategories.DataSource = dt;
            }
			System.Diagnostics.Trace.WriteLine(string.Format("<<< Exiting txtSearch_TextChanged(sender={0},e={1})", sender, e));
        }
    }
}
