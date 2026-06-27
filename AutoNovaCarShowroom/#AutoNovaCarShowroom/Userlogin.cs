using System;
using System.Drawing;
using System.Windows.Forms;
using Npgsql;


namespace AutoNova_Car_Showroom
{
    public partial class Userlogin : Form
    {
        public string conn_string = "Host=localhost;Port=5432;Username=postgres;Password=password;Database=csms1;";
        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string email = textname.Text;
            string password = textpassword.Text;
            bool found = false;
            NpgsqlConnection conn = new NpgsqlConnection(conn_string);
            conn.Open();
            NpgsqlCommand cmd = new NpgsqlCommand("Select email,password from users",conn);
            NpgsqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                string ret_mail = reader.GetString(0);
                string ret_pass = reader.GetString(1);
                if (ret_mail == email && ret_pass == password)
                { found = true; }
            }
            reader.Close();
            conn.Close();
            if(found)
            {
                MessageBox.Show("Logged In....");
                UserDashboard dashboard = new UserDashboard();
                dashboard.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Wrong Email & Password");
            }
            
        }

        private void Name_Click(object sender, EventArgs e)
        {

        }

        private void texname_TextChanged(object sender, EventArgs e)
        {

        }

        public Userlogin()
        {
            InitializeComponent();

            // CONNECT EVENTS
            textname.Enter += textname_Enter;
            textname.Leave += textname_Leave;

            textpassword.Enter += textpassword_Enter;
            textpassword.Leave += textpassword_Leave;
        }

        private void Userlogin_Load(object sender, EventArgs e)
        {
            // NAME PLACEHOLDER
            textname.Text = "Enter Your Name";
            textname.ForeColor = Color.FromArgb(154, 160, 166);

            // PASSWORD PLACEHOLDER
            textpassword.Text = "Enter Your Password";
            textpassword.ForeColor = Color.FromArgb(154, 160, 166);
            textpassword.UseSystemPasswordChar = false;
        }

        // =============================
        // NAME TEXTBOX ENTER
        // =============================

        private void textname_Enter(object sender, EventArgs e)
        {
            if (textname.Text == "Enter Your Name")
            {
                textname.Text = "";
                textname.ForeColor = Color.White;
            }
        }

        // =============================
        // NAME TEXTBOX LEAVE
        // =============================

        private void textname_Leave(object sender, EventArgs e)
        {
            if (textname.Text == "")
            {
                textname.Text = "Enter Your Name";
                textname.ForeColor = Color.FromArgb(154, 160, 166);
            }
        }

        // =============================
        // PASSWORD ENTER
        // =============================

        private void textpassword_Enter(object sender, EventArgs e)
        {
            if (textpassword.Text == "Enter Your Password")
            {
                textpassword.Text = "";
                textpassword.ForeColor = Color.White;
                textpassword.UseSystemPasswordChar = true;
            }
        }

        // =============================
        // PASSWORD LEAVE
        // =============================

        private void textpassword_Leave(object sender, EventArgs e)
        {
            if (textpassword.Text == "")
            {
                textpassword.UseSystemPasswordChar = false;
                textpassword.Text = "Enter Your Password";
                textpassword.ForeColor = Color.FromArgb(154, 160, 166);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            LoginChoice obj = new LoginChoice();
            obj.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UserSignUp obj = new UserSignUp();
            obj.Show();
            this.Hide();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            forget_password pass_obj = new forget_password();
            pass_obj.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(conn_string);
                con.Open();

                MessageBox.Show("Connection OK ✔");

                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error ❌ " + ex.Message);
            }
        }

        private void textname_Enter_1(object sender, EventArgs e)
        {

        }
    }
}