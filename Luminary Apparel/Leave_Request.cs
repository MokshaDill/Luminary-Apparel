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
    public partial class Leave_Request : Form
    {
        SqlConnection con = new SqlConnection("Data Source=LAPTOP-4BLCHTST\\SQLEXPRESS;Initial Catalog=Luminary_Apparel;Integrated Security=True");
        SqlCommand cmd = new SqlCommand();

        public Leave_Request()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
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
