using client_scheduler.Models;
using client_scheduler.Util;
using Google.Protobuf.WellKnownTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace client_scheduler.Services
{
    internal class AppointmentServices
    {
        public Response GetAppointments()
        {
            string query = "SELECT * FROM appointment";
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

        public Response SaveAppointment(Appointment appointment, Customer customer)
        {
            DateTime start = TimeZoneHelper.ConvertToEastern(Convert.ToDateTime(appointment.start));
            DateTime end = TimeZoneHelper.ConvertToEastern(Convert.ToDateTime(appointment.end));
            
            string query = "UPDATE appointment " +
                $"SET title = '{appointment.title}'," +
                $"description = '{appointment.description}'," +
                $"start = '{start:yyyy:MM:dd HH:mm:ss}'," +
                $"end = '{end:yyyy:MM:dd HH:mm:ss}'," +
                $"type = '{appointment.type}'," +
                $"url = '{appointment.url}',"  +
                $"location = '{customer.City}'," +
                $"contact = '{customer.Phone}' " +
                $"WHERE appointmentId = {appointment.appointmentId};";

            Response response = new Response();
            try
            {
                response.data = DatabaseHelper.ExecuteQuery(query);
                response.success = true;
                response.message = "Appointment Updated!";

            }
            catch (Exception ex)
            {
                response.message = ex.Message;
                response.success = false;
            }
            return response;
        }

        public Response AddNewAppointment(Appointment appointment, Customer customer)
        {
            DateTime start = TimeZoneHelper.ConvertToEastern(Convert.ToDateTime(appointment.start));
            DateTime end = TimeZoneHelper.ConvertToEastern(Convert.ToDateTime(appointment.end));
            DateTime now = TimeZoneHelper.ConvertToEastern(DateTime.Now);

            string query = "INSERT INTO appointment (" +
                "customerId," +
                "userId," +
                "title," +
                "description," +
                "location," +
                "contact," +
                "type," +
                "url," +
                "start," +
                "end," +
                "createDate," +
                "createdBy," +
                "lastUpdate," +
                "LastUpdateBy" +
                ")" +
                "VALUES" +
                "( "+
                $"{customer.Id}," +
                $"1," +
                $"'{appointment.title}'," +
                $"'{appointment.description}'," +
                $"'{customer.Address}'," +
                $"'{customer.Phone}'," +
                $"'{appointment.type}'," +
                $"'{appointment.url ?? appointment.url : 'www.google.com'}'," +
                $"'{start:yyyy:MM:dd HH:mm:ss}'," +
                $"'{end:yyyy:MM:dd HH:mm:ss}'," +
                $"'{DateTime.Now:yyyy:MM:dd HH:mm:ss}'," +
                $"'{customer.Name}'," +
                $"'{DateTime.Now:yyyy:MM:dd HH:mm:ss}'," +
                $"'{customer.Name}'" +
                ");";

            Response response = new Response();
            try
            {
                response.data = DatabaseHelper.ExecuteQuery(query);
                response.success = true;
                response.message = "Appointment Updated!";

            }
            catch (Exception ex)
            {
                response.message = ex.Message;
                response.success = false;
            }
            return response;
        }

        public Response DeleteAppointment(int id)
        {
            Response response = new Response();

            if (id <= 0)
            {
                response.success = false;
                response.message = "Invalid appointment Id.";
                return response;
            }

            try
            {
                var parameters = new Dictionary<string, object>
                {
                    {"@AppointmentId", id }
                };

                string query = $"DELETE FROM appointment WHERE appointmentId = @AppointmentId;";

                var deleted = DatabaseHelper.ExecuteNonQuery(query, parameters);
                response.success = deleted == 1 ? true : false;
                response.message = deleted == 1 ? "Deleted succesfully" : "Something went wrong. Appointment not cancelled.";
            }
            catch (Exception ex)
            {
                response.success = false;
                response.message = ex.Message;
            }

            return response;
        }
    }
}
