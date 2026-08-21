using AnyStore.BLL;
using AnyStore.DAL;
using DGVPrinterHelper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows.Forms;

namespace AnyStore.UI
{
    public partial class frmPurchaseAndSales : Form
    {
        public frmPurchaseAndSales()
        {
			_logger.LogDebug(">>> Entering frmPurchaseAndSales()");
            InitializeComponent();
			_logger.LogDebug("<<< Exiting frmPurchaseAndSales()");
        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
			_logger.LogDebug(">>> Entering pictureBoxClose_Click(sender={Sender},e={E})", sender, e);
            this.Hide();
			_logger.LogDebug("<<< Exiting pictureBoxClose_Click(sender={Sender},e={E})", sender, e);
        }
        DeaCustDAL dcDAL = new DeaCustDAL();
        productsDAL pDAL = new productsDAL();
        userDAL uDAL = new userDAL();
        transactionDAL tDAL = new transactionDAL();
        transactionDetailDAL tdDAL = new transactionDetailDAL();

        DataTable transactionDT = new DataTable();
        private void frmPurchaseAndSales_Load(object sender, EventArgs e)
        {
			_logger.LogDebug(">>> Entering frmPurchaseAndSales_Load(sender={Sender},e={E})", sender, e);
            //Get the transactionType value from frmUserDashboard
            string type = frmUserDashboard.transactionType;
            //Set the value on lblTop
            lblTop.Text = type;
			_logger.LogDebug("frmPurchaseAndSales_Load(sender={Sender},e={E}): lblTop.Text → {Arg2}", sender, e, lblTop.Text);

            //Specify Columns for our TransactionDataTable
            transactionDT.Columns.Add("Product Name");
            transactionDT.Columns.Add("Rate");
            transactionDT.Columns.Add("Quantity");
            transactionDT.Columns.Add("Total");
			_logger.LogDebug("<<< Exiting frmPurchaseAndSales_Load(sender={Sender},e={E})", sender, e);
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
			_logger.LogDebug(">>> Entering txtSearch_TextChanged(sender={Sender},e={E})", sender, e);
            //Get the keyword fro the text box
            string keyword = txtSearch.Text;

            if(keyword=="")
            {
                //Clear all the textboxes
                txtName.Text = "";
				_logger.LogDebug("txtSearch_TextChanged(sender={Sender},e={E}): txtName.Text → {Arg2}", sender, e, txtName.Text);
				_logger.LogDebug("txtSearch_TextChanged(sender={Sender},e={E}): txtEmail.Text → {Arg2}", sender, e, txtEmail.Text);
				_logger.LogDebug("txtSearch_TextChanged(sender={Sender},e={E}): txtContact.Text → {Arg2}", sender, e, txtContact.Text);
				_logger.LogDebug("txtSearch_TextChanged(sender={Sender},e={E}): txtAddress.Text → {Arg2}", sender, e, txtAddress.Text);
                txtEmail.Text = "";
                txtContact.Text = "";
                txtAddress.Text = "";
                return;
            }

            //Write the code to get the details and set the value on text boxes
            DeaCustBLL dc = dcDAL.SearchDealerCustomerForTransaction(keyword);
			_logger.LogDebug("txtSearch_TextChanged(sender={Sender},e={E}): dc → {Dc}", sender, e, dc);

            //Now transfer or set the value from DeCustBLL to textboxes
            txtName.Text = dc.name;
            txtEmail.Text = dc.email;
            txtContact.Text = dc.contact;
            txtAddress.Text = dc.address;
			_logger.LogDebug("<<< Exiting txtSearch_TextChanged(sender={Sender},e={E})", sender, e);
        }

