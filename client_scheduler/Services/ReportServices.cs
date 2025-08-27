using MySql.Data.MySqlClient;
using client_scheduler.Models;
using client_scheduler.Util;
using System.Data;

public class ReportGenerator
{

    public Dictionary<string, List<AppointmentTypeReport>> GetAppointmentTypesByMonth()
    {
        try
        {
            string query = @"
                SELECT
                    DATE_FORMAT(start, '%Y-%m') as Month,
                    type as Type,
                    COUNT(*) as Count
                FROM appointment
                WHERE type IS NOT NULL AND type != ''
                GROUP BY DATE_FORMAT(start, '%Y-%m'), type
                ORDER BY Month DESC, Count DESC";
            var dt = DatabaseHelper.ExecuteQuery(query);
            var results = new List<AppointmentTypeReport>();

            foreach(DataRow row in dt.Rows)
            {
                results.Add(new AppointmentTypeReport
                {
                    Month = row["Month"].ToString(),
                    Type = row["Type"].ToString(),
                    Count = Convert.ToInt32(row["Count"])
                });
            }

            // Use Dictionary collection with lambda expressions
            var groupedResults = results
                .GroupBy(r => r.Month)
                .ToDictionary(
                    group => group.Key,
                    group => group.OrderByDescending(x => x.Count).ToList()
                );

            return groupedResults;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error generating appointment types report: {ex.Message}");
            return new Dictionary<string, List<AppointmentTypeReport>>();
        }
    }


    public Dictionary<int, List<UserScheduleReport>> GetUserScheduleReport()
    {
        try
        {
            string query = @"
                SELECT
                    u.userId,
                    u.userName,
                    a.title as AppointmentTitle,
                    a.start as StartTime,
                    a.end as EndTime,
                    c.customerName,
                    a.type
                FROM user u
                LEFT JOIN appointment a ON u.userId = a.userId
                LEFT JOIN customer c ON a.customerId = c.customerId
                ORDER BY u.userName, a.start";
            var dt = DatabaseHelper.ExecuteQuery(query);
            var results = new List<UserScheduleReport>();

            foreach (DataRow row in dt.Rows)
            {
                DateTime dbStart = Convert.ToDateTime(row["StartTime"]);
                DateTime dbEnd = Convert.ToDateTime(row["EndTime"]);
                DateTime startFromEastern = TimeZoneHelper.ConvertFromEastern(dbStart);
                DateTime endFromEastern = TimeZoneHelper.ConvertFromEastern(dbEnd);
                results.Add(new UserScheduleReport
                {
                    UserId = Convert.ToInt32(row["userId"]),
                    UserName = row["userName"].ToString(),
                    AppointmentTitle = row["AppointmentTitle"]?.ToString() ?? "No Appointments",
                    StartTime = startFromEastern,
                    EndTime = endFromEastern,
                    CustomerName = row["customerName"]?.ToString() ?? "N/A",
                    Type = row["type"]?.ToString() ?? "N/A"
                });
            }

            // Use Dictionary collection with lambda expressions
            var userSchedules = results
                .GroupBy(r => r.UserId)
                .ToDictionary(
                    group => group.Key,
                    group => group.Where(x => x.StartTime != DateTime.MinValue)
                         .OrderBy(x => x.StartTime)
                         .ToList()
                );

            return userSchedules;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error generating user schedule report: {ex.Message}");
            return new Dictionary<int, List<UserScheduleReport>>();
        }
    }


    public Dictionary<string, LocationReport> GetAppointmentsByLocation()
    {
        try
        {
            string query = @"
                SELECT
                    location,
                    COUNT(*) as AppointmentCount,
                    GROUP_CONCAT(DISTINCT type) as Types
                FROM appointment
                WHERE location IS NOT NULL AND location != ''
                GROUP BY location
                ORDER BY AppointmentCount DESC";

            var dt = DatabaseHelper.ExecuteQuery(query);
            var results = new Dictionary<string, LocationReport>();

            foreach(DataRow row in dt.Rows)
            {
                var location = row["location"].ToString();
                var typeString = row["Types"].ToString();

                var types = new HashSet<string>(
                    typeString.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                              .Select(x => x.Trim())
                              .Where(x => !string.IsNullOrEmpty(x))
                );

                results[location] = new LocationReport
                {
                    Location = location,
                    AppointmentCount = Convert.ToInt32(row["AppointmentCount"]),
                    AppointmentTypes = types
                };
            }

            return results;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error generating location report: {ex.Message}");
            return new Dictionary<string, LocationReport>();
        }
    }


    public ReportDisplayData GenerateAppointmentTypesReport()
    {
        try
        {
            var report = GetAppointmentTypesByMonth();

            var displayData = new ReportDisplayData
            {
                Title = "Appointment Types by Month",
                Headers = new List<string> { "Month", "Type", "Count" },
                Rows = new List<List<string>>(),
                Summary = new Dictionary<string, object>()
            };

            // Use SortedDictionary for ordered output
            var sortedReport = new SortedDictionary<string, List<AppointmentTypeReport>>(report);

            foreach (var kvp in sortedReport)
            {
                kvp.Value.ForEach(type =>
                {
                    displayData.Rows.Add(new List<string>
                    {
                        kvp.Key,
                        type.Type,
                        type.Count.ToString()
                    });
                });
            }

            // Add summary data
            var totalAppointments = report.Values
                .SelectMany(list => list)
                .Sum(r => r.Count);

            var mostPopularType = report.Values
                .SelectMany(list => list)
                .GroupBy(r => r.Type)
                .Select(g => new { Type = g.Key, Total = g.Sum(r => r.Count) })
                .OrderByDescending(x => x.Total)
                .FirstOrDefault();

            displayData.Summary["Total Appointments"] = totalAppointments;
            displayData.Summary["Most Popular Type"] = mostPopularType?.Type ?? "N/A";
            displayData.Summary["Total Types"] = report.Values.SelectMany(list => list).Select(r => r.Type).Distinct().Count();

            return displayData;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error generating appointment types report: {ex.Message}");
            return new ReportDisplayData
            {
                Title = "Appointment Types by Month",
                Headers = new List<string>(),
                Rows = new List<List<string>>(),
                Summary = new Dictionary<string, object>()
            };
        }
    }


