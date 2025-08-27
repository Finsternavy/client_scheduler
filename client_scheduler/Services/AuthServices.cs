using client_scheduler.Localization;
using client_scheduler.Models;
using client_scheduler.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace client_scheduler.Services
{
    internal class AuthServices
    {
        public Response Authenticate(AuthAttempt attempt)
        {
            string query = $"SELECT userId, userName FROM user WHERE userName = '{ attempt.name }' AND password = '{ attempt.password }';";
            Response response = new Response();
            try
            {
                response.data = DatabaseHelper.ExecuteQuery(query);
                if (response.data.Rows.Count > 0)
                {
                    response.success = true;
                } else
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
