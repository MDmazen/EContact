using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EContact.EContactClasses
{
    class ContactClass
    {
        //getter setter properties
        //Acts as data carrier in our application 
        public int ContactID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ContactNo { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }

        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        //SELRCTING data from database
        public DataTable Select()
        {
            //step 1:database connection
            SqlConnection conn = new SqlConnection(myconnstrng);
            DataTable dt = new DataTable();
            try
            {
                //step 2:writeing sql query
                string sql = "SELECT * FROM tbl_contact";
                //creating cmd using sql and conn
                SqlCommand cmd = new SqlCommand(sql, conn);
                //creating sql dataADAPTER USING CMD 
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                conn.Open();
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {

            }
            finally
            {
                conn.Close();
            }
            return dt;
        }
        //inserting data into database
        public bool Insert(ContactClass c)
        {
            //creating a defult return type and setting it to false 
            bool isSuccess = false;

            //step 1:Connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            try
            {
                //step 2:creating asql query to insert data
                string sql = "INSERT INTO tbl_contact (FirstName, LastName,ContactNo, Address, Gender) VALUES (@FirstName, @LastName,@ContactNo, @Address, @Gender)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                //create parameters to add data
                cmd.Parameters.AddWithValue("@FirstName", c.FirstName);
                cmd.Parameters.AddWithValue("@LastName", c.LastName);
                cmd.Parameters.AddWithValue("@ContactNo", c.ContactNo);
                cmd.Parameters.AddWithValue("@Address", c.Address);
                cmd.Parameters.AddWithValue("@Gender", c.Gender);

                //connection open here
                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                //if the query runs successfully then value of rows will be greate than zero else its value will be 0
                if (rows > 0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }

            }
            catch (Exception ex)
            {

            }
            finally
            {
                conn.Close();
            }
            return isSuccess;
        }
        //methode to update date in database from our application 

        public bool Update(ContactClass c)
        {
            //create a defult return tupe and its default value to false 
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);
            try
            {
                //sql to update date in our database 
                string sql = "UPDATE tbl_contact SET FirstName=@FirstName, LastName=@LastName, ContactNo=@ContactNo, Address=@Address, Gender=@Gender WHERE ContactID=@ContactID";
                //Creating sql command
                SqlCommand cmd = new SqlCommand(sql, conn);
                //create parameters to add value
                cmd.Parameters.AddWithValue("@FirstName", c.FirstName);
                cmd.Parameters.AddWithValue("@LastName", c.LastName);
                cmd.Parameters.AddWithValue("@ContactNo", c.ContactNo);
                cmd.Parameters.AddWithValue("@Address", c.Address);
                cmd.Parameters.AddWithValue("@Gender", c.Gender);
                cmd.Parameters.AddWithValue("@ContactID", c.ContactID);
                //open database connection
                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                //if the query runs succesfully then the value of rows will be greater than zero else it's value will be zero
                if (rows>0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                conn.Close();

            }
            return isSuccess;

        }
        //methode to delete date from database

        public bool Delete(ContactClass c)
        {
            //create a defult return value and set its value to false 
            bool isSuccess = false;
            //create sql connection 
            SqlConnection conn = new SqlConnection(myconnstrng);
            try
            {
                //sql to delete date
                string sql = "DELETE FROM tbl_contact WHERE ContactID=@ContactID";
                //create sql Command
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@ContactID", c.ContactID);
                //open connection
                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                //if the qury run sucessfully then the value of rows is greater than zero else the value 0
                if (rows>0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;

                }

            }
            catch (Exception ex)
            {

            }
            finally
            {
                //close connection
                conn.Close();
            }

            return isSuccess;
        }
    }
}
