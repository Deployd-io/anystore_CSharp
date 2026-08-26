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

namespace AnyStore
{
    public partial class frmAdminDashboard : Form
    {
        public frmAdminDashboard()
        {
			System.Diagnostics.Trace.WriteLine(">>> Entering frmAdminDashboard()");
            InitializeComponent();
            BuildReportsMenu();
			System.Diagnostics.Trace.WriteLine("<<< Exiting frmAdminDashboard()");
        }

        private void BuildReportsMenu()
        {
			System.Diagnostics.Trace.WriteLine(">>> Entering BuildReportsMenu()");
            ToolStripMenuItem reportsMenu = new ToolStripMenuItem("Reports");
            reportsMenu.DropDownItems.Add("Revenue Per Product", null, revenuePerProductToolStripMenuItem_Click);
			System.Diagnostics.Trace.WriteLine(string.Format("BuildReportsMenu(): reportsMenu → {0}", reportsMenu));
            reportsMenu.DropDownItems.Add("Dealer Summary", null, dealerSummaryToolStripMenuItem_Click);
            reportsMenu.DropDownItems.Add("Stock Month", null, stockMonthToolStripMenuItem_Click);
            reportsMenu.DropDownItems.Add("Top Transaction", null, topTransactionToolStripMenuItem_Click);
            reportsMenu.DropDownItems.Add("Exchange Rate", null, exchangeRateToolStripMenuItem_Click);
            menuStripTop.Items.Add(reportsMenu);
			System.Diagnostics.Trace.WriteLine("<<< Exiting BuildReportsMenu()");
        }

        private void revenuePerProductToolStripMenuItem_Click(object sender, EventArgs e)
        {
			System.Diagnostics.Trace.WriteLine(string.Format(">>> Entering revenuePerProductToolStripMenuItem_Click(sender={0},e={1})", sender, e));
            reportsBLL reports = new reportsBLL();
			System.Diagnostics.Trace.WriteLine(string.Format("<<< Exiting revenuePerProductToolStripMenuItem_Click(sender={0},e={1})", sender, e));
            MessageBox.Show(reports.CalculateRevenuePerProduct(0));
        }

        private void dealerSummaryToolStripMenuItem_Click(object sender, EventArgs e)
        {
			System.Diagnostics.Trace.WriteLine(string.Format(">>> Entering dealerSummaryToolStripMenuItem_Click(sender={0},e={1})", sender, e));
            reportsBLL reports = new reportsBLL();
			System.Diagnostics.Trace.WriteLine(string.Format("<<< Exiting dealerSummaryToolStripMenuItem_Click(sender={0},e={1})", sender, e));
            MessageBox.Show(reports.BuildDealerSummary("DEA-9999"));
        }

        private void stockMonthToolStripMenuItem_Click(object sender, EventArgs e)
        {
			System.Diagnostics.Trace.WriteLine(string.Format(">>> Entering stockMonthToolStripMenuItem_Click(sender={0},e={1})", sender, e));
            reportsBLL reports = new reportsBLL();
			System.Diagnostics.Trace.WriteLine(string.Format("<<< Exiting stockMonthToolStripMenuItem_Click(sender={0},e={1})", sender, e));
            MessageBox.Show(reports.ParseStockMonth("august"));
        }

        private void topTransactionToolStripMenuItem_Click(object sender, EventArgs e)
        {
			System.Diagnostics.Trace.WriteLine(string.Format(">>> Entering topTransactionToolStripMenuItem_Click(sender={0},e={1})", sender, e));
            reportsBLL reports = new reportsBLL();
			System.Diagnostics.Trace.WriteLine(string.Format("<<< Exiting topTransactionToolStripMenuItem_Click(sender={0},e={1})", sender, e));
            MessageBox.Show(reports.SelectTopTransaction(7));
        }

