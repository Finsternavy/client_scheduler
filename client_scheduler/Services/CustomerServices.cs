using client_scheduler.Models;
using client_scheduler.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using client_scheduler.Services;

namespace client_scheduler.Services
{
    internal class CustomerServices
    {
        AppointmentServices _appointmentServices = new AppointmentServices();
        public Response GetCustomer(int id)
        {
            string query = $"SELECT c.customerId, c.customerName, c.addressId, c.active, a.address, a.address2, a.cityId, a.postalCode, a.phone, ci.city, ci.countryId, co.country FROM customer c LEFT JOIN address a On c.addressId = a.addressId LEFT JOIN city ci ON a.cityId = ci.cityId LEFT JOIN country co ON ci.countryId = co.countryId WHERE c.customerId = {id}";
            Response response = new Response();
            try
            {
                response.data = DatabaseHelper.ExecuteQuery(query);
                if (response.data.Rows.Count > 0)
                {
                    response.success = true;
                }
                else
                {
                    response.success = false;
                }

            }
            catch (Exception ex)
            {
                response.message = ex.Message;
            }
            return response;
        }

        public Response GetAll()
        {
            string query = $"SELECT " +
                $"c.customerId, " +
                $"c.customerName, " +
                $"c.addressId, " +
                $"c.active, " +
                $"a.address, " +
                $"a.address2, " +
                $"a.cityId, " +
                $"a.postalCode, " +
                $"a.phone, " +
                $"ci.city, " +
                $"ci.countryId, " +
                $"co.country " +
                $"FROM customer c LEFT JOIN address a On c.addressId = a.addressId LEFT JOIN city ci ON a.cityId = ci.cityId LEFT JOIN country co ON ci.countryId = co.countryId";
            Response response = new Response();
            try
            {
                response.data = DatabaseHelper.ExecuteQuery(query);
                if (response.data.Rows.Count > 0)
                {
                    response.success = true;
                }
                else
                {
                    response.success = false;
                }

            }
            catch (Exception ex)
            {
                response.message = ex.Message;
            }
            return response;
        }

        public Response SaveCustomer(Customer customer, AppUser user)
        {
            // because of the way ids are linked, save order must be:
            // country, city, address, customer

            DateTime now = DateTime.Now;
            if (customer.CountryId == 0 || customer.CountryId == null)
            {
                var saveCountry = SaveCountry(customer, user, now);
                customer.CountryId = Convert.ToInt32(SaveCountry(customer, user, now).data.Rows[0]["countryId"]);
            }
            //var saveCountryResponse = SaveCountry(customer, user, now);
            //customer.CountryId = Convert.ToInt32(saveCountryResponse.data.Rows[0]["Id"]);
            if (customer.CityId == 0 || customer.CityId == null)
            {
                customer.CityId = Convert.ToInt32(SaveCity(customer, user, now).data.Rows[0]["cityId"]);
            }
            //var saveCityResponse = SaveCity(customer, user, now);
            //customer.CityId = Convert.ToInt32(saveCityResponse.data.Rows[0]["Id"]);
            var saveAddressResponse = SaveAddress(customer, user, now);
            customer.AddressId = Convert.ToInt32(saveAddressResponse.data.Rows[0]["addressId"]);

            string query = $"INSERT INTO customer " +
                $"(" +
                $"customerName," +
                $"addressId," +
                $"active," +
                $"createDate," +
                $"createdBy," +
                $"lastUpdate," +
                $"lastUpdateBy" +
                $")" +
                $"VALUES  (" +
                $"'{customer.Name}'," +
                $"'{customer.AddressId}'," +
                $"'1'," +
                $"'{now:yyyy:MM:dd HH:mm:ss}'," +
                $"'{user.Name}'," +
                $"'{now:yyyy:MM:dd HH:mm:ss}'," +
                $"'{user.Name}');";
            Response response = new Response();
            try
            {
                var rowsAffected = DatabaseHelper.ExecuteNonQuery(query);
                if (rowsAffected > 0)
                {
                    response.success = true;
                    var getIdQuery = $"SELECT * FROM customer WHERE customerName = '{customer.Name}' AND addressId = '{customer.AddressId}'";
                    response.data = DatabaseHelper.ExecuteQuery(getIdQuery);
                }
                else
                {
                    response.success = false;
                    response.message = "Customer not saved.";
                }

            }
            catch (Exception ex)
            {
                response.message = ex.Message;
            }
            return response;
        }

