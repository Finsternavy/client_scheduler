using client_scheduler.Models;
using client_scheduler.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace client_scheduler.Services
{
    internal class CustomerServices
    {
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

        public Response SaveCustomer(Customer customer, User user)
        {
            string query = $"UPDATE customer " +
                $"SET customerName = {customer.Name}," +
                $"active = {customer.Active}," +
                $"lastUpdate = {DateTime.Now}," +
                $"lastUpdateBy = {user.Name}";
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
    }
}
