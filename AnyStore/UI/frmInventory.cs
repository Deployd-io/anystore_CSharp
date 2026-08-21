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
    public partial class frmInventory : Form
    {
        public frmInventory()
        {
			_logger.LogDebug(">>> Entering frmInventory()");
            InitializeComponent();
			_logger.LogDebug("<<< Exiting frmInventory()");
        }
        categoriesDAL cdal = new categoriesDAL();
        productsDAL pdal = new productsDAL();
        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
			_logger.LogDebug(">>> Entering pictureBoxClose_Click(sender={Sender},e={E})", sender, e);
            //Addd Functionality to Close this form
            this.Hide();
			_logger.LogDebug("<<< Exiting pictureBoxClose_Click(sender={Sender},e={E})", sender, e);
        }

        private void frmInventory_Load(object sender, EventArgs e)
        {
			_logger.LogDebug(">>> Entering frmInventory_Load(sender={Sender},e={E})", sender, e);
            //Display the CAtegories in Combobox
            DataTable cDt = cdal.Select();
			_logger.LogDebug("frmInventory_Load(sender={Sender},e={E}): cDt → {CDt}", sender, e, cDt);

            cmbCategories.DataSource = cDt;
			_logger.LogDebug("frmInventory_Load(sender={Sender},e={E}): cmbCategories.DataSource → {Arg2}", sender, e, cmbCategories.DataSource);

            //Give the Value member and display member for Combobox
            cmbCategories.DisplayMember = "title";
			_logger.LogDebug("frmInventory_Load(sender={Sender},e={E}): cmbCategories.DisplayMember → {Arg2}", sender, e, cmbCategories.DisplayMember);
			_logger.LogDebug("frmInventory_Load(sender={Sender},e={E}): cmbCategories.ValueMember → {Arg2}", sender, e, cmbCategories.ValueMember);
            cmbCategories.ValueMember = "title";

            //Display all the products in Datagrid view when the form is loaded
			_logger.LogDebug("<<< Exiting frmInventory_Load(sender={Sender},e={E})", sender, e);
			_logger.LogDebug("frmInventory_Load(sender={Sender},e={E}): dgvProducts.DataSource → {Arg2}", sender, e, dgvProducts.DataSource);
            DataTable pdt = pdal.Select();
			_logger.LogDebug("frmInventory_Load(sender={Sender},e={E}): pdt → {Pdt}", sender, e, pdt);
            dgvProducts.DataSource = pdt;
        }

        private void cmbCategories_SelectedIndexChanged(object sender, EventArgs e)
        {
			_logger.LogDebug(">>> Entering cmbCategories_SelectedIndexChanged(sender={Sender},e={E})", sender, e);
            //Display all the Products Based on Selected CAtegory

            string category = cmbCategories.Text;

			_logger.LogDebug("<<< Exiting cmbCategories_SelectedIndexChanged(sender={Sender},e={E})", sender, e);
			_logger.LogDebug("cmbCategories_SelectedIndexChanged(sender={Sender},e={E}): dgvProducts.DataSource → {Arg2}", sender, e, dgvProducts.DataSource);
            DataTable dt = pdal.DisplayProductsByCategory(category);
			_logger.LogDebug("cmbCategories_SelectedIndexChanged(sender={Sender},e={E}): dt → {Dt}", sender, e, dt);
            dgvProducts.DataSource = dt;
        }

        private void btnAll_Click(object sender, EventArgs e)
        {
			_logger.LogDebug(">>> Entering btnAll_Click(sender={Sender},e={E})", sender, e);
			_logger.LogDebug("btnAll_Click(sender={Sender},e={E}): dgvProducts.DataSource → {Arg2}", sender, e, dgvProducts.DataSource);
            //Display all the productswhen this button is clicked
			_logger.LogDebug("<<< Exiting btnAll_Click(sender={Sender},e={E})", sender, e);
            DataTable dt = pdal.Select();
			_logger.LogDebug("btnAll_Click(sender={Sender},e={E}): dt → {Dt}", sender, e, dt);
            dgvProducts.DataSource = dt;
        }
    }
}
