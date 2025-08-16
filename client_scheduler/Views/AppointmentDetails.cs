using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using client_scheduler.Models;
using client_scheduler.Services;
using client_scheduler.Util;
using MySqlX.XDevAPI.Common;

namespace client_scheduler.Views
{
    public partial class AppointmentDetails : Form
    {
        public DateTime SelectedDate { get; set; }
        public List<Appointment> DayAppointments { get; set; }
        public event EventHandler<string> AppointmentUpdated;
        public event EventHandler<string> AppointmentDeleted;
        public event EventHandler<Appointment> AppointmentCreated;
        private CustomerServices _customerServices = new CustomerServices();
        private AppointmentServices _appointmentServices = new AppointmentServices();
        private Appointment activeAppointment = new Appointment();
        private Customer activeCustomer = new Customer();
        private List<Customer> customers = new List<Customer>();
        public AppointmentDetails(DateTime date, List<Appointment> appointments)
        {
            InitializeComponent();
            SelectedDate = date;
            DayAppointments = appointments ?? new List<Appointment>();
            InitializeForm();
        }

        private void InitializeForm()
        {
            this.Size = new Size(700, 600);
            this.Text = $"Appointments for {SelectedDate.ToString("MMMM dd, yyyy")}";
            this.StartPosition = FormStartPosition.CenterParent;

            warningLabel.Text = "";

            typeSelect.Items.Add("Onboarding");
            typeSelect.Items.Add("Consultation");
            typeSelect.Items.Add("Mentoring");

            appointmentTitleTextBox.TextChanged += (s, e) =>
            {
                activeAppointment.title = appointmentTitleTextBox.Text;
            };

            appointmentDescriptionTextBox.TextChanged += (s, e) =>
            {
                activeAppointment.description = appointmentDescriptionTextBox.Text;
            };

            typeSelect.SelectedIndex = 0;
            typeSelect.SelectedIndexChanged += (s, e) =>
            {
                if (typeSelect.SelectedItem != null)
                {
                    UpdateType(typeSelect.SelectedItem.ToString());
                }
            };

            urlTextBox.TextChanged += (s, e) =>
            {
                activeAppointment.url = urlTextBox.Text;
            };



            GetAllCustomers();

            customerNameSelect.SelectedIndexChanged += (s, e) =>
            {
                Console.WriteLine($"Customer Name changed. New index {customerNameSelect.SelectedIndex}");
                if (customers.Count > customerNameSelect.SelectedIndex)
                {
                    UpdateCustomer(customerNameSelect.SelectedIndex);
                    MapActiveAppointmentValues(activeAppointment, activeCustomer);
                }
            };


            if (DayAppointments.Count > 0)
            {
                PopulateAppointmentsList();
            }
            else
            {
                StageNewAppointment();
            }
        }

        private void GetAllCustomers()
        {
            Response response = _customerServices.GetAll();
            if (response.success)
            {
                for (int i = 0; i < response.data.Rows.Count; i++)
                {
                    var row = response.data.Rows[i];

                    Customer customer = new Customer();
                    customer.Name = row["customerName"].ToString();
                    customer.Id = Convert.ToInt32(row["customerId"].ToString());
                    customer.AddressId = Convert.ToInt32(row["addressId"].ToString());
                    customer.Address = row["address"].ToString();
                    customer.Address2 = row["address2"].ToString();
                    customer.CityId = Convert.ToInt32(row["cityId"].ToString());
                    customer.City = row["city"].ToString();
                    customer.PostalCode = row["postalCode"].ToString();
                    customer.Phone = row["phone"].ToString();
                    customer.CountryId = Convert.ToInt32(row["countryId"].ToString());
                    customer.Country = row["country"].ToString();

                    customerNameSelect.Items.Add(customer.Name);
                    customers.Add(customer);
                }
            }
        }

        private void PopulateAppointmentsList()
        {
            listBox.Items.Clear();
            foreach (var appointment in DayAppointments.OrderBy(x => x.start))
            {
                string displayText = $"{appointment.start:h:mm} - {appointment.end:h:mm tt} | {appointment.title}";
                listBox.Items.Add(displayText);
            }
            if (DayAppointments.Count > 0)
            {
                Response response = _customerServices.GetCustomer(DayAppointments[0].customerId);
                Customer customer = null;
                if (response.data.Rows.Count > 0)
                {
                    customer = new Customer
                    {
                        Id = Convert.ToInt32(response.data.Rows[0]["customerId"].ToString()),
                        Name = response.data.Rows[0]["customerName"].ToString(),
                        Active = Convert.ToBoolean(response.data.Rows[0]["active"]),
                        AddressId = Convert.ToInt32(response.data.Rows[0]["addressId"].ToString()),
                        Address = response.data.Rows[0]["address"].ToString(),
                        Address2 = response.data.Rows[0]["address2"].ToString(),
                        Phone = response.data.Rows[0]["phone"].ToString(),
                        CityId = Convert.ToInt32(response.data.Rows[0]["cityId"].ToString()),
                        City = response.data.Rows[0]["city"].ToString(),
                        PostalCode = response.data.Rows[0]["postalCode"].ToString(),
                        CountryId = Convert.ToInt32(response.data.Rows[0]["countryId"].ToString()),
                        Country = response.data.Rows[0]["country"].ToString()
                    };
                    activeCustomer = customer;

                }
                activeAppointment = DayAppointments[0];
                MapActiveAppointmentValues(activeAppointment, activeCustomer);
                listBox.SelectedIndex = 0;
            }
        }

