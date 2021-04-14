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
    public class Book
    {
        public int BookID { get; set; }
        public string BookName { get; set; }
        public double Price { get; set; }
    }

    public class BookDAO
    {
        string strConnection;
        public BookDAO()
        {
            getConnectionString();
        }
        public string getConnectionString()
        {
            strConnection = ConfigurationManager.ConnectionStrings["BookStore"].ConnectionString;
            return strConnection;
        }
        public DataTable getBooks()
        {
            string SQL = "select * from Books";
            SqlConnection cnn = new SqlConnection(strConnection);
            SqlCommand cmd = new SqlCommand(SQL, cnn);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dtProduct = new DataTable();
            try
            {
                if (cnn.State == ConnectionState.Closed)
                { cnn.Open(); }
                adapter.Fill(dtProduct);
            }
            catch (SqlException e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                cnn.Close();
            }
            return dtProduct;
        }
        public bool addProduct(Book book)
        {
            string SQL = "Insert Books values(@ID,@Name,@Price)";
            SqlConnection cnn = new SqlConnection(strConnection);
            SqlCommand cmd = new SqlCommand(SQL, cnn);
            bool result;

            cmd.Parameters.AddWithValue("@ID", book.BookID);
            cmd.Parameters.AddWithValue("@Name", book.BookName);
            cmd.Parameters.AddWithValue("@Price",book.Price);
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
        public bool updateProduct(Book book)
        {
            string SQL = "Update Books set BookName=@Name,BookPrice=@Price" +
                " where BookID=@ID";
            SqlConnection cnn = new SqlConnection(strConnection);
            SqlCommand cmd = new SqlCommand(SQL, cnn);
            bool result;

            cmd.Parameters.AddWithValue("@ID", book.BookID);
            cmd.Parameters.AddWithValue("@Name", book.BookName);
            cmd.Parameters.AddWithValue("@Price", book.Price);
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

        public bool deleteProduct(int BookID)
        {
            string SQL = "Delete Books where BookID=@ID";
            SqlConnection cnn = new SqlConnection(strConnection);
            SqlCommand cmd = new SqlCommand(SQL, cnn);
            bool result;

            cmd.Parameters.AddWithValue("@ID", BookID);
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
