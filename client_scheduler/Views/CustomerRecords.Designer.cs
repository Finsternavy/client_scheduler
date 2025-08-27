namespace client_scheduler.Views
{
    partial class CustomerRecords
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
            nameLabel = new Label();
            nameTextBox = new TextBox();
            addressLabel = new Label();
            Address = new TextBox();
            address2Label = new Label();
            Address2 = new TextBox();
            cityLabel = new Label();
            citySelect = new ComboBox();
            postalCodeLabel = new Label();
            postalCodeTextBox = new TextBox();
            phoneLabel = new Label();
            phoneTextBox = new TextBox();
            countryLabel = new Label();
            countrySelect = new ComboBox();
            saveButton = new Button();
            cancelButton = new Button();
            customerListBox = new ListBox();
            listBoxLabel = new Label();
            newCustomerBtn = new Button();
            deleteCustomerBtn = new Button();
            SuspendLayout();
            // 
            // nameLabel
            // 
            nameLabel.AutoSize = true;
            nameLabel.Location = new Point(147, 147);
            nameLabel.Name = "nameLabel";
            nameLabel.Size = new Size(42, 15);
            nameLabel.TabIndex = 0;
            nameLabel.Text = "Name:";
            // 
            // nameTextBox
            // 
            nameTextBox.Location = new Point(147, 165);
            nameTextBox.Name = "nameTextBox";
            nameTextBox.Size = new Size(200, 23);
            nameTextBox.TabIndex = 1;
            // 
            // addressLabel
            // 
            addressLabel.AutoSize = true;
            addressLabel.Location = new Point(147, 201);
            addressLabel.Name = "addressLabel";
            addressLabel.Size = new Size(52, 15);
            addressLabel.TabIndex = 2;
            addressLabel.Text = "Address:";
            // 
            // Address
            // 
            Address.Location = new Point(147, 219);
            Address.Name = "Address";
            Address.Size = new Size(200, 23);
            Address.TabIndex = 3;
            // 
            // address2Label
            // 
            address2Label.AutoSize = true;
            address2Label.Location = new Point(437, 201);
            address2Label.Name = "address2Label";
            address2Label.Size = new Size(61, 15);
            address2Label.TabIndex = 4;
            address2Label.Text = "Address 2:";
            // 
            // Address2
            // 
            Address2.Location = new Point(437, 219);
            Address2.Name = "Address2";
            Address2.Size = new Size(200, 23);
            Address2.TabIndex = 5;
            // 
            // cityLabel
            // 
            cityLabel.AutoSize = true;
            cityLabel.Location = new Point(147, 261);
            cityLabel.Name = "cityLabel";
            cityLabel.Size = new Size(31, 15);
            cityLabel.TabIndex = 6;
            cityLabel.Text = "City:";
            // 
            // citySelect
            // 
            citySelect.Location = new Point(147, 276);
            citySelect.Name = "citySelect";
            citySelect.Size = new Size(200, 23);
            citySelect.TabIndex = 7;
            citySelect.SelectedIndexChanged += citySelect_SelectedIndexChanged;
            // 
            // postalCodeLabel
            // 
            postalCodeLabel.AutoSize = true;
            postalCodeLabel.Location = new Point(147, 320);
            postalCodeLabel.Name = "postalCodeLabel";
            postalCodeLabel.Size = new Size(73, 15);
            postalCodeLabel.TabIndex = 8;
            postalCodeLabel.Text = "Postal Code:";
            // 
            // postalCodeTextBox
            // 
            postalCodeTextBox.Location = new Point(147, 338);
            postalCodeTextBox.Name = "postalCodeTextBox";
            postalCodeTextBox.Size = new Size(200, 23);
            postalCodeTextBox.TabIndex = 9;
            // 
            // phoneLabel
            // 
            phoneLabel.AutoSize = true;
            phoneLabel.Location = new Point(437, 147);
            phoneLabel.Name = "phoneLabel";
            phoneLabel.Size = new Size(44, 15);
            phoneLabel.TabIndex = 10;
            phoneLabel.Text = "Phone:";
            // 
            // phoneTextBox
            // 
            phoneTextBox.Location = new Point(437, 165);
            phoneTextBox.Name = "phoneTextBox";
            phoneTextBox.Size = new Size(200, 23);
            phoneTextBox.TabIndex = 11;
            phoneTextBox.TextChanged += ValidatePhoneChange;
            // 
            // countryLabel
            // 
            countryLabel.AutoSize = true;
            countryLabel.Location = new Point(437, 258);
            countryLabel.Name = "countryLabel";
            countryLabel.Size = new Size(53, 15);
            countryLabel.TabIndex = 12;
            countryLabel.Text = "Country:";
            // 
            // countrySelect
            // 
            countrySelect.Location = new Point(437, 276);
            countrySelect.Name = "countrySelect";
            countrySelect.Size = new Size(200, 23);
            countrySelect.TabIndex = 13;
            countrySelect.SelectedIndexChanged += countrySelect_SelectedIndexChanged;
            // 
            // saveButton
            // 
            saveButton.FlatStyle = FlatStyle.Flat;
            saveButton.Location = new Point(562, 404);
            saveButton.Name = "saveButton";
            saveButton.Size = new Size(75, 23);
            saveButton.TabIndex = 14;
            saveButton.Text = "Save";
            saveButton.Click += saveButton_Click;
            // 
            // cancelButton
            // 
            cancelButton.FlatStyle = FlatStyle.Flat;
            cancelButton.Location = new Point(481, 404);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(75, 23);
            cancelButton.TabIndex = 15;
            cancelButton.Text = "Cancel";
            cancelButton.Click += CancelButton_Click;
            // 
            // customerListBox
            // 
            customerListBox.FormattingEnabled = true;
            customerListBox.ItemHeight = 15;
            customerListBox.Location = new Point(147, 41);
            customerListBox.Name = "customerListBox";
            customerListBox.Size = new Size(490, 79);
            customerListBox.TabIndex = 16;
            customerListBox.Click += UpdateActiveCustomer;
            // 
            // listBoxLabel
            // 
            listBoxLabel.AutoSize = true;
            listBoxLabel.Location = new Point(147, 23);
            listBoxLabel.Name = "listBoxLabel";
            listBoxLabel.Size = new Size(105, 15);
            listBoxLabel.TabIndex = 17;
            listBoxLabel.Text = "Select a Customer:";
            // 
            // newCustomerBtn
            // 
            newCustomerBtn.FlatStyle = FlatStyle.Flat;
            newCustomerBtn.Location = new Point(537, 15);
            newCustomerBtn.Name = "newCustomerBtn";
            newCustomerBtn.Size = new Size(100, 23);
            newCustomerBtn.TabIndex = 17;
            newCustomerBtn.Text = "New Customer";
            newCustomerBtn.Click += newCustomerBtn_Click;
            // 
            // deleteCustomerBtn
            // 
            deleteCustomerBtn.FlatStyle = FlatStyle.Flat;
            deleteCustomerBtn.Location = new Point(147, 404);
            deleteCustomerBtn.Name = "deleteCustomerBtn";
            deleteCustomerBtn.Size = new Size(100, 23);
            deleteCustomerBtn.TabIndex = 18;
            deleteCustomerBtn.Text = "Delete Customer";
            deleteCustomerBtn.Click += DeleteCustomerBtn_Click;
            // 
            // CustomerRecords
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(nameLabel);
            Controls.Add(nameTextBox);
            Controls.Add(addressLabel);
            Controls.Add(Address);
            Controls.Add(address2Label);
            Controls.Add(Address2);
            Controls.Add(cityLabel);
            Controls.Add(citySelect);
            Controls.Add(postalCodeLabel);
            Controls.Add(postalCodeTextBox);
            Controls.Add(phoneLabel);
            Controls.Add(phoneTextBox);
            Controls.Add(countryLabel);
            Controls.Add(countrySelect);
            Controls.Add(saveButton);
            Controls.Add(cancelButton);
            Controls.Add(customerListBox);
            Controls.Add(listBoxLabel);
            Controls.Add(newCustomerBtn);
            Controls.Add(deleteCustomerBtn);
            Name = "CustomerRecords";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Customer Records";
            ResumeLayout(false);
            PerformLayout();
        }


        #endregion
        private TextBox nameTextBox;
        private Label nameLabel;
        private TextBox Address;
        private Label addressLabel;
        private TextBox Address2;
        private Label address2Label;
        private ComboBox citySelect;
        private Label cityLabel;
        private TextBox postalCodeTextBox;
        private Label postalCodeLabel;
        private TextBox phoneTextBox;
        private Label phoneLabel;
        private ComboBox countrySelect;
        private Label countryLabel;
        private Button saveButton;
        private Button cancelButton;
        private ListBox customerListBox;
        private Label listBoxLabel;
        private Button newCustomerBtn;
        private Button deleteCustomerBtn;
    }
}