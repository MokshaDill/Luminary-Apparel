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
    public partial class Attendance : Form
    {
        SqlConnection con = new SqlConnection("Data Source=LAPTOP-4BLCHTST\\SQLEXPRESS;Initial Catalog=Luminary_Apparel;Integrated Security=True");
        SqlCommand cmd = new SqlCommand();
        public Attendance()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Attendance_Load(object sender, EventArgs e)
        {

        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to perform this action?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            
            //check conformation box
            if (result == DialogResult.Yes)    
            {
                // Check if all fields are filled
                if (string.IsNullOrWhiteSpace(guna2TextBox1.Text) ||
                    string.IsNullOrWhiteSpace(guna2ComboBox1.Text))
                {
                    MessageBox.Show("Please fill in all fields");
                    return;
                }

                try
                {
                    con.Open();

                    SqlCommand cmd = new SqlCommand("INSERT INTO AttendanceForm(GID, Status, STime, ETime) VALUES(@gid, @status, @stime, @etime)", con);
                    cmd.Parameters.AddWithValue("@gid", guna2TextBox1.Text);
                    cmd.Parameters.AddWithValue("@status", guna2ComboBox1.Text);
                    cmd.Parameters.AddWithValue("@stime", this.guna2DateTimePicker1.Value);
                    cmd.Parameters.AddWithValue("@etime", this.guna2DateTimePicker2.Value);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Saved");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
                finally
                {
                    con.Close();
                }
            }
            else
            {

            }
        }
    }
}
