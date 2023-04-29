using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Luminary_Apparel
{
    public partial class Payroll_details : Form
    {

        SqlConnection connection = new SqlConnection("Data Source=LAPTOP-4BLCHTST\\SQLEXPRESS;Initial Catalog=Luminary_Apparel;Integrated Security=True");


        public int GID;
        private int count;
        private double overtimeHours;

        public Payroll_details(int gID1)
        {
            InitializeComponent();

            GID = gID1;

            showdata();

            CountMonthlyAttendance(2023, 4, GID);


        }

        private void guna2TextBox7_TextChanged(object sender, EventArgs e)
        {

        }


        private void showdata()
        {
            string fname = "";
            string lname = "";
            string jname = "";



            string query = "SELECT Fname, Lname, Photo FROM EmployeeForm WHERE GID = @GID1";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@GID1", GID);

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                fname = reader["Fname"].ToString();
                lname = reader["Lname"].ToString();
                byte[] photoBytes = (byte[])reader["Photo"];

                // Convert byte array to image
                MemoryStream memoryStream = new MemoryStream(photoBytes);
                Image image = Image.FromStream(memoryStream);

                // Set the image to the picture box
                guna2PictureBox1.Image = image;
            }

            // Close connection and clean up resources
            reader.Close();
            connection.Close();


            string query1 = "SELECT JobName FROM JobDetailsForm WHERE GID = @GID2";
            SqlCommand command1 = new SqlCommand(query1, connection);
            command1.Parameters.AddWithValue("@GID2", GID);

            connection.Open();
            SqlDataReader reader1 = command1.ExecuteReader();

            while (reader1.Read())
            {
                jname = reader1["JobName"].ToString();
            }

            // Close connection and clean up resources
            reader1.Close();
            connection.Close();


//-------------------------------------COUNT ATTENDANCE--------------------------------------------------


            DateTime currentDate = DateTime.Now;
            int month = currentDate.Month; // the month for which you want to retrieve the attendance count
            int year = currentDate.Year; // the year for which you want to retrieve the attendance count

            string query3 = "SELECT COUNT(*) FROM AttendanceForm WHERE GID = @GID AND MONTH(STime) = @month AND YEAR(STime) = @year";
            SqlCommand command3 = new SqlCommand(query3, connection);
            command3.Parameters.AddWithValue("@month", month);
            command3.Parameters.AddWithValue("@year", year);
            command3.Parameters.AddWithValue("@GID", GID);

            connection.Open();
            count = (int)command3.ExecuteScalar();
            connection.Close();

//-----------------------------------END COUNT ATTENDANCE--------------------------------------------------

//---------------------------------------OVER-TIME--------------------------------------------------------


            string query4 = "SELECT STime, ETime FROM AttendanceForm WHERE GID = @GID";
            SqlCommand command4 = new SqlCommand(query4, connection);
            command4.Parameters.AddWithValue("@GID", 8777);

            // create variables to store overtime hours and working hours
            overtimeHours = 0;
            double workingHours = 240;

            // open database connection and execute query
            connection.Open();
            SqlDataReader reader4 = command4.ExecuteReader();

            // loop through attendance records and calculate overtime hours
            while (reader4.Read())
            {
                DateTime startTime = Convert.ToDateTime(reader4["STime"]);
                DateTime exitTime = Convert.ToDateTime(reader4["ETime"]);
                TimeSpan difference = exitTime.Subtract(startTime);
                double hours = difference.TotalHours;

                // check if hours worked exceed standard working hours (8 hours)
                if (hours > workingHours)
                {
                    overtimeHours += hours - workingHours;
                }
            }

            // close database connection and reader
            reader.Close();
            connection.Close();

 //-----------------------------------END OVER-TIME--------------------------------------          




            string name = fname+" "+lname;

            guna2TextBox1.Text = GID.ToString();
            guna2TextBox2.Text = name;
            guna2TextBox4.Text = jname;
            guna2TextBox14.Text = count.ToString();
            guna2TextBox15.Text = overtimeHours.ToString();
            //guna2PictureBox1.Image= image;
        }

        private void CountMonthlyAttendance(int year, int month, int gid)
        {
            int daysInMonth = DateTime.DaysInMonth(year, month);
            DateTime startDate = new DateTime(year, month, 1);
            DateTime endDate = new DateTime(year, month, daysInMonth);

            string query = "SELECT COUNT(*) AS AttendanceCount FROM AttendanceForm WHERE GID=@gid AND Status='Full - Day' AND STime>=@startDate AND ETime<=@endDate";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@gid", gid);
            command.Parameters.AddWithValue("@startDate", startDate);
            command.Parameters.AddWithValue("@endDate", endDate);

            connection.Open();
            count = (int)command.ExecuteScalar();
            connection.Close();
        }

        private void label13_Click(object sender, EventArgs e)
        {

        }
    }
}
