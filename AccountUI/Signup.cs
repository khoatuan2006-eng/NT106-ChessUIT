using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace AccountUI
{
    public partial class Signup : Form
    {
        public Signup()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            // --- 1. Lấy dữ liệu từ TẤT CẢ các ô ---
            string tentk = textBox1.Text;
            string email = textBox2.Text;
            string matkhau = textBox3.Text;
            string xnmatkhau = textBox4.Text;
            string fullName = txtFullName.Text; // Lấy từ control mới
            string birthday = dtpBirthday.Value.ToString("yyyy-MM-dd"); // Lấy từ control mới

            // --- 2. Kiểm tra dữ liệu (Giữ nguyên logic cũ của bạn) ---
            if (!Regex.IsMatch(tentk, @"^[A-Za-z0-9]{6,24}$") || tentk == "Tên Đăng Nhập")
            {
                MessageBox.Show("Vui lòng nhập tên tài khoản dài 6-24 ký tự...", "Chú Ý");
                return;
            }
            if (!Regex.IsMatch(email, @"^[a-zA-Z0-9_.]{3,20}@gmail.com(.vn|)$") || email == "Email")
            {
                MessageBox.Show("Vui lòng nhập đúng định dạng email @gmail.com!");
                return;
            }
            if (fullName.Trim() == "" || fullName == "Họ và Tên")
            {
                MessageBox.Show("Vui lòng nhập họ và tên!");
                return;
            }
            if (!Regex.IsMatch(matkhau, @"^[A-Za-z0-9]{6,24}$") || matkhau == "Mật Khẩu")
            {
                MessageBox.Show("Vui lòng nhập MẬT KHẨU dài 6-24 ký tự...", "Lỗi");
                return;
            }
            if (xnmatkhau != matkhau)
            {
                MessageBox.Show("Vui lòng xác nhận mật khẩu chính xác!");
                return;
            }

            // --- 3. Xóa bỏ kiểm tra SQL cũ ---
            // (Server sẽ làm việc này)

            try
            {
                // --- 4. Thay thế bằng code Socket ---

                // Bước 4.1: Mã hóa mật khẩu
                //string hashedPassword = PasswordHasher.HashPassword(matkhau);

                // Bước 4.2: Tạo yêu cầu (theo giao thức đã thống nhất)
                string request = $"REGISTER|{tentk}|{matkhau}|{email}|{fullName}|{birthday}";

                // Bước 4.3: Gửi và nhận phản hồi
                string response = ClientSocket.SendAndReceive(request);
                string[] parts = response.Split('|');
                string command = parts[0];

                // Bước 4.4: Xử lý phản hồi
                if (command == "REGISTER_SUCCESS")
                {
                    MessageBox.Show("Đăng ký thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close(); // Đóng form đăng ký
                }
                else
                {
                    // Hiển thị lỗi từ Server (ví dụ: "Username đã tồn tại")
                    MessageBox.Show(parts[1], "Đăng ký thất bại", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối hoặc server chưa chạy: " + ex.Message, "Lỗi");
            }
        }

        // --- CÁC HÀM SỰ KIỆN (Giữ nguyên) ---

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Quay Lại ?", "Chú Ý", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }

        // Các hàm Enter/Leave cho Tên Đăng Nhập
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

        // Các hàm Enter/Leave cho Email
        private void textBox2_Enter(object sender, EventArgs e)
        {
            if (textBox2.Text == "Email")
            {
                textBox2.Text = "";
                textBox2.ForeColor = Color.DarkSlateBlue;
            }
        }
        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                textBox2.Text = "Email";
                textBox2.ForeColor = Color.Gray;
            }
        }

        // --- HÀM MỚI: Enter/Leave cho Họ và Tên ---
        private void TxtFullName_Enter(object sender, EventArgs e)
        {
            if (txtFullName.Text == "Họ và Tên")
            {
                txtFullName.Text = "";
                txtFullName.ForeColor = Color.DarkSlateBlue;
            }
        }
        private void TxtFullName_Leave(object sender, EventArgs e)
        {
            if (txtFullName.Text == "")
            {
                txtFullName.Text = "Họ và Tên";
                txtFullName.ForeColor = Color.Gray;
            }
        }
        // ------------------------------------

        // Các hàm Enter/Leave cho Mật khẩu
        private void textBox3_Enter(object sender, EventArgs e)
        {
            if (textBox3.Text == "Mật Khẩu")
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
                textBox3.Text = "Mật Khẩu";
                textBox3.ForeColor = Color.Gray;
                textBox3.PasswordChar = '\0';
            }
        }

        // Các hàm Enter/Leave cho Xác nhận Mật khẩu
        private void textBox4_Enter(object sender, EventArgs e)
        {
            if (textBox4.Text == "Xác Nhận Mật Khẩu")
            {
                textBox4.Text = "";
                textBox4.ForeColor = Color.DarkSlateBlue;
                textBox4.PasswordChar = '*';
            }
        }
        private void textBox4_Leave(object sender, EventArgs e)
        {
            if (textBox4.Text == "")
            {
                textBox4.Text = "Xác Nhận Mật Khẩu";
                textBox4.ForeColor = Color.Gray;
                textBox4.PasswordChar = '\0';
            }
        }

        // Các hàm ẩn/hiện mật khẩu
        private void button_passwordhide_Click(object sender, EventArgs e)
        {
            if (textBox3.PasswordChar == '\0')
            {
                button_passwordshow.BringToFront();
                textBox3.PasswordChar = '*';
            }
        }
        private void button_passwordhide2_Click(object sender, EventArgs e)
        {
            if (textBox4.PasswordChar == '\0')
            {
                button_passwordshow2.BringToFront();
                textBox4.PasswordChar = '*';
            }
        }
        private void button_passwordshow_Click(object sender, EventArgs e)
        {
            if (textBox3.PasswordChar == '*')
            {
                button_passwordhide.BringToFront();
                textBox3.PasswordChar = '\0';
            }
        }
        private void button_passwordshow2_Click(object sender, EventArgs e)
        {
            if (textBox4.PasswordChar == '*')
            {
                button_passwordhide2.BringToFront();
                textBox4.PasswordChar = '\0';
            }
        }

        private void Signup_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}