        public Response SaveAddress(Customer customer, AppUser user, DateTime now)
        {
            string query = $"INSERT INTO address " +
                $"(" +
                $"address," +
                $"address2," +
                $"postalCode," +
                $"phone," +
                $"cityId," +
                $"createDate," +
                $"createdBy," +
                $"lastUpdate," +
                $"lastUpdateBy" +
                $")" +
                $"VALUES  (" +
                $"'{customer.Address}'," +
                $"'{customer.Address2}'," +
                $"'{customer.PostalCode}'," +
                $"'{customer.Phone}'," +
                $"'{customer.CityId}'," +
                $"'{now:yyyy:MM:dd HH:mm:ss}'," +
                $"'{user.Name}'," +
                $"'{now:yyyy:MM:dd HH:mm:ss}'," +
                $"'{user.Name}');";
            Response response = new Response();
            try
            {
                var rowsAffected = DatabaseHelper.ExecuteNonQuery(query);
                if (rowsAffected > 0)
                {
                    response.success = true;
                    var getIdQuery = $"SELECT * FROM address WHERE address = '{customer.Address}' AND cityId = '{customer.CityId}';";
                    response.data = DatabaseHelper.ExecuteQuery(getIdQuery);
                }
                else
                {
                    response.success = false;
                    response.message = "Address not saved.";
                }

            }
            catch (Exception ex)
            {
                response.message = ex.Message;
            }
            return response;
        }

        public Response SaveCity(Customer customer, AppUser user, DateTime now)
        {
            string query = $"INSERT INTO city " +
                $"(" +
                $"city," +
                $"countryId," +
                $"createDate," +
                $"createdBy," +
                $"lastUpdate," +
                $"lastUpdateBy" +
                $")" +
                $"VALUES  (" +
                $"'{customer.City}'," +
                $"'{customer.CountryId}'," +
                $"'{now:yyyy:MM:dd HH:mm:ss}'," +
                $"'{user.Name}'," +
                $"'{now:yyyy:MM:dd HH:mm:ss}'," +
                $"'{user.Name}');";
            Response response = new Response();
            try
            {
                var rowsAffected = DatabaseHelper.ExecuteNonQuery(query);
                if (rowsAffected > 0)
                {
                    var getIdQuery = $"SELECT cityId FROM city WHERE city = '{customer.City}' AND countryId = {customer.CountryId}";
                    try
                    {
                        response.data = DatabaseHelper.ExecuteQuery(getIdQuery);
                        if (response.data.Rows.Count > 0)
                        {
                            response.success = true;
                            response.message = "City saved successfully.";
                        } 
                        else
                        {
                            response.success = false;
                            response.message = "City was saved but its ID could not be retrieved. Please check the database.";
                        }
                    }
                    catch (Exception ex)
                    {
                        response.message = $"Error retrieving city ID: {ex.Message}";
                        response.success = false;
                        return response;
                    }
                }
                else
                {
                    response.success = false;
                    response.message = "City not saved.";
                }

            }
            catch (Exception ex)
            {
                response.message = ex.Message;
            }
            return response;
        }

        public Response SaveCountry(Customer customer, AppUser user, DateTime now)
        {
            string query = $"INSERT INTO country " +
                $"(" +
                $"country," +
                $"createDate," +
                $"createdBy," +
                $"lastUpdate," +
                $"lastUpdateBy" +
                $")" +
                $"VALUES  (" +
                $"'{customer.Country}'," +
                $"'{now:yyyy:MM:dd HH:mm:ss}'," +
                $"'{user.Name}'," +
                $"'{now:yyyy:MM:dd HH:mm:ss}'," +
                $"'{user.Name}');";
            Response response = new Response();
            try
            {
                int rowsAffected = DatabaseHelper.ExecuteNonQuery(query);
                if (rowsAffected > 0)
                {
                    response.success = true;
                    var getIdQuery = $"SELECT countryId FROM country WHERE country = '{customer.Country}';";
                    try
                    {
                        response.data = DatabaseHelper.ExecuteQuery(getIdQuery);
                        if (response.data.Rows.Count > 0)
                        {
                            response.success = true;
                            response.message = "Country saved successfully.";
                        }
                        else
                        {
                            response.success = false;
                            response.message = "Country was saved but its ID could not be retrieved. Please check the database.";
                        }
                    }
                    catch (Exception ex)
                    {
                        response.message = $"Error retrieving country ID: {ex.Message}";
                        response.success = false;
                        return response;
                    }
                }
                else
                {
                    response.success = false;
                }

            }
            catch (Exception ex)
            {
                response.message = ex.Message;
            }
            return response;
        }

        public Response GetAllCities()
        {
            Response response = new Response();
            string query = $"SELECT * FROM city";

            try
            {
                response.data = DatabaseHelper.ExecuteQuery(query);
                if (response.data.Rows.Count > 0)
                {
                    response.success = true;
                    response.message = "Success";
                }
                else
                {
                    response.success = false;
                    response.message = "No cities exist";
                }
            }
            catch (Exception ex)
            {
                response.success = false;
                response.message = $"Error: {ex}";
            }

            return response;
        }

        public Response GetAllCountries()
        {
            Response response = new Response();
            string query = $"SELECT * FROM country";

            try
            {
                response.data = DatabaseHelper.ExecuteQuery(query);
                if (response.data.Rows.Count > 0)
                {
                    response.success = true;
                    response.message = "Success";
                }
                else
                {
                    response.success = false;
                    response.message = "No countries exist";
                }
            }
            catch (Exception ex)
            {
                response.success = false;
                response.message = $"Error: {ex}";
            }

            return response;
        }

