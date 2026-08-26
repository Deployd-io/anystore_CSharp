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
    public partial class frmUserDashboard : Form
    {
        public frmUserDashboard()
        {
			System.Diagnostics.Trace.WriteLine(">>> Entering frmUserDashboard()");
            InitializeComponent();
			System.Diagnostics.Trace.WriteLine("<<< Exiting frmUserDashboard()");
        }

        //Set a public static method to specify whether the form is purchase or sales
        public static string transactionType;
        private void frmUserDashboard_FormClosed(object sender, FormClosedEventArgs e)
        {
			System.Diagnostics.Trace.WriteLine(string.Format(">>> Entering frmUserDashboard_FormClosed(sender={0},e={1})", sender, e));
            frmLogin login = new frmLogin();
            login.Show();
            this.Hide();
			System.Diagnostics.Trace.WriteLine(string.Format("<<< Exiting frmUserDashboard_FormClosed(sender={0},e={1})", sender, e));
        }

        private void frmUserDashboard_Load(object sender, EventArgs e)
        {
			System.Diagnostics.Trace.WriteLine(string.Format(">>> Entering frmUserDashboard_Load(sender={0},e={1})", sender, e));
			System.Diagnostics.Trace.WriteLine(string.Format("frmUserDashboard_Load(sender={0},e={1}): lblLoggedInUser.Text → {2}", sender, e, lblLoggedInUser.Text));
            lblLoggedInUser.Text = frmLogin.loggedIn;
        }

        private void dealerAndCustomerToolStripMenuItem_Click(object sender, EventArgs e)
        {
			System.Diagnostics.Trace.WriteLine(string.Format(">>> Entering dealerAndCustomerToolStripMenuItem_Click(sender={0},e={1})", sender, e));
            frmDeaCust DeaCust = new frmDeaCust();
			System.Diagnostics.Trace.WriteLine(string.Format("<<< Exiting dealerAndCustomerToolStripMenuItem_Click(sender={0},e={1})", sender, e));
            DeaCust.Show();
        }

        private void purchaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
			System.Diagnostics.Trace.WriteLine(string.Format(">>> Entering purchaseToolStripMenuItem_Click(sender={0},e={1})", sender, e));
            //set value on transactionType static method
            transactionType = "Purchase";
			System.Diagnostics.Trace.WriteLine(string.Format("purchaseToolStripMenuItem_Click(sender={0},e={1}): transactionType → {2}", sender, e, transactionType));
            frmPurchaseAndSales purchase = new frmPurchaseAndSales();
            purchase.Show();
            
			System.Diagnostics.Trace.WriteLine(string.Format("<<< Exiting purchaseToolStripMenuItem_Click(sender={0},e={1})", sender, e));
        }

        private void salesFormsToolStripMenuItem_Click(object sender, EventArgs e)
        {
			System.Diagnostics.Trace.WriteLine(string.Format(">>> Entering salesFormsToolStripMenuItem_Click(sender={0},e={1})", sender, e));
            //Set the value to transacionType method to sales
            transactionType = "Sales";
			System.Diagnostics.Trace.WriteLine(string.Format("salesFormsToolStripMenuItem_Click(sender={0},e={1}): transactionType → {2}", sender, e, transactionType));
            frmPurchaseAndSales sales = new frmPurchaseAndSales();
            sales.Show();
            
			System.Diagnostics.Trace.WriteLine(string.Format("<<< Exiting salesFormsToolStripMenuItem_Click(sender={0},e={1})", sender, e));
        }

        private void inventoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
			System.Diagnostics.Trace.WriteLine(string.Format(">>> Entering inventoryToolStripMenuItem_Click(sender={0},e={1})", sender, e));
            frmInventory inventory = new frmInventory();
			System.Diagnostics.Trace.WriteLine(string.Format("<<< Exiting inventoryToolStripMenuItem_Click(sender={0},e={1})", sender, e));
            inventory.Show();
        }
    }
}
