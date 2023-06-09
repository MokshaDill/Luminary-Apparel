﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Xamarin.Forms;
using Font = iTextSharp.text.Font;
using Image = System.Drawing.Image;
using Paragraph = iTextSharp.text.Paragraph;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Luminary_Apparel
{
    public partial class Payroll_details : Form
    {

        SqlConnection connection = new SqlConnection("Data Source=LAPTOP-4BLCHTST\\SQLEXPRESS;Initial Catalog=Luminary_Apparel;Integrated Security=True");


        public int GID;
        private int count;
        private double overtimeHours;
        private string BasicSalary;
        private string name;
        private double bounus;
        private double otprice;
        private string EPF;
        private string ETF;
        private double ldeduction;
        private double netsalary;
        private double netETF;
        private double netEPF;

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
            string jexp = "";
            string loanstatus = "";
            string maxloan = "";
            double loanpermonth=0;
            string otperhour = "";
            
            



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

//-----------------------------------RETRIVING JOB DETAILS------------------------------------------------

            string query1 = "SELECT JobName, Experience FROM JobDetailsForm WHERE GID = @GID2";
            SqlCommand command1 = new SqlCommand(query1, connection);
            command1.Parameters.AddWithValue("@GID2", GID);

            connection.Open();
            SqlDataReader reader1 = command1.ExecuteReader();

            while (reader1.Read())
            {
                jname = reader1["JobName"].ToString();
                jexp = reader1["Experience"].ToString();
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


            //----------------------------------COLLECT BASIC ALARY DETAILS---------------------------

            string query5 = "SELECT BasicSalary, MaxLoanAmount, OTPerHour, EPFRate ,ETFRate FROM SalaryDB WHERE JobType = @jname AND Experience = @exp";
            SqlCommand command5 = new SqlCommand(query5, connection);
            command5.Parameters.AddWithValue("@jname", jname);
            command5.Parameters.AddWithValue("@exp", jexp);

            connection.Open();
            SqlDataReader reader5 = command5.ExecuteReader();

            while (reader5.Read())
            {
               BasicSalary = reader5["BasicSalary"].ToString();
               maxloan = reader5["MaxLoanAmount"].ToString();
               otperhour = reader5["OTPerHour"].ToString();
                EPF = reader5["EPFRate"].ToString();
                ETF = reader5["ETFRate"].ToString();
            }

            // Close connection and clean up resources
            reader5.Close();
            connection.Close();


            //---------------------------------------------------------END---------------------------------------------

            //--------------------------------------------LOAN DB---------------------------------------------------------

            string query6 = "SELECT LoanStatus FROM Loan WHERE GID = @GID";
            SqlCommand command6 = new SqlCommand(query6, connection);
            command6.Parameters.AddWithValue("@GID", GID);

            connection.Open();
            SqlDataReader reader6 = command6.ExecuteReader();

            while (reader6.Read())
            {
                loanstatus = reader6["LoanStatus"].ToString();
            }

            // Close connection and clean up resources
            reader5.Close();
            connection.Close();


            if (loanstatus == "Online")
            {
                double maxloanx = Convert.ToDouble(maxloan);
                loanpermonth = Math.Round(maxloanx,2) / 48; // 4 year package
            }




            name = fname+" "+lname;

            double othour = Convert.ToDouble(otperhour);
            otprice = overtimeHours* othour;

            netEPF = (Convert.ToDouble(BasicSalary) * Convert.ToDouble(EPF))/100;
            netETF = (Convert.ToDouble(BasicSalary) * Convert.ToDouble(ETF)) / 100;

            //loan deduction
            ldeduction =  Math.Round(Convert.ToDouble(loanpermonth),2);

            //total deduction
            double totaldeduction = ( loanpermonth + netEPF + netETF);

            //net salary
            netsalary = Math.Round(Convert.ToDouble(BasicSalary) - Convert.ToDouble(loanpermonth) - netEPF - netETF + bounus,2);


            //bounus
            if (count >= 25)
            {
                bounus = 500*(count-25);
            }



            guna2TextBox1.Text = GID.ToString();
            guna2TextBox2.Text = name;
            guna2TextBox4.Text = jname;
            guna2TextBox14.Text = count.ToString();
            guna2TextBox15.Text = overtimeHours.ToString();
            guna2TextBox5.Text = BasicSalary.ToString();
            guna2TextBox12.Text = bounus.ToString();
            guna2TextBox6.Text = otprice.ToString();
            guna2TextBox8.Text = netEPF.ToString();
            guna2TextBox9.Text = netETF.ToString();
            guna2TextBox16.Text =ldeduction.ToString();
            guna2TextBox10.Text = totaldeduction.ToString();
            guna2TextBox11.Text = netsalary.ToString();

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

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (Document doc = new Document(iTextSharp.text.PageSize.LETTER, 10, 10, 42, 35))
                {
                    using (PdfWriter wri = PdfWriter.GetInstance(doc, new FileStream("test.pdf", FileMode.Create)))
                    {
                        doc.Open();

                        iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(@"C:\Users\moksh\OneDrive\Desktop\LUMINARY.png");
                        logo.ScaleAbsolute(100, 100);
                        logo.SetAbsolutePosition(doc.PageSize.Width / 2 - 50, doc.PageSize.Height - 150);
                        doc.Add(logo);



                        Paragraph paragraph = new Paragraph( Chunk.NEWLINE );
                        doc.Add(paragraph);

                        for (int i = 0; i < 5; i++)
                        {
                            doc.Add(Chunk.NEWLINE);
                        }



                        // Add header
                        Paragraph header = new Paragraph("Payroll Receipt", new Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 18, iTextSharp.text.Font.BOLD));
                        header.Alignment = iTextSharp.text.Element.ALIGN_CENTER;
                        doc.Add(header);

                        Paragraph comname = new Paragraph("Luminary Apparel (Pvt) Ltd", new Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 15, iTextSharp.text.Font.NORMAL));
                        comname.Alignment = iTextSharp.text.Element.ALIGN_CENTER;
                        doc.Add(comname);

                        Paragraph address = new Paragraph("Avissawella Road, Homagama, Colombo", new Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 15, iTextSharp.text.Font.NORMAL));
                        address.Alignment = iTextSharp.text.Element.ALIGN_CENTER;
                        doc.Add(address);

                        Paragraph country = new Paragraph("Sri Lanka", new Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 15, iTextSharp.text.Font.NORMAL));
                        country.Alignment = iTextSharp.text.Element.ALIGN_CENTER;
                        doc.Add(country);

                        for (int i = 0; i < 2; i++)
                        {
                            doc.Add(Chunk.NEWLINE);
                        }

                        // Add employee information
                        Paragraph empInfo = new Paragraph();
                        empInfo.IndentationLeft = 90;
                        empInfo.Add(new Chunk("Employee Name: "));
                        empInfo.Add(new Chunk(name, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 12, iTextSharp.text.Font.NORMAL)));
                        empInfo.Add(Chunk.NEWLINE);
                        empInfo.Add(new Chunk("Employee ID: "));
                        empInfo.Add(new Chunk(GID.ToString(), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 12, iTextSharp.text.Font.NORMAL)));
                        empInfo.Add(Chunk.NEWLINE);
                        empInfo.Add(new Chunk("Current Date: "));
                        empInfo.Add(new Chunk(DateTime.Now.ToString("yyyy/dd/MM"), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 12, iTextSharp.text.Font.NORMAL)));
                        empInfo.Add(Chunk.NEWLINE);
                        empInfo.Add(new Chunk("Current Time: "));
                        empInfo.Add(new Chunk(DateTime.Now.ToString("HH:mm:ss"), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 12, iTextSharp.text.Font.NORMAL)));
                        empInfo.SpacingBefore = 10f;
                        empInfo.SpacingAfter = 10f;
                        

                        empInfo.Alignment = iTextSharp.text.Element.ALIGN_LEFT;
                        doc.Add(empInfo);


                        

                        // Add salary information
                        PdfPTable salaryTable = new PdfPTable(2);
                        salaryTable.WidthPercentage = 70;
                        salaryTable.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;

                        // Add header row
                        PdfPCell headerCell1 = new PdfPCell(new Paragraph("Description", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 12, iTextSharp.text.Font.BOLD)));
                        headerCell1.Padding = 8;
                        headerCell1.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                        salaryTable.AddCell(headerCell1);

                        PdfPCell headerCell2 = new PdfPCell(new Paragraph("Amount", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 12, iTextSharp.text.Font.BOLD)));
                        headerCell2.Padding = 8;
                        headerCell2.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                        salaryTable.AddCell(headerCell2);

                        // Add data rows
                        PdfPCell dataCell1 = new PdfPCell(new Paragraph("Basic Salary", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 12, iTextSharp.text.Font.NORMAL)));
                        dataCell1.Padding = 5;
                        salaryTable.AddCell(dataCell1);

                        PdfPCell dataCell2 = new PdfPCell(new Paragraph(BasicSalary, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 12, iTextSharp.text.Font.NORMAL)));
                        dataCell2.Padding = 5;
                        dataCell2.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
                        salaryTable.AddCell(dataCell2);

                        PdfPCell dataCell3 = new PdfPCell(new Paragraph("Bonus", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 12, iTextSharp.text.Font.NORMAL)));
                        dataCell3.Padding = 5;
                        salaryTable.AddCell(dataCell3);

                        PdfPCell dataCell4 = new PdfPCell(new Paragraph(bounus.ToString(), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 12, iTextSharp.text.Font.NORMAL)));
                        dataCell4.Padding = 5;
                        dataCell4.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
                        salaryTable.AddCell(dataCell4);

                        PdfPCell dataCell5 = new PdfPCell(new Paragraph("Over-Time", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 12, iTextSharp.text.Font.NORMAL)));
                        dataCell5.Padding = 5;
                        salaryTable.AddCell(dataCell5);

                        PdfPCell dataCell6 = new PdfPCell(new Paragraph(otprice.ToString(), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 12, iTextSharp.text.Font.NORMAL)));
                        dataCell6.Padding = 5;
                        dataCell6.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
                        salaryTable.AddCell(dataCell6);

                        PdfPCell dataCell7 = new PdfPCell(new Paragraph("E.P.F", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 12, iTextSharp.text.Font.NORMAL)));
                        dataCell7.Padding = 5;
                        salaryTable.AddCell(dataCell7);

                        PdfPCell dataCell8 = new PdfPCell(new Paragraph(netEPF.ToString(), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 12, iTextSharp.text.Font.NORMAL)));
                        dataCell8.Padding = 5;
                        dataCell8.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
                        salaryTable.AddCell(dataCell8);

                        PdfPCell dataCell9 = new PdfPCell(new Paragraph("E.T.F", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 12, iTextSharp.text.Font.NORMAL)));
                        dataCell9.Padding = 5;
                        salaryTable.AddCell(dataCell9);

                        PdfPCell dataCell10 = new PdfPCell(new Paragraph(netETF.ToString(), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 12, iTextSharp.text.Font.NORMAL)));
                        dataCell10.Padding = 5;
                        dataCell10.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
                        salaryTable.AddCell(dataCell10);

                        PdfPCell dataCell11 = new PdfPCell(new Paragraph("Loan Deduction", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 12, iTextSharp.text.Font.NORMAL)));
                        dataCell11.Padding = 5;
                        salaryTable.AddCell(dataCell11);

                        PdfPCell dataCell12 = new PdfPCell(new Paragraph(ldeduction.ToString(), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 12, iTextSharp.text.Font.NORMAL)));
                        dataCell12.Padding = 5;
                        dataCell12.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
                        salaryTable.AddCell(dataCell12);

                        PdfPCell dataCell13 = new PdfPCell(new Paragraph("Net Salary", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 12, iTextSharp.text.Font.NORMAL)));
                        dataCell13.Padding = 10;
                        salaryTable.AddCell(dataCell13);

                        PdfPCell dataCell14 = new PdfPCell(new Paragraph(netsalary.ToString(), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 12, iTextSharp.text.Font.NORMAL)));
                        dataCell14.Padding = 10;
                        dataCell14.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
                        salaryTable.AddCell(dataCell14);

                        doc.Add(salaryTable);


                        for (int i = 0; i < 3; i++)
                        {
                            doc.Add(Chunk.NEWLINE);
                        }


                        // Add signature line
                        Paragraph signature = new Paragraph(".................................");
                        signature.IndentationRight = 90;
                        signature.Alignment = iTextSharp.text.Element.ALIGN_RIGHT;
                        doc.Add(signature);

                        Paragraph signaturename = new Paragraph("Signature        ");
                        signaturename.IndentationRight = 90;
                        signaturename.Alignment = iTextSharp.text.Element.ALIGN_RIGHT;
                        doc.Add(signaturename);

                        for (int i = 0; i < 4; i++)
                        {
                            doc.Add(Chunk.NEWLINE);
                        }

                        Paragraph genmess = new Paragraph();
                        //genmess.IndentationLeft = 90;
                        genmess.Add(new Chunk("***This document has been automatically generated."));
                        genmess.Add(new Chunk(name, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 7, iTextSharp.text.Font.NORMAL)));
                        genmess.Alignment = iTextSharp.text.Element.ALIGN_CENTER;
                        doc.Add(genmess);

                        // Close document
                        doc.Close();
                    }
                }
                MessageBox.Show("PDF file has been generated successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred while generating PDF file: " + ex.Message);
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
