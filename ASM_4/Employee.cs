using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASM_4
{
    public class Employee
    {
        public string username { get; set; }
        public string password { get; set; }
        public bool role { get; set; }
    }

    public class EmployeeDAO
    {
        string strConnection;
        public EmployeeDAO()
        {
            getConnectionString();
        }
        public string getConnectionString()
        {
            strConnection = ConfigurationManager.ConnectionStrings["BookStore"].ConnectionString;
            return strConnection;
        }
        public Employee checkLogin(string username,string password)
        {
            string SQL = "Select * from Employee where EmpID=@ID and EmpPassword=@PW";
            SqlConnection cnn = new SqlConnection(strConnection);
            SqlCommand cmd = new SqlCommand(SQL, cnn);
            cmd.Parameters.AddWithValue("@ID", username);
            cmd.Parameters.AddWithValue("@PW", password);
            Employee e=new Employee();
            try
            {
                if (cnn.State == ConnectionState.Closed)
                { cnn.Open(); }
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string empID = reader["EmpID"].ToString();
                    string empPass = reader["EmpPassword"].ToString();
                    bool empRole = bool.Parse(reader["EmpRole"].ToString());

                    e.username = empID;
                    e.password = empPass;
                    e.role = empRole;
                    return e;
                }
            }
            catch(SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                cnn.Close();
            }
            return null;
        }
        public bool updateAccount(string username,string password)
        {
            string SQL = "Update Employee set EmpPassword=@PW" +
                " where EmpID=@ID";
            SqlConnection cnn = new SqlConnection(strConnection);
            SqlCommand cmd = new SqlCommand(SQL, cnn);
            bool result;

            cmd.Parameters.AddWithValue("@ID", username);
            cmd.Parameters.AddWithValue("@PW", password);
            try
            {
                if (cnn.State == ConnectionState.Closed)
                { cnn.Open(); }
                result = cmd.ExecuteNonQuery() > 0;
            }
            catch (SqlException e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                cnn.Close();
            }
            return result;
        }
    }
}
