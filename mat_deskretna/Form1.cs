using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mat_deskretna
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void text_TextChanged(object sender, EventArgs e)
        {

        }

        private void Go_button_Click(object sender, EventArgs e)
        {
            string text_start = text.Text;
            string[] result = text_start.Split(new string[] { "nie", "jeśli", "to", "i", "lub" }, StringSplitOptions.RemoveEmptyEntries);
          
            try
            {
                p_label.Text = "p = " + result[0];
                q_label.Text = "q = " + result[1];
                r_label.Text = "r = " + result[2];

                rezalt_panal.Visible = true;
                panal_p_q_r.Visible = true;
                pictureBox1.Visible = true;

            }
            catch
            {
                MessageBox.Show("Coś jest nie tak z tekstem, sprawdź go.", "Error!!!");
            }
               
        }

        private void Go_button_KeyPress(object sender, KeyPressEventArgs e)
        {
           Go_button_Click( sender, e);
        }
    }
}
