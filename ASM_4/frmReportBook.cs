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
    public partial class frmReportBook : Form
    {
        DataTable dtBooks;
        BookDAO dao = new BookDAO();
        public frmReportBook()
        {
            CenterToParent();
            InitializeComponent();
        }
        private void loadData()
        {
            dtBooks = dao.getBooks();
            dtBooks.PrimaryKey = new DataColumn[] { dtBooks.Columns["BookID"] };
            
            bsBooks.DataSource = dtBooks;

            dgvBooks.DataSource = bsBooks;
            bsBooks.Sort = "BookPrice DESC";
        }
            private void frmReportBook_Load(object sender, EventArgs e)
        {
            loadData();
        }
    }
}
