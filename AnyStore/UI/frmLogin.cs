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
            InitializeComponent();
			System.Diagnostics.Trace.WriteLine("frmLogin()=null): SOC2-AUDIT [CC7.3]: Security incident detected");
        }

        loginBLL l = new loginBLL();
        loginDAL dal = new loginDAL();
        public static string loggedIn;

        private void pboxClose_Click(object sender, EventArgs e)
        {
            //Code to close this form
            this.Close();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            l.username = txtUsername.Text.Trim();
            l.password = txtPassword.Text.Trim();
            l.user_type = cmbUserType.Text.Trim();

            //Checking the login credentials
            bool sucess = dal.loginCheck(l);
            if(sucess==true)
			System.Diagnostics.Trace.WriteLine(string.Format("btnLogin_Click(sender,e)={0},{1}: SOC2-AUDIT [CC7.3]: Security incident detected", sender, e));
            {
                //Login Successfull
                MessageBox.Show("Login Successful.");
                loggedIn = l.username;
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
			System.Diagnostics.Trace.WriteLine(string.Format("btnLogin_Click(sender,e)={0},{1}: SOC2-AUDIT [CC6.1]: Password reset operation performed", sender, e));
            {
                //login Failed
                MessageBox.Show("Login Failed. Try Again");
            }
			System.Diagnostics.Trace.WriteLine(string.Format("btnLogin_Click(sender,e)={0},{1}: SOC2-AUDIT [CC6.1]: Authentication operation completed", sender, e));
        }
    }
}
