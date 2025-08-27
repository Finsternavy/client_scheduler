using client_scheduler.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace client_scheduler.Views
{
    public partial class Reports : Form
    {
        ReportGenerator reportGenerator = new ReportGenerator();
        Dictionary<string, List<AppointmentTypeReport>> appointmentTypes = new Dictionary<string, List<AppointmentTypeReport>>();
        Dictionary<int, List<UserScheduleReport>> userSchedules = new Dictionary<int, List<UserScheduleReport>>();
        Dictionary<string, LocationReport> locationReports = new Dictionary<string, LocationReport>();
        ReportDisplayData appointmentTypesDisplay = new ReportDisplayData();
        ReportDisplayData userSchedulesDisplay = new ReportDisplayData();
        ReportDisplayData locationReportsDisplay = new ReportDisplayData();
        public Reports()
        {
            InitializeComponent();
        }

        private void allReportsBtn_Click(object sender, EventArgs e)
        {
            try
            {
                AllReports allReports = reportGenerator.GenerateAllReports();

                if (allReports != null)
                {
                    appointmentTypes = allReports.AppointmentTypes;
                    userSchedules = allReports.UserSchedules;
                    locationReports = allReports.Locations;

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating reports: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //DisplayReportsInDataGridView(reportsGrid, appointmentTypesDisplay);

        }

        private void ByLocation_Clicked(object sender, EventArgs e)
        {
            locationReportsDisplay = reportGenerator.GenerateLocationReport();
            DisplayReportsInDataGridView(reportsGrid, locationReportsDisplay);
        }

        private void ByUser_Clicked(object sender, EventArgs e)
        {
            userSchedulesDisplay = reportGenerator.GenerateUserScheduleReport();
            DisplayReportsInDataGridView(reportsGrid, userSchedulesDisplay);
        }

        private void ByType_Clicked(object sender, EventArgs e)
        {
            appointmentTypesDisplay = reportGenerator.GenerateAppointmentTypesReport();
            DisplayReportsInDataGridView(reportsGrid, appointmentTypesDisplay);
        }

        private void DisplayReportsInDataGridView(DataGridView dgv, ReportDisplayData reportData)
        {
            dgv.Columns.Clear();
            dgv.Rows.Clear();

            foreach (string header in reportData.Headers)
            {
                dgv.Columns.Add(header, header);
            }
            dgv.RowHeadersVisible = false;

            foreach (var row in reportData.Rows)
            {
                dgv.Rows.Add(row.ToArray());
            }
            dgv.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);

            int totalWidth = dgv.Width - 20;
            int usedWidth = dgv.Columns.Cast<DataGridViewColumn>().Sum(c => c.Width);
            int remainingWidth = totalWidth - usedWidth;

            if (remainingWidth > 0 && dgv.Columns.Count > 0)
            {
                int extraWidthPerColumn = remainingWidth / dgv.Columns.Count;
                foreach (DataGridViewColumn column in dgv.Columns)
                {
                    column.Width += extraWidthPerColumn;
                }
            }

            this.Text = reportData.Title;

            if (reportData.Summary.Count > 0)
            {
                string summary = string.Join(", ", reportData.Summary.Select(kv => $"{kv.Key}: {kv.Value}"));
                MessageBox.Show(summary, "Report Summary", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
