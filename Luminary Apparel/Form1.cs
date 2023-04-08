using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;

namespace Luminary_Apparel
{
    public partial class Form1 : KryptonForm
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void kryptonPalette1_PalettePaint(object sender, PaletteLayoutEventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            pictureBox1.BorderStyle = BorderStyle.None;
            pictureBox1.BackColor = Color.Transparent;
            pictureBox1.Parent = this;
        }

        private void kryptonTextBox1_TextChanged(object sender, EventArgs e)
        {
            var placeholderTextBox1 = new PlaceholderTextBox();
            placeholderTextBox1.Size = new Size(200, 30);
            placeholderTextBox1.Location = new Point(50, 50);
            placeholderTextBox1.PlaceholderText = "Enter text here";
            Controls.Add(placeholderTextBox1);


        }

        private void kryptonTextBox2_TextChanged(object sender, EventArgs e)
        {
            var placeholderTextBox = new PlaceholderTextBox();
            placeholderTextBox.Size = new Size(200, 30);
            placeholderTextBox.Location = new Point(50, 50);
            placeholderTextBox.PlaceholderText = "Enter text here";
            Controls.Add(placeholderTextBox);


        }
    }
}
