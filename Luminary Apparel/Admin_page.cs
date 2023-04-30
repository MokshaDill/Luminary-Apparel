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


            if (string.IsNullOrEmpty(guna2TextBox15.Text) || string.IsNullOrEmpty(guna2ComboBox1.Text) || string.IsNullOrEmpty(guna2ComboBox2.Text) || string.IsNullOrEmpty(guna2TextBox3.Text) || string.IsNullOrEmpty(guna2TextBox4.Text) || string.IsNullOrEmpty(guna2TextBox5.Text) || string.IsNullOrEmpty(guna2TextBox6.Text) || string.IsNullOrEmpty(guna2TextBox7.Text))
            {
                MessageBox.Show("Please fill in all the required fields.");
            }
            else
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("INSERT INTO SalaryDB(JobID,JobType,Experience,BasicSalary,OTPerHour,EPFRate,ETFRate,MaxLoanAmount) VALUES(@JID,@JType,@Exp,@BasicSal,@OTPerHr,@EPFRate,@ETFRate,@MaxLoanAmt)", con);

                cmd.Parameters.AddWithValue("@JID", guna2TextBox15.Text);
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



        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(guna2TextBox1.Text) || string.IsNullOrEmpty(guna2TextBox2.Text) || string.IsNullOrEmpty(guna2TextBox8.Text) || string.IsNullOrEmpty(guna2ComboBox4.Text) || string.IsNullOrEmpty(guna2TextBox9.Text) || string.IsNullOrEmpty(guna2TextBox10.Text))
            {
                MessageBox.Show("Please fill out all required fields.");
                return;
            }

            // Validate email address
            if (!IsValidEmail(guna2TextBox10.Text))
            {
                MessageBox.Show("Please enter a valid email address.");
                return;
            }

            con.Open();

            SqlCommand cmd = new SqlCommand("INSERT INTO Login(ID, Username, Password, Position, GID, Email) VALUES(@ID, @uname, @pass, @position, @GID, @email)", con);

            cmd.Parameters.AddWithValue("@ID", guna2TextBox1.Text);
            cmd.Parameters.AddWithValue("@uname", guna2TextBox2.Text);
            cmd.Parameters.AddWithValue("@pass", guna2TextBox8.Text);
            cmd.Parameters.AddWithValue("@position", guna2ComboBox4.Text);
            cmd.Parameters.AddWithValue("@GID", guna2TextBox9.Text);
            cmd.Parameters.AddWithValue("@email", guna2TextBox10.Text);

            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Saved");






        }

        private bool IsValidEmail(string text)
        {
            throw new NotImplementedException();
        }

        private void guna2Button7_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(guna2TextBox14.Text))
            {
                MessageBox.Show("Please enter a loan number.");
                return;
            }

            if (string.IsNullOrEmpty(guna2TextBox12.Text))
            {
                MessageBox.Show("Please enter a GID.");
                return;
            }

            if (string.IsNullOrEmpty(guna2ComboBox6.Text))
            {
                MessageBox.Show("Please select HR Approval status.");
                return;
            }

            if (string.IsNullOrEmpty(guna2ComboBox7.Text))
            {
                MessageBox.Show("Please select loan status.");
                return;
            }

            con.Open();

            SqlCommand cmd = new SqlCommand("INSERT INTO Loan(LoanNo, GID, HRApproval, LoanStatus, Description) VALUES(@LoanNo, @GID, @HRApproval, @LoanStatus, @Description)", con);

            cmd.Parameters.AddWithValue("@LoanNo", guna2TextBox14.Text);
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

        private void guna2Button5_Click(object sender, EventArgs e)
        { 
            con.Open();

            SqlCommand cmd = new SqlCommand("UPDATE LeaveDB SET LeaveCount = @lc WHERE JobType = @jt", con);

            cmd.Parameters.AddWithValue("@lc", guna2TextBox11.Text);
            cmd.Parameters.AddWithValue("@jt", guna2ComboBox5.Text);

            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Saved");

        }

        private void Admin_page_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'luminary_ApparelDataSet8.BankDetailsForm' table. You can move, or remove it, as needed.
            this.bankDetailsFormTableAdapter.Fill(this.luminary_ApparelDataSet8.BankDetailsForm);
            // TODO: This line of code loads data into the 'luminary_ApparelDataSet7.JobDetailsForm' table. You can move, or remove it, as needed.
            this.jobDetailsFormTableAdapter.Fill(this.luminary_ApparelDataSet7.JobDetailsForm);
            // TODO: This line of code loads data into the 'luminary_ApparelDataSet6.LeaveDB' table. You can move, or remove it, as needed.
            this.leaveDBTableAdapter.Fill(this.luminary_ApparelDataSet6.LeaveDB);
            // TODO: This line of code loads data into the 'luminary_ApparelDataSet5.SalaryDB' table. You can move, or remove it, as needed.
            this.salaryDBTableAdapter.Fill(this.luminary_ApparelDataSet5.SalaryDB);
            // TODO: This line of code loads data into the 'luminary_ApparelDataSet4.EmployeeForm' table. You can move, or remove it, as needed.
            this.employeeFormTableAdapter.Fill(this.luminary_ApparelDataSet4.EmployeeForm);
            // TODO: This line of code loads data into the 'luminary_ApparelDataSet3.LoanRequest' table. You can move, or remove it, as needed.
            this.loanRequestTableAdapter1.Fill(this.luminary_ApparelDataSet3.LoanRequest);
            // TODO: This line of code loads data into the 'luminary_ApparelDataSet2.LoanRequest' table. You can move, or remove it, as needed.
            this.loanRequestTableAdapter.Fill(this.luminary_ApparelDataSet2.LoanRequest);
            // TODO: This line of code loads data into the 'luminary_ApparelDataSet1.Login' table. You can move, or remove it, as needed.
            this.loginTableAdapter.Fill(this.luminary_ApparelDataSet1.Login);

        }

        private void fillByToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.loanRequestTableAdapter1.FillBy(this.luminary_ApparelDataSet3.LoanRequest);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

        }

        private void fillBy1ToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.loanRequestTableAdapter1.FillBy1(this.luminary_ApparelDataSet3.LoanRequest);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

        }

        private void guna2DataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void pieChart1_ChildChanged(object sender, System.Windows.Forms.Integration.ChildChangedEventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            guna2TextBox15.Clear();
            guna2ComboBox1.SelectedIndex = -1;
            guna2ComboBox2.SelectedIndex = -1;
            guna2TextBox3.Clear();
            guna2TextBox4.Clear();
            guna2TextBox5.Clear();
            guna2TextBox7.Clear();
            guna2TextBox6.Clear();
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            guna2TextBox1.Clear();
            guna2TextBox2.Clear();
            guna2TextBox8.Clear();
            guna2ComboBox4.SelectedIndex = -1;
            guna2TextBox9.Clear();
            guna2TextBox10.Clear();
        }

        private void guna2Button8_Click(object sender, EventArgs e)
        {
            // Clear all textboxes
            guna2TextBox14.Clear();
            guna2TextBox12.Clear();
            guna2TextBox13.Clear();

            // Reset all comboboxes to default value
            guna2ComboBox6.SelectedIndex = 0;
            guna2ComboBox7.SelectedIndex = 0;
        }
    }
}
