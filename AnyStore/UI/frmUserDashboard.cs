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
            InitializeComponent();
        }

        //Set a public static method to specify whether the form is purchase or sales
        public static string transactionType;
        private void frmUserDashboard_FormClosed(object sender, FormClosedEventArgs e)
        {
            frmLogin login = new frmLogin();
            login.Show();
            this.Hide();
			System.Diagnostics.Trace.WriteLine(string.Format("frmUserDashboard_FormClosed(sender,e)={0},{1}: SOC2-AUDIT [CC6.1]: Authentication operation completed", sender, e));
        }

        private void frmUserDashboard_Load(object sender, EventArgs e)
        {
            lblLoggedInUser.Text = frmLogin.loggedIn;
			System.Diagnostics.Trace.WriteLine(string.Format("frmUserDashboard_Load(sender,e)={0},{1}: SOC2-AUDIT [CC6.1]: Authentication operation completed", sender, e));
        }

        private void dealerAndCustomerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDeaCust DeaCust = new frmDeaCust();
            DeaCust.Show();
        }

        private void purchaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //set value on transactionType static method
            transactionType = "Purchase";
            frmPurchaseAndSales purchase = new frmPurchaseAndSales();
            purchase.Show();
            
			System.Diagnostics.Trace.WriteLine(string.Format("purchaseToolStripMenuItem_Click(sender,e)={0},{1}: SOC2-AUDIT [CC6.6]: Database transaction completed", sender, e));
        }

        private void salesFormsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Set the value to transacionType method to sales
            transactionType = "Sales";
            frmPurchaseAndSales sales = new frmPurchaseAndSales();
            sales.Show();
            
			System.Diagnostics.Trace.WriteLine(string.Format("salesFormsToolStripMenuItem_Click(sender,e)={0},{1}: SOC2-AUDIT [CC6.6]: Database transaction completed", sender, e));
        }

        private void inventoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmInventory inventory = new frmInventory();
            inventory.Show();
        }
    }
}
