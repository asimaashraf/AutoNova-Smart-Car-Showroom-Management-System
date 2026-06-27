using System;
using System.IO;
using System.Drawing;
using System.Net;
using System.Net.Mail;
using System.Windows.Forms;
using Npgsql;
using MailKit.Net.Smtp;
using MimeKit;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;
namespace AutoNova_Car_Showroom
{
    public partial class UserSignUp : Form
    {
        public int otp;
        public string name;
        public string email;
        public string password;
        public string confirm_pass;
        public string conn_string = "Host=localhost;Port=5432;Username=postgres;Password=password;Database=csms1;";
        private void textPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void textConfirmpassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }


        public UserSignUp()
        {
            InitializeComponent();

            // CONNECT EVENTS
            textName.Enter += textName_Enter;
            textName.Leave += textName_Leave;

            textEmail.Enter += textEmail_Enter;
            textEmail.Leave += textEmail_Leave;

            textPassword.Enter += textPassword_Enter;
            textPassword.Leave += textPassword_Leave;

            textConfirmpassword.Enter += textConfirmpassword_Enter;
            textConfirmpassword.Leave += textConfirmpassword_Leave;

          
        }

        private void UserSignUp_Load(object sender, EventArgs e)
        {
            // NAME
            textName.Text = "Enter Your Name";
            textName.ForeColor = Color.FromArgb(154, 160, 166);

            // EMAIL
            textEmail.Text = "Enter Your Email";
            textEmail.ForeColor = Color.FromArgb(154, 160, 166);

            // PASSWORD
            textPassword.Text = "Enter Your Password";
            textPassword.ForeColor = Color.FromArgb(154, 160, 166);
            textPassword.UseSystemPasswordChar = false;

            // CONFIRM PASSWORD
            textConfirmpassword.Text = "Confirm Password";
            textConfirmpassword.ForeColor = Color.FromArgb(154, 160, 166);
            textConfirmpassword.UseSystemPasswordChar = false;

          
        }

        // ======================
        // NAME
        // ======================

        private void textName_Enter(object sender, EventArgs e)
        {
            if (textName.Text == "Enter Your Name")
            {
                textName.Text = "";
                textName.ForeColor = Color.White;
            }
        }

        private void textName_Leave(object sender, EventArgs e)
        {
            if (textName.Text == "")
            {
                textName.Text = "Enter Your Name";
                textName.ForeColor = Color.FromArgb(154, 160, 166);
            }
        }

        // ======================
        // EMAIL
        // ======================

        private void textEmail_Enter(object sender, EventArgs e)
        {
            if (textEmail.Text == "Enter Your Email")
            {
                textEmail.Text = "";
                textEmail.ForeColor = Color.White;
            }
        }

        private void textEmail_Leave(object sender, EventArgs e)
        {
            if (textEmail.Text == "")
            {
                textEmail.Text = "Enter Your Email";
                textEmail.ForeColor = Color.FromArgb(154, 160, 166);
            }
        }

        // ======================
        // PASSWORD
        // ======================

        private void textPassword_Enter(object sender, EventArgs e)
        {
            if (textPassword.Text == "Enter Your Password")
            {
                textPassword.Text = "";
                textPassword.ForeColor = Color.White;
                textPassword.UseSystemPasswordChar = true;
            }
        }

        private void textPassword_Leave(object sender, EventArgs e)
        {
            if (textPassword.Text == "")
            {
                textPassword.UseSystemPasswordChar = false;
                textPassword.Text = "Enter Your Password";
                textPassword.ForeColor = Color.FromArgb(154, 160, 166);
            }
        }

        // ======================
        // CONFIRM PASSWORD
        // ======================

        private void textConfirmpassword_Enter(object sender, EventArgs e)
        {
            if (textConfirmpassword.Text == "Confirm Password")
            {
                textConfirmpassword.Text = "";
                textConfirmpassword.ForeColor = Color.White;
                textConfirmpassword.UseSystemPasswordChar = true;
            }
        }

        private void textConfirmpassword_Leave(object sender, EventArgs e)
        {
            if (textConfirmpassword.Text == "")
            {
                textConfirmpassword.UseSystemPasswordChar = false;
                textConfirmpassword.Text = "Confirm Password";
                textConfirmpassword.ForeColor = Color.FromArgb(154, 160, 166);
            }
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Userlogin obj = new Userlogin();
            obj.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
             Random random = new Random();
             otp = random.Next(100000, 999999);
             name = textName.Text;
             email = textEmail.Text;
             password = textPassword.Text;
             confirm_pass = textConfirmpassword.Text;
            MessageBox.Show(name, email);
            MessageBox.Show(password);
            try
            {
                if (name == "" || email == "" || password == "" || confirm_pass == "")
                {
                    MessageBox.Show("Fields cannot be empty cannot generate an otp");
                    return;
                }
                else
                {
                    // Email setup
                    var message = new MimeMessage();
                    message.From.Add(new MailboxAddress("AutoNova", "sheikhijaz137@gmail.com"));
                    message.To.Add(new MailboxAddress("", email));
                    message.Subject = "OTP Verification";

                    message.Body = new TextPart("plain")
                    {
                        Text = $"Hello {name}, your OTP is: {otp}"
                    };

                    using (var client = new SmtpClient())
                    {
                        client.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);

                        client.Authenticate("sheikhijaz137@gmail.com", "huufvptcuhnhtlxr"); // no spaces

                        client.Send(message);
                        client.Disconnect(true);
                    }

                    MessageBox.Show("OTP Sent Successfully!");
                }
        }
            catch(Exception ex) 
            {
                MessageBox.Show("Something Went Wrong " + ex.ToString());
            }
}

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                int in_otp = int.Parse(textOTP.Text);
                if (in_otp == otp)
                {
                    NpgsqlConnection con = new NpgsqlConnection(conn_string);
                    con.Open();
                    NpgsqlCommand cmd = new NpgsqlCommand("Insert into users (name,email,password,otp) values(@uname,@uemail,@upass,@uotp)", con);
                    cmd.Parameters.AddWithValue("@uname",name);
                    cmd.Parameters.AddWithValue("@uemail", email);
                    cmd.Parameters.AddWithValue("@upass", confirm_pass);
                    cmd.Parameters.AddWithValue("@uotp", otp);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Signed up Successfully");
                }
            }
            catch(Exception ex)
            {
               
                MessageBox.Show($"Something went wrong {ex}");
                
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}