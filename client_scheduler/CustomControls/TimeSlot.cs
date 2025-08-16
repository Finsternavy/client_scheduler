using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace client_scheduler.CustomControls
{
    public class TimeSlot : UserControl
    {
        public TimeOnly Start {  get; set; }
        public TimeOnly End { get; set; }
        public bool IsAvailable { get; set; } = true;
        public bool IsSelected { get; set; } = false;
        public event EventHandler<TimeSlot> TimeSlotClicked;

        public TimeSlot(TimeOnly startTime,  TimeOnly endTime)
        {
            Start = startTime;
            End = endTime;
            InitializeComponent();
        }

        public void InitializeComponent()
        {
            this.Size = new Size(120, 40);
            this.BackColor = Color.FromArgb(44, 44, 44);
            this.ForeColor = Color.White;
            this.BorderStyle = BorderStyle.None;
            this.Cursor = Cursors.Hand;

            this.Click += TimeSlot_Click;
            this.Paint += TimeSlot_Paint;
        }

        private void TimeSlot_Click(object sender, EventArgs e)
        {
            if (IsAvailable)
            {
                IsSelected = !IsSelected;
                this.Invalidate();
                TimeSlotClicked?.Invoke(this, this);
            }
        }

        private void TimeSlot_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Rectangle rect = this.ClientRectangle;

            if (!IsAvailable)
            {
                g.FillRectangle(Brushes.LightGray, rect);
            }
            else if (IsSelected)
            {
                g.FillRectangle(Brushes.LightBlue, rect);
            }
            else
            {
                g.FillRectangle(new SolidBrush(Color.FromArgb(44,44,44)), rect);
            }

            Color borderColor = IsSelected ? Color.Blue : Color.Transparent;
            g.DrawRectangle(new Pen(borderColor, 2), rect.X + 1, rect.Y + 1, rect.Width - 2, rect.Height - 2);

            string timeText = $"{Start:h:mm} - {End:h:mm tt}";
            using (Font font = new Font("Arial", 9, FontStyle.Bold))
            {
                SizeF textSize = g.MeasureString(timeText, font);
                float x = (rect.Width - textSize.Width) / 2;
                float y = (rect.Height - textSize.Height) / 2;

                Color textColor = IsAvailable ? Color.White : Color.Gray;
                g.DrawString(timeText, font, new SolidBrush(textColor), x, y);
            }

            if (!IsAvailable)
            {
                g.DrawString("BUSY", new Font("Arial", 7, FontStyle.Bold), Brushes.Red, 5, 5);
            }
            else if (IsSelected)
            {
                g.DrawString("SELECTED", new Font("Arial", 7, FontStyle.Bold), Brushes.Green, 5, 5);
            }
        }

        public void SetAvailable(bool available)
        {
            IsAvailable = available;
            this.Invalidate();
        }
    }
}
