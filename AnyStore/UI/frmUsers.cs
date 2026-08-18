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
    public partial class frmUsers : Form
    {
        public frmUsers()
        {
			_logger.LogDebug(">>> Entering frmUsers()");
            InitializeComponent();
			_logger.LogDebug("<<< Exiting frmUsers()");
        }

        userBLL u = new userBLL();
        userDAL dal = new userDAL();

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
			_logger.LogDebug(">>> Entering pictureBoxClose_Click(sender={Sender},e={E})", sender, e);
            this.Close();
			_logger.LogDebug("<<< Exiting pictureBoxClose_Click(sender={Sender},e={E})", sender, e);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
			_logger.LogDebug(">>> Entering btnAdd_Click(sender={Sender},e={E})", sender, e);
            

            //Gettting Data FRom UI
            u.first_name = txtFirstName.Text;
			_logger.LogDebug("btnAdd_Click(sender={Sender},e={E}): u.first_name → {Arg2}", sender, e, u.first_name);
			_logger.LogDebug("btnAdd_Click(sender={Sender},e={E}): u.last_name → {Arg2}", sender, e, u.last_name);
			_logger.LogDebug("btnAdd_Click(sender={Sender},e={E}): u.email → {Arg2}", sender, e, u.email);
			_logger.LogDebug("btnAdd_Click(sender={Sender},e={E}): u.username → {Arg2}", sender, e, u.username);
			_logger.LogDebug("btnAdd_Click(sender={Sender},e={E}): u.password → {Arg2}", sender, e, u.password);
			_logger.LogDebug("btnAdd_Click(sender={Sender},e={E}): u.contact → {Arg2}", sender, e, u.contact);
			_logger.LogDebug("btnAdd_Click(sender={Sender},e={E}): u.address → {Arg2}", sender, e, u.address);
			_logger.LogDebug("btnAdd_Click(sender={Sender},e={E}): u.gender → {Arg2}", sender, e, u.gender);
			_logger.LogDebug("btnAdd_Click(sender={Sender},e={E}): u.user_type → {Arg2}", sender, e, u.user_type);
			_logger.LogDebug("btnAdd_Click(sender={Sender},e={E}): u.added_date → {Arg2}", sender, e, u.added_date);
            u.last_name = txtLastName.Text;
            u.email = txtEmail.Text;
            u.username = txtUsername.Text;
            u.password = txtPassword.Text;
            u.contact = txtContact.Text;
            u.address = txtAddress.Text;
            u.gender = cmbGender.Text;
            u.user_type = cmbUserType.Text;
            u.added_date = DateTime.Now;

            //Getting Username of the logged in user
            string loggedUser = frmLogin.loggedIn;
            userBLL usr = dal.GetIDFromUsername(loggedUser);

            u.added_by = usr.id;
			_logger.LogDebug("btnAdd_Click(sender={Sender},e={E}): u.added_by → {Arg2}", sender, e, u.added_by);

            //Inserting Data into DAtabase
            bool success = dal.Insert(u);
            //If the data is successfully inserted then the value of success will be true else it will be false
            if(success==true)
            {
                //Data Successfully Inserted
                MessageBox.Show("User successfully created.");
                clear();
            }
            else
            {
                //Failed to insert data
                MessageBox.Show("Failed to add new user");
            }
            //Refreshing Data Grid View
            DataTable dt = dal.Select();
			_logger.LogDebug("btnAdd_Click(sender={Sender},e={E}): dt → {Dt}", sender, e, dt);
			_logger.LogDebug("btnAdd_Click(sender={Sender},e={E}): dgvUsers.DataSource → {Arg2}", sender, e, dgvUsers.DataSource);
            dgvUsers.DataSource = dt;
			_logger.LogDebug("<<< Exiting btnAdd_Click(sender={Sender},e={E})", sender, e);
        }

        private void frmUsers_Load(object sender, EventArgs e)
        {
			_logger.LogDebug(">>> Entering frmUsers_Load(sender={Sender},e={E})", sender, e);
            DataTable dt = dal.Select();
            dgvUsers.DataSource = dt;
			_logger.LogDebug("frmUsers_Load(sender={Sender},e={E}): dt → {Dt}", sender, e, dt);
			_logger.LogDebug("frmUsers_Load(sender={Sender},e={E}): dgvUsers.DataSource → {Arg2}", sender, e, dgvUsers.DataSource);
			_logger.LogDebug("<<< Exiting frmUsers_Load(sender={Sender},e={E})", sender, e);
        }
        private void clear()
        {
            txtUserID.Text = "";
            txtFirstName.Text = "";
			_logger.LogDebug("clear(): txtFirstName.Text → {Arg0}", txtFirstName.Text);
			_logger.LogDebug("clear(): txtLastName.Text → {Arg0}", txtLastName.Text);
			_logger.LogDebug("clear(): txtEmail.Text → {Arg0}", txtEmail.Text);
			_logger.LogDebug("clear(): txtUsername.Text → {Arg0}", txtUsername.Text);
			_logger.LogDebug("clear(): txtPassword.Text → {Arg0}", txtPassword.Text);
			_logger.LogDebug("clear(): txtContact.Text → {Arg0}", txtContact.Text);
			_logger.LogDebug("clear(): txtAddress.Text → {Arg0}", txtAddress.Text);
			_logger.LogDebug("clear(): cmbGender.Text → {Arg0}", cmbGender.Text);
			_logger.LogDebug("clear(): txtUserID.Text → {Arg0}", txtUserID.Text);
			_logger.LogDebug("clear(): cmbUserType.Text → {Arg0}", cmbUserType.Text);
            txtLastName.Text = "";
            txtEmail.Text = "";
            txtUsername.Text = "";
            txtPassword.Text = "";
            txtContact.Text = "";
            txtAddress.Text = "";
            cmbGender.Text = "";
            cmbUserType.Text = "";
			_logger.LogDebug("<<< Exiting clear()");
        }

        private void dgvUsers_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
			_logger.LogDebug(">>> Entering dgvUsers_RowHeaderMouseClick(sender={Sender},e={E})", sender, e);
            //Get the index of particular row
            int rowIndex = e.RowIndex;
            txtUserID.Text = dgvUsers.Rows[rowIndex].Cells[0].Value.ToString();
            txtFirstName.Text = dgvUsers.Rows[rowIndex].Cells[1].Value.ToString();
            txtLastName.Text = dgvUsers.Rows[rowIndex].Cells[2].Value.ToString();
            txtEmail.Text = dgvUsers.Rows[rowIndex].Cells[3].Value.ToString();
            txtUsername.Text = dgvUsers.Rows[rowIndex].Cells[4].Value.ToString();
            txtPassword.Text = dgvUsers.Rows[rowIndex].Cells[5].Value.ToString();
            txtContact.Text = dgvUsers.Rows[rowIndex].Cells[6].Value.ToString();
            txtAddress.Text = dgvUsers.Rows[rowIndex].Cells[7].Value.ToString();
            cmbGender.Text = dgvUsers.Rows[rowIndex].Cells[8].Value.ToString();
            cmbUserType.Text = dgvUsers.Rows[rowIndex].Cells[9].Value.ToString();
			_logger.LogDebug("<<< Exiting dgvUsers_RowHeaderMouseClick(sender={Sender},e={E})", sender, e);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
			_logger.LogDebug(">>> Entering btnUpdate_Click(sender={Sender},e={E})", sender, e);
            //Get the values from User UI
            u.id = Convert.ToInt32(txtUserID.Text);
            u.first_name = txtFirstName.Text;
			_logger.LogDebug("btnUpdate_Click(sender={Sender},e={E}): u.first_name → {Arg2}", sender, e, u.first_name);
			_logger.LogDebug("btnUpdate_Click(sender={Sender},e={E}): u.last_name → {Arg2}", sender, e, u.last_name);
			_logger.LogDebug("btnUpdate_Click(sender={Sender},e={E}): u.email → {Arg2}", sender, e, u.email);
			_logger.LogDebug("btnUpdate_Click(sender={Sender},e={E}): u.username → {Arg2}", sender, e, u.username);
			_logger.LogDebug("btnUpdate_Click(sender={Sender},e={E}): u.password → {Arg2}", sender, e, u.password);
			_logger.LogDebug("btnUpdate_Click(sender={Sender},e={E}): u.contact → {Arg2}", sender, e, u.contact);
			_logger.LogDebug("btnUpdate_Click(sender={Sender},e={E}): u.address → {Arg2}", sender, e, u.address);
			_logger.LogDebug("btnUpdate_Click(sender={Sender},e={E}): u.gender → {Arg2}", sender, e, u.gender);
			_logger.LogDebug("btnUpdate_Click(sender={Sender},e={E}): u.user_type → {Arg2}", sender, e, u.user_type);
			_logger.LogDebug("btnUpdate_Click(sender={Sender},e={E}): u.added_date → {Arg2}", sender, e, u.added_date);
			_logger.LogDebug("btnUpdate_Click(sender={Sender},e={E}): u.added_by → {Arg2}", sender, e, u.added_by);
            u.last_name = txtLastName.Text;
            u.email = txtEmail.Text;
            u.username = txtUsername.Text;
            u.password = txtPassword.Text;
            u.contact = txtContact.Text;
            u.address = txtAddress.Text;
            u.gender = cmbGender.Text;
            u.user_type = cmbUserType.Text;
            u.added_date = DateTime.Now;
            u.added_by = 1;

            //Updating Data into database
            bool success = dal.Update(u);
            //if data is updated successfully then the value of success will be true else it will be false
            if(success==true)
            {
                //Data Updated Successfully
                MessageBox.Show("User successfully updated");
                clear();
            }
            else
            {
                //failed to update user
                MessageBox.Show("Failed to update user");
            }
            //Refreshing Data Grid View
            DataTable dt = dal.Select();
			_logger.LogDebug("btnUpdate_Click(sender={Sender},e={E}): dt → {Dt}", sender, e, dt);
			_logger.LogDebug("btnUpdate_Click(sender={Sender},e={E}): dgvUsers.DataSource → {Arg2}", sender, e, dgvUsers.DataSource);
            dgvUsers.DataSource = dt;
			_logger.LogDebug("<<< Exiting btnUpdate_Click(sender={Sender},e={E})", sender, e);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
			_logger.LogDebug(">>> Entering btnDelete_Click(sender={Sender},e={E})", sender, e);
            //Getting User ID from Form
            u.id = Convert.ToInt32(txtUserID.Text);

            bool success = dal.Delete(u);
            //if data is deleted then the value of success will be true else it will be false
            if(success==true)
            {
                //User Deleted Successfully 
                MessageBox.Show("User deleted successfully");
                clear();
            }
            else
            {
                //Failed to Delete User
                MessageBox.Show("Failed to delete user");

            }
            //refreshing Datagrid view
            DataTable dt = dal.Select();
			_logger.LogDebug("btnDelete_Click(sender={Sender},e={E}): dt → {Dt}", sender, e, dt);
			_logger.LogDebug("btnDelete_Click(sender={Sender},e={E}): dgvUsers.DataSource → {Arg2}", sender, e, dgvUsers.DataSource);
            dgvUsers.DataSource = dt;
			_logger.LogDebug("<<< Exiting btnDelete_Click(sender={Sender},e={E})", sender, e);
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
			_logger.LogDebug(">>> Entering txtSearch_TextChanged(sender={Sender},e={E})", sender, e);
            //Get Keyword from Text box
            string keywords = txtSearch.Text;

            //Chec if the keywords has value or not
            if(keywords!=null)
            {
                //Show user based on keywords
                DataTable dt = dal.Search(keywords);
				_logger.LogDebug("txtSearch_TextChanged(sender={Sender},e={E}): dt → {Dt}", sender, e, dt);
				_logger.LogDebug("txtSearch_TextChanged(sender={Sender},e={E}): dgvUsers.DataSource → {Arg2}", sender, e, dgvUsers.DataSource);
                dgvUsers.DataSource = dt;
            }
            else
            {
                //show all users from the database
                DataTable dt = dal.Select();
                dgvUsers.DataSource = dt;
            }
			_logger.LogDebug("<<< Exiting txtSearch_TextChanged(sender={Sender},e={E})", sender, e);
        }
    }
}
