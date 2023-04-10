using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Luminary_Apparel
{

    public partial class Main_Login : Form
    {
        SqlConnection con = new SqlConnection("Data Source=LAPTOP-4BLCHTST\\SQLEXPRESS;Initial Catalog=Luminary_Apparel;Integrated Security=True");

        Thread th;
        public Main_Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2TextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (guna2CheckBox1.Checked)
            {
                guna2TextBox2.UseSystemPasswordChar = false;
            }
            else
            {
                guna2TextBox2.UseSystemPasswordChar = true;
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            string sql = "SELECT * FROM Login WHERE Username ='" + guna2TextBox1.Text.Trim() + "' AND Password ='" + guna2TextBox2.Text.Trim() + "' ";
            SqlDataAdapter log = new SqlDataAdapter(sql, con);
            DataTable dataTable = new DataTable();
            log.Fill(dataTable);



            if (dataTable.Rows.Count == 1)
            {
                timer1.Start();
                guna2ProgressBar1.Show();
            }
            else if (guna2TextBox1.Text == "" || guna2TextBox2.Text == "")
            {
                MessageBox.Show("Please fill up all field");
            }
            else
            {
                MessageBox.Show("Incorrect Username or Password");
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            guna2ProgressBar1.Increment(35);
            if (guna2ProgressBar1.Value == guna2ProgressBar1.Maximum)
            {
                timer1.Stop();
                //MainPage a = new MainPage();
                //this.Hide();
                //a.Show();

                th = new Thread(OpeNewForm);
                th.SetApartmentState(ApartmentState.STA);
                th.Start();
                this.Close();


            }

        }

        private void OpeNewForm()
        {
            Application.Run(new Main_Page());
        }
    }
}
