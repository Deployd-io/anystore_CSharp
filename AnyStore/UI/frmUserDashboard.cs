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
			Console.WriteLine(">>> Entering frmUserDashboard()");
            InitializeComponent();
			Console.WriteLine("<<< Exiting frmUserDashboard()");
        }

        //Set a public static method to specify whether the form is purchase or sales
        public static string transactionType;
        private void frmUserDashboard_FormClosed(object sender, FormClosedEventArgs e)
        {
			Console.WriteLine(">>> Entering frmUserDashboard_FormClosed(sender={0},e={1})", sender, e);
            frmLogin login = new frmLogin();
            login.Show();
            this.Hide();
			Console.WriteLine("<<< Exiting frmUserDashboard_FormClosed(sender={0},e={1})", sender, e);
        }

        private void frmUserDashboard_Load(object sender, EventArgs e)
        {
			Console.WriteLine(">>> Entering frmUserDashboard_Load(sender={0},e={1})", sender, e);
			Console.WriteLine("frmUserDashboard_Load(sender={0},e={1}): lblLoggedInUser.Text → {2}", sender, e, lblLoggedInUser.Text);
            lblLoggedInUser.Text = frmLogin.loggedIn;
			Console.WriteLine("<<< Exiting frmUserDashboard_Load(sender={0},e={1})", sender, e);
        }

        private void dealerAndCustomerToolStripMenuItem_Click(object sender, EventArgs e)
        {
			Console.WriteLine(">>> Entering dealerAndCustomerToolStripMenuItem_Click(sender={0},e={1})", sender, e);
            frmDeaCust DeaCust = new frmDeaCust();
            DeaCust.Show();
			Console.WriteLine("<<< Exiting dealerAndCustomerToolStripMenuItem_Click(sender={0},e={1})", sender, e);
        }

        private void purchaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
			Console.WriteLine(">>> Entering purchaseToolStripMenuItem_Click(sender={0},e={1})", sender, e);
            //set value on transactionType static method
            transactionType = "Purchase";
			Console.WriteLine("purchaseToolStripMenuItem_Click(sender={0},e={1}): transactionType → {2}", sender, e, transactionType);
            frmPurchaseAndSales purchase = new frmPurchaseAndSales();
            purchase.Show();
            
			Console.WriteLine("<<< Exiting purchaseToolStripMenuItem_Click(sender={0},e={1})", sender, e);
        }

        private void salesFormsToolStripMenuItem_Click(object sender, EventArgs e)
        {
			Console.WriteLine(">>> Entering salesFormsToolStripMenuItem_Click(sender={0},e={1})", sender, e);
            //Set the value to transacionType method to sales
            transactionType = "Sales";
			Console.WriteLine("salesFormsToolStripMenuItem_Click(sender={0},e={1}): transactionType → {2}", sender, e, transactionType);
            frmPurchaseAndSales sales = new frmPurchaseAndSales();
            sales.Show();
            
			Console.WriteLine("<<< Exiting salesFormsToolStripMenuItem_Click(sender={0},e={1})", sender, e);
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
