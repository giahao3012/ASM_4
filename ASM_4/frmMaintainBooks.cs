using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ASM_4
{
    public partial class frmMaintainBooks : Form
    {
        DataTable dtBooks;
        BookDAO dao = new BookDAO();
        //bool AddOrEdit;
        public frmMaintainBooks()
        {
            CenterToScreen();
            InitializeComponent();
        }

        private void loadData()
        {
            dtBooks = dao.getBooks();
            dtBooks.PrimaryKey = new DataColumn[] { dtBooks.Columns["BookID"] };

            bsBooks.DataSource = dtBooks;

            txtBookId.DataBindings.Clear();
            txtBookName.DataBindings.Clear();
            txtBookPrice.DataBindings.Clear();

            txtBookId.DataBindings.Add("Text", bsBooks, "BookID");
            txtBookName.DataBindings.Add("Text", bsBooks, "BookName");
            txtBookPrice.DataBindings.Add("Text", bsBooks, "BookPrice");
            lbTotalPrice.Text = "Total Price: " + dtBooks.Compute("SUM(BookPrice)", string.Empty);

            dgvBookList.DataSource = bsBooks;
            bnBookList.BindingSource = bsBooks;
        }
        private void frmMaintainBooks_Load(object sender, EventArgs e)
        {
            loadData();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string Bookname = txtBookName.Text;
            if (Bookname == string.Empty)
            {
                MessageBox.Show("Book name can not be empty.");
                return;
            }
            double price = 0;
            if (!double.TryParse(txtBookPrice.Text, out price))
            {
                MessageBox.Show("Price must be a number.");
                return;
            }
            if (price <= 0)
            {
                MessageBox.Show("Price must greater than 0.");
                return;
            }

            Book book = new Book
            {
                BookID = int.Parse(txtBookId.Text),
                BookName = Bookname,
                Price = price
            };
            if (dao.addProduct(book))
            {
                MessageBox.Show("Add Sucessfull.");
                loadData();
            }
            else
            {
                MessageBox.Show("Add Failed.");
            }
            
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string Bookname = txtBookName.Text;
            if (Bookname == string.Empty)
            {
                MessageBox.Show("Book name can not be empty.");
                return;
            }
            double price = 0;
            if (!double.TryParse(txtBookPrice.Text, out price))
            {
                MessageBox.Show("Price must be a number.");
                return;
            }
            if (price <= 0)
            {
                MessageBox.Show("Price must greater than 0.");
                return;
            }
            Book book = new Book
            {
                BookID = int.Parse(txtBookId.Text),
                BookName = Bookname,
                Price = price
            };
            if (dao.updateProduct(book))
            {
                MessageBox.Show("Update Sucessfull.");
                loadData();
            }
            else
            {
                MessageBox.Show("Update Failed.");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int id = int.Parse(txtBookId.Text);
            if(dao.deleteProduct(id))
            {
                MessageBox.Show("Delete Sucessfull.");
                loadData();
            }
            else
            {
                MessageBox.Show("Delete Failed.");
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            txtBookId.DataBindings.Clear();
            txtBookName.DataBindings.Clear();
            txtBookPrice.DataBindings.Clear();
            int id = 1;
            if (dtBooks.Rows.Count > 0)
            {
                id = int.Parse(dtBooks.Compute("MAX(BookID)", "").ToString()) + 1;
            }
            txtBookId.Text = id.ToString();
            txtBookName.Text = "";
            txtBookPrice.Text = "";
        }

        private void btnNewForm_Click(object sender, EventArgs e)
        {
            frmReportBook frmReport = new frmReportBook();
            frmReport.Show();
        }

        private void txtNameFilter_TextChanged(object sender, EventArgs e)
        {
            DataView dv = dtBooks.DefaultView;
            string filter = "BookName like '%" + txtNameFilter.Text + "%'";
            dv.RowFilter = filter;
            dgvBookList.DataSource = dtBooks;
            lbTotalPrice.Text = "Total Price: " + dtBooks.Compute("SUM(BookPrice)", string.Empty);
        }
    }
}
