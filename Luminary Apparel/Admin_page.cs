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
    public partial class Admin_page : Form
    {

        SqlConnection con = new SqlConnection("Data Source=LAPTOP-4BLCHTST\\SQLEXPRESS;Initial Catalog=Luminary_Apparel;Integrated Security=True");
        SqlCommand cmd = new SqlCommand();
        public Admin_page()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {


        }

        private void guna2ComboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void guna2ComboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {


            con.Open();

            SqlCommand cmd = new SqlCommand("INSERT INTO SalaryDB(JobID,JobType,Experience,BasicSalary,OTPerHour,EPFRate,ETFRate,MaxLoanAmount) VALUES(@JID,@JType,@Exp,@BasicSal,@OTPerHr,@EPFRate,@ETFRate,@MaxLoanAmt)", con);

            cmd.Parameters.AddWithValue("@JID", guna2ComboBox3.Text);
            cmd.Parameters.AddWithValue("@JType", guna2ComboBox1.Text);
            cmd.Parameters.AddWithValue("@Exp", guna2ComboBox2.Text);
            cmd.Parameters.AddWithValue("@BasicSal", guna2TextBox3.Text);
            cmd.Parameters.AddWithValue("@OTPerHr", guna2TextBox4.Text);
            cmd.Parameters.AddWithValue("@EPFRate", guna2TextBox5.Text);
            cmd.Parameters.AddWithValue("@ETFRate", guna2TextBox7.Text);
            cmd.Parameters.AddWithValue("@MaxLoanAmt", guna2TextBox6.Text);

            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Saved");


        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {


            con.Open();

            SqlCommand cmd = new SqlCommand("UPDATE LeaveDB SET LeaveCount = @lc WHERE JobType = @jt", con);

            cmd.Parameters.AddWithValue("@lc", guna2TextBox11.Text);
            cmd.Parameters.AddWithValue("@jt", guna2ComboBox5.Text);

            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Saved");



        }

        private void guna2Button7_Click(object sender, EventArgs e)
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

        private void guna2Button9_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
