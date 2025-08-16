namespace client_scheduler.Views
{
    partial class Dashboard
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
            customerRecordsBtn = new Button();
            SuspendLayout();
            // 
            // customerRecordsBtn
            // 
            customerRecordsBtn.Location = new Point(686, 40);
            customerRecordsBtn.Name = "customerRecordsBtn";
            customerRecordsBtn.Size = new Size(124, 44);
            customerRecordsBtn.TabIndex = 0;
            customerRecordsBtn.Text = "Customer Records";
            customerRecordsBtn.UseVisualStyleBackColor = true;
            customerRecordsBtn.Click += customerRecordsBtn_Click;
            // 
            // Dashboard
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(820, 800);
            Controls.Add(customerRecordsBtn);
            MinimumSize = new Size(820, 800);
            Name = "Dashboard";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Dashboard";
            ResumeLayout(false);
        }

        #endregion

        private Button customerRecordsBtn;
    }
}