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
using Microsoft.Extensions.Logging;

namespace AnyStore.UI
{
	private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    public partial class frmTransactions : Form
    {
        public frmTransactions()
        {
			_logger.LogDebug(">>> Entering frmTransactions()");
            InitializeComponent();
			_logger.LogDebug("<<< Exiting frmTransactions()");
        }

        transactionDAL tdal = new transactionDAL();
        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
			_logger.LogDebug(">>> Entering pictureBoxClose_Click(sender={Sender},e={E})", sender, e);
            this.Hide();
			_logger.LogDebug("<<< Exiting pictureBoxClose_Click(sender={Sender},e={E})", sender, e);
        }

        private void frmTransactions_Load(object sender, EventArgs e)
        {
			_logger.LogDebug(">>> Entering frmTransactions_Load(sender={Sender},e={E})", sender, e);
			_logger.LogDebug("frmTransactions_Load(sender={Sender},e={E}): dgvTransactions.DataSource → {Arg2}", sender, e, dgvTransactions.DataSource);
            //Dispay all the transactions
			_logger.LogDebug("<<< Exiting frmTransactions_Load(sender={Sender},e={E})", sender, e);
            DataTable dt = tdal.DisplayAllTransactions();
			_logger.LogDebug("frmTransactions_Load(sender={Sender},e={E}): dt → {Dt}", sender, e, dt);
            dgvTransactions.DataSource = dt;
        }

        private void cmbTransactionType_SelectedIndexChanged(object sender, EventArgs e)
        {
			_logger.LogDebug(">>> Entering cmbTransactionType_SelectedIndexChanged(sender={Sender},e={E})", sender, e);
            //Get the Value from Combobox
            string type = cmbTransactionType.Text;

			_logger.LogDebug("<<< Exiting cmbTransactionType_SelectedIndexChanged(sender={Sender},e={E})", sender, e);
			_logger.LogDebug("cmbTransactionType_SelectedIndexChanged(sender={Sender},e={E}): dgvTransactions.DataSource → {Arg2}", sender, e, dgvTransactions.DataSource);
            DataTable dt = tdal.DisplayTransactionByType(type);
			_logger.LogDebug("cmbTransactionType_SelectedIndexChanged(sender={Sender},e={E}): dt → {Dt}", sender, e, dt);
            dgvTransactions.DataSource = dt;
        }

        private void btnAll_Click(object sender, EventArgs e)
        {
			_logger.LogDebug(">>> Entering btnAll_Click(sender={Sender},e={E})", sender, e);
			_logger.LogDebug("btnAll_Click(sender={Sender},e={E}): dgvTransactions.DataSource → {Arg2}", sender, e, dgvTransactions.DataSource);
            //Dispay all the transactions
			_logger.LogDebug("<<< Exiting btnAll_Click(sender={Sender},e={E})", sender, e);
            DataTable dt = tdal.DisplayAllTransactions();
			_logger.LogDebug("btnAll_Click(sender={Sender},e={E}): dt → {Dt}", sender, e, dt);
            dgvTransactions.DataSource = dt;
        }
    }
}
