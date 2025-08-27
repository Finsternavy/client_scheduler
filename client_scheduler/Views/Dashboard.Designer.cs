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
            prevMonth = new Button();
            nextMonth = new Button();
            monthLabel = new Label();
            reportsBtn = new Button();
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
            // prevMonth
            // 
            prevMonth.Location = new Point(12, 40);
            prevMonth.Name = "prevMonth";
            prevMonth.Size = new Size(75, 23);
            prevMonth.TabIndex = 1;
            prevMonth.Text = "<<";
            prevMonth.UseVisualStyleBackColor = true;
            prevMonth.Click += PrevMonth_Click;
            // 
            // nextMonth
            // 
            nextMonth.Location = new Point(212, 40);
            nextMonth.Name = "nextMonth";
            nextMonth.Size = new Size(75, 23);
            nextMonth.TabIndex = 2;
            nextMonth.Text = ">>";
            nextMonth.UseVisualStyleBackColor = true;
            nextMonth.Click += NextMonth_Click;
            // 
            // monthLabel
            // 
            monthLabel.AutoSize = true;
            monthLabel.Location = new Point(105, 44);
            monthLabel.Name = "monthLabel";
            monthLabel.Size = new Size(86, 15);
            monthLabel.TabIndex = 3;
            monthLabel.Text = "Current Month";
            //
            // reportsBtn
            //
            reportsBtn.Location = new Point(556, 40);
            reportsBtn.Name = "reportsBtn";
            reportsBtn.Size = new Size(124, 44);
            reportsBtn.TabIndex = 4;
            reportsBtn.Text = "Reports";
            reportsBtn.UseVisualStyleBackColor = true;
            reportsBtn.Click += reportsBtn_Click;
            // 
            // Dashboard
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(820, 800);
            Controls.Add(customerRecordsBtn);
            Controls.Add(prevMonth);
            Controls.Add(monthLabel);
            Controls.Add(reportsBtn);
            Controls.Add(nextMonth);
            MinimumSize = new Size(820, 800);
            Name = "Dashboard";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Dashboard";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button customerRecordsBtn;
        private Button prevMonth;
        private Button nextMonth;
        private Label monthLabel;
        private Button reportsBtn;
    }
}