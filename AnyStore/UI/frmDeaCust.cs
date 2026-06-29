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
    public partial class frmDeaCust : Form
    {
        public frmDeaCust()
        {
			Console.WriteLine(">>> Entering frmDeaCust()");
            InitializeComponent();
			Console.WriteLine("<<< Exiting frmDeaCust()");
        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
			Console.WriteLine(">>> Entering pictureBoxClose_Click(sender={0},e={1})", sender, e);
            //Write the code to close this form
            this.Hide();
			Console.WriteLine("<<< Exiting pictureBoxClose_Click(sender={0},e={1})", sender, e);
        }

        DeaCustBLL dc = new DeaCustBLL();
        DeaCustDAL dcDal = new DeaCustDAL();

        userDAL uDal = new userDAL();
        private void btnAdd_Click(object sender, EventArgs e)
        {
			Console.WriteLine(">>> Entering btnAdd_Click(sender={0},e={1})", sender, e);
            //Get the Values from Form
            dc.type = cmbDeaCust.Text;
            dc.name = txtName.Text;
            dc.email = txtEmail.Text;
            dc.contact = txtContact.Text;
            dc.address = txtAddress.Text;
            dc.added_date = DateTime.Now;
            //Getting the ID to Logged in user and passign its value in dealer or cutomer module
            string loggedUsr = frmLogin.loggedIn;
            userBLL usr = uDal.GetIDFromUsername(loggedUsr);
            dc.added_by = usr.id;

            //Creating boolean variable to check whether the dealer or cutomer is added or not
            bool success = dcDal.Insert(dc);

            if(success==true)
            {
                //Dealer or Cutomer inserted successfully 
                MessageBox.Show("Dealer or Customer Added Successfully");
                Clear();
                //Refresh Data Grid View
                DataTable dt = dcDal.Select();
                dgvDeaCust.DataSource = dt;
				Console.WriteLine("btnAdd_Click(sender={0},e={1}): dt → {2}", sender, e, dt);
				Console.WriteLine("btnAdd_Click(sender={0},e={1}): dgvDeaCust.DataSource → {2}", sender, e, dgvDeaCust.DataSource);
            }
            else
            {
                //failed to insert dealer or customer
            }
			Console.WriteLine("<<< Exiting btnAdd_Click(sender={0},e={1})", sender, e);
        }
        public void Clear()
        {
			Console.WriteLine(">>> Entering Clear()");
            txtDeaCustID.Text = "";
            txtName.Text = "";
			Console.WriteLine("Clear(): txtName.Text → {0}", txtName.Text);
			Console.WriteLine("Clear(): txtEmail.Text → {0}", txtEmail.Text);
			Console.WriteLine("Clear(): txtContact.Text → {0}", txtContact.Text);
			Console.WriteLine("Clear(): txtAddress.Text → {0}", txtAddress.Text);
			Console.WriteLine("Clear(): txtSearch.Text → {0}", txtSearch.Text);
			Console.WriteLine("Clear(): txtDeaCustID.Text → {0}", txtDeaCustID.Text);
            txtEmail.Text = "";
            txtContact.Text = "";
            txtAddress.Text = "";
            txtSearch.Text = "";
			Console.WriteLine("<<< Exiting Clear()");
        }

        private void frmDeaCust_Load(object sender, EventArgs e)
        {
			Console.WriteLine(">>> Entering frmDeaCust_Load(sender={0},e={1})", sender, e);
            //Refresh Data Grid View
            DataTable dt = dcDal.Select();
            dgvDeaCust.DataSource = dt;
			Console.WriteLine("<<< Exiting frmDeaCust_Load(sender={0},e={1})", sender, e);
			Console.WriteLine("frmDeaCust_Load(sender={0},e={1}): dt → {2}", sender, e, dt);
			Console.WriteLine("frmDeaCust_Load(sender={0},e={1}): dgvDeaCust.DataSource → {2}", sender, e, dgvDeaCust.DataSource);
        }

        private void dgvDeaCust_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
			Console.WriteLine(">>> Entering dgvDeaCust_RowHeaderMouseClick(sender={0},e={1})", sender, e);
            //int variable to get the identityof row clicked
            int rowIndex = e.RowIndex;

            txtDeaCustID.Text = dgvDeaCust.Rows[rowIndex].Cells[0].Value.ToString();
            cmbDeaCust.Text = dgvDeaCust.Rows[rowIndex].Cells[1].Value.ToString();
            txtName.Text = dgvDeaCust.Rows[rowIndex].Cells[2].Value.ToString();
            txtEmail.Text = dgvDeaCust.Rows[rowIndex].Cells[3].Value.ToString();
            txtContact.Text = dgvDeaCust.Rows[rowIndex].Cells[4].Value.ToString();
            txtAddress.Text = dgvDeaCust.Rows[rowIndex].Cells[5].Value.ToString();
			Console.WriteLine("<<< Exiting dgvDeaCust_RowHeaderMouseClick(sender={0},e={1})", sender, e);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
			Console.WriteLine(">>> Entering btnUpdate_Click(sender={0},e={1})", sender, e);
            //Get the values from Form
            dc.id = int.Parse(txtDeaCustID.Text);
            dc.type = cmbDeaCust.Text;
            dc.name = txtName.Text;
            dc.email = txtEmail.Text;
            dc.contact = txtContact.Text;
            dc.address = txtAddress.Text;
            dc.added_date = DateTime.Now;
            //Getting the ID to Logged in user and passign its value in dealer or cutomer module
            string loggedUsr = frmLogin.loggedIn;
            userBLL usr = uDal.GetIDFromUsername(loggedUsr);
            dc.added_by = usr.id;

            //create boolean variable to check whether the dealer or customer is updated or not
            bool success = dcDal.Update(dc);
            
            if(success==true)
            {
                //Dealer and Customer update Successfully
                MessageBox.Show("Dealer or Customer updated Successfully");
                Clear();
                //Refresh the Data Grid View
                DataTable dt = dcDal.Select();
                dgvDeaCust.DataSource = dt;
				Console.WriteLine("btnUpdate_Click(sender={0},e={1}): dt → {2}", sender, e, dt);
				Console.WriteLine("btnUpdate_Click(sender={0},e={1}): dgvDeaCust.DataSource → {2}", sender, e, dgvDeaCust.DataSource);
            }
            else
            {
                //Failed to udate Dealer or Customer
                MessageBox.Show("Failed to Udpate Dealer or Customer");
            }
			Console.WriteLine("<<< Exiting btnUpdate_Click(sender={0},e={1})", sender, e);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
			Console.WriteLine(">>> Entering btnDelete_Click(sender={0},e={1})", sender, e);
            //Get the id of the user to be deleted from form
            dc.id = int.Parse(txtDeaCustID.Text);

            //Create boolean variable to check wheteher the dealer or customer is deleted or not
            bool success = dcDal.Delete(dc);

            if(success==true)
            {
                //Dealer or Customer Deleted Successfully
                MessageBox.Show("Dealer or Customer Deleted Successfully");
                Clear();
                //Refresh the Data Grid View
                DataTable dt = dcDal.Select();
                dgvDeaCust.DataSource = dt;
				Console.WriteLine("btnDelete_Click(sender={0},e={1}): dt → {2}", sender, e, dt);
				Console.WriteLine("btnDelete_Click(sender={0},e={1}): dgvDeaCust.DataSource → {2}", sender, e, dgvDeaCust.DataSource);
            }
            else
            {
                //Dealer or Customer Failed to Delete
                MessageBox.Show("Failed to Delete Dealer or Customer");
            }
			Console.WriteLine("<<< Exiting btnDelete_Click(sender={0},e={1})", sender, e);
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
			Console.WriteLine(">>> Entering txtSearch_TextChanged(sender={0},e={1})", sender, e);
            //Get the keyowrd from text box
            string keyword = txtSearch.Text;

            if(keyword!=null)
            {
                //Search the Dealer or Customer
                DataTable dt = dcDal.Search(keyword);
                dgvDeaCust.DataSource = dt;
				Console.WriteLine("txtSearch_TextChanged(sender={0},e={1}): dt → {2}", sender, e, dt);
				Console.WriteLine("txtSearch_TextChanged(sender={0},e={1}): dgvDeaCust.DataSource → {2}", sender, e, dgvDeaCust.DataSource);
            }
            else
            {
                //Show all the Dealer or Customer
                DataTable dt = dcDal.Select();
                dgvDeaCust.DataSource = dt;
            }
			Console.WriteLine("<<< Exiting txtSearch_TextChanged(sender={0},e={1})", sender, e);
        }
    }
}
