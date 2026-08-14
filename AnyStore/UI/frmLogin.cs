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
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
			_logger.LogDebug(">>> Entering frmLogin()");
            InitializeComponent();
			_logger.LogDebug("<<< Exiting frmLogin()");
        }

        loginBLL l = new loginBLL();
        loginDAL dal = new loginDAL();
        public static string loggedIn;

        private void pboxClose_Click(object sender, EventArgs e)
        {
			_logger.LogDebug(">>> Entering pboxClose_Click(sender={Sender},e={E})", sender, e);
            //Code to close this form
            this.Close();
			_logger.LogDebug("<<< Exiting pboxClose_Click(sender={Sender},e={E})", sender, e);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
			_logger.LogDebug(">>> Entering btnLogin_Click(sender={Sender},e={E})", sender, e);
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
				_logger.LogDebug("btnLogin_Click(sender={Sender},e={E}): loggedIn → {LoggedIn}", sender, e, loggedIn);
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
			_logger.LogDebug("<<< Exiting btnLogin_Click(sender={Sender},e={E})", sender, e);
        }
    }
}