        private void txtSearchProduct_TextChanged(object sender, EventArgs e)
        {
			_logger.LogDebug(">>> Entering txtSearchProduct_TextChanged(sender={Sender},e={E})", sender, e);
            //Get the keyword from productsearch textbox
            string keyword = txtSearchProduct.Text;

            //Check if we have value to txtSearchProduct or not
            if(keyword=="")
            {
                txtProductName.Text = "";
				_logger.LogDebug("txtSearchProduct_TextChanged(sender={Sender},e={E}): txtProductName.Text → {Arg2}", sender, e, txtProductName.Text);
				_logger.LogDebug("txtSearchProduct_TextChanged(sender={Sender},e={E}): txtInventory.Text → {Arg2}", sender, e, txtInventory.Text);
				_logger.LogDebug("txtSearchProduct_TextChanged(sender={Sender},e={E}): txtRate.Text → {Arg2}", sender, e, txtRate.Text);
				_logger.LogDebug("txtSearchProduct_TextChanged(sender={Sender},e={E}): TxtQty.Text → {Arg2}", sender, e, TxtQty.Text);
                txtInventory.Text = "";
                txtRate.Text = "";
                TxtQty.Text = "";
                return;
            }

            //Search the product and display on respective textboxes
            productsBLL p = pDAL.GetProductsForTransaction(keyword);

            //Set the values on textboxes based on p object
            txtProductName.Text = p.name;
            txtInventory.Text = p.qty.ToString();
            txtRate.Text = p.rate.ToString();
			_logger.LogDebug("<<< Exiting txtSearchProduct_TextChanged(sender={Sender},e={E})", sender, e);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
			_logger.LogDebug(">>> Entering btnAdd_Click(sender={Sender},e={E})", sender, e);
            //Get Product Name, Rate and Qty customer wants to buy
            string productName = txtProductName.Text;
            decimal Rate = decimal.Parse(txtRate.Text);
            decimal Qty = decimal.Parse(TxtQty.Text);

            decimal Total = Rate * Qty; //Total=RatexQty

            //Display the Subtotal in textbox
            //Get the subtotal value from textbox
            decimal subTotal = decimal.Parse(txtSubTotal.Text);
			_logger.LogDebug("btnAdd_Click(sender={Sender},e={E}): subTotal → {SubTotal}", sender, e, subTotal);
            subTotal = subTotal + Total;

            //Check whether the product is selected or not
            if(productName=="")
            {
                //Display error MEssage
                MessageBox.Show("Select the product first. Try Again.");
            }
            else
            {
                //Add product to the dAta Grid View
                transactionDT.Rows.Add(productName,Rate,Qty,Total);

                //Show in DAta Grid View
                dgvAddedProducts.DataSource = transactionDT;
				_logger.LogDebug("btnAdd_Click(sender={Sender},e={E}): dgvAddedProducts.DataSource → {Arg2}", sender, e, dgvAddedProducts.DataSource);
                //Display the Subtotal in textbox
                txtSubTotal.Text = subTotal.ToString();

                //Clear the Textboxes
                txtSearchProduct.Text = "";
				_logger.LogDebug("btnAdd_Click(sender={Sender},e={E}): txtSearchProduct.Text → {Arg2}", sender, e, txtSearchProduct.Text);
				_logger.LogDebug("btnAdd_Click(sender={Sender},e={E}): txtProductName.Text → {Arg2}", sender, e, txtProductName.Text);
				_logger.LogDebug("btnAdd_Click(sender={Sender},e={E}): txtInventory.Text → {Arg2}", sender, e, txtInventory.Text);
				_logger.LogDebug("btnAdd_Click(sender={Sender},e={E}): txtRate.Text → {Arg2}", sender, e, txtRate.Text);
				_logger.LogDebug("btnAdd_Click(sender={Sender},e={E}): TxtQty.Text → {Arg2}", sender, e, TxtQty.Text);
                txtProductName.Text = "";
                txtInventory.Text = "0.00";
                txtRate.Text = "0.00";
                TxtQty.Text = "0.00";
            }
			_logger.LogDebug("<<< Exiting btnAdd_Click(sender={Sender},e={E})", sender, e);
        }

        private void txtDiscount_TextChanged(object sender, EventArgs e)
        {
			_logger.LogDebug(">>> Entering txtDiscount_TextChanged(sender={Sender},e={E})", sender, e);
            //Get the value fro discount textbox
            string value = txtDiscount.Text;

            if(value=="")
            {
                //Display Error Message
                MessageBox.Show("Please Add Discount First");
            }
            else
            {
                //Get the discount in decimal value
                decimal subTotal = decimal.Parse(txtSubTotal.Text);
				_logger.LogDebug("txtDiscount_TextChanged(sender={Sender},e={E}): subTotal → {SubTotal}", sender, e, subTotal);
                decimal discount = decimal.Parse(txtDiscount.Text);

                //Calculate the grandtotal based on discount
                decimal grandTotal = ((100 - discount) / 100) * subTotal;

                //Display the GrandTotla in TextBox
                txtGrandTotal.Text = grandTotal.ToString();
            }
            
			_logger.LogDebug("<<< Exiting txtDiscount_TextChanged(sender={Sender},e={E})", sender, e);
        }

