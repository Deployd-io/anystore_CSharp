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
using Microsoft.Extensions.Logging;

namespace AnyStore
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    public partial class frmUserDashboard : Form
    {
        public frmUserDashboard()
        {
			_logger.LogDebug(">>> Entering frmUserDashboard()");
            InitializeComponent();
			_logger.LogDebug("<<< Exiting frmUserDashboard()");
        }

        //Set a public static method to specify whether the form is purchase or sales
        public static string transactionType;
        private void frmUserDashboard_FormClosed(object sender, FormClosedEventArgs e)
        {
			_logger.LogDebug(">>> Entering frmUserDashboard_FormClosed(sender={Sender},e={E})", sender, e);
            frmLogin login = new frmLogin();
            login.Show();
            this.Hide();
			_logger.LogDebug("<<< Exiting frmUserDashboard_FormClosed(sender={Sender},e={E})", sender, e);
        }

        private void frmUserDashboard_Load(object sender, EventArgs e)
        {
			_logger.LogDebug(">>> Entering frmUserDashboard_Load(sender={Sender},e={E})", sender, e);
			_logger.LogDebug("frmUserDashboard_Load(sender={Sender},e={E}): lblLoggedInUser.Text → {Arg2}", sender, e, lblLoggedInUser.Text);
            lblLoggedInUser.Text = frmLogin.loggedIn;
        }

        private void dealerAndCustomerToolStripMenuItem_Click(object sender, EventArgs e)
        {
			_logger.LogDebug(">>> Entering dealerAndCustomerToolStripMenuItem_Click(sender={Sender},e={E})", sender, e);
            frmDeaCust DeaCust = new frmDeaCust();
			_logger.LogDebug("<<< Exiting dealerAndCustomerToolStripMenuItem_Click(sender={Sender},e={E})", sender, e);
            DeaCust.Show();
        }

        private void purchaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
			_logger.LogDebug(">>> Entering purchaseToolStripMenuItem_Click(sender={Sender},e={E})", sender, e);
            //set value on transactionType static method
            transactionType = "Purchase";
            frmPurchaseAndSales purchase = new frmPurchaseAndSales();
			_logger.LogDebug("purchaseToolStripMenuItem_Click(sender={Sender},e={E}): transactionType → {TransactionType}", sender, e, transactionType);
            purchase.Show();
            
			_logger.LogDebug("<<< Exiting purchaseToolStripMenuItem_Click(sender={Sender},e={E})", sender, e);
        }

        private void salesFormsToolStripMenuItem_Click(object sender, EventArgs e)
        {
			_logger.LogDebug(">>> Entering salesFormsToolStripMenuItem_Click(sender={Sender},e={E})", sender, e);
            //Set the value to transacionType method to sales
            transactionType = "Sales";
            frmPurchaseAndSales sales = new frmPurchaseAndSales();
			_logger.LogDebug("salesFormsToolStripMenuItem_Click(sender={Sender},e={E}): transactionType → {TransactionType}", sender, e, transactionType);
            sales.Show();
            
			_logger.LogDebug("<<< Exiting salesFormsToolStripMenuItem_Click(sender={Sender},e={E})", sender, e);
        }

        private void inventoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
			_logger.LogDebug(">>> Entering inventoryToolStripMenuItem_Click(sender={Sender},e={E})", sender, e);
            frmInventory inventory = new frmInventory();
			_logger.LogDebug("<<< Exiting inventoryToolStripMenuItem_Click(sender={Sender},e={E})", sender, e);
            inventory.Show();
        }
    }
}
