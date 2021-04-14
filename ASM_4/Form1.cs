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
    public partial class frmLogin : Form
    {
        EmployeeDAO dao = new EmployeeDAO();
        public frmLogin()
        {
            CenterToScreen();
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            Employee emp = new Employee();
            string username = txtUsername.Text;
            if (username == string.Empty)
            {
                MessageBox.Show("Username is not be empty.");
                return;
            }
            string password = txtPassword.Text;
            if (password == string.Empty)
            {
                MessageBox.Show("Password is not be empty.");
                return;
            }
            emp = dao.checkLogin(username, password);
            if(emp==null)
            {
                MessageBox.Show("Wrong username or password!!!!");
            }
            else
            {
                if (emp.role == false)
                {
                    MessageBox.Show("Your Account Detail.");

                    frmChangeAccount frm = new frmChangeAccount(emp);
                    DialogResult r = frm.ShowDialog();
                    if (r == DialogResult.OK)
                    {
                        emp = frm.employee;
                        //dtProduct.Rows.Add(pro.ProductID, pro.ProductName, pro.UnitPrice, pro.ProductQuantity);
                    }
                }
                else
                {
                    MessageBox.Show("Login Successfull.");
                    frmMaintainBooks frm = new frmMaintainBooks();
                    //DialogResult r = 
                    frm.ShowDialog();
                    //if (r == DialogResult.OK)
                    //{
                    //    //emp = frm.employee;
                    //    //dtProduct.Rows.Add(pro.ProductID, pro.ProductName, pro.UnitPrice, pro.ProductQuantity);
                    //}
                }
            }
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {

        }
    }
}