        private void StageNewAppointment()
        {
            DateTime nowEastern = TimeZoneHelper.ConvertToEastern(DateTime.Now);
            activeAppointment = new Appointment
            {
                appointmentId = 0,
                customerId = 0,
                userId = 1, // change this after storing user
                title = "Appointment Title",
                description = "Appointment description...",
                location = "",
                contact = "",
                type = typeSelect.Items[0].ToString(),
                url = "www.google.com",
                start = new DateTime(),
                end = new DateTime(), // this isn't correct. It should look for the first open slot
                startEastern = nowEastern,
                endEastern = nowEastern
            };
        }

        private void MapActiveAppointmentValues(Appointment appointment, Customer customer)
        {
            if (appointment != null)
            {
                appointmentTitleTextBox.Text = appointment.title;
                customerNameSelect.SelectedItem = customer.Name;
                customerPhoneTextBox.Text = customer.Phone;
                appointmentTime.Text = $"{TimeOnly.FromDateTime(appointment.start):h:mm} - {TimeOnly.FromDateTime(appointment.end):h:mm tt}";
                appointmentDescriptionTextBox.Text = appointment.description;
                typeSelect.SelectedItem = appointment.type;
                urlTextBox.Text = appointment.url;
                locationTextBox.Text = customer.Address;
                typeSelect.SelectedItem = appointment.type;
            }
        }

        private void UpdateActiveAppointment(object sender, EventArgs e)
        {
            ListBox listBox = (ListBox)sender;
            warningLabel.Text = "";
            if (listBox.SelectedIndex >= 0 && listBox.SelectedIndex < DayAppointments.Count)
            {
                activeAppointment = DayAppointments[listBox.SelectedIndex];
                Response response = _customerServices.GetCustomer(activeAppointment.customerId);
                if (response.data.Rows.Count > 0)
                {
                    Customer customer = new Customer
                    {
                        Id = Convert.ToInt32(response.data.Rows[0]["customerId"].ToString()),
                        Name = response.data.Rows[0]["customerName"].ToString(),
                        Active = Convert.ToBoolean(response.data.Rows[0]["active"]),
                        AddressId = Convert.ToInt32(response.data.Rows[0]["addressId"].ToString()),
                        Address = response.data.Rows[0]["address"].ToString(),
                        Address2 = response.data.Rows[0]["address2"].ToString(),
                        Phone = response.data.Rows[0]["phone"].ToString(),
                        CityId = Convert.ToInt32(response.data.Rows[0]["cityId"].ToString()),
                        City = response.data.Rows[0]["city"].ToString(),
                        PostalCode = response.data.Rows[0]["postalCode"].ToString(),
                        CountryId = Convert.ToInt32(response.data.Rows[0]["countryId"].ToString()),
                        Country = response.data.Rows[0]["country"].ToString()
                    };
                    MapActiveAppointmentValues(activeAppointment, customer);
                }
            }
        }


        private bool ValidateAppointment()
        {
            bool valid = true;
            List<string> missingList = new List<string>();

            if (activeAppointment.title == null || activeAppointment.title == string.Empty)
            {
                missingList.Add("Customer Id");
            }
            if(activeAppointment.type == null || activeAppointment.type == string.Empty)
            {
                missingList.Add("Appointment Type");
            }
            if (activeAppointment.start == DateTime.MinValue)
            {
                missingList.Add("Appointment Start");
            }
            if (activeAppointment.end == DateTime.MinValue)
            {
                missingList.Add("Appointment End");
            }

            if (missingList.Count > 0)
            {
                valid = false;
            }
            if (string.IsNullOrEmpty(activeCustomer.Name))
            {
                missingList.Add("Customer Name");
            }
            if (string.IsNullOrEmpty(activeCustomer.Phone))
            {
                missingList.Add("Customer Phone Number");
            }

            if (missingList.Count > 0)
            {
                valid = false;
                string errorString = "Please complete the following fields:\n\n";

                foreach()
                MessageBox.Show(errorString)
            }

            return valid;
        }

        private void SaveNewAppointment()
        {
            try
            {
                Response response = _appointmentServices.AddNewAppointment(activeAppointment, activeCustomer);
                if (response.success == true)
                {
                    warningLabel.Text = "Save successful!";
                    warningLabel.ForeColor = Color.Green;
                    Console.WriteLine("Save successful.");
                    AppointmentCreated?.Invoke(this, activeAppointment);
                    this.Close();
                }
                if (response.success == false)
                {
                    warningLabel.Text = "Save failed.";
                    warningLabel.ForeColor = Color.Red;
                    Console.WriteLine("Something went wrong..");
                }
            }
            catch (Exception ex)
            {
                warningLabel.Text = ex.Message;
                Console.WriteLine(ex.Message);
            }
        }

