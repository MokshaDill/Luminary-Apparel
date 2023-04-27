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

            SqlCommand cmd = new SqlCommand("INSERT INTO Loan(LoanNo, GID, HRApproval, LoanStatus, Description) VALUES(@LoanNo, @GID, @HRApproval, @LoanStatus, @Description)", con);

            cmd.Parameters.AddWithValue("@LoanNo", guna2ComboBox9.Text);
            cmd.Parameters.AddWithValue("@GID", guna2TextBox12.Text);
            cmd.Parameters.AddWithValue("@HRApproval", guna2ComboBox6.Text);
            cmd.Parameters.AddWithValue("@LoanStatus", guna2ComboBox7.Text);
            cmd.Parameters.AddWithValue("@Description", guna2TextBox13.Text);

            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Saved");

        }
    }
}
