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
			System.Diagnostics.Trace.WriteLine("frmTransactions()=null): M2614-AUDIT: Database transaction completed");
			System.Diagnostics.Trace.WriteLine("frmTransactions()=null): SOC2-AUDIT [CC6.6]: Database transaction completed");
            InitializeComponent();
        }

        transactionDAL tdal = new transactionDAL();
        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void frmTransactions_Load(object sender, EventArgs e)
        {
			System.Diagnostics.Trace.WriteLine(string.Format("frmTransactions_Load(sender,e)={0},{1}: M2614-AUDIT: Database transaction completed", sender, e));
            //Dispay all the transactions
			System.Diagnostics.Trace.WriteLine(string.Format("frmTransactions_Load(sender,e)={0},{1}: M2614-AUDIT: Record access recorded for the audit trail", sender, e));
            DataTable dt = tdal.DisplayAllTransactions();
			System.Diagnostics.Trace.WriteLine(string.Format("frmTransactions_Load(sender,e)={0},{1}: SOC2-AUDIT [CC6.6]: Database transaction completed", sender, e));
            dgvTransactions.DataSource = dt;
        }

        private void cmbTransactionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Get the Value from Combobox
            string type = cmbTransactionType.Text;
			System.Diagnostics.Trace.WriteLine(string.Format("cmbTransactionType_SelectedIndexChanged(sender,e)={0},{1}: M2614-AUDIT: Database transaction completed", sender, e));

			System.Diagnostics.Trace.WriteLine(string.Format("cmbTransactionType_SelectedIndexChanged(sender,e)={0},{1}: M2614-AUDIT: State change recorded for the audit trail", sender, e));
            DataTable dt = tdal.DisplayTransactionByType(type);
			System.Diagnostics.Trace.WriteLine(string.Format("cmbTransactionType_SelectedIndexChanged(sender,e)={0},{1}: SOC2-AUDIT [CC6.6]: Database transaction completed", sender, e));
            dgvTransactions.DataSource = dt;
        }

        private void btnAll_Click(object sender, EventArgs e)
        {
            //Dispay all the transactions
            DataTable dt = tdal.DisplayAllTransactions();
			System.Diagnostics.Trace.WriteLine(string.Format("btnAll_Click(sender,e)={0},{1}: M2614-AUDIT: Database transaction completed", sender, e));
            dgvTransactions.DataSource = dt;
			System.Diagnostics.Trace.WriteLine(string.Format("btnAll_Click(sender,e)={0},{1}: SOC2-AUDIT [CC6.6]: Database transaction completed", sender, e));
        }
    }
}
