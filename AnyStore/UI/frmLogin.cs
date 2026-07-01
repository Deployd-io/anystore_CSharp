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
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
			Console.WriteLine(">>> Entering frmLogin()");
            InitializeComponent();
			Console.WriteLine("<<< Exiting frmLogin()");
        }

        loginBLL l = new loginBLL();
        loginDAL dal = new loginDAL();
        public static string loggedIn;

        private void pboxClose_Click(object sender, EventArgs e)
        {
			Console.WriteLine(">>> Entering pboxClose_Click(sender={0},e={1})", sender, e);
            //Code to close this form
            this.Close();
			Console.WriteLine("<<< Exiting pboxClose_Click(sender={0},e={1})", sender, e);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
			Console.WriteLine(">>> Entering btnLogin_Click(sender={0},e={1})", sender, e);
            l.username = txtUsername.Text.Trim();
            l.password = txtPassword.Text.Trim();
            l.user_type = cmbUserType.Text.Trim();

            //Checking the login credentials
            bool sucess = dal.loginCheck(l);
            if(sucess==true)
            {
                //Login Successfull
                MessageBox.Show("Login Successful.");
                loggedIn = l.username;
				Console.WriteLine("btnLogin_Click(sender={0},e={1}): loggedIn → {2}", sender, e, loggedIn);
                //Need to open Respective Forms based on User Type
                switch(l.user_type)
                {
                    case "Admin":
                        {
                            //Display Admin Dashboard
                            frmAdminDashboard admin = new frmAdminDashboard();
                            admin.Show();
                            this.Hide();
                        }
                        break;

                    case "User":
                        {
                            //Display User Dashboard
                            frmUserDashboard user = new frmUserDashboard();
                            user.Show();
                            this.Hide();
                        }
                        break;

                    default:
                        {
                            //Display an error message
                            MessageBox.Show("Invalid User Type.");
                        }
                        break;
                }
            }
            else
            {
                //login Failed
                MessageBox.Show("Login Failed. Try Again");
            }
			Console.WriteLine("<<< Exiting btnLogin_Click(sender={0},e={1})", sender, e);
        }
    }
}
