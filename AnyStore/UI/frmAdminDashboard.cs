using AnyStore.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnyStore
{
    public partial class frmAdminDashboard : Form
    {
        public frmAdminDashboard()
        {
			Console.WriteLine(">>> Entering frmAdminDashboard()");
            InitializeComponent();
			Console.WriteLine("<<< Exiting frmAdminDashboard()");
        }

        private void usersToolStripMenuItem_Click(object sender, EventArgs e)
        {
			Console.WriteLine(">>> Entering usersToolStripMenuItem_Click(sender={0},e={1})", sender, e);
            frmUsers user = new frmUsers();
            user.Show();
			Console.WriteLine("<<< Exiting usersToolStripMenuItem_Click(sender={0},e={1})", sender, e);
        }

        private void frmAdminDashboard_FormClosed(object sender, FormClosedEventArgs e)
        {
			Console.WriteLine(">>> Entering frmAdminDashboard_FormClosed(sender={0},e={1})", sender, e);
            frmLogin login = new frmLogin();
            login.Show();
            this.Hide();
			Console.WriteLine("<<< Exiting frmAdminDashboard_FormClosed(sender={0},e={1})", sender, e);
        }

        private void frmAdminDashboard_Load(object sender, EventArgs e)
        {
			Console.WriteLine(">>> Entering frmAdminDashboard_Load(sender={0},e={1})", sender, e);
			Console.WriteLine("frmAdminDashboard_Load(sender={0},e={1}): lblLoggedInUser.Text → {2}", sender, e, lblLoggedInUser.Text);
            lblLoggedInUser.Text = frmLogin.loggedIn;
			Console.WriteLine("<<< Exiting frmAdminDashboard_Load(sender={0},e={1})", sender, e);
        }

        private void categoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
			Console.WriteLine(">>> Entering categoryToolStripMenuItem_Click(sender={0},e={1})", sender, e);
            frmCategories category = new frmCategories();
            category.Show();
			Console.WriteLine("<<< Exiting categoryToolStripMenuItem_Click(sender={0},e={1})", sender, e);
        }

        private void productsToolStripMenuItem_Click(object sender, EventArgs e)
        {
			Console.WriteLine(">>> Entering productsToolStripMenuItem_Click(sender={0},e={1})", sender, e);
            frmProducts product = new frmProducts();
            product.Show();
			Console.WriteLine("<<< Exiting productsToolStripMenuItem_Click(sender={0},e={1})", sender, e);
        }

        private void deealerAndCustomerToolStripMenuItem_Click(object sender, EventArgs e)
        {
			Console.WriteLine(">>> Entering deealerAndCustomerToolStripMenuItem_Click(sender={0},e={1})", sender, e);
            frmDeaCust DeaCust = new frmDeaCust();
            DeaCust.Show();
			Console.WriteLine("<<< Exiting deealerAndCustomerToolStripMenuItem_Click(sender={0},e={1})", sender, e);
        }

        private void transactionToolStripMenuItem_Click(object sender, EventArgs e)
        {
			Console.WriteLine(">>> Entering transactionToolStripMenuItem_Click(sender={0},e={1})", sender, e);
            frmTransactions transaction = new frmTransactions();
            transaction.Show();
			Console.WriteLine("<<< Exiting transactionToolStripMenuItem_Click(sender={0},e={1})", sender, e);
        }

        private void inventoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
			Console.WriteLine(">>> Entering inventoryToolStripMenuItem_Click(sender={0},e={1})", sender, e);
            frmInventory inventory = new frmInventory();
            inventory.Show();
			Console.WriteLine("<<< Exiting inventoryToolStripMenuItem_Click(sender={0},e={1})", sender, e);
        }
    }
}