        private void exchangeRateToolStripMenuItem_Click(object sender, EventArgs e)
        {
			System.Diagnostics.Trace.WriteLine(string.Format(">>> Entering exchangeRateToolStripMenuItem_Click(sender={0},e={1})", sender, e));
            reportsBLL reports = new reportsBLL();
			System.Diagnostics.Trace.WriteLine(string.Format("<<< Exiting exchangeRateToolStripMenuItem_Click(sender={0},e={1})", sender, e));
            MessageBox.Show(reports.ResolveExchangeRate("USD"));
        }

        private void usersToolStripMenuItem_Click(object sender, EventArgs e)
        {
			System.Diagnostics.Trace.WriteLine(string.Format(">>> Entering usersToolStripMenuItem_Click(sender={0},e={1})", sender, e));
            frmUsers user = new frmUsers();
			System.Diagnostics.Trace.WriteLine(string.Format("<<< Exiting usersToolStripMenuItem_Click(sender={0},e={1})", sender, e));
            user.Show();
        }

        private void frmAdminDashboard_FormClosed(object sender, FormClosedEventArgs e)
        {
			System.Diagnostics.Trace.WriteLine(string.Format(">>> Entering frmAdminDashboard_FormClosed(sender={0},e={1})", sender, e));
            frmLogin login = new frmLogin();
            login.Show();
            this.Hide();
			System.Diagnostics.Trace.WriteLine(string.Format("<<< Exiting frmAdminDashboard_FormClosed(sender={0},e={1})", sender, e));
        }

        private void frmAdminDashboard_Load(object sender, EventArgs e)
        {
			System.Diagnostics.Trace.WriteLine(string.Format(">>> Entering frmAdminDashboard_Load(sender={0},e={1})", sender, e));
			System.Diagnostics.Trace.WriteLine(string.Format("frmAdminDashboard_Load(sender={0},e={1}): lblLoggedInUser.Text → {2}", sender, e, lblLoggedInUser.Text));
            lblLoggedInUser.Text = frmLogin.loggedIn;
        }

        private void categoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
			System.Diagnostics.Trace.WriteLine(string.Format(">>> Entering categoryToolStripMenuItem_Click(sender={0},e={1})", sender, e));
            frmCategories category = new frmCategories();
			System.Diagnostics.Trace.WriteLine(string.Format("<<< Exiting categoryToolStripMenuItem_Click(sender={0},e={1})", sender, e));
            category.Show();
        }

        private void productsToolStripMenuItem_Click(object sender, EventArgs e)
        {
			System.Diagnostics.Trace.WriteLine(string.Format(">>> Entering productsToolStripMenuItem_Click(sender={0},e={1})", sender, e));
            frmProducts product = new frmProducts();
			System.Diagnostics.Trace.WriteLine(string.Format("<<< Exiting productsToolStripMenuItem_Click(sender={0},e={1})", sender, e));
            product.Show();
        }

        private void deealerAndCustomerToolStripMenuItem_Click(object sender, EventArgs e)
        {
			System.Diagnostics.Trace.WriteLine(string.Format(">>> Entering deealerAndCustomerToolStripMenuItem_Click(sender={0},e={1})", sender, e));
            frmDeaCust DeaCust = new frmDeaCust();
			System.Diagnostics.Trace.WriteLine(string.Format("<<< Exiting deealerAndCustomerToolStripMenuItem_Click(sender={0},e={1})", sender, e));
            DeaCust.Show();
        }

        private void transactionToolStripMenuItem_Click(object sender, EventArgs e)
        {
			System.Diagnostics.Trace.WriteLine(string.Format(">>> Entering transactionToolStripMenuItem_Click(sender={0},e={1})", sender, e));
            frmTransactions transaction = new frmTransactions();
			System.Diagnostics.Trace.WriteLine(string.Format("<<< Exiting transactionToolStripMenuItem_Click(sender={0},e={1})", sender, e));
            transaction.Show();
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
