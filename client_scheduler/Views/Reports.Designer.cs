namespace client_scheduler.Views
{
    partial class Reports
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
            reportsGrid = new DataGridView();
            byTypeBtn = new Button();
            byUserBtn = new Button();
            byLocationBtn = new Button();
            ((System.ComponentModel.ISupportInitialize)reportsGrid).BeginInit();
            SuspendLayout();
            // 
            // reportsGrid
            // 
            reportsGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            reportsGrid.Location = new Point(12, 112);
            reportsGrid.Name = "reportsGrid";
            reportsGrid.Size = new Size(776, 426);
            reportsGrid.TabIndex = 0;
            // 
            // byTypeBtn
            // 
            byTypeBtn.Location = new Point(487, 44);
            byTypeBtn.Name = "byTypeBtn";
            byTypeBtn.Size = new Size(124, 44);
            byTypeBtn.TabIndex = 2;
            byTypeBtn.Text = "By Type Report";
            byTypeBtn.UseVisualStyleBackColor = true;
            byTypeBtn.Click += ByType_Clicked;
            // 
            // byUserBtn
            // 
            byUserBtn.Location = new Point(357, 44);
            byUserBtn.Name = "byUserBtn";
            byUserBtn.Size = new Size(124, 44);
            byUserBtn.TabIndex = 3;
            byUserBtn.Text = "User Schedule Report";
            byUserBtn.UseVisualStyleBackColor = true;
            byUserBtn.Click += ByUser_Clicked;
            // 
            // byLocationBtn
            // 
            byLocationBtn.Location = new Point(227, 44);
            byLocationBtn.Name = "byLocationBtn";
            byLocationBtn.Size = new Size(124, 44);
            byLocationBtn.TabIndex = 4;
            byLocationBtn.Text = "By Location Report";
            byLocationBtn.UseVisualStyleBackColor = true;
            byLocationBtn.Click += ByLocation_Clicked;
            // 
            // Reports
            // 
            ClientSize = new Size(800, 500);
            Controls.Add(byLocationBtn);
            Controls.Add(byUserBtn);
            Controls.Add(byTypeBtn);
            Controls.Add(reportsGrid);
            Name = "Reports";
            Text = "Reports";
            StartPosition = FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)reportsGrid).EndInit();
            ResumeLayout(false);
        }

        private DataGridView reportsGrid;
        #endregion

        private Button byTypeBtn;
        private Button byUserBtn;
        private Button byLocationBtn;
    }
}