        public Response UpdateCustomer(Customer customer, AppUser user)
        {

            Response response = new Response();
            DateTime now = DateTime.Now;

            if (customer.CountryId == 0 || customer.CountryId == null)
            {
                customer.CountryId = Convert.ToInt32(SaveCountry(customer, user, now).data.Rows[0]["countryId"]);
            }
            else
            {
                var rowsAffected = UpdateCountry(customer, user, now);
                if (rowsAffected == 0)
                {
                    response.message = "Error updating country.";
                    response.success = false;
                    return response;
                }
            }
            if (customer.CityId == 0 || customer.CityId == null)
            {
                customer.CityId = Convert.ToInt32(SaveCity(customer, user, now).data.Rows[0]["cityId"]);
            }
            else
            {
                var rowsAffected = UpdateCity(customer, user, now);
                if (rowsAffected == 0)
                {
                    response.message = "Error updating city.";
                    response.success = false;
                    return response;
                }
            }
            if (customer.AddressId == 0 || customer.AddressId == null)
            {
                customer.AddressId = Convert.ToInt32(SaveAddress(customer, user, now).data.Rows[0]["addressId"]);
            }
            else
            {
                var rowsAffected = UpdateAddress(customer, user, now);
                if (rowsAffected == 0)
                {
                    response.message = "Error updating address.";
                    response.success = false;
                    return response;
                }
            }


            string query = $"UPDATE customer " +
                $"SET " +
                $"customerName = '{customer.Name}', " +
                $"addressId = '{customer.AddressId}', " +
                $"lastUpdate = '{now:yyyy:MM:dd HH:mm:ss}', " +
                $"lastUpdateBy = '{user.Name}' " +
                $"WHERE customerId = '{customer.Id}';";

            try
            {
                var rowsAffected = DatabaseHelper.ExecuteNonQuery(query);
                if (rowsAffected > 0)
                {
                    response.success = true;
                    response.message = "Customer Updated!";
                }
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
                response.success = false;
            }
            return response;
        }

        public int UpdateAddress(Customer customer, AppUser user, DateTime now)
        {
            string query = $"UPDATE address " +
                $"SET " +
                $"address = '{customer.Address}', " +
                $"address2 = '{customer.Address2}', " +
                $"postalCode = '{customer.PostalCode}', " +
                $"phone = '{customer.Phone}', " +
                $"cityId = '{customer.CityId}', " +
                $"lastUpdate = '{now:yyyy:MM:dd HH:mm:ss}', " +
                $"lastUpdateBy = '{user.Name}' " +
                $"WHERE addressId = '{customer.AddressId}';";

            try
            {
                var rowsAffected = DatabaseHelper.ExecuteNonQuery(query);
                return rowsAffected;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating address: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }
            return 0;
        }

        public int UpdateCity(Customer customer, AppUser user, DateTime now)
        {
            string query = $"Update city " +
                $"SET " +
                $"city = '{customer.City}', " +
                $"lastUpdate = '{now:yyyy:MM:dd HH:mm:sss}'," +
                $"lastUpdateBy = '{user.Name}' " +
                $"WHERE cityId = '{customer.CityId}';";

            try
            {
                var rowsAffected = DatabaseHelper.ExecuteNonQuery(query);
                return rowsAffected;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating city: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }
        }

        public int UpdateCountry(Customer customer, AppUser user, DateTime now)
        {
            string query = $"Update country " +
                $"SET " +
                $"country = '{customer.Country}', " +
                $"lastUpdate = '{now:yyyy:MM:dd HH:mm:ss}', " +
                $"lastUpdateBy = '{user.Name}' " +
                $"WHERE countryId = {customer.CountryId};";

            try
            {
                var rowsAffected = DatabaseHelper.ExecuteNonQuery(query);
                return rowsAffected;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating country: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }
        }

        public Response DeleteCustomer(int customerId, AppUser user)
        {
            Response response = new Response();

            if (customerId <= 0)
            {
                response.success = false;
                response.message = "Invalid customer Id or no customer selected.";
                return response;
            }

            // delete all appointments for this customer first
            Response appointmentsResponse = new Response();
            try
            {
                appointmentsResponse = _appointmentServices.DeleteAllCustomerAppointments(customerId);
                if (!appointmentsResponse.success)
                {
                    response.success = false;
                    response.message = appointmentsResponse.message;
                    return response;
                }
            }
            catch (Exception ex)
            {
                response.success = false;
                response.message = $"Error deleting customer appointments: {ex.Message}";
                return response;
            }

            try
            {

                string query = $"DELETE FROM customer WHERE customerId = '{customerId}';";
                var rowsAffected = DatabaseHelper.ExecuteNonQuery(query);
                if (rowsAffected > 0)
                {
                    response.success = true;
                    response.message = $"Customer deleted successfully. {appointmentsResponse.message}";
                }
                else
                {
                    response.success = false;
                    response.message = "No customer found with the provided ID.";
                }
            }
            catch (Exception ex)
            {
                response.success = false;
                response.message = $"Error deleting customer: {ex.Message}";
            }
            return response;
        }
    }
}
