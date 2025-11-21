namespace AccountUI
{
    partial class Signup
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Signup));
            pictureBox1 = new PictureBox();
            pictureBox2 = new PictureBox();
            textBox2 = new TextBox();
            textBox1 = new TextBox();
            pictureBox3 = new PictureBox();
            textBox3 = new TextBox();
            pictureBox4 = new PictureBox();
            textBox4 = new TextBox();
            button1 = new Button();
            button2 = new Button();
            label4 = new Label();
            button_passwordhide = new Button();
            button_passwordhide2 = new Button();
            button_passwordshow = new Button();
            button_passwordshow2 = new Button();
            txtFullName = new TextBox();
            dtpBirthday = new DateTimePicker();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.White;
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(650, 175);
            pictureBox1.Margin = new Padding(4);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(49, 49);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 19;
            pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            pictureBox2.BackColor = Color.White;
            pictureBox2.Image = Properties.Resources.Password;
            pictureBox2.Location = new Point(650, 385);
            pictureBox2.Margin = new Padding(4);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(49, 49);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 18;
            pictureBox2.TabStop = false;
            // 
            // textBox2
            // 
            textBox2.Font = new Font("Times New Roman", 16.2F);
            textBox2.ForeColor = SystemColors.GrayText;
            textBox2.Location = new Point(707, 175);
            textBox2.Margin = new Padding(4);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(493, 45);
            textBox2.TabIndex = 1;
            textBox2.Text = "Email";
            textBox2.Enter += textBox2_Enter;
            textBox2.Leave += textBox2_Leave;
            // 
            // textBox1
            // 
            textBox1.Font = new Font("Times New Roman", 16.2F);
            textBox1.ForeColor = SystemColors.GrayText;
            textBox1.Location = new Point(707, 105);
            textBox1.Margin = new Padding(4);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(493, 45);
            textBox1.TabIndex = 0;
            textBox1.Text = "Tên Đăng Nhập";
            textBox1.TextChanged += textBox1_TextChanged;
            textBox1.Enter += textBox1_Enter;
            textBox1.Leave += textBox1_Leave;
            // 
            // pictureBox3
            // 
            pictureBox3.BackColor = Color.White;
            pictureBox3.Image = Properties.Resources.Password;
            pictureBox3.Location = new Point(650, 455);
            pictureBox3.Margin = new Padding(4);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(49, 49);
            pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox3.TabIndex = 22;
            pictureBox3.TabStop = false;
            // 
            // textBox3
            // 
            textBox3.Font = new Font("Times New Roman", 16.2F);
            textBox3.ForeColor = SystemColors.GrayText;
            textBox3.Location = new Point(707, 385);
            textBox3.Margin = new Padding(4);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(493, 45);
            textBox3.TabIndex = 4;
            textBox3.Text = "Mật Khẩu";
            textBox3.Enter += textBox3_Enter;
            textBox3.Leave += textBox3_Leave;
            // 
            // pictureBox4
            // 
            pictureBox4.BackColor = Color.White;
            pictureBox4.Image = (Image)resources.GetObject("pictureBox4.Image");
            pictureBox4.Location = new Point(650, 105);
            pictureBox4.Margin = new Padding(4);
            pictureBox4.Name = "pictureBox4";
            pictureBox4.Size = new Size(49, 49);
            pictureBox4.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox4.TabIndex = 25;
            pictureBox4.TabStop = false;
            // 
            // textBox4
            // 
            textBox4.Font = new Font("Times New Roman", 16.2F);
            textBox4.ForeColor = SystemColors.GrayText;
            textBox4.Location = new Point(707, 455);
            textBox4.Margin = new Padding(4);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(493, 45);
            textBox4.TabIndex = 5;
            textBox4.Text = "Xác Nhận Mật Khẩu";
            textBox4.Enter += textBox4_Enter;
            textBox4.Leave += textBox4_Leave;
            // 
            // button1
            // 
            button1.AutoSize = true;
            button1.BackColor = Color.DarkSlateBlue;
            button1.Font = new Font("Times New Roman", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 163);
            button1.ForeColor = Color.White;
            button1.Location = new Point(810, 530);
            button1.Margin = new Padding(4);
            button1.Name = "button1";
            button1.Size = new Size(280, 68);
            button1.TabIndex = 6;
            button1.Text = "Đăng Ký";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.AutoSize = true;
            button2.BackColor = Color.DarkSlateBlue;
            button2.Font = new Font("Times New Roman", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 163);
            button2.ForeColor = Color.White;
            button2.Location = new Point(810, 610);
            button2.Margin = new Padding(4);
            button2.Name = "button2";
            button2.Size = new Size(280, 68);
            button2.TabIndex = 7;
            button2.Text = "Quay Lại";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.Transparent;
            label4.Font = new Font("Times New Roman", 36F, FontStyle.Bold, GraphicsUnit.Point, 163);
            label4.ForeColor = Color.White;
            label4.Location = new Point(50, 250);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(573, 81);
            label4.TabIndex = 28;
            label4.Text = "CHESS ONLINE";
            label4.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // button_passwordhide
            // 
            button_passwordhide.Image = (Image)resources.GetObject("button_passwordhide.Image");
            button_passwordhide.Location = new Point(1205, 385);
            button_passwordhide.Margin = new Padding(4);
            button_passwordhide.Name = "button_passwordhide";
            button_passwordhide.Size = new Size(50, 50);
            button_passwordhide.TabIndex = 29;
            button_passwordhide.UseVisualStyleBackColor = true;
            button_passwordhide.Click += button_passwordhide_Click;
            // 
            // button_passwordhide2
            // 
            button_passwordhide2.Image = (Image)resources.GetObject("button_passwordhide2.Image");
            button_passwordhide2.Location = new Point(1205, 454);
            button_passwordhide2.Margin = new Padding(4);
            button_passwordhide2.Name = "button_passwordhide2";
            button_passwordhide2.Size = new Size(50, 50);
            button_passwordhide2.TabIndex = 30;
            button_passwordhide2.UseVisualStyleBackColor = true;
            button_passwordhide2.Click += button_passwordhide2_Click;
            // 
            // button_passwordshow
            // 
            button_passwordshow.Image = (Image)resources.GetObject("button_passwordshow.Image");
            button_passwordshow.Location = new Point(1205, 384);
            button_passwordshow.Margin = new Padding(4);
            button_passwordshow.Name = "button_passwordshow";
            button_passwordshow.Size = new Size(50, 50);
            button_passwordshow.TabIndex = 31;
            button_passwordshow.UseVisualStyleBackColor = true;
            button_passwordshow.Click += button_passwordshow_Click;
            // 
            // button_passwordshow2
            // 
            button_passwordshow2.Image = (Image)resources.GetObject("button_passwordshow2.Image");
            button_passwordshow2.Location = new Point(1205, 454);
            button_passwordshow2.Margin = new Padding(4);
            button_passwordshow2.Name = "button_passwordshow2";
            button_passwordshow2.Size = new Size(50, 50);
            button_passwordshow2.TabIndex = 32;
            button_passwordshow2.UseVisualStyleBackColor = true;
            button_passwordshow2.Click += button_passwordshow2_Click;
            // 
            // txtFullName
            // 
            txtFullName.Font = new Font("Times New Roman", 16.2F);
            txtFullName.ForeColor = SystemColors.GrayText;
            txtFullName.Location = new Point(707, 245);
            txtFullName.Margin = new Padding(4);
            txtFullName.Name = "txtFullName";
            txtFullName.Size = new Size(493, 45);
            txtFullName.TabIndex = 2;
            txtFullName.Text = "Họ và Tên";
            txtFullName.Enter += TxtFullName_Enter;
            txtFullName.Leave += TxtFullName_Leave;
            // 
            // dtpBirthday
            // 
            dtpBirthday.CalendarFont = new Font("Times New Roman", 16.2F);
            dtpBirthday.Font = new Font("Times New Roman", 16.2F);
            dtpBirthday.Location = new Point(707, 315);
            dtpBirthday.Name = "dtpBirthday";
            dtpBirthday.Size = new Size(493, 45);
            dtpBirthday.TabIndex = 3;
            // 
            // Signup
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1300, 750);
            Controls.Add(label4);
            Controls.Add(pictureBox4);
            Controls.Add(textBox1);
            Controls.Add(pictureBox1);
            Controls.Add(textBox2);
            Controls.Add(txtFullName);
            Controls.Add(dtpBirthday);
            Controls.Add(pictureBox2);
            Controls.Add(textBox3);
            Controls.Add(pictureBox3);
            Controls.Add(textBox4);
            Controls.Add(button_passwordshow2);
            Controls.Add(button_passwordshow);
            Controls.Add(button_passwordhide2);
            Controls.Add(button_passwordhide);
            Controls.Add(button2);
            Controls.Add(button1);
            Margin = new Padding(4);
            Name = "Signup";
            Text = "Signup";
            Load += Signup_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private TextBox textBox2;
        private TextBox textBox1;
        private PictureBox pictureBox3;
        private TextBox textBox3;
        private PictureBox pictureBox4;
        private TextBox textBox4;
        private Button button1;
        private Button button2;
        private Label label4;
        private Button button_passwordhide;
        private Button button_passwordhide2;
        private Button button_passwordshow;
        private Button button_passwordshow2;
        private TextBox txtFullName;
        private DateTimePicker dtpBirthday;
    }
}