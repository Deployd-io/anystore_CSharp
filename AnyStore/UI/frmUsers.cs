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
    public partial class frmUsers : Form
    {
        public frmUsers()
        {
			Console.WriteLine(">>> Entering frmUsers()");
            InitializeComponent();
			Console.WriteLine("<<< Exiting frmUsers()");
        }

        userBLL u = new userBLL();
        userDAL dal = new userDAL();

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
			Console.WriteLine(">>> Entering pictureBoxClose_Click(sender={0},e={1})", sender, e);
            this.Close();
			Console.WriteLine("<<< Exiting pictureBoxClose_Click(sender={0},e={1})", sender, e);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
			Console.WriteLine(">>> Entering btnAdd_Click(sender={0},e={1})", sender, e);
            

            //Gettting Data FRom UI
            u.first_name = txtFirstName.Text;
			Console.WriteLine("btnAdd_Click(sender={0},e={1}): u.first_name → {2}", sender, e, u.first_name);
			Console.WriteLine("btnAdd_Click(sender={0},e={1}): u.last_name → {2}", sender, e, u.last_name);
			Console.WriteLine("btnAdd_Click(sender={0},e={1}): u.email → {2}", sender, e, u.email);
			Console.WriteLine("btnAdd_Click(sender={0},e={1}): u.username → {2}", sender, e, u.username);
			Console.WriteLine("btnAdd_Click(sender={0},e={1}): u.password → {2}", sender, e, u.password);
			Console.WriteLine("btnAdd_Click(sender={0},e={1}): u.contact → {2}", sender, e, u.contact);
			Console.WriteLine("btnAdd_Click(sender={0},e={1}): u.address → {2}", sender, e, u.address);
			Console.WriteLine("btnAdd_Click(sender={0},e={1}): u.gender → {2}", sender, e, u.gender);
			Console.WriteLine("btnAdd_Click(sender={0},e={1}): u.user_type → {2}", sender, e, u.user_type);
			Console.WriteLine("btnAdd_Click(sender={0},e={1}): u.added_date → {2}", sender, e, u.added_date);
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

			Console.WriteLine("btnAdd_Click(sender={0},e={1}): u.added_by → {2}", sender, e, u.added_by);
            u.added_by = usr.id;

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
            dgvUsers.DataSource = dt;
			Console.WriteLine("<<< Exiting btnAdd_Click(sender={0},e={1})", sender, e);
			Console.WriteLine("btnAdd_Click(sender={0},e={1}): dt → {2}", sender, e, dt);
			Console.WriteLine("btnAdd_Click(sender={0},e={1}): dgvUsers.DataSource → {2}", sender, e, dgvUsers.DataSource);
        }

        private void frmUsers_Load(object sender, EventArgs e)
        {
			Console.WriteLine(">>> Entering frmUsers_Load(sender={0},e={1})", sender, e);
            DataTable dt = dal.Select();
            dgvUsers.DataSource = dt;
			Console.WriteLine("<<< Exiting frmUsers_Load(sender={0},e={1})", sender, e);
			Console.WriteLine("frmUsers_Load(sender={0},e={1}): dt → {2}", sender, e, dt);
			Console.WriteLine("frmUsers_Load(sender={0},e={1}): dgvUsers.DataSource → {2}", sender, e, dgvUsers.DataSource);
        }
        private void clear()
        {
			Console.WriteLine(">>> Entering clear()");
            txtUserID.Text = "";
            txtFirstName.Text = "";
			Console.WriteLine("clear(): txtFirstName.Text → {0}", txtFirstName.Text);
			Console.WriteLine("clear(): txtLastName.Text → {0}", txtLastName.Text);
			Console.WriteLine("clear(): txtEmail.Text → {0}", txtEmail.Text);
			Console.WriteLine("clear(): txtUsername.Text → {0}", txtUsername.Text);
			Console.WriteLine("clear(): txtPassword.Text → {0}", txtPassword.Text);
			Console.WriteLine("clear(): txtContact.Text → {0}", txtContact.Text);
			Console.WriteLine("clear(): txtAddress.Text → {0}", txtAddress.Text);
			Console.WriteLine("clear(): cmbGender.Text → {0}", cmbGender.Text);
			Console.WriteLine("clear(): cmbUserType.Text → {0}", cmbUserType.Text);
			Console.WriteLine("clear(): txtUserID.Text → {0}", txtUserID.Text);
            txtLastName.Text = "";
            txtEmail.Text = "";
            txtUsername.Text = "";
            txtPassword.Text = "";
            txtContact.Text = "";
            txtAddress.Text = "";
            cmbGender.Text = "";
            cmbUserType.Text = "";
			Console.WriteLine("<<< Exiting clear()");
        }

        private void dgvUsers_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
			Console.WriteLine(">>> Entering dgvUsers_RowHeaderMouseClick(sender={0},e={1})", sender, e);
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
			Console.WriteLine("<<< Exiting dgvUsers_RowHeaderMouseClick(sender={0},e={1})", sender, e);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
			Console.WriteLine(">>> Entering btnUpdate_Click(sender={0},e={1})", sender, e);
            //Get the values from User UI
            u.id = Convert.ToInt32(txtUserID.Text);
            u.first_name = txtFirstName.Text;
			Console.WriteLine("btnUpdate_Click(sender={0},e={1}): u.first_name → {2}", sender, e, u.first_name);
			Console.WriteLine("btnUpdate_Click(sender={0},e={1}): u.last_name → {2}", sender, e, u.last_name);
			Console.WriteLine("btnUpdate_Click(sender={0},e={1}): u.email → {2}", sender, e, u.email);
			Console.WriteLine("btnUpdate_Click(sender={0},e={1}): u.username → {2}", sender, e, u.username);
			Console.WriteLine("btnUpdate_Click(sender={0},e={1}): u.password → {2}", sender, e, u.password);
			Console.WriteLine("btnUpdate_Click(sender={0},e={1}): u.contact → {2}", sender, e, u.contact);
			Console.WriteLine("btnUpdate_Click(sender={0},e={1}): u.address → {2}", sender, e, u.address);
			Console.WriteLine("btnUpdate_Click(sender={0},e={1}): u.gender → {2}", sender, e, u.gender);
			Console.WriteLine("btnUpdate_Click(sender={0},e={1}): u.user_type → {2}", sender, e, u.user_type);
			Console.WriteLine("btnUpdate_Click(sender={0},e={1}): u.added_date → {2}", sender, e, u.added_date);
			Console.WriteLine("btnUpdate_Click(sender={0},e={1}): u.added_by → {2}", sender, e, u.added_by);
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
            dgvUsers.DataSource = dt;
			Console.WriteLine("<<< Exiting btnUpdate_Click(sender={0},e={1})", sender, e);
			Console.WriteLine("btnUpdate_Click(sender={0},e={1}): dt → {2}", sender, e, dt);
			Console.WriteLine("btnUpdate_Click(sender={0},e={1}): dgvUsers.DataSource → {2}", sender, e, dgvUsers.DataSource);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
			Console.WriteLine(">>> Entering btnDelete_Click(sender={0},e={1})", sender, e);
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
            dgvUsers.DataSource = dt;
			Console.WriteLine("<<< Exiting btnDelete_Click(sender={0},e={1})", sender, e);
			Console.WriteLine("btnDelete_Click(sender={0},e={1}): dt → {2}", sender, e, dt);
			Console.WriteLine("btnDelete_Click(sender={0},e={1}): dgvUsers.DataSource → {2}", sender, e, dgvUsers.DataSource);
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
			Console.WriteLine(">>> Entering txtSearch_TextChanged(sender={0},e={1})", sender, e);
            //Get Keyword from Text box
            string keywords = txtSearch.Text;

            //Chec if the keywords has value or not
            if(keywords!=null)
            {
                //Show user based on keywords
                DataTable dt = dal.Search(keywords);
                dgvUsers.DataSource = dt;
				Console.WriteLine("txtSearch_TextChanged(sender={0},e={1}): dt → {2}", sender, e, dt);
				Console.WriteLine("txtSearch_TextChanged(sender={0},e={1}): dgvUsers.DataSource → {2}", sender, e, dgvUsers.DataSource);
            }
            else
            {
                //show all users from the database
                DataTable dt = dal.Select();
                dgvUsers.DataSource = dt;
            }
			Console.WriteLine("<<< Exiting txtSearch_TextChanged(sender={0},e={1})", sender, e);
        }
    }
}
