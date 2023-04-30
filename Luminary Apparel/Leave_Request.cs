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

            if (guna2TextBox1.Text == "" || guna2ComboBox1.Text == "" || guna2ComboBox2.Text == "" || guna2ComboBox3.Text == "")
            {
                MessageBox.Show("Please fill in all required fields.");
            }
            else
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO Leave(GID, Reason, LeaveType, Date, NOofDay) VALUES(@GID, @Reason, @LeaveType, @Date, @NOofDay)", con);
                cmd.Parameters.AddWithValue("@GID", guna2TextBox1.Text);
                cmd.Parameters.AddWithValue("@Reason", guna2ComboBox1.Text);
                cmd.Parameters.AddWithValue("@LeaveType", guna2ComboBox2.Text);
                cmd.Parameters.AddWithValue("@Date", guna2DateTimePicker1.Value);
                cmd.Parameters.AddWithValue("@NOofDay", guna2ComboBox3.Text);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Saved");
            }




        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();   
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            guna2TextBox1.Clear();
            guna2ComboBox1.SelectedIndex = -1;
            guna2ComboBox2.SelectedIndex = -1;
            guna2DateTimePicker1.Value = DateTime.Now;
            guna2ComboBox3.SelectedIndex = -1;
        }
    }
}
