using System;
using System.Drawing; // Thêm using này nếu chưa có
using System.Windows.Forms;

namespace AccountUI
{
    public partial class Recovery : Form
    {
        public Recovery()
        {
            InitializeComponent();
        }

        // --- Nút "Gửi" (Kiểm tra Email tồn tại) ---
        private void button3_Click(object sender, EventArgs e)
        {
            string email = textBox1.Text;

            // --- Kiểm tra cơ bản ---
            if (email.Trim() == "" || email == "Email")
            {
                MessageBox.Show("Vui lòng nhập email!", "Chú Ý");
                return;
            }
            // Giữ lại kiểm tra định dạng email của bạn
            if (!System.Text.RegularExpressions.Regex.IsMatch(email, @"^[a-zA-Z0-9_.]{3,20}@gmail.com(.vn|)$"))
            {
                MessageBox.Show("Vui lòng nhập đúng định dạng email @gmail.com!", "Chú Ý");
                return;
            }
            // ---------------------

            try
            {
                // --- Giao tiếp với Server ---
                // 1. Tạo lệnh kiểm tra email
                string request = $"CHECK_EMAIL|{email}";

                // 2. Gửi và nhận phản hồi
                string response = ClientSocket.SendAndReceive(request);

                // 3. Xử lý phản hồi
                if (response == "EMAIL_EXISTS")
                {
                    MessageBox.Show("Email hợp lệ. Bạn có thể tiến hành đặt lại mật khẩu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Cho phép nhập mã xác nhận (textBox2) và nhấn nút Xác nhận (button1)
                    // Hoặc đơn giản là mở luôn form Resetpassword
                    // Tạm thời: Kích hoạt ô nhập mã và nút xác nhận
                    textBox2.Enabled = true;
                    button1.Enabled = true; // button1 là nút "Xác Nhận" mã

                    // Bạn cũng có thể mở luôn form Reset Password ở đây nếu không cần bước nhập mã:
                    // Resetpassword rspassword = new Resetpassword(email); // Truyền email sang
                    // rspassword.ShowDialog();
                    // this.Close(); // Đóng form Recovery sau khi Reset xong
                }
                else // Bao gồm EMAIL_NOT_FOUND hoặc lỗi khác
                {
                    MessageBox.Show("Email này không tồn tại trong hệ thống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                // ---------------------------
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối hoặc server chưa chạy: " + ex.Message, "Lỗi");
            }
        }

        // --- Nút "Xác Nhận" (Sau khi nhập mã - Tạm thời chỉ mở form Reset) ---
        private void button1_Click_1(object sender, EventArgs e)
        {
            string email = textBox1.Text; // Lấy lại email đã nhập
            string confirmationCode = textBox2.Text; // Lấy mã xác nhận

            if (confirmationCode.Trim() == "" || confirmationCode == "Mã Xác Nhận")
            {
                MessageBox.Show("Vui Lòng Nhập Mã Xác Nhận", "Chú Ý");
                return;
            }

            // --- Tạm thời bỏ qua kiểm tra mã xác nhận ---
            // (Trong bài tập TCP cơ bản này, việc gửi/nhận mã khá phức tạp)
            // Chỉ cần mở form Reset Password và truyền email qua
            Resetpassword rspassword = new Resetpassword(email); // Truyền email sang form Reset
            rspassword.ShowDialog();
            this.Close(); // Đóng form Recovery sau khi Reset xong
            // ------------------------------------------
        }


        // --- Nút "Quay Lại" ---
        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Quay Lại ?", "Chú Ý", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }


        // --- Các hàm sự kiện Enter/Leave/TextChanged cho TextBox (Giữ nguyên) ---
        private void textBox1_TextChanged(object sender, EventArgs e) { } // Email
        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (textBox1.Text == "Email")
            {
                textBox1.Text = "";
                textBox1.ForeColor = Color.DarkSlateBlue;
            }
        }
        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                textBox1.Text = "Email";
                textBox1.ForeColor = Color.Gray;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e) { } // Mã Xác Nhận
        private void textBox2_Enter(object sender, EventArgs e)
        {
            if (textBox2.Text == "Mã Xác Nhận")
            {
                textBox2.Text = "";
                textBox2.ForeColor = Color.DarkSlateBlue;
            }
        }
        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                textBox2.Text = "Mã Xác Nhận";
                textBox2.ForeColor = Color.Gray;
            }
        }

        // --- Có thể bạn có hàm này từ code cũ, nếu không cần thì xóa đi ---
        // private void button1_Click(object sender, EventArgs e)
        // {
        //     // Hàm này có vẻ bị trùng, hàm button1_Click_1 ở trên mới đúng là nút "Xác Nhận" mã
        // }
    }
}