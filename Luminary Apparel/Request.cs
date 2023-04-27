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
    public partial class Request : Form
    {
        public Request()
        {
            InitializeComponent();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Leave_Request lr = new Leave_Request();
            lr.ShowDialog();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            Loan_Request loan_Request= new Loan_Request();
            loan_Request.ShowDialog();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
