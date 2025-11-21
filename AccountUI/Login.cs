using ChessLogic; // Để truy cập ClientManager
using System;
using System.Threading; // Cần cho Thread
using System.Windows.Threading;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using ChessClient;

namespace AccountUI
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();


            

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            string tentk = textBox1.Text;
            string matkhau = textBox2.Text;

            // (Logic kiểm tra input của bạn giữ nguyên)
            // ...

            button1.Enabled = false;
            button1.Text = "Đang xử lý...";

            try
            {
                // 1. Kết nối
                await ClientManager.ConnectToServerAsync("127.0.0.1", 8888);

                // 2. Gửi lệnh LOGIN
                string request = $"LOGIN|{tentk}|{matkhau}";
                await ClientManager.Instance.SendAsync(request);

                // 3. CHỦ ĐỘNG CHỜ PHẢN HỒI
                // (Chạy trên một luồng riêng để không làm đơ UI)
                string response = await Task.Run(() => ClientManager.Instance.WaitForMessage());

                // 4. Xử lý phản hồi (Đã quay lại UI thread)
                var parts = response.Split('|');
                var command = parts[0];

                if (command == "LOGIN_SUCCESS")
                {
                    MessageBox.Show("Đăng nhập thành công!", "Thông báo");
                    this.Hide();
                    MainMenu mainmenu = new MainMenu();
                    mainmenu.ShowDialog();
                    this.Close();
                }
                else if (command == "ERROR")
                {
                    MessageBox.Show(parts[1], "Đăng nhập thất bại");
                    button1.Enabled = true;
                    button1.Text = "Đăng Nhập";
                    ClientManager.Disconnect();
                }
                else
                {
                    // (Lỗi không mong muốn)
                    button1.Enabled = true;
                    button1.Text = "Đăng Nhập";
                    ClientManager.Disconnect();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối hoặc server chưa chạy: " + ex.Message, "Lỗi kết nối");
                button1.Enabled = true;
                button1.Text = "Đăng Nhập";
            }
        }


        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Signup signup = new Signup();
            signup.ShowDialog();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Recovery recovery = new Recovery();
            recovery.ShowDialog();
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (textBox1.Text == "Tên Đăng Nhập")
            {
                textBox1.Text = "";
                textBox1.ForeColor = Color.DarkSlateBlue;
            }
        }
        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                textBox1.Text = "Tên Đăng Nhập";
                textBox1.ForeColor = Color.Gray;
            }
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            if (textBox2.Text == "Mật Khẩu")
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
                textBox2.Text = "Mật Khẩu";
                textBox2.ForeColor = Color.Gray;
                textBox2.PasswordChar = '\0';
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

        private void button_passwordshow_Click(object sender, EventArgs e)
        {
            if (textBox2.PasswordChar == '*')
            {
                button_passwordhide.BringToFront();
                textBox2.PasswordChar = '\0';
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

    }
}