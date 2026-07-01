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
			Console.WriteLine(">>> Entering frmTransactions()");
            InitializeComponent();
			Console.WriteLine("<<< Exiting frmTransactions()");
        }

        transactionDAL tdal = new transactionDAL();
        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
			Console.WriteLine(">>> Entering pictureBoxClose_Click(sender={0},e={1})", sender, e);
            this.Hide();
			Console.WriteLine("<<< Exiting pictureBoxClose_Click(sender={0},e={1})", sender, e);
        }

        private void frmTransactions_Load(object sender, EventArgs e)
        {
			Console.WriteLine(">>> Entering frmTransactions_Load(sender={0},e={1})", sender, e);
            //Dispay all the transactions
            DataTable dt = tdal.DisplayAllTransactions();
            dgvTransactions.DataSource = dt;
			Console.WriteLine("<<< Exiting frmTransactions_Load(sender={0},e={1})", sender, e);
			Console.WriteLine("frmTransactions_Load(sender={0},e={1}): dt → {2}", sender, e, dt);
			Console.WriteLine("frmTransactions_Load(sender={0},e={1}): dgvTransactions.DataSource → {2}", sender, e, dgvTransactions.DataSource);
        }

        private void cmbTransactionType_SelectedIndexChanged(object sender, EventArgs e)
        {
			Console.WriteLine(">>> Entering cmbTransactionType_SelectedIndexChanged(sender={0},e={1})", sender, e);
            //Get the Value from Combobox
            string type = cmbTransactionType.Text;

            DataTable dt = tdal.DisplayTransactionByType(type);
            dgvTransactions.DataSource = dt;
			Console.WriteLine("<<< Exiting cmbTransactionType_SelectedIndexChanged(sender={0},e={1})", sender, e);
			Console.WriteLine("cmbTransactionType_SelectedIndexChanged(sender={0},e={1}): dt → {2}", sender, e, dt);
			Console.WriteLine("cmbTransactionType_SelectedIndexChanged(sender={0},e={1}): dgvTransactions.DataSource → {2}", sender, e, dgvTransactions.DataSource);
        }

        private void btnAll_Click(object sender, EventArgs e)
        {
			Console.WriteLine(">>> Entering btnAll_Click(sender={0},e={1})", sender, e);
            //Dispay all the transactions
            DataTable dt = tdal.DisplayAllTransactions();
            dgvTransactions.DataSource = dt;
			Console.WriteLine("<<< Exiting btnAll_Click(sender={0},e={1})", sender, e);
			Console.WriteLine("btnAll_Click(sender={0},e={1}): dt → {2}", sender, e, dt);
			Console.WriteLine("btnAll_Click(sender={0},e={1}): dgvTransactions.DataSource → {2}", sender, e, dgvTransactions.DataSource);
        }
    }
}
