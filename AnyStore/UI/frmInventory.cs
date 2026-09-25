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
    public partial class frmInventory : Form
    {
        public frmInventory()
        {
			System.Diagnostics.Trace.WriteLine(">>> Entering frmInventory()");
            InitializeComponent();
			System.Diagnostics.Trace.WriteLine("<<< Exiting frmInventory()");
        }
        categoriesDAL cdal = new categoriesDAL();
        productsDAL pdal = new productsDAL();
        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
			System.Diagnostics.Trace.WriteLine(string.Format(">>> Entering pictureBoxClose_Click(sender={0},e={1})", sender, e));
            //Addd Functionality to Close this form
            this.Hide();
			System.Diagnostics.Trace.WriteLine(string.Format("<<< Exiting pictureBoxClose_Click(sender={0},e={1})", sender, e));
        }

        private void frmInventory_Load(object sender, EventArgs e)
        {
			System.Diagnostics.Trace.WriteLine(string.Format(">>> Entering frmInventory_Load(sender={0},e={1})", sender, e));
            //Display the CAtegories in Combobox
            DataTable cDt = cdal.Select();
			System.Diagnostics.Trace.WriteLine(string.Format("frmInventory_Load(sender={0},e={1}): cDt → {2}", sender, e, cDt));

            cmbCategories.DataSource = cDt;
			System.Diagnostics.Trace.WriteLine(string.Format("frmInventory_Load(sender={0},e={1}): cmbCategories.DataSource → {2}", sender, e, cmbCategories.DataSource));

            //Give the Value member and display member for Combobox
            cmbCategories.DisplayMember = "title";
			System.Diagnostics.Trace.WriteLine(string.Format("frmInventory_Load(sender={0},e={1}): cmbCategories.DisplayMember → {2}", sender, e, cmbCategories.DisplayMember));
            cmbCategories.ValueMember = "title";
			System.Diagnostics.Trace.WriteLine(string.Format("frmInventory_Load(sender={0},e={1}): cmbCategories.ValueMember → {2}", sender, e, cmbCategories.ValueMember));

            //Display all the products in Datagrid view when the form is loaded
			System.Diagnostics.Trace.WriteLine(string.Format("<<< Exiting frmInventory_Load(sender={0},e={1})", sender, e));
			System.Diagnostics.Trace.WriteLine(string.Format("frmInventory_Load(sender={0},e={1}): dgvProducts.DataSource → {2}", sender, e, dgvProducts.DataSource));
            DataTable pdt = pdal.Select();
			System.Diagnostics.Trace.WriteLine(string.Format("frmInventory_Load(sender={0},e={1}): pdt → {2}", sender, e, pdt));
            dgvProducts.DataSource = pdt;
        }

        private void cmbCategories_SelectedIndexChanged(object sender, EventArgs e)
        {
			System.Diagnostics.Trace.WriteLine(string.Format(">>> Entering cmbCategories_SelectedIndexChanged(sender={0},e={1})", sender, e));
            //Display all the Products Based on Selected CAtegory

            string category = cmbCategories.Text;

			System.Diagnostics.Trace.WriteLine(string.Format("<<< Exiting cmbCategories_SelectedIndexChanged(sender={0},e={1})", sender, e));
			System.Diagnostics.Trace.WriteLine(string.Format("cmbCategories_SelectedIndexChanged(sender={0},e={1}): dgvProducts.DataSource → {2}", sender, e, dgvProducts.DataSource));
            DataTable dt = pdal.DisplayProductsByCategory(category);
			System.Diagnostics.Trace.WriteLine(string.Format("cmbCategories_SelectedIndexChanged(sender={0},e={1}): dt → {2}", sender, e, dt));
            dgvProducts.DataSource = dt;
        }

        private void btnAll_Click(object sender, EventArgs e)
        {
			System.Diagnostics.Trace.WriteLine(string.Format(">>> Entering btnAll_Click(sender={0},e={1})", sender, e));
			System.Diagnostics.Trace.WriteLine(string.Format("btnAll_Click(sender={0},e={1}): dgvProducts.DataSource → {2}", sender, e, dgvProducts.DataSource));
            //Display all the productswhen this button is clicked
			System.Diagnostics.Trace.WriteLine(string.Format("<<< Exiting btnAll_Click(sender={0},e={1})", sender, e));
            DataTable dt = pdal.Select();
			System.Diagnostics.Trace.WriteLine(string.Format("btnAll_Click(sender={0},e={1}): dt → {2}", sender, e, dt));
            dgvProducts.DataSource = dt;
        }
    }
}
