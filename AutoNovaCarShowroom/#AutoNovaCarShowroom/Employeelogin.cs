using Npgsql;
using System;
using System.Drawing;
using System.Windows.Forms;


namespace AutoNova_Car_Showroom
{
    public partial class Employeelogin : Form
    {
        public string conn_string = "Host=localhost;Port=5432;Username=postgres;Password=password;Database=csms1;";
        public Employeelogin()
        {
            InitializeComponent();

            // CONNECT EVENTS
           
            this.Load += Employeelogin_Load;

            textmail.Enter += textname_Enter;
            textmail.Leave += textname_Leave;

            textpassword.Enter += textpassword_Enter;
            textpassword.Leave += textpassword_Leave;

            textrole.Enter += textrole_Enter;
            textrole.Leave += textrole_Leave;
        }

        private void Employeelogin_Load(object sender, EventArgs e)
        {
            // NAME PLACEHOLDER
            textmail.Text = "Enter Your Name";
            textmail.ForeColor = Color.FromArgb(154, 160, 166);

            // PASSWORD PLACEHOLDER
            textpassword.Text = "Enter Your Password";
            textpassword.ForeColor = Color.FromArgb(154, 160, 166);
            textpassword.UseSystemPasswordChar = false;

            textrole.Text = "Enter Your Role";
            textrole.ForeColor = Color.FromArgb(154, 160, 166);

            
        }

        // NAME ENTER
        private void textname_Enter(object sender, EventArgs e)
        {
            if (textmail.Text == "Enter Your Name")
            {
                textmail.Text = "";
                textmail.ForeColor = Color.White;
            }
        }

        // NAME LEAVE
        private void textname_Leave(object sender, EventArgs e)
        {
            if (textmail.Text == "")
            {
                textmail.Text = "Enter Your Name";
                textmail.ForeColor = Color.FromArgb(154, 160, 166);
            }
        }

        // PASSWORD ENTER
        private void textpassword_Enter(object sender, EventArgs e)
        {
            if (textpassword.Text == "Enter Your Password")
            {
                textpassword.Text = "";
                textpassword.ForeColor = Color.White;
                textpassword.UseSystemPasswordChar = true;
            }
        }

        // PASSWORD LEAVE
        private void textpassword_Leave(object sender, EventArgs e)
        {
            if (textpassword.Text == "")
            {
                textpassword.UseSystemPasswordChar = false;
                textpassword.Text = "Enter Your Password";
                textpassword.ForeColor = Color.FromArgb(154, 160, 166);
            }
        }

        // ROLE ENTER
        private void textrole_Enter(object sender, EventArgs e)
        {
            if (textrole.Text == "Enter Your Role")
            {
                textrole.Text = "";
                textrole.ForeColor = Color.White;
            }
        }

        // ROLE LEAVE
        private void textrole_Leave(object sender, EventArgs e)
        {
            if (textrole.Text == "")
            {
                textrole.Text = "Enter Your Role";
                textrole.ForeColor = Color.FromArgb(154, 160, 166);
            }
        }

        // BACK BUTTON
        private void button3_Click(object sender, EventArgs e)
        {
            LoginChoice obj = new LoginChoice();
            obj.Show();
            this.Hide();
        }

        // EMPTY EVENTS (Designer errors avoid karne ke liye)

        private void textname_TextChanged(object sender, EventArgs e) { }

        private void textpassword_TextChanged(object sender, EventArgs e) { }

        private void textrole_TextChanged(object sender, EventArgs e) { }

        private void button2_Click(object sender, EventArgs e) {
            // 1. Capture and Clean Input
            string inputEmail = textmail.Text.Trim().ToLower();
            string inputPass = textpassword.Text; // Assuming plain text for now
            string inputRole = textrole.Text;

            if (string.IsNullOrEmpty(inputEmail) || string.IsNullOrEmpty(inputPass))
            {
                MessageBox.Show("Please enter both Email and Password.");
                return;
            }

            try
            {
                using (NpgsqlConnection con = new NpgsqlConnection(conn_string))
                {
                    con.Open();

                    // 2. The SQL Command: Check all 4 fields + is_active status
                    string sql = @"SELECT COUNT(*) FROM employees 
                           WHERE email = @email 
                           AND password = @pass 
                           AND role = @role 
                           AND is_active = true";

                    using (NpgsqlCommand cmd = new NpgsqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@email", inputEmail);
                        //cmd.Parameters.AddWithValue("@name", inputName);
                        cmd.Parameters.AddWithValue("@pass", inputPass);
                        cmd.Parameters.AddWithValue("@role", inputRole);

                        // 3. ExecuteScalar returns the count (0 or 1)
                        int userCount = Convert.ToInt32(cmd.ExecuteScalar());

                        if (userCount == 1)
                        {
                            MessageBox.Show("Login Successfull");
                            EmployeeDashboard obj = new EmployeeDashboard();
                            obj.Show();
                            this.Hide();

                            // Proceed to main dashboard
                            // FormMain main = new FormMain();
                            // main.Show();
                            // this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Login Failed. Please check your credentials or contact the Admin.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Login Error: " + ex.Message);
            }
            
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) { }

        private void textrole_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void textname_Click(object sender, EventArgs e)
        {

        }

        private void Employeelogin_Load_1(object sender, EventArgs e)
        {

        }
    }
}