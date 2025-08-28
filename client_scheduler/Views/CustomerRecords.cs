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
using client_scheduler.Services;

namespace client_scheduler.Views
{
    public partial class CustomerRecords : Form
    {
        List<Customer> customers = new List<Customer>();
        CustomerServices _customerServices = new CustomerServices();
        Customer activeCustomer = new Customer();
        AppUser activeUser = new AppUser();
        List<City> cityList = new List<City>();
        List<Country> countryList = new List<Country>();
        public event EventHandler<string> CustomerDeleted;

        public CustomerRecords(AppUser user)
        {
            this.activeUser = user;
            InitializeComponent();
            GetAllCities();
            GetAllCountries();
            GetAllCustomers();
            MapActiveCustomer();
        }

        private void GetAllCustomers()
        {
            customerListBox.Items.Clear();
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

                    customers.Add(customer);
                    customerListBox.Items.Add($"{customer.Name}");
                }

            }
        }

        private void UpdateActiveCustomer(object sender, EventArgs e)
        {
            if (customerListBox.SelectedIndex >= 0 && customerListBox.SelectedIndex < customers.Count)
            {
                activeCustomer = customers[customerListBox.SelectedIndex];

                MapActiveCustomer();

            }
        }

        private void MapActiveCustomer()
        {

            nameTextBox.DataBindings.Clear();
            Address.DataBindings.Clear();
            Address2.DataBindings.Clear();
            postalCodeTextBox.DataBindings.Clear();
            phoneTextBox.DataBindings.Clear();
            citySelect.DataBindings.Clear();
            countrySelect.DataBindings.Clear();

            nameTextBox.DataBindings.Add("Text", activeCustomer, "Name", true, DataSourceUpdateMode.OnPropertyChanged);
            Address.DataBindings.Add("Text", activeCustomer, "Address", true, DataSourceUpdateMode.OnPropertyChanged);
            Address2.DataBindings.Add("Text", activeCustomer, "Address2", true, DataSourceUpdateMode.OnPropertyChanged);
            postalCodeTextBox.DataBindings.Add("Text", activeCustomer, "PostalCode", true, DataSourceUpdateMode.OnPropertyChanged);
            phoneTextBox.DataBindings.Add("Text", activeCustomer, "Phone", true, DataSourceUpdateMode.OnPropertyChanged);
            citySelect.SelectedIndex = cityList.FindIndex(c => c.Id == activeCustomer.CityId);
            countrySelect.SelectedIndex = countryList.FindIndex(c => c.Id == activeCustomer.CountryId);
            
        }

        private void newCustomerBtn_Click(object sender, EventArgs e)
        {
            activeCustomer = new Customer
            {
                Id = 0,
                Name = "New Customer",
                Address = string.Empty,
                Address2 = string.Empty,
                PostalCode = string.Empty,
                Phone = string.Empty,
                CityId = -1,
                CountryId = -1
            };

            citySelect.SelectedIndex = -1;
            countrySelect.SelectedIndex = -1;

            nameTextBox.DataBindings.Clear();
            Address.DataBindings.Clear();
            Address2.DataBindings.Clear();
            postalCodeTextBox.DataBindings.Clear();
            phoneTextBox.DataBindings.Clear();
            citySelect.DataBindings.Clear();
            countrySelect.DataBindings.Clear();

            nameTextBox.DataBindings.Add("Text", activeCustomer, "Name", true, DataSourceUpdateMode.OnPropertyChanged);
            Address.DataBindings.Add("Text", activeCustomer, "Address", true, DataSourceUpdateMode.OnPropertyChanged);
            Address2.DataBindings.Add("Text", activeCustomer, "Address2", true, DataSourceUpdateMode.OnPropertyChanged);
            postalCodeTextBox.DataBindings.Add("Text", activeCustomer, "PostalCode", true, DataSourceUpdateMode.OnPropertyChanged);
            phoneTextBox.DataBindings.Add("Text", activeCustomer, "Phone", true, DataSourceUpdateMode.OnPropertyChanged);
            citySelect.DataBindings.Add("SelectedIndex", activeCustomer, "CityId", true, DataSourceUpdateMode.OnPropertyChanged);
            countrySelect.DataBindings.Add("SelectedIndex", activeCustomer, "CountryId", true, DataSourceUpdateMode.OnPropertyChanged);
        
        }

        private void citySelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (citySelect.SelectedIndex >= 0 && citySelect.SelectedIndex < cityList.Count)
            {
                activeCustomer.CityId = cityList[citySelect.SelectedIndex].Id;
                activeCustomer.City = cityList[citySelect.SelectedIndex].Name;
            }
        }

        private void countrySelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (countrySelect.SelectedIndex >= 0 && countrySelect.SelectedIndex < countryList.Count)
            {
                activeCustomer.CountryId = countryList[countrySelect.SelectedIndex].Id;
                activeCustomer.Country = countryList[countrySelect.SelectedIndex].Name;
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (!ValidateCustomer())
            {
                return;
            }
            if (activeCustomer.Id == null || activeCustomer.Id == 0)
            {

                TrimFields();
                Response response = _customerServices.SaveCustomer(activeCustomer, activeUser);
                if (response.success)
                {
                    MessageBox.Show("Customer saved successfully!");
                    GetAllCities();
                    GetAllCountries();
                    GetAllCustomers();
                }
                else
                {
                    MessageBox.Show($"Error saving customer: {response.message}");
                }
            }
            if (activeCustomer.Id != null && activeCustomer.Id != 0)
            {
                TrimFields();
                Response response = _customerServices.UpdateCustomer(activeCustomer, activeUser);
                if (response.success)
                {
                    MessageBox.Show("Customer updated successfully!");
                    GetAllCities();
                    GetAllCountries();
                    GetAllCustomers();
                }
                else
                {
                    MessageBox.Show($"Error updating customer: {response.message}");
                }
            }
            // close the form
            this.Close();
        }

        private void TrimFields()
        {
            activeCustomer.Name = activeCustomer.Name.Trim();
            activeCustomer.Address = activeCustomer.Address.Trim();
            activeCustomer.Address2 = activeCustomer.Address2.Trim();
            activeCustomer.PostalCode = activeCustomer.PostalCode.Trim();
            activeCustomer.Phone = activeCustomer.Phone.Trim();
        }

        private void GetAllCities()
        {

            citySelect.Items.Clear();
            Response response = _customerServices.GetAllCities();

            if (response.success)
            {

                for (int i = 0; i < response.data.Rows.Count; i++)
                {
                    City tmpCity = new City();
                    var row = response.data.Rows[i];
                    tmpCity.Id = Convert.ToInt32(row["cityId"].ToString());
                    tmpCity.Name = row["city"].ToString();
                    tmpCity.CountryId = Convert.ToInt32(row["countryId"].ToString());
                    cityList.Add(tmpCity);
                }

                foreach (var city in cityList)
                {
                    citySelect.Items.Add(city.Name);
                }
            }
            else
            {
                MessageBox.Show($"{response.message}");
            }
        }

        private void GetAllCountries()
        {
            // to-do

            countrySelect.Items.Clear();
            Response response = _customerServices.GetAllCountries();

            if (response.success)
            {

                for (int i = 0; i < response.data.Rows.Count; i++)
                {
                    Country tmpCountry = new Country();
                    var row = response.data.Rows[i];
                    tmpCountry.Id = Convert.ToInt32(row["countryId"].ToString());
                    tmpCountry.Name = row["country"].ToString();
                    countryList.Add(tmpCountry);
                }
                
                foreach (var country in countryList)
                {
                    countrySelect.Items.Add(country.Name);
                }
            }
            else
            {
                MessageBox.Show($"{response.message}");
            }
        }

        private bool ValidateCustomer()
        {
            bool valid = true;
            List<string> missingList = new List<string>();

            if (string.IsNullOrEmpty(activeCustomer.Name))
            {
                missingList.Add("Customer Name");
            }
            if (string.IsNullOrEmpty(activeCustomer.Address))
            {
                missingList.Add("Customer Address");
            } 
            
            if (string.IsNullOrEmpty(activeCustomer.Phone))
            {
                missingList.Add("Customer Phone");
            }
            if (PhoneIssues())
            {
                if (activeCustomer.Country == "USA")
                missingList.Add("Customer Phone (format: XXX-XXX-XXXX)");
                if (activeCustomer.Country == "England")
                    missingList.Add("Customer Phone (format: XXX-XXXX-XXXX)");
            }
            if (string.IsNullOrEmpty(activeCustomer.City))
            {
                missingList.Add("Customer City");
            }
            if (string.IsNullOrEmpty(activeCustomer.Country))
            {
                missingList.Add("Customer Country");
            }
            if (string.IsNullOrEmpty(activeCustomer.PostalCode))
            {
                missingList.Add("Customer Postal Code");
            }

            if (missingList.Count > 0)
            {
                valid = false;
            }

            if (missingList.Count > 0)
            {
                valid = false;
                string errorString = "Please complete the following fields:\n\n";

                foreach (var item in missingList)
                {
                    errorString += $"{item}\n";
                }
                MessageBox.Show(errorString);
            }

            return valid;
        }

        private bool PhoneIssues()
        {
            // check if activeCustomer.Phone is in the format XXX-XXX-XXXX where X is a digit
            string phone = activeCustomer.Phone;
            if (activeCustomer.Country == "USA")
            {
                if (phone.Length != 12)
                    return true;

                // Check first three characters are digits
                for (int i = 0; i < 12; i++)
                {
                    if (i == 3 || i == 7)
                    {
                        i++;
                    }
                    if (!char.IsDigit(phone[i]))
                        return true;
                }

                // Check fourth character is a dash
                if (phone[3] != '-' || phone[7] != '-')
                    return true;

                return false;
            }
            if (activeCustomer.Country == "England")
            {
                // format is XXX-XXXX-XXXX
                if (phone.Length != 13)
                    return true;
                // Check first two characters are digits
                for (int i = 0; i < 12; i++)
                {
                    if (i == 3 || i == 8)
                    {
                        i++;
                    }
                    if (!char.IsDigit(phone[i]))
                        return true;
                }
                // Check second and seventh characters are a dash
                if (phone[3] != '-' || phone[8] != '-')
                    return true;

                return false;
            }
            return true;
        }

        private void ValidatePhoneChange(object sender, EventArgs e)
        {
            // phone should only included numbers and dashes
            TextBox phoneTB = (TextBox)sender;
            string original = phoneTB.Text;
            string validText = "";

            // Remove any characters that aren't numbers or dashes
            foreach (char c in original)
            {
                if (char.IsDigit(c) || c == '-')
                {
                    validText += c;
                }
            }

            // remove double dashes
            validText = validText.Replace("--", "-");

            // If the text changed, update it
            if (validText != original)
            {
                phoneTB.Text = validText;
                phoneTB.SelectionStart = validText.Length; // Move cursor to the end
            }

            activeCustomer.Phone = phoneTextBox.Text;
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DeleteCustomerBtn_Click(object sender, EventArgs e)
        {
            if (activeCustomer.Id == null || activeCustomer.Id == 0)
            {
                MessageBox.Show("Please select a customer to delete.");
                return;
            }

            // delete all customer appointments first

            Response response = _customerServices.DeleteCustomer(activeCustomer.Id, activeUser);

            if (response.success)
            {
                MessageBox.Show(response.message);
                GetAllCities();
                GetAllCountries();
                GetAllCustomers();
                CustomerDeleted?.Invoke(this, response.message);
            }
            else
            {
                MessageBox.Show($"Error deleting customer: {response.message}");
            }
            this.Close();
        }
    }
}
