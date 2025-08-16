using client_scheduler.Services;
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
using client_scheduler.Util;

namespace client_scheduler.Views
{
    public partial class Dashboard : Form
    {
        private AppointmentServices _appointmentServices = new AppointmentServices();
        private BindingSource source;
        private DateTime currentDate;
        private DataGridView calendarGrid;
        public List<Appointment> appointmentList;
        public Dashboard()
        {
            InitializeComponent();
            InitializeCalendarView();
            currentDate = DateTime.Now;
            appointmentList = new List<Appointment>();
            LoadAppointments();
            UpdateCalendarGrid();
        }

        private void InitializeCalendarView()
        {
            this.Text = "Calendar View";

            calendarGrid = new DataGridView
            {
                ColumnCount = 7,
                RowCount = 7,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.CellSelect,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                RowHeadersVisible = false,
                ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize,
                GridColor = Color.LightGray,
                Location = new Point(10, 89),
                Name = "calendarGrid",
                Size = new Size(800, 760),

            };

            calendarGrid.CellClick += CalendarGrid_CellClick;

            int totalWidth = 800;
            int columnWidth = totalWidth / 7;
            // headers
            string[] dayNames = {
                "Sunday",
                "Monday",
                "Tuesday",
                "Wednesday",
                "Thursday",
                "Friday",
                "Saturday"
            };

            for (int i = 0; i < 7; i++)
            {
                calendarGrid.Columns[i].HeaderText = dayNames[i];
                calendarGrid.Columns[i].Width = columnWidth;
                calendarGrid.Columns[i].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                calendarGrid.DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft;
                if (i < 6)
                {
                    calendarGrid.Rows[i].Height = columnWidth;
                }
            }

            /*
             * calendarGrid.Rows[0].HeaderCell.Value = "Week 1";
            calendarGrid.Rows[1].HeaderCell.Value = "Week 2";
            calendarGrid.Rows[2].HeaderCell.Value = "Week 3";
            calendarGrid.Rows[3].HeaderCell.Value = "Week 4";
            calendarGrid.Rows[4].HeaderCell.Value = "Week 5";
            calendarGrid.Rows[5].HeaderCell.Value = "Week 6";
            */

            calendarGrid.ColumnHeadersDefaultCellStyle.BackColor = Color.LightBlue;
            calendarGrid.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 10, FontStyle.Bold);
            calendarGrid.RowHeadersDefaultCellStyle.BackColor = Color.LightGray;
            calendarGrid.RowHeadersDefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);

