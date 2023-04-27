using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Luminary_Apparel
{
    public partial class Payroll_details : Form
    {

        SqlConnection connection = new SqlConnection("Data Source=LAPTOP-4BLCHTST\\SQLEXPRESS;Initial Catalog=Luminary_Apparel;Integrated Security=True");


        public int GID;

        public Payroll_details(int gID1)
        {
            InitializeComponent();

            GID = gID1;

            showdata();
        }

        private void guna2TextBox7_TextChanged(object sender, EventArgs e)
        {

        }


        private void showdata()
        {
            string fname = "";
            string lname = "";
            string jname = "";

            string query = "SELECT Fname, Lname FROM EmployeeForm WHERE GID = @GID1";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@GID1", GID);

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                fname = reader["Fname"].ToString();
                lname = reader["Lname"].ToString();
            }

            // Close connection and clean up resources
            reader.Close();
            connection.Close();

            string query1 = "SELECT JobName FROM JobDetailsForm WHERE GID = @GID1";
            SqlCommand command1 = new SqlCommand(query, connection);
            command1.Parameters.AddWithValue("@GID1", GID);

            connection.Open();
            SqlDataReader reader1 = command1.ExecuteReader();

            while (reader1.Read())
            {
                jname = reader1["JobName"].ToString();
            }

            // Close connection and clean up resources
            reader.Close();
            connection.Close();


            string name= fname+" "+lname;

            guna2TextBox1.Text = GID.ToString();
            guna2TextBox2.Text = name;
            guna2TextBox4.Text = jname;
        }

    }
}
