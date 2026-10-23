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
    public partial class frmTransactions : Form
    {
        public frmTransactions()
        {
			System.Diagnostics.Trace.WriteLine(">>> Entering frmTransactions()");
            InitializeComponent();
			System.Diagnostics.Trace.WriteLine("<<< Exiting frmTransactions()");
        }

        transactionDAL tdal = new transactionDAL();
        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
			System.Diagnostics.Trace.WriteLine(string.Format(">>> Entering pictureBoxClose_Click(sender={0},e={1})", sender, e));
            this.Hide();
			System.Diagnostics.Trace.WriteLine(string.Format("<<< Exiting pictureBoxClose_Click(sender={0},e={1})", sender, e));
        }

        private void frmTransactions_Load(object sender, EventArgs e)
        {
			System.Diagnostics.Trace.WriteLine(string.Format(">>> Entering frmTransactions_Load(sender={0},e={1})", sender, e));
			System.Diagnostics.Trace.WriteLine(string.Format("frmTransactions_Load(sender={0},e={1}): dgvTransactions.DataSource → {2}", sender, e, dgvTransactions.DataSource));
            //Dispay all the transactions
			System.Diagnostics.Trace.WriteLine(string.Format("<<< Exiting frmTransactions_Load(sender={0},e={1})", sender, e));
            DataTable dt = tdal.DisplayAllTransactions();
			System.Diagnostics.Trace.WriteLine(string.Format("frmTransactions_Load(sender={0},e={1}): dt → {2}", sender, e, dt));
            dgvTransactions.DataSource = dt;
        }

        private void cmbTransactionType_SelectedIndexChanged(object sender, EventArgs e)
        {
			System.Diagnostics.Trace.WriteLine(string.Format(">>> Entering cmbTransactionType_SelectedIndexChanged(sender={0},e={1})", sender, e));
            //Get the Value from Combobox
            string type = cmbTransactionType.Text;

			System.Diagnostics.Trace.WriteLine(string.Format("<<< Exiting cmbTransactionType_SelectedIndexChanged(sender={0},e={1})", sender, e));
			System.Diagnostics.Trace.WriteLine(string.Format("cmbTransactionType_SelectedIndexChanged(sender={0},e={1}): dgvTransactions.DataSource → {2}", sender, e, dgvTransactions.DataSource));
            DataTable dt = tdal.DisplayTransactionByType(type);
			System.Diagnostics.Trace.WriteLine(string.Format("cmbTransactionType_SelectedIndexChanged(sender={0},e={1}): dt → {2}", sender, e, dt));
            dgvTransactions.DataSource = dt;
        }

        private void btnAll_Click(object sender, EventArgs e)
        {
			System.Diagnostics.Trace.WriteLine(string.Format(">>> Entering btnAll_Click(sender={0},e={1})", sender, e));
			System.Diagnostics.Trace.WriteLine(string.Format("btnAll_Click(sender={0},e={1}): dgvTransactions.DataSource → {2}", sender, e, dgvTransactions.DataSource));
            //Dispay all the transactions
			System.Diagnostics.Trace.WriteLine(string.Format("<<< Exiting btnAll_Click(sender={0},e={1})", sender, e));
            DataTable dt = tdal.DisplayAllTransactions();
			System.Diagnostics.Trace.WriteLine(string.Format("btnAll_Click(sender={0},e={1}): dt → {2}", sender, e, dt));
            dgvTransactions.DataSource = dt;
        }
    }
}