            this.Controls.Add(calendarGrid);
            //source = new BindingSource();
            //calendarGrid.DataSource = source;
            //LoadData();

        }

        private void UpdateCalendarGrid()
        {
            // clear existing
            for (int row = 0; row < 6; row++)
            {
                for (int col = 0; col < 7; col++)
                {
                    calendarGrid.Rows[row].Cells[col].Value = "";
                    calendarGrid.Rows[row].Cells[col].Style.BackColor = Color.White;
                }
            }

            DateTime firstDayOfMonth = new DateTime(currentDate.Year, currentDate.Month, 1);
            int daysInMonth = DateTime.DaysInMonth(currentDate.Year, currentDate.Month);
            int startDayOfWeek = (int)firstDayOfMonth.DayOfWeek;

            int dayCounter = 1;
            for (int row = 0; row < 6; row++)
            {
                for (int col = 0; col < 7; col++)
                {
                    if ((row == 0 && col < startDayOfWeek) || dayCounter > daysInMonth)
                    {
                        calendarGrid.Rows[row].Cells[col].Value = "";
                        calendarGrid.Rows[row].Cells[col].Style.BackColor = Color.LightGray;
                    }
                    else
                    {
                        DateTime cellDate = new DateTime(currentDate.Year, currentDate.Month, dayCounter);
                        
                        var dayAppointments = appointmentList.Where(x => x.start.Date == cellDate.Date).ToList();
                        dayAppointments = dayAppointments.OrderBy(x => x.start).ToList();
                        string cellContent = dayCounter.ToString();

                        if (dayAppointments.Any())
                        {
                            cellContent += "\n";
                            foreach (var appointment in dayAppointments.Take(1))
                            {
                                cellContent += $"{appointment.title}\nType: {appointment.type}\nStart: {appointment.start.ToString("HH:mm")} - {appointment.end.ToString("HH:mm")}";
                            }
                            if (dayAppointments.Count > 1)
                            {
                                cellContent += $"... +{dayAppointments.Count - 1} more";
                            }
                        }

                        calendarGrid.Rows[row].Cells[col].Value = cellContent;

                        if (dayCounter == DateTime.Now.Day &&
                            currentDate.Month == DateTime.Now.Month &&
                            currentDate.Year == DateTime.Now.Year)
                        {
                            calendarGrid.Rows[row].Cells[col].Style.BackColor = Color.Yellow;
                            calendarGrid.Rows[row].Cells[col].Style.Font = new Font("Arial", 10, FontStyle.Bold);
                        }

                        dayCounter++;
                    }
                }
            }
        }

        private void CalendarGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                DateTime firstDayOfMonth = new DateTime(currentDate.Year, currentDate.Month, 1);
                int startDayOfWeek = (int)firstDayOfMonth.DayOfWeek;

                int dayNumber = (e.RowIndex * 7 + e.ColumnIndex) - startDayOfWeek + 1;

                if (dayNumber > 0 && dayNumber <= DateTime.DaysInMonth(currentDate.Year, currentDate.Month))
                {
                    DateTime clickedDay = GetDateFromCell(e.RowIndex, e.ColumnIndex);
                    if (clickedDay.DayOfWeek == DayOfWeek.Saturday || clickedDay.DayOfWeek == DayOfWeek.Sunday)
                    {
                        MessageBox.Show("No appointment available on weekends");
                    }
                    else
                    {
                        DateTime clickDate = new DateTime(currentDate.Year, currentDate.Month, dayNumber);
                        var dayAppointments = appointmentList.Where(x => x.start.Date == clickDate.Date).ToList();

                        var detailsForm = new AppointmentDetails(clickDate, dayAppointments.OrderBy(x => x.start).ToList());

                        detailsForm.AppointmentCreated += OnAppointmentCreated;
                        detailsForm.AppointmentUpdated += OnAppointmentUpdated;
                        detailsForm.AppointmentDeleted += OnAppointmentDeleted;

                        detailsForm.ShowDialog();
                    }
                    
                }
            }
        }

        private DateTime GetDateFromCell(int row, int col)
        {
            DateTime firstDayOfMonth = new DateTime(currentDate.Year, currentDate.Month, 1);
            int startDayOfWeek = (int)firstDayOfMonth.DayOfWeek;
            int dayNumber = (row * 7) + col - startDayOfWeek + 1;

            return new DateTime(currentDate.Year, currentDate.Month, dayNumber);
        }

        private void OnAppointmentCreated(object sender, Appointment appointment)
        {
            MessageBox.Show("New Appointment Added!");
            LoadAppointments();
            UpdateCalendarGrid();
        }

        private void OnAppointmentUpdated(object sender, string message)
        {
            MessageBox.Show(message);
            LoadAppointments();
            UpdateCalendarGrid();
        }

        private void OnAppointmentDeleted(object sender, string message)
        {
            MessageBox.Show(message);
            LoadAppointments();
            UpdateCalendarGrid();
        }

        private void UpdateAppointmentInDatabase(Appointment appointment)
        {
            // to-do
        }

        private void DeleteAppointmentFromDatabase(Appointment appointment)
        {
            // to-do
        }

        private void LoadAppointments()
        {
            Response response = null;
            appointmentList.Clear();
            try
            {
                response = _appointmentServices.GetAppointments();
                if (response.data.Rows.Count > 0)
                {
                    // good things are happening
                    //source.DataSource = response.data;
                    DataTable dt = response.data;
                    foreach (DataRow row in dt.Rows)
                    {
                        Appointment tmp = MapToAppointment(row);
                        appointmentList.Add(tmp);
                        Console.Write($"Appointment: {tmp}");
                    }
                    appointmentList.OrderBy(x => x.start).ToList();
                }

            }
            catch (Exception ex)
            {
                // bad things are happening
            }

        }

        private Appointment MapToAppointment(DataRow row)
        {
            DateTime startTimeEastern = Convert.ToDateTime(row["start"].ToString());
            DateTime endTimeEastern = Convert.ToDateTime(row["end"].ToString());

            DateTime startTimeLocal = TimeZoneHelper.ConvertFromEastern(startTimeEastern);
            DateTime endTimeLocal = TimeZoneHelper.ConvertFromEastern(endTimeEastern);
            return new Appointment
            {
                appointmentId = Convert.ToInt32(row["appointmentId"].ToString()),
                customerId = Convert.ToInt32(row["customerId"].ToString()),
                userId = Convert.ToInt32(row["userId"].ToString()),
                title = row["title"].ToString(),
                description = row["description"].ToString(),
                location = row["location"].ToString(),
                contact = row["contact"].ToString(),
                type = row["type"].ToString(),
                url = row["url"].ToString(),
                start = startTimeLocal,
                end = endTimeLocal,
                startEastern = startTimeEastern,
                endEastern = endTimeEastern

            };

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                Application.Exit();
            }
            base.OnFormClosing(e);
        }

        private void customerRecordsBtn_Click(object sender, EventArgs e)
        {
            // open customer editor form
        }
    }
}
