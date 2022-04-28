using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Econtact.econtactClasses;
using System.Configuration;
using System.Data.SqlClient;

namespace Econtact
{
    public partial class Econtact : Form
    {
        public Econtact()
        {
            InitializeComponent();
        }
        contactClass c = new contactClass(); 
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //Get the value from the input fields
            c.FirstName = txtboxFirstName.Text;
            c.LastName = txtboxLastName.Text;
            c.ContactNo = txtBoxContactNumber.Text;
            c.Address = txtBoxAddress.Text;
            c.Gender = cmbGender.Text;

            //Inserting Data into DAtabase using the method that created
            bool success = c.Insert(c);
            if (success == true)
            {
                //Successfully Inserted
                MessageBox.Show("New Contact Successfully Inserted");
                //Call the clear method here
                Clear();
            }
            else
            {
                //Failed to Add Contact
                MessageBox.Show("Failed to add New Contact. Try again.");
            }
                //Load Data on Data GRidview
                DataTable dt = c.Select();
                dataGridView1.DataSource=dt;
        }
    

            private void Econtact_Load(object sender, EventArgs e)
            {
                //Load Data on Data GRidview
            DataTable dt = c.Select();
            dataGridView1.DataSource=dt;
            
        }

            private void pictureBox1_Click(object sender, EventArgs e)
            {
                this.Close();
            }
        //Method to Clear Fields
            public void Clear()
            {
                txtboxFirstName.Text = "";
                txtboxLastName.Text = "";
                txtBoxContactNumber.Text = "";
                txtBoxAddress.Text = "";
                cmbGender.Text = "";
                txtboxContactID.Text = "";
            }

            private void btnUpdate_Click(object sender, EventArgs e)
            {
                //Get the DAta from the textboxes
                c.ContactID = int.Parse(txtboxContactID.Text);
                c.FirstName = txtboxFirstName.Text;
                c.LastName = txtboxLastName.Text;
                c.ContactNo = txtBoxContactNumber.Text;
                c.Address = txtBoxAddress.Text;
                c.Gender = cmbGender.Text;
                //Update DAta in Database
                bool success = c.Update(c);
                if (success == true)
                {
                    //Updated Successfully
                    MessageBox.Show("Contact has been successfully Updated.");
                    //Load Data on Data GRidview
                    DataTable dt = c.Select();
                    dataGridView1.DataSource = dt;
                    //Call Clear Method
                    Clear();
                }
                else
                {
                    //Failed to Update
                    MessageBox.Show("Failed to Update Contact.Try Again.");
                }
            }

            private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
            {
                //Get the DAta From DAta grid View and load it to the textboxes respectively
                //Identify the row on which mouseis clicked 
                int rowIndex = e.RowIndex;
                txtboxContactID.Text = dataGridView1.Rows[rowIndex].Cells[0].Value.ToString();
                txtboxFirstName.Text = dataGridView1.Rows[rowIndex].Cells[1].Value.ToString();
                txtboxLastName.Text = dataGridView1.Rows[rowIndex].Cells[2].Value.ToString();
                txtBoxContactNumber.Text = dataGridView1.Rows[rowIndex].Cells[3].Value.ToString();
                txtBoxAddress.Text = dataGridView1.Rows[rowIndex].Cells[4].Value.ToString();
                cmbGender.Text = dataGridView1.Rows[rowIndex].Cells[5].Value.ToString();


            }

            private void btnClear_Click(object sender, EventArgs e)
            {
                //Call Clear Method here
                Clear();
            }

            private void btnDelete_Click(object sender, EventArgs e)
            {
                //Get the Contact ID from the Application
                c.ContactID = Convert.ToInt32(txtboxContactID.Text);
                bool success = c.Delete(c);
                if (success == true)
                {
                    //Successfully Deleted
                    MessageBox.Show("Contact successfully deleted.");
                    //Refresh Data GridView
                    DataTable dt = c.Select();
                    dataGridView1.DataSource = dt;
                    //Call the clear method here
                    Clear();


                }
                else 
                {
                    //Failed to Delete
                    MessageBox.Show("Failed to Delete Contact. Try Again.");
                }

            }
            static string myconnstr = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;
            private void txtboxSearch_TextChanged(object sender, EventArgs e)
            {
                //Get the value from text box
                string keyword = txtboxSearch.Text;

                SqlConnection conn = new SqlConnection(myconnstr);
                SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM tbl_contact WHERE FirstName LIKE '%" + keyword + "%' OR LastName LIKE '%" + keyword + "%' OR Address LIKE '%" + keyword + "%'", conn);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                dataGridView1.DataSource = dt;

            }


    }
}
