using Guna.UI2.WinForms;
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
    public partial class Loan_Request : Form
    {
        SqlConnection con = new SqlConnection("Data Source=LAPTOP-4BLCHTST\\SQLEXPRESS;Initial Catalog=Luminary_Apparel;Integrated Security=True");
        SqlCommand cmd = new SqlCommand();

        public Loan_Request()
        {
            InitializeComponent();
        }

        private void Loan_Request_Load(object sender, EventArgs e)
        {

        }

        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void guna2TextBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {

            con.Open();

            SqlCommand cmd = new SqlCommand("INSERT INTO LoanRequest(GID, NIC, Fname, Lname, LoanAmount, Reason, ContactNumber, Monthlyfee, GuarantName1, GuarantGID1, GuarantName2, GuarantGID2) VALUES(@GID, @NIC, @Fname, @Lname, @LoanAmount, @Reason, @ContactNumber, @Monthlyfee, @GuarantName1, @GuarantGID1, @GuarantName2, @GuarantGID2)", con);

            cmd.Parameters.AddWithValue("@GID", guna2TextBox1.Text);
            cmd.Parameters.AddWithValue("@NIC", guna2TextBox2.Text);
            cmd.Parameters.AddWithValue("@Fname", guna2TextBox3.Text);
            cmd.Parameters.AddWithValue("@Lname", guna2TextBox4.Text);
            //cmd.Parameters.AddWithValue("@jobID", guna2TextBox5.Text);
            cmd.Parameters.AddWithValue("@LoanAmount", guna2TextBox5.Text);
            cmd.Parameters.AddWithValue("@Reason", guna2ComboBox1.Text);
            cmd.Parameters.AddWithValue("@ContactNumber", guna2TextBox6.Text);
            cmd.Parameters.AddWithValue("@Monthlyfee", guna2TextBox9.Text);
            cmd.Parameters.AddWithValue("@GuarantName1", guna2TextBox8.Text);
            cmd.Parameters.AddWithValue("@GuarantGID1", guna2TextBox10.Text);
            cmd.Parameters.AddWithValue("@GuarantName2", guna2TextBox11.Text);
            cmd.Parameters.AddWithValue("@GuarantGID2", guna2TextBox7.Text);

            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Saved");

        }
    }
}
