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
    public partial class frmChangeAccount : Form
    {
        public Employee employee { get; set; }
        public frmChangeAccount()
        {
            InitializeComponent();
        }
        public frmChangeAccount(Employee e) : this()
        {
            employee=e;
            InitData();
            CenterToScreen();
        }
        private void InitData()
        {
                txtusername.Text = employee.username;
                txtpass.Text = employee.password;
                if (employee.role == false)
                {
                    txtRole.Text = "Deactivate";
                }
        }
        private void frmChangeAccount_Load(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            EmployeeDAO dao = new EmployeeDAO();

            string username = txtusername.Text;
            string password = txtpass.Text;
            string re_password = txtrePass.Text;

            if(!password.Equals(re_password))
            {
                MessageBox.Show("Re-Password is wrong with Password!!!");
                return;
            }

            if(dao.updateAccount(username,password))
            {
                MessageBox.Show("Update Successfull.");
            }
        }
    }
}