        private void txtVat_TextChanged(object sender, EventArgs e)
        {
			_logger.LogDebug(">>> Entering txtVat_TextChanged(sender={Sender},e={E})", sender, e);
            //Check if the grandTotal has value or not if it has not value then calculate the discount first
            string check = txtGrandTotal.Text;
            if(check=="")
            {
                //Deisplay the error message to calcuate discount
                MessageBox.Show("Calculate the discount and set the Grand Total First.");
            }
            else
            {
                //Calculate VAT
                //Getting the VAT Percent first
                decimal previousGT = decimal.Parse(txtGrandTotal.Text);
                decimal vat = decimal.Parse(txtVat.Text);
                decimal grandTotalWithVAT=((100+vat)/100)*previousGT;

                //Displaying new grand total with vat
                txtGrandTotal.Text = grandTotalWithVAT.ToString();
            }
			_logger.LogDebug("<<< Exiting txtVat_TextChanged(sender={Sender},e={E})", sender, e);
        }

        private void txtPaidAmount_TextChanged(object sender, EventArgs e)
        {
			_logger.LogDebug(">>> Entering txtPaidAmount_TextChanged(sender={Sender},e={E})", sender, e);
            //Get the paid amount and grand total
            decimal grandTotal = decimal.Parse(txtGrandTotal.Text);
            decimal paidAmount = decimal.Parse(txtPaidAmount.Text);

            decimal returnAmount = paidAmount - grandTotal;

            //Display the return amount as well
            txtReturnAmount.Text = returnAmount.ToString();
			_logger.LogDebug("<<< Exiting txtPaidAmount_TextChanged(sender={Sender},e={E})", sender, e);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
			_logger.LogDebug(">>> Entering btnSave_Click(sender={Sender},e={E})", sender, e);
            //Get the Values from PurchaseSales Form First
            transactionsBLL transaction = new transactionsBLL();

            transaction.type = lblTop.Text;
			_logger.LogDebug("btnSave_Click(sender={Sender},e={E}): transaction.type → {Arg2}", sender, e, transaction.type);

            //Get the ID of Dealer or Customer Here
            //Lets get name of the dealer or customer first
            string deaCustName = txtName.Text;
            DeaCustBLL dc = dcDAL.GetDeaCustIDFromName(deaCustName);
			_logger.LogDebug("btnSave_Click(sender={Sender},e={E}): dc → {Dc}", sender, e, dc);

            transaction.dea_cust_id = dc.id;
			_logger.LogDebug("btnSave_Click(sender={Sender},e={E}): transaction.dea_cust_id → {Arg2}", sender, e, transaction.dea_cust_id);
            transaction.grandTotal = Math.Round(decimal.Parse(txtGrandTotal.Text),2);
            transaction.transaction_date = DateTime.Now;
			_logger.LogDebug("btnSave_Click(sender={Sender},e={E}): transaction.transaction_date → {Arg2}", sender, e, transaction.transaction_date);
            transaction.tax = decimal.Parse(txtVat.Text);
            transaction.discount = decimal.Parse(txtDiscount.Text);

            //Get the Username of Logged in user
            string username = frmLogin.loggedIn;
            userBLL u = uDAL.GetIDFromUsername(username);

            transaction.added_by = u.id;
			_logger.LogDebug("btnSave_Click(sender={Sender},e={E}): transaction.added_by → {Arg2}", sender, e, transaction.added_by);
			_logger.LogDebug("btnSave_Click(sender={Sender},e={E}): transaction.transactionDetails → {Arg2}", sender, e, transaction.transactionDetails);
            transaction.transactionDetails = transactionDT;

            //Lets Create a Boolean Variable and set its value to false
            bool success = false;

            //Actual Code to Insert Transaction And Transaction Details
            using (TransactionScope scope = new TransactionScope())
			using Microsoft.Extensions.Logging;
            {
                int transactionID = -1;
                //Create aboolean value and insert transaction 
                bool w = tDAL.Insert_Transaction(transaction, out transactionID);

                //Use for loop to insert Transaction Details
                for(int i=0;i<transactionDT.Rows.Count;i++)
                {
					private static readonly Microsoft.Extensions.Logging.ILogger _logger = Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
                    //Get all the details of the product
                    transactionDetailBLL transactionDetail = new transactionDetailBLL();
                    //Get the Product name and convert it to id
                    string ProductName = transactionDT.Rows[i][0].ToString();
                    productsBLL p = pDAL.GetProductIDFromName(ProductName);

                    transactionDetail.product_id = p.id;
                    transactionDetail.rate = decimal.Parse(transactionDT.Rows[i][1].ToString());
                    transactionDetail.qty = decimal.Parse(transactionDT.Rows[i][2].ToString());
                    transactionDetail.total = Math.Round(decimal.Parse(transactionDT.Rows[i][3].ToString()),2);
                    transactionDetail.dea_cust_id = dc.id;
                    transactionDetail.added_date = DateTime.Now;
                    transactionDetail.added_by = u.id;

                    //Here Increase or Decrease Product Quantity based on Purchase or sales
                    string transactionType = lblTop.Text;

                    //Lets check whether we are on Purchase or Sales
                    bool x=false;
                    if(transactionType=="Purchase")
                    {
                        //Increase the Product
                        x = pDAL.IncreaseProduct(transactionDetail.product_id, transactionDetail.qty);
                    }
                    else if(transactionType=="Sales")
                    {
                        //Decrease the Product Quntiyt
                        x = pDAL.DecreaseProduct(transactionDetail.product_id, transactionDetail.qty);
                    }

                    //Insert Transaction Details inside the database
                    bool y = tdDAL.InsertTransactionDetail(transactionDetail);
                    success = w && x && y;
                }
                
                if (success == true)
                {
                    //Transaction Complete
                    scope.Complete();

                    //Code to Print Bill
                    DGVPrinter printer = new DGVPrinter();

                    printer.Title = "\r\n\r\n\r\n ANYSTORE PVT. LTD. \r\n\r\n";
                    printer.SubTitle = "Kathmandu, Nepal \r\n Phone: 01-045XXXX \r\n\r\n";
                    printer.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
                    printer.PageNumbers = true;
                    printer.PageNumberInHeader = false;
                    printer.PorportionalColumns = true;
                    printer.HeaderCellAlignment = StringAlignment.Near;
                    printer.Footer = "Discount: "+ txtDiscount.Text +"% \r\n" + "VAT: " + txtVat.Text + "% \r\n" + "Grand Total: "+ txtGrandTotal.Text + "\r\n\r\n" +"Thank you for doing business with us.";
                    printer.FooterSpacing = 15;
                    printer.PrintDataGridView(dgvAddedProducts);

                    MessageBox.Show("Transaction Completed Sucessfully");
                    //Celar the Data Grid View and Clear all the TExtboxes
                    dgvAddedProducts.DataSource = null;
                    dgvAddedProducts.Rows.Clear();

                    txtSearch.Text = "";
                    txtName.Text = "";
                    txtEmail.Text = "";
                    txtContact.Text = "";
                    txtAddress.Text = "";
                    txtSearchProduct.Text = "";
                    txtProductName.Text = "";
                    txtInventory.Text = "0";
                    txtRate.Text = "0";
                    TxtQty.Text = "0";
                    txtSubTotal.Text = "0";
                    txtDiscount.Text = "0";
                    txtVat.Text = "0";
                    txtGrandTotal.Text = "0";
                    txtPaidAmount.Text = "0";
                    txtReturnAmount.Text = "0";
                }
                else
                {
                    //Transaction Failed
                    MessageBox.Show("Transaction Failed");
                }
            }
			_logger.LogDebug("<<< Exiting btnSave_Click(sender={Sender},e={E})", sender, e);
        }
    }
}
