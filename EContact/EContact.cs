using EContact.EContactClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EContact
{
    public partial class EContact : Form
    {
        public EContact()
        {
            InitializeComponent();
        }
         ContactClass c = new ContactClass();

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // get the value from the input fields
            c.FirstName = txtBoxFirstName.Text;
            c.LastName = txtBoxLastName.Text;
            c.ContactNo = txtBoxContactNumber.Text;
            c.Address = txtBoxAddress.Text;
            c.Gender = cmbGender.Text;

            //insert the Data into Database using the method we created when we write the insertion code in contactClass
            bool success = c.Insert(c);
            if (success == true)
            {
                //inserted successfully
                MessageBox.Show("new Contact inserted successfuly");
                //the clear method will be Called here
                Clear();
            }
            else
            {
                //failed to add Contact
                MessageBox.Show("Failed to add Contact, try again. ");
            }
            //load Data on Data GRidview
            DataTable dt = c.Select();
            dgvContactList.DataSource = dt;
        }


        private void EContact_Load(object sender, EventArgs e)
        {
            //load Data on Data GRidview
            DataTable dt = c.Select();
            dgvContactList.DataSource = dt;
        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public void Clear()
        {
            txtBoxFirstName.Text = "";
            txtBoxLastName.Text = "";
            txtBoxContactNumber.Text = "";
            txtBoxAddress.Text = "";
            cmbGender.Text = "";
            txtBoxContactID.Text = "";
        }
        private void btnUbdate_Click(object sender, EventArgs e)
        {
            //get the Data from textBoxes
            c.ContactID = int.Parse(txtBoxContactID.Text);
            c.FirstName = txtBoxFirstName.Text;
            c.LastName = txtBoxLastName.Text;
            c.ContactNo = txtBoxContactNumber.Text;
            c.Address = txtBoxAddress.Text;
            c.Gender = cmbGender.Text;
            //this to update the Data in  Database
            bool success = c.Update(c);
            if (success == true)
            {
                //the update is done
                MessageBox.Show("the Contact has been successfully Updated");
                //load Data on Data GRidview
                DataTable dt = c.Select();
                dgvContactList.DataSource = dt;
                // call the clear method
                Clear();
            }
            else
            {
                //Failed to update
                MessageBox.Show("Failed to Update the Contact");
            }

        }

        private void dgvContactList_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //we will get the Data from Data Grid View and Load it to the textboxes respectively
            //identify the row on which the mouse will click (is clicked)
            int rowIndex = e.RowIndex;
            txtBoxContactID.Text = dgvContactList.Rows[rowIndex].Cells[0].Value.ToString();
            txtBoxFirstName.Text = dgvContactList.Rows[rowIndex].Cells[1].Value.ToString();
            txtBoxLastName.Text = dgvContactList.Rows[rowIndex].Cells[2].Value.ToString();
            txtBoxContactNumber.Text = dgvContactList.Rows[rowIndex].Cells[3].Value.ToString();
            txtBoxAddress.Text = dgvContactList.Rows[rowIndex].Cells[4].Value.ToString();
            cmbGender.Text = dgvContactList.Rows[rowIndex].Cells[5].Value.ToString();

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            ////Get the contactID from the application
            c.ContactID = Convert.ToInt32(txtBoxContactID.Text);
            bool success = c.Delete(c);
            if (success == true)
            {
                // the data of the customer has benn deleted successfully
                MessageBox.Show("Contact you choose has been successfully deleted.");
                //Refresh Data GridView
                DataTable dt = c.Select();
                dgvContactList.DataSource = dt;
                // call the clear method
                Clear();
            }
            else
            {
                // Failed to delete the data
                MessageBox.Show("Failed to delete the contact you choose, try again. ");
            }
        }

         static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;
        private void txtBoxSearch_TextChanged(object sender, EventArgs e)
        {
            //Get the value from textbox
            string keyword = txtBoxSearch.Text;
            SqlConnection conn = new SqlConnection(myconnstrng);
            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM Tbl_contact WHERE FirstName Like '%"+keyword+ "%' OR LastName Like '%" + keyword + "%' OR Address Like '%" + keyword + "%'", conn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dgvContactList.DataSource = dt;
        }
    }
}
