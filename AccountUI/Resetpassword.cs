using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace AccountUI
{
    public partial class Resetpassword : Form
    {
        private string userEmail; // Biến mới để lưu email
        public Resetpassword(string email) // Nhận email từ form Recovery
        {
            InitializeComponent();
            userEmail = email; // Lưu lại email
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            if (textBox2.Text == "Mật Khẩu Mới")
            {
                textBox2.Text = "";
                textBox2.ForeColor = Color.DarkSlateBlue;
                textBox2.PasswordChar = '*';
            }
        }
        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                textBox2.Text = "Mật Khẩu Mới";
                textBox2.ForeColor = Color.Gray;
                textBox2.PasswordChar = '\0';
            }
        }
        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
        private void textBox3_Enter(object sender, EventArgs e)
        {
            if (textBox3.Text == "Xác Nhận Mật Khẩu")
            {
                textBox3.Text = "";
                textBox3.ForeColor = Color.DarkSlateBlue;
                textBox3.PasswordChar = '*';
            }
        }
        private void textBox3_Leave(object sender, EventArgs e)
        {
            if (textBox3.Text == "")
            {
                textBox3.Text = "Xác Nhận Mật Khẩu";
                textBox3.ForeColor = Color.Gray;
                textBox3.PasswordChar = '\0';
            }
        }

        private void button_passwordhide_Click(object sender, EventArgs e)
        {
            if (textBox2.PasswordChar == '\0')
            {
                button_passwordshow.BringToFront();
                textBox2.PasswordChar = '*';
            }
        }

        private void button_passwordhide2_Click(object sender, EventArgs e)
        {
            if (textBox3.PasswordChar == '\0')
            {
                button_passwordshow2.BringToFront();
                textBox3.PasswordChar = '*';
            }
        }

        private void button_passwordshow_Click(object sender, EventArgs e)
        {
            if (textBox2.PasswordChar == '*')
            {
                button_passwordhide.BringToFront();
                textBox2.PasswordChar = '\0';
            }
        }

        private void button_passwordshow2_Click(object sender, EventArgs e)
        {
            if (textBox3.PasswordChar == '*')
            {
                button_passwordhide.BringToFront();
                textBox3.PasswordChar = '\0';
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Quay Lại ?", "Chú Ý", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if ( result == DialogResult.Yes)
            {
                this.Close();
            }
        }
    }
}
