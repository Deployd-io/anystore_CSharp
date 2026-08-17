using AnyStore.BLL;
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
    public partial class frmAdminDashboard : Form
    {
        public frmAdminDashboard()
        {
			_logger.LogDebug(">>> Entering frmAdminDashboard()");
            InitializeComponent();
            BuildReportsMenu();
			_logger.LogDebug("<<< Exiting frmAdminDashboard()");
        }

        private void BuildReportsMenu()
        {
			_logger.LogDebug(">>> Entering BuildReportsMenu()");
            ToolStripMenuItem reportsMenu = new ToolStripMenuItem("Reports");
            reportsMenu.DropDownItems.Add("Revenue Per Product", null, revenuePerProductToolStripMenuItem_Click);
			_logger.LogDebug("BuildReportsMenu(): reportsMenu → {ReportsMenu}", reportsMenu);
            reportsMenu.DropDownItems.Add("Dealer Summary", null, dealerSummaryToolStripMenuItem_Click);
            reportsMenu.DropDownItems.Add("Stock Month", null, stockMonthToolStripMenuItem_Click);
            reportsMenu.DropDownItems.Add("Top Transaction", null, topTransactionToolStripMenuItem_Click);
            reportsMenu.DropDownItems.Add("Exchange Rate", null, exchangeRateToolStripMenuItem_Click);
            menuStripTop.Items.Add(reportsMenu);
			_logger.LogDebug("<<< Exiting BuildReportsMenu()");
        }

        private void revenuePerProductToolStripMenuItem_Click(object sender, EventArgs e)
        {
			_logger.LogDebug(">>> Entering revenuePerProductToolStripMenuItem_Click(sender={Sender},e={E})", sender, e);
            reportsBLL reports = new reportsBLL();
            MessageBox.Show(reports.CalculateRevenuePerProduct(0));
			_logger.LogDebug("<<< Exiting revenuePerProductToolStripMenuItem_Click(sender={Sender},e={E})", sender, e);
        }

        private void dealerSummaryToolStripMenuItem_Click(object sender, EventArgs e)
        {
			_logger.LogDebug(">>> Entering dealerSummaryToolStripMenuItem_Click(sender={Sender},e={E})", sender, e);
            reportsBLL reports = new reportsBLL();
            MessageBox.Show(reports.BuildDealerSummary("DEA-9999"));
			_logger.LogDebug("<<< Exiting dealerSummaryToolStripMenuItem_Click(sender={Sender},e={E})", sender, e);
        }

        private void stockMonthToolStripMenuItem_Click(object sender, EventArgs e)
        {
			_logger.LogDebug(">>> Entering stockMonthToolStripMenuItem_Click(sender={Sender},e={E})", sender, e);
            reportsBLL reports = new reportsBLL();
            MessageBox.Show(reports.ParseStockMonth("august"));
			_logger.LogDebug("<<< Exiting stockMonthToolStripMenuItem_Click(sender={Sender},e={E})", sender, e);
        }

        private void topTransactionToolStripMenuItem_Click(object sender, EventArgs e)
        {
			_logger.LogDebug(">>> Entering topTransactionToolStripMenuItem_Click(sender={Sender},e={E})", sender, e);
            reportsBLL reports = new reportsBLL();
            MessageBox.Show(reports.SelectTopTransaction(7));
			_logger.LogDebug("<<< Exiting topTransactionToolStripMenuItem_Click(sender={Sender},e={E})", sender, e);
        }

        private void exchangeRateToolStripMenuItem_Click(object sender, EventArgs e)
        {
			_logger.LogDebug(">>> Entering exchangeRateToolStripMenuItem_Click(sender={Sender},e={E})", sender, e);
            reportsBLL reports = new reportsBLL();
            MessageBox.Show(reports.ResolveExchangeRate("USD"));
			_logger.LogDebug("<<< Exiting exchangeRateToolStripMenuItem_Click(sender={Sender},e={E})", sender, e);
        }

        private void usersToolStripMenuItem_Click(object sender, EventArgs e)
        {
			_logger.LogDebug(">>> Entering usersToolStripMenuItem_Click(sender={Sender},e={E})", sender, e);
            frmUsers user = new frmUsers();
            user.Show();
			_logger.LogDebug("<<< Exiting usersToolStripMenuItem_Click(sender={Sender},e={E})", sender, e);
        }

        private void frmAdminDashboard_FormClosed(object sender, FormClosedEventArgs e)
        {
			_logger.LogDebug(">>> Entering frmAdminDashboard_FormClosed(sender={Sender},e={E})", sender, e);
            frmLogin login = new frmLogin();
            login.Show();
            this.Hide();
			_logger.LogDebug("<<< Exiting frmAdminDashboard_FormClosed(sender={Sender},e={E})", sender, e);
        }

        private void frmAdminDashboard_Load(object sender, EventArgs e)
        {
			_logger.LogDebug(">>> Entering frmAdminDashboard_Load(sender={Sender},e={E})", sender, e);
			_logger.LogDebug("frmAdminDashboard_Load(sender={Sender},e={E}): lblLoggedInUser.Text → {Arg2}", sender, e, lblLoggedInUser.Text);
            lblLoggedInUser.Text = frmLogin.loggedIn;
			_logger.LogDebug("<<< Exiting frmAdminDashboard_Load(sender={Sender},e={E})", sender, e);
        }

        private void categoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
			_logger.LogDebug(">>> Entering categoryToolStripMenuItem_Click(sender={Sender},e={E})", sender, e);
            frmCategories category = new frmCategories();
            category.Show();
			_logger.LogDebug("<<< Exiting categoryToolStripMenuItem_Click(sender={Sender},e={E})", sender, e);
        }

        private void productsToolStripMenuItem_Click(object sender, EventArgs e)
        {
			_logger.LogDebug(">>> Entering productsToolStripMenuItem_Click(sender={Sender},e={E})", sender, e);
            frmProducts product = new frmProducts();
            product.Show();
			_logger.LogDebug("<<< Exiting productsToolStripMenuItem_Click(sender={Sender},e={E})", sender, e);
        }

        private void deealerAndCustomerToolStripMenuItem_Click(object sender, EventArgs e)
        {
			_logger.LogDebug(">>> Entering deealerAndCustomerToolStripMenuItem_Click(sender={Sender},e={E})", sender, e);
            frmDeaCust DeaCust = new frmDeaCust();
            DeaCust.Show();
			_logger.LogDebug("<<< Exiting deealerAndCustomerToolStripMenuItem_Click(sender={Sender},e={E})", sender, e);
        }

        private void transactionToolStripMenuItem_Click(object sender, EventArgs e)
        {
			_logger.LogDebug(">>> Entering transactionToolStripMenuItem_Click(sender={Sender},e={E})", sender, e);
            frmTransactions transaction = new frmTransactions();
            transaction.Show();
			_logger.LogDebug("<<< Exiting transactionToolStripMenuItem_Click(sender={Sender},e={E})", sender, e);
        }

        private void inventoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
			_logger.LogDebug(">>> Entering inventoryToolStripMenuItem_Click(sender={Sender},e={E})", sender, e);
            frmInventory inventory = new frmInventory();
            inventory.Show();
			_logger.LogDebug("<<< Exiting inventoryToolStripMenuItem_Click(sender={Sender},e={E})", sender, e);
        }
    }
}
