namespace AccountUI
{
    partial class Recovery
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Recovery));
            button2 = new Button();
            pictureBox1 = new PictureBox();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            button3 = new Button();
            label4 = new Label();
            label2 = new Label();
            button1 = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // button2
            // 
            button2.AutoSize = true;
            button2.BackColor = Color.DarkSlateBlue;
            button2.Font = new Font("Times New Roman", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 163);
            button2.ForeColor = Color.White;
            button2.Location = new Point(175, 409);
            button2.Name = "button2";
            button2.Size = new Size(224, 54);
            button2.TabIndex = 32;
            button2.Text = "Quay Lại";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.White;
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(56, 136);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(39, 39);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 35;
            pictureBox1.TabStop = false;
            // 
            // textBox1
            // 
            textBox1.Font = new Font("Times New Roman", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 163);
            textBox1.ForeColor = SystemColors.GrayText;
            textBox1.Location = new Point(98, 136);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(395, 39);
            textBox1.TabIndex = 33;
            textBox1.Text = "Email";
            textBox1.TextChanged += textBox1_TextChanged;
            textBox1.Enter += textBox1_Enter;
            textBox1.Leave += textBox1_Leave;
            // 
            // textBox2
            // 
            textBox2.Font = new Font("Times New Roman", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 163);
            textBox2.ForeColor = SystemColors.GrayText;
            textBox2.Location = new Point(98, 275);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(395, 39);
            textBox2.TabIndex = 36;
            textBox2.Text = "Mã Xác Nhận";
            textBox2.TextChanged += textBox2_TextChanged;
            textBox2.Enter += textBox2_Enter;
            textBox2.Leave += textBox2_Leave;
            // 
            // button3
            // 
            button3.AutoSize = true;
            button3.BackColor = Color.DarkSlateBlue;
            button3.Font = new Font("Times New Roman", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 163);
            button3.ForeColor = Color.White;
            button3.Location = new Point(175, 199);
            button3.Name = "button3";
            button3.Size = new Size(224, 54);
            button3.TabIndex = 38;
            button3.Text = "Gửi";
            button3.UseVisualStyleBackColor = false;
            button3.Click += button3_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.Transparent;
            label4.Font = new Font("Times New Roman", 22.2F, FontStyle.Bold, GraphicsUnit.Point, 163);
            label4.ForeColor = Color.White;
            label4.Location = new Point(134, 24);
            label4.Name = "label4";
            label4.Size = new Size(296, 42);
            label4.TabIndex = 39;
            label4.Text = "CHESS ONLINE";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Times New Roman", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 163);
            label2.ForeColor = Color.White;
            label2.Location = new Point(36, 85);
            label2.Name = "label2";
            label2.Size = new Size(488, 25);
            label2.TabIndex = 40;
            label2.Text = "Nhập Email Để Nhận Mã Khôi Phục Mật Khẩu";
            // 
            // button1
            // 
            button1.AutoSize = true;
            button1.BackColor = Color.DarkSlateBlue;
            button1.Font = new Font("Times New Roman", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 163);
            button1.ForeColor = Color.White;
            button1.Location = new Point(175, 336);
            button1.Name = "button1";
            button1.Size = new Size(224, 54);
            button1.TabIndex = 41;
            button1.Text = "Xác Nhận";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click_1;
            // 
            // Recovery
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(966, 488);
            Controls.Add(button1);
            Controls.Add(label2);
            Controls.Add(label4);
            Controls.Add(button3);
            Controls.Add(textBox2);
            Controls.Add(pictureBox1);
            Controls.Add(textBox1);
            Controls.Add(button2);
            Name = "Recovery";
            Text = "Recovery";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button2;
        private PictureBox pictureBox1;
        private TextBox textBox1;
        private TextBox textBox2;
        private Button button3;
        private Label label4;
        private Label label2;
        private Button button1;
    }
}