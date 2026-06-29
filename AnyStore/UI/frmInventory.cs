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
			Console.WriteLine(">>> Entering frmInventory()");
            InitializeComponent();
			Console.WriteLine("<<< Exiting frmInventory()");
        }
        categoriesDAL cdal = new categoriesDAL();
        productsDAL pdal = new productsDAL();
        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
			Console.WriteLine(">>> Entering pictureBoxClose_Click(sender={0},e={1})", sender, e);
            //Addd Functionality to Close this form
            this.Hide();
			Console.WriteLine("<<< Exiting pictureBoxClose_Click(sender={0},e={1})", sender, e);
        }

        private void frmInventory_Load(object sender, EventArgs e)
        {
			Console.WriteLine(">>> Entering frmInventory_Load(sender={0},e={1})", sender, e);
            //Display the CAtegories in Combobox
            DataTable cDt = cdal.Select();

			Console.WriteLine("frmInventory_Load(sender={0},e={1}): cDt → {2}", sender, e, cDt);
			Console.WriteLine("frmInventory_Load(sender={0},e={1}): cmbCategories.DataSource → {2}", sender, e, cmbCategories.DataSource);
            cmbCategories.DataSource = cDt;

            //Give the Value member and display member for Combobox
            cmbCategories.DisplayMember = "title";
			Console.WriteLine("frmInventory_Load(sender={0},e={1}): cmbCategories.DisplayMember → {2}", sender, e, cmbCategories.DisplayMember);
			Console.WriteLine("frmInventory_Load(sender={0},e={1}): cmbCategories.ValueMember → {2}", sender, e, cmbCategories.ValueMember);
            cmbCategories.ValueMember = "title";

            //Display all the products in Datagrid view when the form is loaded
            DataTable pdt = pdal.Select();
            dgvProducts.DataSource = pdt;
			Console.WriteLine("<<< Exiting frmInventory_Load(sender={0},e={1})", sender, e);
			Console.WriteLine("frmInventory_Load(sender={0},e={1}): pdt → {2}", sender, e, pdt);
			Console.WriteLine("frmInventory_Load(sender={0},e={1}): dgvProducts.DataSource → {2}", sender, e, dgvProducts.DataSource);
        }

        private void cmbCategories_SelectedIndexChanged(object sender, EventArgs e)
        {
			Console.WriteLine(">>> Entering cmbCategories_SelectedIndexChanged(sender={0},e={1})", sender, e);
            //Display all the Products Based on Selected CAtegory

            string category = cmbCategories.Text;

            DataTable dt = pdal.DisplayProductsByCategory(category);
            dgvProducts.DataSource = dt;
			Console.WriteLine("<<< Exiting cmbCategories_SelectedIndexChanged(sender={0},e={1})", sender, e);
			Console.WriteLine("cmbCategories_SelectedIndexChanged(sender={0},e={1}): dt → {2}", sender, e, dt);
			Console.WriteLine("cmbCategories_SelectedIndexChanged(sender={0},e={1}): dgvProducts.DataSource → {2}", sender, e, dgvProducts.DataSource);
        }

        private void btnAll_Click(object sender, EventArgs e)
        {
			Console.WriteLine(">>> Entering btnAll_Click(sender={0},e={1})", sender, e);
            //Display all the productswhen this button is clicked
            DataTable dt = pdal.Select();
            dgvProducts.DataSource = dt;
			Console.WriteLine("<<< Exiting btnAll_Click(sender={0},e={1})", sender, e);
			Console.WriteLine("btnAll_Click(sender={0},e={1}): dt → {2}", sender, e, dt);
			Console.WriteLine("btnAll_Click(sender={0},e={1}): dgvProducts.DataSource → {2}", sender, e, dgvProducts.DataSource);
        }
    }
}