        private void UpdateAppointment()
        {
            try
            {
                Response response = _appointmentServices.SaveAppointment(activeAppointment, activeCustomer);
                if (response.success == true)
                {
                    warningLabel.Text = "Save successful!";
                    warningLabel.ForeColor = Color.Green;
                    Console.WriteLine("Save successful.");
                    AppointmentUpdated?.Invoke(this, response.message);
                    this.Close();
                }
                if (response.success == false)
                {
                    warningLabel.Text = "Save failed.";
                    warningLabel.ForeColor = Color.Red;
                    Console.WriteLine("Something went wrong..");
                }
            }
            catch (Exception ex)
            {
                warningLabel.Text = ex.Message;
                Console.WriteLine(ex.Message);
            }
        }

        private void SaveBtn_Click()
        {
            Dictionary<string, object> validation = ValidateAppointment();
            if ((bool)validation["isValid"] == false)
            {
                string invalidString = "The following parameters are missing values:\n\n";
                if (validation["invalidList"] is List<string> invalidList)
                {
                    foreach(string error in invalidList)
                    {
                        invalidString = $"{invalidString} {error}, ";
                    }
                }
                MessageBox.Show($"{invalidString}");
            }
            else
            {
                if (activeCustomer != null && activeCustomer.Id != null && activeAppointment.appointmentId != 0)
                {
                    UpdateAppointment();
                }
                else
                {
                    SaveNewAppointment();
                }
            }



        }

        private void DeleteSelectedAppointment(ListBox listBox)
        {
            // to-do
        }

        private void UpdateActiveTitle(object sender, EventArgs e)
        {
            if (activeAppointment != null && activeAppointment.title != null)
            {
                activeAppointment.title = appointmentTitleTextBox.Text;
            }
        }

        private void ShowTimeSlotSelector(object sender, EventArgs e)
        {
            try
            {
                TimeSlotSelectorForm timeSlotForm = new TimeSlotSelectorForm(SelectedDate, DayAppointments);

                if (timeSlotForm.ShowDialog() == DialogResult.OK)
                {
                    activeAppointment.start = SelectedDate.Date.Add(timeSlotForm.SelectedStartTime.ToTimeSpan());
                    activeAppointment.end = SelectedDate.Date.Add(timeSlotForm.SelectedEndTime.ToTimeSpan());

                    UpdateTimeDisplay();


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error selecting time slot: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateTimeDisplay()
        {
            TimeOnly startTime = TimeOnly.FromDateTime(activeAppointment.start);
            TimeOnly endTime = TimeOnly.FromDateTime(activeAppointment.end);
            appointmentTime.Text = $"{startTime:h:mm}-{endTime:h:mm tt}";
        }

        private void CreateNewAppointment(object sender, EventArgs e)
        {
            // new appointment
            activeCustomer = new Customer();
            activeAppointment = new Appointment();
            MapActiveAppointmentValues(activeAppointment, activeCustomer);
        }

        private void UpdateType(string type)
        {
            if (activeAppointment != null && activeAppointment.type != type)
            {
                activeAppointment.type = type;
            }
        }

        private void UpdateCustomer(int index)
        {
            if (index >= 0 && activeCustomer != null && customers.Count > 0 && customers[index].Id != null)
            {
                activeCustomer.Id = customers[index].Id;
                activeCustomer.Name = customers[index].Name;
                activeCustomer.AddressId = customers[index].AddressId;
                activeCustomer.Address = customers[index].Address;
                activeCustomer.Address2 = customers[index].Address2;
                activeCustomer.CityId = customers[index].CityId;
                activeCustomer.City = customers[index].City;
                activeCustomer.CountryId = customers[index].CountryId;
                activeCustomer.Country = customers[index].Country;
                activeCustomer.PostalCode = customers[index].PostalCode;
                activeCustomer.Phone = customers[index].Phone;
                
            }
            else
            {
                Customer newCustomer = new Customer();
                customers.Add(newCustomer);
            }
        }

        private void UpdateTitle(string newTitle)
        {
            activeAppointment.title = newTitle;
            appointmentTitleTextBox.Text = newTitle;
        }

        private void UpdateDescription(string newDescription)
        {
            activeAppointment.description = newDescription;
            appointmentDescriptionTextBox.Text = newDescription;
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            if (activeAppointment.appointmentId != null)
            {
                Response response = new Response();
                try
                {
                    response = _appointmentServices.DeleteAppointment(activeAppointment.appointmentId);

                    AppointmentDeleted?.Invoke(this, response.message);
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting appointment: {ex.Message}");
                }
            }
        }
    }
}
