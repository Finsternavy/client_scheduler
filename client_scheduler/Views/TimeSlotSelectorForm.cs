using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using client_scheduler.Models;
using client_scheduler.CustomControls;
using client_scheduler.Util;

namespace client_scheduler.Views
{
    public partial class TimeSlotSelectorForm : Form
    {
        public TimeOnly SelectedStartTime { get; set; }
        public TimeOnly SelectedEndTime { get; set; }
        private List<Appointment> existingAppointments;
        private DateTime selectedDate;
        public TimeSlotSelectorForm(DateTime date, List<Appointment> appointments)
        {
            selectedDate = date;
            existingAppointments = appointments ?? new List<Appointment>();
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Size = new Size(600, 400);
            this.Text = $"Select Time Slot for {selectedDate.ToString("MMMM dd, yyyy")}";
            this.StartPosition = FormStartPosition.CenterParent;

            var titleLabel = new Label
            {
                Text = $"Available Time Slots for {selectedDate.ToString("dddd, MMMM dd, yyyy")}",
                Font = new Font("Arial", 12, FontStyle.Bold),
                Location = new Point(20, 20),
                Size = new Size(400, 30)
            };

            var timeSlotPanel = new FlowLayoutPanel
            {
                Location = new Point(20, 60),
                Size = new Size(540, 250),
                AutoScroll = true,
                BorderStyle = BorderStyle.FixedSingle
            };

            var selectButton = new Button
            {
                Text = "Select Time Slot",
                Location = new Point(20, 320),
                Size = new Size(120, 30),
                Enabled = false
            };

            selectButton.Click += (s, e) =>
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            };

            var cancelButton = new Button
            {
                Text = "Cancel",
                Location = new Point(150, 320),
                Size = new Size(80, 30)
            };
            cancelButton.Click += (s, e) =>
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            };

            this.Controls.AddRange(new Control[] { titleLabel, timeSlotPanel, selectButton, cancelButton });
            PopulateTimeSlots(timeSlotPanel, selectButton);
        }

        private void PopulateTimeSlots(FlowLayoutPanel timeSlotPanel, Button selectButton)
        {

            DateTime startEastern = DateTime.Today.AddHours(9);
            DateTime endEastern = DateTime.Today.AddHours(17);
            DateTime startLocal = TimeZoneHelper.ConvertFromEastern(startEastern);
            DateTime endLocal = TimeZoneHelper.ConvertFromEastern(endEastern);
            TimeOnly startTime = TimeOnly.FromDateTime(startLocal); 
            TimeOnly endTime = TimeOnly.FromDateTime(endLocal); 

            int slotCount = 0;
            while (startTime < endTime)
            {
                TimeOnly slotEndTime = startTime.AddMinutes(15);

                bool isAvailable = IsTimeSlotAvailable(startTime, slotEndTime);

                var timeSlot = new TimeSlot(startTime, slotEndTime);
                timeSlot.SetAvailable(isAvailable);

                if (isAvailable)
                {
                    timeSlot.TimeSlotClicked += (sender, slot) =>
                    {
                        foreach (Control control in timeSlotPanel.Controls)
                        {
                            if (control is TimeSlot otherSlot && otherSlot != slot)
                            {
                                otherSlot.IsSelected = false;
                                otherSlot.Invalidate();
                            }
                        }

                        SelectedStartTime = slot.Start;
                        SelectedEndTime = slot.End;

                        selectButton.Enabled = true;
                    };
                }
                timeSlotPanel.Controls.Add(timeSlot);
                startTime = slotEndTime;
                slotCount++;
            }
        }

        private bool IsTimeSlotAvailable(TimeOnly startTime, TimeOnly endTime)
        {
            foreach(Appointment appointment in existingAppointments)
            {
                if (appointment.start.Date == selectedDate.Date)
                {
                    TimeOnly start = TimeOnly.FromDateTime(appointment.start);
                    TimeOnly end = TimeOnly.FromDateTime(appointment.end);
                    if (!(endTime <= start || startTime >= end))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private void selectBtn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
