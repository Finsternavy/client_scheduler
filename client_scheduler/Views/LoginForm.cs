using System.Configuration;
using System.Drawing.Drawing2D;
using MySql.Data.MySqlClient;
using System.Net.NetworkInformation;
using System.Data;
using client_scheduler.Services;
using client_scheduler.Models;
using client_scheduler.Localization;
using client_scheduler.Views;
using Microsoft.VisualBasic.ApplicationServices;
using client_scheduler.Models;

namespace client_scheduler
{
    public partial class LoginForm : Form
    {
        private AuthServices _authService = new AuthServices();

        string activeMessage = null;
        public AppUser activeUser = new AppUser();
        public LoginForm()
        {
            InitializeComponent();
            this.Load += LoginForm_Load;
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            loginResponseMessage.Text = "";
            Apply_Styling(this);
            LocalizationHelper.InitializeFromSystemCulture();
            ApplyLocalization();
        }

        private static void Apply_Styling(Control parent)
        {
            Color btnForeColor = Color.White;
            Color btnBackColor = Color.Transparent;
            Color btnHoverForeColor = Color.Black;
            Color btnHoverBackColor = Color.White;
            foreach (Control control in parent.Controls)
            {
                if (control is Label label)
                {
                    label.ForeColor = Color.White;
                }

                if (control is Button button)
                {
                    button.FlatStyle = FlatStyle.Flat;
                    button.ForeColor = btnForeColor;
                    button.BackColor = btnBackColor;

                    button.MouseEnter += (s, args) =>
                    {
                        button.FlatAppearance.BorderSize = 0;
                        button.BackColor = btnHoverBackColor;
                        button.ForeColor = btnHoverForeColor;
                    };
                    button.MouseLeave += (s, args) =>
                    {
                        button.ForeColor = btnForeColor;
                        button.BackColor = btnBackColor;
                        button.FlatAppearance.BorderSize = 1;
                    };
                }
            }
        }

        private void ApplyLocalization()
        {
            this.Text = LocalizationHelper.GetString("LoginFormTitle");
            emailLabel.Text = LocalizationHelper.GetString("Username_Label");
            passwordLabel.Text = LocalizationHelper.GetString("Password_Label");
            loginBtn.Text = LocalizationHelper.GetString("Login_Button_Text");
            testSpanishBtn.Text = LocalizationHelper.GetString("Test_Spanish_Text");
            testEnglishBtn.Text = LocalizationHelper.GetString("Test_English_Text");

            if (activeMessage != null)
            {
                switch(activeMessage)
                {
                    case "success":
                        loginResponseMessage.Text = LocalizationHelper.GetString("Welcome_Text", activeUser.Name) + " " + LocalizationHelper.GetString("Login_Success");
                        break;
                    case "failed":
                        loginResponseMessage.Text = LocalizationHelper.GetString("Login_Failed");
                        break;
                    case "error":
                        loginResponseMessage.Text = LocalizationHelper.GetString("Login_Error");
                        break;
                    case "missing both":
                        loginResponseMessage.Text = LocalizationHelper.GetString("Missing_Both");
                        break;
                    case "missing username":
                        loginResponseMessage.Text = LocalizationHelper.GetString("Missing_Username");
                        break;
                    case "missing password":
                        loginResponseMessage.Text = LocalizationHelper.GetString("Missing_Password");
                        break;
                }
            }
        }

        private void SetCultureSpanish (object sender, EventArgs e)
        {
            LocalizationHelper.SetCulture("es-ES");
            ApplyLocalization();
        }

        private void SetCultureEnglish(object sender, EventArgs e)
        {
            LocalizationHelper.SetCulture("en-EN");
            ApplyLocalization();
        }

        private void loginBtn_Click(object sender, EventArgs e)
        {
            //getData();
            AuthAttempt attempt = new AuthAttempt(emailTextBox.Text.Trim(), passwordTextBox.Text.Trim());
            Response response = new Response();
            if (attempt.name == "" && attempt.password == "")
            {
                loginResponseMessage.Text = LocalizationHelper.GetString("Missing_Both");
                activeMessage = "missing both";
            }
            if (attempt.name != ""  && attempt.password != "")
            {
                try
                {
                    response = _authService.Authenticate(attempt);
                    if (response.success)
                    {
                        activeUser.Name = response.data.Rows[0]["userName"].ToString();
                        activeUser.Id = Convert.ToInt32(response.data.Rows[0]["userId"].ToString());
                        response.message = LocalizationHelper.GetString("Welcome_Text", response.data.Rows[0]["userName"]) + " " + LocalizationHelper.GetString("Login_Success");
                        activeMessage = "success";
                        // add a log when the user logged in
                        try
                        {
                            // Login_History should save in bin/Debug/net8.0-windows/Login_History.txt
                            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                            string logEntry = $"{timestamp} - User {activeUser.Name} logged in successfully.{Environment.NewLine}";
                            File.AppendAllText("Login_History.txt", logEntry);
                        } 
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error logging user login: " + ex.Message);
                        }
                    Dashboard dashboard = new Dashboard(activeUser);
                        this.Hide();
                        dashboard.Show();

                    }
                    else
                    {
                        response.message = LocalizationHelper.GetString("Login_Failed");
                        activeMessage = "failed";
                    }
                }
                catch (Exception ex)
                {
                    response.success = false;
                    response.message = LocalizationHelper.GetString("Login_Error", ex.Message);
                    activeMessage = "error";
                }

                loginResponseMessage.Text = response.message;
            }
            if (attempt.name != "" && attempt.password == "")
            {
                loginResponseMessage.Text = LocalizationHelper.GetString("Missing_Password");
                activeMessage = "missing password";
            }
            if (attempt.name == "" && attempt.password != "")
            {
                loginResponseMessage.Text = LocalizationHelper.GetString("Missing_Username"); ;
                activeMessage = "missing username";
            }
        }

        /*public void getData()
        {
            string conString = DatabaseHelper.GetConnectionString();
            MySqlConnection con = new MySqlConnection(conString);
            con.Open();
            string query = "Select * from appointment;";
            MySqlCommand cmd = new MySqlCommand(query, con);
            MySqlDataReader reader = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(reader);
            dataGridView1.DataSource = dt;
        }*/


    }

}
