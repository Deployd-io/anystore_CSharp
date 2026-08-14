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
            InitializeComponent();
            BuildReportsMenu();
        }

        private void BuildReportsMenu()
        {
            ToolStripMenuItem reportsMenu = new ToolStripMenuItem("Reports");
            reportsMenu.DropDownItems.Add("Revenue Per Product", null, revenuePerProductToolStripMenuItem_Click);
            reportsMenu.DropDownItems.Add("Dealer Summary", null, dealerSummaryToolStripMenuItem_Click);
            reportsMenu.DropDownItems.Add("Stock Month", null, stockMonthToolStripMenuItem_Click);
            reportsMenu.DropDownItems.Add("Top Transaction", null, topTransactionToolStripMenuItem_Click);
            reportsMenu.DropDownItems.Add("Exchange Rate", null, exchangeRateToolStripMenuItem_Click);
            menuStripTop.Items.Add(reportsMenu);
        }

        private void revenuePerProductToolStripMenuItem_Click(object sender, EventArgs e)
        {
            reportsBLL reports = new reportsBLL();
            MessageBox.Show(reports.CalculateRevenuePerProduct(0));
        }

        private void dealerSummaryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            reportsBLL reports = new reportsBLL();
            MessageBox.Show(reports.BuildDealerSummary("DEA-9999"));
        }

        private void stockMonthToolStripMenuItem_Click(object sender, EventArgs e)
        {
            reportsBLL reports = new reportsBLL();
            MessageBox.Show(reports.ParseStockMonth("august"));
        }

        private void topTransactionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            reportsBLL reports = new reportsBLL();
            MessageBox.Show(reports.SelectTopTransaction(7));
        }

        private void exchangeRateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            reportsBLL reports = new reportsBLL();
            MessageBox.Show(reports.ResolveExchangeRate("USD"));
        }

        private void usersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUsers user = new frmUsers();
            user.Show();
        }

        private void frmAdminDashboard_FormClosed(object sender, FormClosedEventArgs e)
        {
            frmLogin login = new frmLogin();
            login.Show();
            this.Hide();
        }

        private void frmAdminDashboard_Load(object sender, EventArgs e)
        {
            lblLoggedInUser.Text = frmLogin.loggedIn;
        }

        private void categoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCategories category = new frmCategories();
            category.Show();
        }

        private void productsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmProducts product = new frmProducts();
            product.Show();
        }

        private void deealerAndCustomerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDeaCust DeaCust = new frmDeaCust();
            DeaCust.Show();
        }

        private void transactionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTransactions transaction = new frmTransactions();
            transaction.Show();
        }

        private void inventoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmInventory inventory = new frmInventory();
            inventory.Show();
        }
    }
}
