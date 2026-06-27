using MailKit.Net.Smtp;
using MimeKit;
using Npgsql;
using System;
using System.Drawing;
using System.Windows.Forms;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace AutoNova_Car_Showroom
{
    public partial class forget_password : Form
    {
        public bool found = false;
        public int otp;
        public string email;

        public string conn_string =
            "Host=localhost;Port=5432;Username=postgres;Password=password;Database=csms1;";

        public forget_password()
        {
            InitializeComponent();

            textEmail.Enter += textEmail_Enter;
            textEmail.Leave += textEmail_Leave;

            textPassword.Enter += textPassword_Enter;
            textPassword.Leave += textPassword_Leave;

            textOTP.Enter += textOTP_Enter;
            textOTP.Leave += textOTP_Leave;
        }

        // =========================
        // FORM LOAD
        // =========================
        private void forget_password_Load(object sender, EventArgs e)
        {
            SetPlaceholders();
        }

        private void SetPlaceholders()
        {
            textEmail.Text = "Enter Your Email";
            textEmail.ForeColor = Color.Gray;

            textPassword.Text = "Enter Your Password";
            textPassword.ForeColor = Color.Gray;

            textOTP.Text = "Enter OTP";
            textOTP.ForeColor = Color.Gray;
        }

        // =========================
        // SEND OTP
        // =========================
        private void button3_Click(object sender, EventArgs e)
        {
            email = textEmail.Text;
            found = false;

            if (string.IsNullOrWhiteSpace(email) || email == "Enter Your Email")
            {
                MessageBox.Show("Please enter email");
                return;
            }

            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(conn_string))
                {
                    conn.Open();

                    using (NpgsqlCommand cmd =
                        new NpgsqlCommand("SELECT email FROM users WHERE email=@mail", conn))
                    {
                        cmd.Parameters.AddWithValue("@mail", email);

                        object result = cmd.ExecuteScalar();

                        if (result != null)
                            found = true;
                    }
                }

                if (!found)
                {
                    MessageBox.Show("Email not found!");
                    return;
                }

                Random random = new Random();
                otp = random.Next(100000, 999999);

                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("AutoNova", "sheikhijaz137@gmail.com"));
                message.To.Add(new MailboxAddress("", email));
                message.Subject = "OTP Verification";

                message.Body = new TextPart("plain")
                {
                    Text = $"Hello, your OTP is: {otp}"
                };

                using (var client = new SmtpClient())
                {
                    client.Connect("smtp.gmail.com", 587,
                        MailKit.Security.SecureSocketOptions.StartTls);

                    client.Authenticate("sheikhijaz137@gmail.com", "huufvptcuhnhtlxr");
                    client.Send(message);
                    client.Disconnect(true);
                }

                MessageBox.Show("OTP Sent Successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        // =========================
        // RESET PASSWORD
        // =========================
        private void button2_Click(object sender, EventArgs e)
        {
            if (!found)
            {
                MessageBox.Show("First verify email");
                return;
            }

            if (!int.TryParse(textOTP.Text, out int otp_in))
            {
                MessageBox.Show("Enter valid OTP");
                return;
            }

            if (otp_in != otp)
            {
                MessageBox.Show("Wrong OTP");
                return;
            }

            string pass = textPassword.Text;

            if (string.IsNullOrWhiteSpace(pass) || pass == "Enter Your Password")
            {
                MessageBox.Show("Enter new password");
                return;
            }

            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(conn_string))
                {
                    conn.Open();

                    using (NpgsqlCommand cmd = new NpgsqlCommand(
                        "UPDATE users SET password=@new_pass WHERE email=@mail", conn))
                    {
                        cmd.Parameters.AddWithValue("@new_pass", pass);
                        cmd.Parameters.AddWithValue("@mail", email);

                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Password Updated Successfully");

                Userlogin login = new Userlogin();
                login.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        // =========================
        // BACK BUTTON
        // =========================
        private void button5_Click(object sender, EventArgs e)
        {
            Userlogin obj = new Userlogin();
            obj.Show();
            this.Hide();
        }

        // =========================
        // PLACEHOLDERS
        // =========================

        private void textEmail_Enter(object sender, EventArgs e)
        {
            if (textEmail.ForeColor == Color.Gray)
            {
                textEmail.Text = "";
                textEmail.ForeColor = Color.White;
            }
        }

        private void textEmail_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textEmail.Text))
            {
                textEmail.Text = "Enter Your Email";
                textEmail.ForeColor = Color.Gray;
            }
        }

        private void textPassword_Enter(object sender, EventArgs e)
        {
            if (textPassword.ForeColor == Color.Gray)
            {
                textPassword.Text = "";
                textPassword.ForeColor = Color.White;
            }
        }

        private void textPassword_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textPassword.Text))
            {
                textPassword.Text = "Enter Your Password";
                textPassword.ForeColor = Color.Gray;
            }
        }

        private void textOTP_Enter(object sender, EventArgs e)
        {
            if (textOTP.ForeColor == Color.Gray)
            {
                textOTP.Text = "";
                textOTP.ForeColor = Color.White;
            }
        }

        private void textOTP_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textOTP.Text))
            {
                textOTP.Text = "Enter OTP";
                textOTP.ForeColor = Color.Gray;
            }
        }
    }
}