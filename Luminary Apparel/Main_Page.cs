using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Luminary_Apparel
{
    public partial class Main_Page : Form
    {
        public Main_Page()
        {
            InitializeComponent();
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            Admin_Login adminlogin = new Admin_Login();    
            adminlogin.ShowDialog();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button7_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to perform this action?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)    //check conformation box
            {
                Main_Login a = new Main_Login();
                this.Hide();
                a.Show();
            }
            else
            {
                //MainPage b = new MainPage();
                //this.Hide();
                //b.Show();  // error ---> remove previous data
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            Add_Employee employee= new Add_Employee();
            employee.ShowDialog();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            Attendance atten = new Attendance();
            atten.ShowDialog();
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            Request req = new Request();
            req.ShowDialog();
        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            About about= new About();
            about.ShowDialog();
        }
    }
}
