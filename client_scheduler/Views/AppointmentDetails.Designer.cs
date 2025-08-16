using System.Drawing.Design;

namespace client_scheduler.Views
{
    partial class AppointmentDetails
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

        private void InitializeComponent()
        {
            listBoxLabel = new Label();
            listBox = new ListBox();
            customerNameLabel = new Label();
            customerNameSelect = new ComboBox();
            customerPhoneLabel = new Label();
            customerPhoneTextBox = new TextBox();
            appointmentTitleTextBox = new TextBox();
            appointmentDescriptionTextBox = new TextBox();
            cancelBtn = new Button();
            saveBtn = new Button();
            warningLabel = new Label();
            selectTimeSlotBtn = new Button();
            appointmentTime = new Label();
            newAppointmentBtn = new Button();
            typeLabel = new Label();
            typeSelect = new ComboBox();
            locationLabel = new Label();
            locationTextBox = new TextBox();
            urlLabel = new Label();
            urlTextBox = new TextBox();
            SuspendLayout();
            // 
            // listBoxLabel
            // 
            listBoxLabel.ForeColor = Color.White;
            listBoxLabel.Location = new Point(79, 43);
            listBoxLabel.Name = "listBoxLabel";
            listBoxLabel.Size = new Size(100, 16);
            listBoxLabel.TabIndex = 0;
            listBoxLabel.Text = "Appointment list";
            // 
            // listBox
            // 
            listBox.BackColor = Color.FromArgb(44, 44, 44);
            listBox.ForeColor = Color.White;
            listBox.ItemHeight = 15;
            listBox.Location = new Point(79, 62);
            listBox.Name = "listBox";
            listBox.Size = new Size(534, 64);
            listBox.TabIndex = 0;
            listBox.SelectedIndexChanged += UpdateActiveAppointment;
            // 
            // customerNameLabel
            // 
            customerNameLabel.ForeColor = Color.White;
            customerNameLabel.Location = new Point(79, 200);
            customerNameLabel.Name = "customerNameLabel";
            customerNameLabel.Size = new Size(103, 16);
            customerNameLabel.TabIndex = 0;
            customerNameLabel.Text = "Name";
            // 
            // customerNameSelect
            // 
            customerNameSelect.BackColor = Color.FromArgb(44, 44, 44);
            customerNameSelect.ForeColor = Color.White;
            customerNameSelect.Location = new Point(79, 218);
            customerNameSelect.Name = "customerNameSelect";
            customerNameSelect.Size = new Size(240, 23);
            customerNameSelect.TabIndex = 0;
            customerNameSelect.Text = "Customer Name";
            // 
            // customerPhoneLabel
            // 
            customerPhoneLabel.ForeColor = Color.White;
            customerPhoneLabel.Location = new Point(376, 200);
            customerPhoneLabel.Name = "customerPhoneLabel";
            customerPhoneLabel.Size = new Size(188, 16);
            customerPhoneLabel.TabIndex = 1;
            customerPhoneLabel.Text = "Phone";
            // 
            // customerPhoneTextBox
            // 
            customerPhoneTextBox.BackColor = Color.FromArgb(44, 44, 44);
            customerPhoneTextBox.BorderStyle = BorderStyle.None;
            customerPhoneTextBox.ForeColor = Color.White;
            customerPhoneTextBox.Location = new Point(376, 218);
            customerPhoneTextBox.Name = "customerPhoneTextBox";
            customerPhoneTextBox.ReadOnly = true;
            customerPhoneTextBox.Size = new Size(237, 16);
            customerPhoneTextBox.TabIndex = 0;
            customerPhoneTextBox.Text = "Phone";
            // 
            // appointmentTitleTextBox
            // 
            appointmentTitleTextBox.BackColor = Color.FromArgb(44, 44, 44);
            appointmentTitleTextBox.BorderStyle = BorderStyle.None;
            appointmentTitleTextBox.Font = new Font("Arial", 16F);
            appointmentTitleTextBox.ForeColor = Color.White;
            appointmentTitleTextBox.Location = new Point(79, 147);
            appointmentTitleTextBox.Name = "appointmentTitleTextBox";
            appointmentTitleTextBox.Size = new Size(534, 25);
            appointmentTitleTextBox.TabIndex = 0;
            appointmentTitleTextBox.Text = "Appointment Title";
            appointmentTitleTextBox.TextChanged += UpdateActiveTitle;
            // 
            // appointmentDescriptionTextBox
            // 
            appointmentDescriptionTextBox.BackColor = Color.FromArgb(44, 44, 44);
            appointmentDescriptionTextBox.BorderStyle = BorderStyle.None;
            appointmentDescriptionTextBox.ForeColor = Color.White;
            appointmentDescriptionTextBox.Location = new Point(79, 378);
            appointmentDescriptionTextBox.Multiline = true;
            appointmentDescriptionTextBox.Name = "appointmentDescriptionTextBox";
            appointmentDescriptionTextBox.Size = new Size(534, 85);
            appointmentDescriptionTextBox.TabIndex = 0;
            // 
            // cancelBtn
            // 
            cancelBtn.BackColor = Color.FromArgb(33, 33, 33);
            cancelBtn.FlatStyle = FlatStyle.Flat;
            cancelBtn.ForeColor = Color.White;
            cancelBtn.Location = new Point(79, 485);
            cancelBtn.Name = "cancelBtn";
            cancelBtn.Size = new Size(156, 38);
            cancelBtn.TabIndex = 4;
            cancelBtn.Text = "Cancel Appointment";
            cancelBtn.UseVisualStyleBackColor = false;
            cancelBtn.Click += cancelBtn_Click;
            // 
            // saveBtn
            // 
            saveBtn.BackColor = Color.FromArgb(33, 33, 33);
            saveBtn.FlatStyle = FlatStyle.Flat;
            saveBtn.ForeColor = Color.White;
            saveBtn.Location = new Point(457, 485);
            saveBtn.Name = "saveBtn";
            saveBtn.Size = new Size(156, 38);
            saveBtn.TabIndex = 5;
            saveBtn.Text = "Save Changes";
            saveBtn.UseVisualStyleBackColor = false;
            saveBtn.Click += SaveBtn_Click;
            // 
            // warningLabel
            // 
            warningLabel.ForeColor = Color.Red;
            warningLabel.Location = new Point(211, 9);
            warningLabel.Name = "warningLabel";
            warningLabel.Size = new Size(240, 36);
            warningLabel.TabIndex = 6;
            warningLabel.Text = "Warning label";
            // 
            // selectTimeSlotBtn
            // 
            selectTimeSlotBtn.FlatStyle = FlatStyle.Flat;
            selectTimeSlotBtn.ForeColor = Color.White;
            selectTimeSlotBtn.Location = new Point(310, 342);
            selectTimeSlotBtn.Name = "selectTimeSlotBtn";
            selectTimeSlotBtn.Size = new Size(120, 30);
            selectTimeSlotBtn.TabIndex = 7;
            selectTimeSlotBtn.Text = "Select Time Slot";
            selectTimeSlotBtn.Click += ShowTimeSlotSelector;
            // 
            // appointmentTime
            // 
            appointmentTime.BackColor = Color.White;
            appointmentTime.Font = new Font("Arial", 16F, FontStyle.Bold);
            appointmentTime.ForeColor = Color.Black;
            appointmentTime.Location = new Point(427, 342);
            appointmentTime.Name = "appointmentTime";
            appointmentTime.Size = new Size(186, 30);
            appointmentTime.TabIndex = 8;
            appointmentTime.Text = "12:00 - 12:15 PM";
            // 
            // newAppointmentBtn
            // 
            newAppointmentBtn.BackColor = Color.FromArgb(33, 33, 33);
            newAppointmentBtn.FlatStyle = FlatStyle.Flat;
            newAppointmentBtn.ForeColor = Color.White;
            newAppointmentBtn.Location = new Point(457, 33);
            newAppointmentBtn.Name = "newAppointmentBtn";
            newAppointmentBtn.Size = new Size(156, 23);
            newAppointmentBtn.TabIndex = 9;
            newAppointmentBtn.Text = "New Appointment";
            newAppointmentBtn.UseVisualStyleBackColor = false;
            newAppointmentBtn.Click += CreateNewAppointment;
            // 
            // typeLabel
            // 
            typeLabel.ForeColor = Color.White;
            typeLabel.Location = new Point(79, 330);
            typeLabel.Name = "typeLabel";
            typeLabel.Size = new Size(146, 16);
            typeLabel.TabIndex = 11;
            typeLabel.Text = "Appointment Type";
            // 
            // typeSelect
            // 
            typeSelect.BackColor = Color.FromArgb(44, 44, 44);
            typeSelect.ForeColor = Color.White;
            typeSelect.Location = new Point(79, 349);
            typeSelect.Name = "typeSelect";
            typeSelect.Size = new Size(195, 23);
            typeSelect.TabIndex = 10;
            // 
            // locationLabel
            // 
            locationLabel.ForeColor = Color.White;
            locationLabel.Location = new Point(376, 253);
            locationLabel.Name = "locationLabel";
            locationLabel.Size = new Size(188, 16);
            locationLabel.TabIndex = 13;
            locationLabel.Text = "Appointment Location";
            // 
            // locationTextBox
            // 
            locationTextBox.BackColor = Color.FromArgb(44, 44, 44);
            locationTextBox.BorderStyle = BorderStyle.None;
            locationTextBox.ForeColor = Color.White;
            locationTextBox.Location = new Point(376, 271);
            locationTextBox.Name = "locationTextBox";
            locationTextBox.ReadOnly = true;
            locationTextBox.Size = new Size(237, 16);
            locationTextBox.TabIndex = 12;
            // 
            // urlLabel
            // 
            urlLabel.ForeColor = Color.White;
            urlLabel.Location = new Point(79, 253);
            urlLabel.Name = "urlLabel";
            urlLabel.Size = new Size(191, 16);
            urlLabel.TabIndex = 15;
            urlLabel.Text = "URL";
            // 
            // urlTextBox
            // 
            urlTextBox.BackColor = Color.FromArgb(44, 44, 44);
            urlTextBox.BorderStyle = BorderStyle.None;
            urlTextBox.ForeColor = Color.White;
            urlTextBox.Location = new Point(79, 271);
            urlTextBox.Name = "urlTextBox";
            urlTextBox.Size = new Size(240, 16);
            urlTextBox.TabIndex = 14;
            urlTextBox.Text = "https://";
            // 
            // AppointmentDetails
            // 
            BackColor = Color.FromArgb(33, 33, 33);
            ClientSize = new Size(684, 561);
            Controls.Add(urlLabel);
            Controls.Add(urlTextBox);
            Controls.Add(locationLabel);
            Controls.Add(locationTextBox);
            Controls.Add(typeLabel);
            Controls.Add(typeSelect);
            Controls.Add(newAppointmentBtn);
            Controls.Add(listBoxLabel);
            Controls.Add(listBox);
            Controls.Add(customerNameLabel);
            Controls.Add(customerNameSelect);
            Controls.Add(customerPhoneLabel);
            Controls.Add(customerPhoneTextBox);
            Controls.Add(appointmentTitleTextBox);
            Controls.Add(appointmentDescriptionTextBox);
            Controls.Add(cancelBtn);
            Controls.Add(saveBtn);
            Controls.Add(warningLabel);
            Controls.Add(selectTimeSlotBtn);
            Controls.Add(appointmentTime);
            Name = "AppointmentDetails";
            ResumeLayout(false);
            PerformLayout();
        }

        private Label listBoxLabel;
        private ListBox listBox;
        private Label customerNameLabel;
        private ComboBox customerNameSelect;
        private Label customerPhoneLabel;
        private TextBox customerPhoneTextBox;
        private TextBox appointmentTitleTextBox;
        private TextBox appointmentDescriptionTextBox;
        private Button cancelBtn;
        private Button saveBtn;
        private Label warningLabel;
        private Button selectTimeSlotBtn;
        private Label appointmentTime;
        private Button newAppointmentBtn;
        private Label typeLabel;
        private ComboBox typeSelect;
        private Label locationLabel;
        private TextBox locationTextBox;
        private Label urlLabel;
        private TextBox urlTextBox;
    }
}