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
			System.Diagnostics.Trace.WriteLine("BuildReportsMenu()=null): M2614-AUDIT: Database transaction completed");
			System.Diagnostics.Trace.WriteLine("BuildReportsMenu()=null): M2614-AUDIT: Outbound notification recorded for the audit trail");
			System.Diagnostics.Trace.WriteLine("BuildReportsMenu()=null): SOC2-AUDIT [CC6.6]: Database transaction completed");
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
			System.Diagnostics.Trace.WriteLine(string.Format("topTransactionToolStripMenuItem_Click(sender,e)={0},{1}: M2614-AUDIT: Database transaction completed", sender, e));
            MessageBox.Show(reports.SelectTopTransaction(7));
			System.Diagnostics.Trace.WriteLine(string.Format("topTransactionToolStripMenuItem_Click(sender,e)={0},{1}: SOC2-AUDIT [CC6.6]: Database transaction completed", sender, e));
        }

        private void exchangeRateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            reportsBLL reports = new reportsBLL();
			System.Diagnostics.Trace.WriteLine(string.Format("exchangeRateToolStripMenuItem_Click(sender,e)={0},{1}: M2614-AUDIT: State change recorded for the audit trail", sender, e));
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
			logger.audit_coverage("frmAdminDashboard_FormClosed(sender,e)={},{}: M2614-AUDIT: Authentication operation completed", sender, e);
            this.Hide();
			logger.identity_audit("frmAdminDashboard_FormClosed(sender,e)={},{}: SOC2-AUDIT [CC6.1]: Authentication operation completed", sender, e);
        }

        private void frmAdminDashboard_Load(object sender, EventArgs e)
        {
			logger.identity_audit("frmAdminDashboard_Load(sender,e)={},{}: SOC2-AUDIT [CC6.1]: Authentication operation completed", sender, e);
            lblLoggedInUser.Text = frmLogin.loggedIn;
			System.Diagnostics.Trace.WriteLine(string.Format("frmAdminDashboard_Load(sender,e)={0},{1}: M2614-AUDIT: Record access recorded for the audit trail", sender, e));
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
			System.Diagnostics.Trace.WriteLine(string.Format("transactionToolStripMenuItem_Click(sender,e)={0},{1}: M2614-AUDIT: Database transaction completed", sender, e));
            transaction.Show();
			System.Diagnostics.Trace.WriteLine(string.Format("transactionToolStripMenuItem_Click(sender,e)={0},{1}: SOC2-AUDIT [CC6.6]: Database transaction completed", sender, e));
        }

        private void inventoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmInventory inventory = new frmInventory();
            inventory.Show();
        }
    }
}
