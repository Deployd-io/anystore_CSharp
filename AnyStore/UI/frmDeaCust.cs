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
    public partial class frmDeaCust : Form
    {
        public frmDeaCust()
        {
			_logger.LogDebug(">>> Entering frmDeaCust()");
            InitializeComponent();
			_logger.LogDebug("<<< Exiting frmDeaCust()");
        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
			_logger.LogDebug(">>> Entering pictureBoxClose_Click(sender={Sender},e={E})", sender, e);
            //Write the code to close this form
            this.Hide();
			_logger.LogDebug("<<< Exiting pictureBoxClose_Click(sender={Sender},e={E})", sender, e);
        }

        DeaCustBLL dc = new DeaCustBLL();
        DeaCustDAL dcDal = new DeaCustDAL();

        userDAL uDal = new userDAL();
        private void btnAdd_Click(object sender, EventArgs e)
        {
			_logger.LogDebug(">>> Entering btnAdd_Click(sender={Sender},e={E})", sender, e);
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
				_logger.LogDebug("btnAdd_Click(sender={Sender},e={E}): dt → {Dt}", sender, e, dt);
				_logger.LogDebug("btnAdd_Click(sender={Sender},e={E}): dgvDeaCust.DataSource → {Arg2}", sender, e, dgvDeaCust.DataSource);
                dgvDeaCust.DataSource = dt;
            }
            else
            {
                //failed to insert dealer or customer
            }
			_logger.LogDebug("<<< Exiting btnAdd_Click(sender={Sender},e={E})", sender, e);
        }
        public void Clear()
        {
			_logger.LogDebug(">>> Entering Clear()");
			_logger.LogDebug("Clear(): txtSearch.Text → {Arg0}", txtSearch.Text);
			_logger.LogDebug("Clear(): txtName.Text → {Arg0}", txtName.Text);
			_logger.LogDebug("Clear(): txtEmail.Text → {Arg0}", txtEmail.Text);
			_logger.LogDebug("Clear(): txtContact.Text → {Arg0}", txtContact.Text);
			_logger.LogDebug("Clear(): txtAddress.Text → {Arg0}", txtAddress.Text);
			_logger.LogDebug("Clear(): txtDeaCustID.Text → {Arg0}", txtDeaCustID.Text);
            txtDeaCustID.Text = "";
            txtName.Text = "";
            txtEmail.Text = "";
            txtContact.Text = "";
            txtAddress.Text = "";
            txtSearch.Text = "";
        }

        private void frmDeaCust_Load(object sender, EventArgs e)
        {
			_logger.LogDebug(">>> Entering frmDeaCust_Load(sender={Sender},e={E})", sender, e);
			_logger.LogDebug("frmDeaCust_Load(sender={Sender},e={E}): dgvDeaCust.DataSource → {Arg2}", sender, e, dgvDeaCust.DataSource);
            //Refresh Data Grid View
			_logger.LogDebug("<<< Exiting frmDeaCust_Load(sender={Sender},e={E})", sender, e);
            DataTable dt = dcDal.Select();
			_logger.LogDebug("frmDeaCust_Load(sender={Sender},e={E}): dt → {Dt}", sender, e, dt);
            dgvDeaCust.DataSource = dt;
        }

        private void dgvDeaCust_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
			_logger.LogDebug(">>> Entering dgvDeaCust_RowHeaderMouseClick(sender={Sender},e={E})", sender, e);
            //int variable to get the identityof row clicked
            int rowIndex = e.RowIndex;

            txtDeaCustID.Text = dgvDeaCust.Rows[rowIndex].Cells[0].Value.ToString();
            cmbDeaCust.Text = dgvDeaCust.Rows[rowIndex].Cells[1].Value.ToString();
            txtName.Text = dgvDeaCust.Rows[rowIndex].Cells[2].Value.ToString();
            txtEmail.Text = dgvDeaCust.Rows[rowIndex].Cells[3].Value.ToString();
            txtContact.Text = dgvDeaCust.Rows[rowIndex].Cells[4].Value.ToString();
            txtAddress.Text = dgvDeaCust.Rows[rowIndex].Cells[5].Value.ToString();
			_logger.LogDebug("<<< Exiting dgvDeaCust_RowHeaderMouseClick(sender={Sender},e={E})", sender, e);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
			_logger.LogDebug(">>> Entering btnUpdate_Click(sender={Sender},e={E})", sender, e);
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
				_logger.LogDebug("btnUpdate_Click(sender={Sender},e={E}): dt → {Dt}", sender, e, dt);
				_logger.LogDebug("btnUpdate_Click(sender={Sender},e={E}): dgvDeaCust.DataSource → {Arg2}", sender, e, dgvDeaCust.DataSource);
                dgvDeaCust.DataSource = dt;
            }
            else
            {
                //Failed to udate Dealer or Customer
                MessageBox.Show("Failed to Udpate Dealer or Customer");
            }
			_logger.LogDebug("<<< Exiting btnUpdate_Click(sender={Sender},e={E})", sender, e);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
			_logger.LogDebug(">>> Entering btnDelete_Click(sender={Sender},e={E})", sender, e);
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
				_logger.LogDebug("btnDelete_Click(sender={Sender},e={E}): dt → {Dt}", sender, e, dt);
				_logger.LogDebug("btnDelete_Click(sender={Sender},e={E}): dgvDeaCust.DataSource → {Arg2}", sender, e, dgvDeaCust.DataSource);
                dgvDeaCust.DataSource = dt;
            }
            else
            {
                //Dealer or Customer Failed to Delete
                MessageBox.Show("Failed to Delete Dealer or Customer");
            }
			_logger.LogDebug("<<< Exiting btnDelete_Click(sender={Sender},e={E})", sender, e);
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
			_logger.LogDebug(">>> Entering txtSearch_TextChanged(sender={Sender},e={E})", sender, e);
            //Get the keyowrd from text box
            string keyword = txtSearch.Text;

            if(keyword!=null)
            {
                //Search the Dealer or Customer
                DataTable dt = dcDal.Search(keyword);
				_logger.LogDebug("txtSearch_TextChanged(sender={Sender},e={E}): dt → {Dt}", sender, e, dt);
				_logger.LogDebug("txtSearch_TextChanged(sender={Sender},e={E}): dgvDeaCust.DataSource → {Arg2}", sender, e, dgvDeaCust.DataSource);
                dgvDeaCust.DataSource = dt;
            }
            else
            {
                //Show all the Dealer or Customer
                DataTable dt = dcDal.Select();
                dgvDeaCust.DataSource = dt;
            }
			_logger.LogDebug("<<< Exiting txtSearch_TextChanged(sender={Sender},e={E})", sender, e);
        }
    }
}
