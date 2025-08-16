using client_scheduler.Localization;

namespace client_scheduler
{
    partial class LoginForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            loginBtn = new Button();
            emailTextBox = new TextBox();
            passwordTextBox = new TextBox();
            emailLabel = new Label();
            passwordLabel = new Label();
            loginResponseMessage = new Label();
            testSpanishBtn = new Button();
            testEnglishBtn = new Button();
            SuspendLayout();
            // 
            // loginBtn
            // 
            loginBtn.BackColor = Color.Transparent;
            loginBtn.FlatStyle = FlatStyle.Flat;
            loginBtn.Location = new Point(354, 317);
            loginBtn.Name = "loginBtn";
            loginBtn.Size = new Size(75, 23);
            loginBtn.TabIndex = 1;
            loginBtn.Text = "Login";
            loginBtn.UseVisualStyleBackColor = true;
            loginBtn.Click += loginBtn_Click;
            // 
            // emailTextBox
            // 
            emailTextBox.Location = new Point(318, 209);
            emailTextBox.Name = "emailTextBox";
            emailTextBox.Size = new Size(146, 23);
            emailTextBox.TabIndex = 2;
            // 
            // passwordTextBox
            // 
            passwordTextBox.Location = new Point(318, 266);
            passwordTextBox.Name = "passwordTextBox";
            passwordTextBox.Size = new Size(146, 23);
            passwordTextBox.TabIndex = 3;
            // 
            // emailLabel
            // 
            emailLabel.AutoSize = true;
            emailLabel.Location = new Point(318, 191);
            emailLabel.Name = "emailLabel";
            emailLabel.Size = new Size(36, 15);
            emailLabel.TabIndex = 4;
            emailLabel.Text = "Email";
            // 
            // passwordLabel
            // 
            passwordLabel.AutoSize = true;
            passwordLabel.Location = new Point(318, 248);
            passwordLabel.Name = "passwordLabel";
            passwordLabel.Size = new Size(57, 15);
            passwordLabel.TabIndex = 5;
            passwordLabel.Text = "Password";
            // 
            // loginResponseMessage
            // 
            loginResponseMessage.AutoSize = true;
            loginResponseMessage.Font = new Font("Microsoft Sans Serif", 16F);
            loginResponseMessage.ForeColor = Color.White;
            loginResponseMessage.Location = new Point(39, 43);
            loginResponseMessage.Name = "loginResponseMessage";
            loginResponseMessage.Size = new Size(151, 26);
            loginResponseMessage.TabIndex = 7;
            loginResponseMessage.Text = "Welcome Text";
            // 
            // testSpanishBtn
            // 
            testSpanishBtn.Location = new Point(499, 18);
            testSpanishBtn.Name = "testSpanishBtn";
            testSpanishBtn.Size = new Size(98, 23);
            testSpanishBtn.TabIndex = 8;
            testSpanishBtn.Text = "Test Spanish";
            testSpanishBtn.UseVisualStyleBackColor = true;
            testSpanishBtn.Click += SetCultureSpanish;
            // 
            // testEnglishBtn
            // 
            testEnglishBtn.Location = new Point(603, 18);
            testEnglishBtn.Name = "testEnglishBtn";
            testEnglishBtn.Size = new Size(98, 23);
            testEnglishBtn.TabIndex = 9;
            testEnglishBtn.Text = "Test English";
            testEnglishBtn.UseVisualStyleBackColor = true;
            testEnglishBtn.Click += SetCultureEnglish;
            // 
            // LoginForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(33, 33, 33);
            ClientSize = new Size(800, 800);
            Controls.Add(testEnglishBtn);
            Controls.Add(testSpanishBtn);
            Controls.Add(loginResponseMessage);
            Controls.Add(passwordLabel);
            Controls.Add(emailLabel);
            Controls.Add(passwordTextBox);
            Controls.Add(emailTextBox);
            Controls.Add(loginBtn);
            Name = "LoginForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }


        #endregion
        private Button loginBtn;
        private TextBox emailTextBox;
        private TextBox passwordTextBox;
        private Label emailLabel;
        private Label passwordLabel;
        private Label loginResponseMessage;
        private Button testSpanishBtn;
        private Button testEnglishBtn;
    }
}