    public ReportDisplayData GenerateUserScheduleReport()
    {
        try
        {
            var report = GetUserScheduleReport();

            var displayData = new ReportDisplayData
            {
                Title = "User Schedule Report",
                Headers = new List<string> { "User ID", "User Name", "Appointment Title", "Start Time", "End Time", "Customer", "Type" },
                Rows = new List<List<string>>(),
                Summary = new Dictionary<string, object>()
            };

            // Use LinkedList for user processing
            var userList = new LinkedList<int>(report.Keys);

            foreach (var userId in userList)
            {
                var userAppointments = report[userId];
                if (userAppointments.Any())
                {
                    var userName = userAppointments.First().UserName;

                    userAppointments.ForEach(appt =>
                    {
                        displayData.Rows.Add(new List<string>
                        {
                            userId.ToString(),
                            userName,
                            appt.AppointmentTitle,
                            appt.StartTime.ToString("MM/dd/yyyy HH:mm"),
                            appt.EndTime.ToString("MM/dd/yyyy HH:mm"),
                            appt.CustomerName,
                            appt.Type
                        });
                    });
                }
            }

            // Add summary data
            displayData.Summary["Total Users"] = userList.Count;
            displayData.Summary["Total Appointments"] = displayData.Rows.Count;
            displayData.Summary["Users with Appointments"] = report.Values.Count(list => list.Any());

            return displayData;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error generating user schedule report: {ex.Message}");
            return new ReportDisplayData
            {
                Title = "User Schedule Report",
                Headers = new List<string>(),
                Rows = new List<List<string>>(),
                Summary = new Dictionary<string, object>()
            };
        }
    }

    public ReportDisplayData GenerateLocationReport()
    {
        try
        {
            var report = GetAppointmentsByLocation();

            var displayData = new ReportDisplayData
            {
                Title = "Appointments by Location",
                Headers = new List<string> { "Location", "Appointment Count", "Types" },
                Rows = new List<List<string>>(),
                Summary = new Dictionary<string, object>()
            };

            foreach (var kvp in report)
            {
                displayData.Rows.Add(new List<string>
                {
                    kvp.Key,
                    kvp.Value.AppointmentCount.ToString(),
                    string.Join(", ", kvp.Value.AppointmentTypes)
                });
            }

            // Add summary data
            displayData.Summary["Total Locations"] = report.Count;
            displayData.Summary["Total Appointments"] = report.Values.Sum(r => r.AppointmentCount);
            displayData.Summary["Most Popular Location"] = report
                .OrderByDescending(kvp => kvp.Value.AppointmentCount)
                .FirstOrDefault().Key ?? "N/A";

            return displayData;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error generating location report: {ex.Message}");
            return new ReportDisplayData
            {
                Title = "Appointments by Location",
                Headers = new List<string>(),
                Rows = new List<List<string>>(),
                Summary = new Dictionary<string, object>()
            };
        }
    }

    public AllReports GenerateAllReports()
    {
        try
        {
            var allReports = new AllReports();
            try
            {
                allReports.AppointmentTypes = GetAppointmentTypesByMonth();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error generating all reports: {ex.Message}");
                allReports.AppointmentTypes = new Dictionary<string, List<AppointmentTypeReport>>();
            }

            try
            {
                allReports.UserSchedules = GetUserScheduleReport();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error generating all reports: {ex.Message}");
                allReports.UserSchedules = new Dictionary<int, List<UserScheduleReport>>();
            }

            try
            {
                allReports.Locations = GetAppointmentsByLocation();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error generating all reports: {ex.Message}");
                allReports.Locations = new Dictionary<string, LocationReport>();
            }

            try 
            {
                allReports.AppointmentTypesDisplay = GenerateAppointmentTypesReport();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error generating all reports: {ex.Message}");
                allReports.AppointmentTypesDisplay = new ReportDisplayData
                {
                    Title = "Appointment Types by Month",
                    Headers = new List<string>(),
                    Rows = new List<List<string>>(),
                    Summary = new Dictionary<string, object>()
                };
            }

            try 
            {
                allReports.UserSchedulesDisplay = GenerateUserScheduleReport();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error generating all reports: {ex.Message}");
                allReports.UserSchedulesDisplay = new ReportDisplayData
                {
                    Title = "User Schedule Report",
                    Headers = new List<string>(),
                    Rows = new List<List<string>>(),
                    Summary = new Dictionary<string, object>()
                };
            }

            try 
            {
                allReports.LocationsDisplay = GenerateLocationReport();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error generating all reports: {ex.Message}");
                allReports.LocationsDisplay = new ReportDisplayData
                {
                    Title = "Appointments by Location",
                    Headers = new List<string>(),
                    Rows = new List<List<string>>(),
                    Summary = new Dictionary<string, object>()
                };
            }

            return allReports;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error generating all reports: {ex.Message}");
            return new AllReports();
        }
    }
}