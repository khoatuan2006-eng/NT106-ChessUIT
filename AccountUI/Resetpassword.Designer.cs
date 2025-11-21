namespace AccountUI
{
    partial class Resetpassword
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Resetpassword));
            pictureBox3 = new PictureBox();
            label2 = new Label();
            textBox3 = new TextBox();
            pictureBox2 = new PictureBox();
            textBox2 = new TextBox();
            button2 = new Button();
            button1 = new Button();
            label4 = new Label();
            label3 = new Label();
            button_passwordhide2 = new Button();
            button_passwordhide = new Button();
            button_passwordshow2 = new Button();
            button_passwordshow = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            SuspendLayout();
            // 
            // pictureBox3
            // 
            pictureBox3.BackColor = Color.White;
            pictureBox3.Image = Properties.Resources.Password;
            pictureBox3.Location = new Point(43, 248);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(39, 39);
            pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox3.TabIndex = 28;
            pictureBox3.TabStop = false;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.White;
            label2.Font = new Font("Times New Roman", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 163);
            label2.ForeColor = Color.DarkSlateBlue;
            label2.Location = new Point(101, 260);
            label2.Name = "label2";
            label2.Size = new Size(0, 26);
            label2.TabIndex = 27;
            // 
            // textBox3
            // 
            textBox3.Font = new Font("Times New Roman", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 163);
            textBox3.ForeColor = SystemColors.GrayText;
            textBox3.Location = new Point(88, 248);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(395, 39);
            textBox3.TabIndex = 26;
            textBox3.Text = "Xác Nhận Mật Khẩu";
            textBox3.TextChanged += textBox3_TextChanged;
            textBox3.Enter += textBox3_Enter;
            textBox3.Leave += textBox3_Leave;
            // 
            // pictureBox2
            // 
            pictureBox2.BackColor = Color.White;
            pictureBox2.Image = Properties.Resources.Password;
            pictureBox2.Location = new Point(43, 171);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(39, 39);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 25;
            pictureBox2.TabStop = false;
            // 
            // textBox2
            // 
            textBox2.Font = new Font("Times New Roman", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 163);
            textBox2.ForeColor = SystemColors.GrayText;
            textBox2.Location = new Point(88, 171);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(395, 39);
            textBox2.TabIndex = 23;
            textBox2.Text = "Mật Khẩu Mới";
            textBox2.Enter += textBox2_Enter;
            textBox2.Leave += textBox2_Leave;
            // 
            // button2
            // 
            button2.AutoSize = true;
            button2.BackColor = Color.DarkSlateBlue;
            button2.Font = new Font("Times New Roman", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 163);
            button2.ForeColor = Color.White;
            button2.Location = new Point(160, 396);
            button2.Name = "button2";
            button2.Size = new Size(224, 54);
            button2.TabIndex = 30;
            button2.Text = "Quay Lại";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // button1
            // 
            button1.AutoSize = true;
            button1.BackColor = Color.DarkSlateBlue;
            button1.Font = new Font("Times New Roman", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 163);
            button1.ForeColor = Color.White;
            button1.Location = new Point(160, 319);
            button1.Name = "button1";
            button1.Size = new Size(224, 54);
            button1.TabIndex = 29;
            button1.Text = "Xác Nhận";
            button1.UseVisualStyleBackColor = false;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.Transparent;
            label4.Font = new Font("Times New Roman", 22.2F, FontStyle.Bold, GraphicsUnit.Point, 163);
            label4.ForeColor = Color.White;
            label4.Location = new Point(123, 42);
            label4.Name = "label4";
            label4.Size = new Size(296, 42);
            label4.TabIndex = 31;
            label4.Text = "CHESS ONLINE";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.Transparent;
            label3.Font = new Font("Times New Roman", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 163);
            label3.ForeColor = Color.White;
            label3.Location = new Point(133, 109);
            label3.Name = "label3";
            label3.Size = new Size(272, 25);
            label3.TabIndex = 32;
            label3.Text = "Hãy Nhập Mật Khẩu Mới";
            // 
            // button_passwordhide2
            // 
            button_passwordhide2.Image = (Image)resources.GetObject("button_passwordhide2.Image");
            button_passwordhide2.Location = new Point(443, 248);
            button_passwordhide2.Name = "button_passwordhide2";
            button_passwordhide2.Size = new Size(40, 40);
            button_passwordhide2.TabIndex = 34;
            button_passwordhide2.UseVisualStyleBackColor = true;
            button_passwordhide2.Click += button_passwordhide2_Click;
            // 
            // button_passwordhide
            // 
            button_passwordhide.Image = (Image)resources.GetObject("button_passwordhide.Image");
            button_passwordhide.Location = new Point(443, 170);
            button_passwordhide.Name = "button_passwordhide";
            button_passwordhide.Size = new Size(40, 40);
            button_passwordhide.TabIndex = 33;
            button_passwordhide.UseVisualStyleBackColor = true;
            button_passwordhide.Click += button_passwordhide_Click;
            // 
            // button_passwordshow2
            // 
            button_passwordshow2.Image = (Image)resources.GetObject("button_passwordshow2.Image");
            button_passwordshow2.Location = new Point(443, 248);
            button_passwordshow2.Name = "button_passwordshow2";
            button_passwordshow2.Size = new Size(40, 40);
            button_passwordshow2.TabIndex = 36;
            button_passwordshow2.UseVisualStyleBackColor = true;
            button_passwordshow2.Click += button_passwordshow2_Click;
            // 
            // button_passwordshow
            // 
            button_passwordshow.Image = (Image)resources.GetObject("button_passwordshow.Image");
            button_passwordshow.Location = new Point(443, 170);
            button_passwordshow.Name = "button_passwordshow";
            button_passwordshow.Size = new Size(40, 40);
            button_passwordshow.TabIndex = 35;
            button_passwordshow.UseVisualStyleBackColor = true;
            button_passwordshow.Click += button_passwordshow_Click;
            // 
            // Resetpassword
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(968, 489);
            Controls.Add(button_passwordshow2);
            Controls.Add(button_passwordshow);
            Controls.Add(button_passwordhide2);
            Controls.Add(button_passwordhide);
            Controls.Add(label3);
            Controls.Add(label4);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(pictureBox3);
            Controls.Add(label2);
            Controls.Add(textBox3);
            Controls.Add(pictureBox2);
            Controls.Add(textBox2);
            Name = "Resetpassword";
            Text = "Resetpassword";
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox3;
        private Label label2;
        private TextBox textBox3;
        private PictureBox pictureBox2;
        private TextBox textBox2;
        private Button button2;
        private Button button1;
        private Label label4;
        private Label label3;
        private Button button_passwordhide2;
        private Button button_passwordhide;
        private Button button_passwordshow2;
        private Button button_passwordshow;
    }
}