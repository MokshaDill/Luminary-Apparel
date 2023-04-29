using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.Design;
using System.Windows.Forms;

namespace Luminary_Apparel
{
    public partial class Add_Employee : Form
    {
        SqlConnection con = new SqlConnection("Data Source=LAPTOP-4BLCHTST\\SQLEXPRESS;Initial Catalog=Luminary_Apparel;Integrated Security=True");
        SqlCommand cmd = new SqlCommand();

        string image= null;

        public Add_Employee()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
   
        
        
        {

        }

        private void guna2TextBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2DateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void guna2ComboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {

            con.Open();
            //SqlCommand cmd = new SqlCommand("INSERT INTO EmployeeForm(NIC,Fname, Lname, Address, Gender, Status, Bday, Phone1,Phone2, Position,Email,Photo, GID) VALUES('" + guna2TextBox1.Text + "','" + guna2TextBox2.Text + "','" + guna2TextBox2.Text + "','" + guna2TextBox5.Text + "','" + guna2ComboBox1.Text + "','" + guna2ComboBox2 + "','" + this.guna2DateTimePicker1.Value.ToString("MM/dd/yyyy hh/mm/ss") + "','" + guna2TextBox6.Text + "','" + guna2TextBox7.Text + "','" + guna2ComboBox3.Text + "' , '" + guna2TextBox8.Text + "', '" + arr + "', '" + guna2TextBox9.Text + "')", con);

            SqlCommand cmd = new SqlCommand("INSERT INTO JobDetailsForm(JobName,Experience, DateConform, PayFrequency, PaymentMethod, Qualification, GID) VALUES(@Jname, @ex, @date, @pay, @payment, @quli,  @GID)", con);

            cmd.Parameters.AddWithValue("@Jname", guna2ComboBox7.Text);
            cmd.Parameters.AddWithValue("@ex", guna2ComboBox4.Text);
            cmd.Parameters.AddWithValue("@pay", guna2ComboBox5.Text);
            cmd.Parameters.AddWithValue("@payment", guna2ComboBox6.Text);
            cmd.Parameters.AddWithValue("@quli", guna2TextBox10.Text);
            cmd.Parameters.AddWithValue("@date", this.guna2DateTimePicker2.Value);
            cmd.Parameters.AddWithValue("@GID", guna2TextBox4.Text);

            cmd.ExecuteNonQuery();
            MessageBox.Show("Saved");
            con.Close();



        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            con.Open();
            //SqlCommand cmd = new SqlCommand("INSERT INTO EmployeeForm(NIC,Fname, Lname, Address, Gender, Status, Bday, Phone1,Phone2, Position,Email,Photo, GID) VALUES('" + guna2TextBox1.Text + "','" + guna2TextBox2.Text + "','" + guna2TextBox2.Text + "','" + guna2TextBox5.Text + "','" + guna2ComboBox1.Text + "','" + guna2ComboBox2 + "','" + this.guna2DateTimePicker1.Value.ToString("MM/dd/yyyy hh/mm/ss") + "','" + guna2TextBox6.Text + "','" + guna2TextBox7.Text + "','" + guna2ComboBox3.Text + "' , '" + guna2TextBox8.Text + "', '" + arr + "', '" + guna2TextBox9.Text + "')", con);

            SqlCommand cmd = new SqlCommand("INSERT INTO BankDetailsForm(GID, FullName, BankNo, Branch, BankName) VALUES(@GID, @FullName, @BankNo, @Branch, @BankName)", con);

            cmd.Parameters.AddWithValue("@GID", guna2TextBox11.Text);
            cmd.Parameters.AddWithValue("@FullName", guna2TextBox12.Text);
            cmd.Parameters.AddWithValue("@BankNo", guna2TextBox13.Text);
            cmd.Parameters.AddWithValue("@Branch", guna2ComboBox9.Text);
            cmd.Parameters.AddWithValue("@BankName", guna2ComboBox8.Text);

            cmd.ExecuteNonQuery();
            MessageBox.Show("Saved");
            con.Close();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Image img = guna2PictureBox1.Image;
            byte[] arr;
            ImageConverter converter = new ImageConverter();
            arr = (byte[])converter.ConvertTo(img, typeof(byte[]));

            if (guna2TextBox1.Text == "" || guna2TextBox2.Text == "" || guna2TextBox3.Text == "" || guna2TextBox5.Text == "" || guna2ComboBox1.Text == "" || guna2ComboBox2.Text == "" || guna2ComboBox3.Text == "" || guna2TextBox6.Text == "" || guna2TextBox7.Text == "" || guna2TextBox8.Text == "" || guna2TextBox9.Text == "")
            {
                MessageBox.Show("Please fill all the required fields.");
            }
            else
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO EmployeeForm(NIC,Fname, Lname, Address, Gender, Status, Bday, Phone1, Phone2, Position, Email, Photo, GID) VALUES(@NIC, @Fname, @Lname, @Address, @Gender, @Status, @Bday, @Phone1, @Phone2, @Position, @Email, @Photo, @GID)", con);

                cmd.Parameters.AddWithValue("@NIC", guna2TextBox1.Text);
                cmd.Parameters.AddWithValue("@Fname", guna2TextBox2.Text);
                cmd.Parameters.AddWithValue("@Lname", guna2TextBox3.Text);
                cmd.Parameters.AddWithValue("@Address", guna2TextBox5.Text);
                cmd.Parameters.AddWithValue("@Gender", guna2ComboBox1.Text);
                cmd.Parameters.AddWithValue("@Status", guna2ComboBox2.Text);
                cmd.Parameters.AddWithValue("@Bday", this.guna2DateTimePicker1.Value);
                cmd.Parameters.AddWithValue("@Phone1", guna2TextBox6.Text);
                cmd.Parameters.AddWithValue("@Phone2", guna2TextBox7.Text);
                cmd.Parameters.AddWithValue("@Position", guna2ComboBox3.Text);
                cmd.Parameters.AddWithValue("@Email", guna2TextBox8.Text);
                cmd.Parameters.AddWithValue("@Photo", arr);
                cmd.Parameters.AddWithValue("@GID", guna2TextBox9.Text);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Saved");
                con.Close();
            }

        }

        private void guna2Button8_Click(object sender, EventArgs e)
        {
            using(OpenFileDialog ofd=new OpenFileDialog())
            {
                if(ofd.ShowDialog() == DialogResult.OK)
                {
                    image = ofd.FileName;
                    guna2PictureBox1.Image = Image.FromFile(ofd.FileName);
                }
            }
        }

        private void guna2Button9_Click(object sender, EventArgs e)
        {
            // Clear the image displayed in the Guna2PictureBox
            guna2PictureBox1.Image = null;


        }
    }
}
