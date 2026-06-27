using Npgsql;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace AutoNova_Car_Showroom
{
    public partial class carbooking : Form
    {
        public string con_string = "Host=localhost;Port=5432;Username=postgres;Password=password;Database=csms1;";
        public List<string> reg_lis = new List<string>();
        public int id;

        public carbooking()
        {
            InitializeComponent();
        }

        private void carbooking_Load(object sender, EventArgs e)
        {
            loadcars();
        }

        public void loadcars()
        {
            reg_lis.Clear();
            comboBox1.Items.Clear();

            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(con_string))
                {
                    conn.Open();

                    NpgsqlCommand cmd = new NpgsqlCommand(
                        "Select registration_id from cars where is_Active = True AND availability = 'yes'", conn);

                    NpgsqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        reg_lis.Add(reader.GetString(0));
                    }
                }

                foreach (string ids in reg_lis)
                {
                    comboBox1.Items.Add(ids);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_email.Text) ||
                string.IsNullOrEmpty(comboBox1.Text) ||
                string.IsNullOrEmpty(cb_method.Text))
            {
                MessageBox.Show("Please fill all fields (Email, Car, Payment Method)");
                return;
            }

            string buyer_mail = txt_email.Text;
            string car_id = comboBox1.Text;
            string payment_method = cb_method.Text;
            DateTime date_time = DateTime.Now;
            string status = "booked";
            decimal price = 0;

            try
            {
                using (NpgsqlConnection con = new NpgsqlConnection(con_string))
                {
                    con.Open();

                    // =========================
                    // GET USER ID
                    // =========================
                    using (NpgsqlCommand cmd0 = new NpgsqlCommand(
                        "SELECT user_id FROM users WHERE email = @mail", con))
                    {
                        cmd0.Parameters.AddWithValue("@mail", buyer_mail);

                        object result = cmd0.ExecuteScalar();

                        if (result == null)
                        {
                            MessageBox.Show("User not found!");
                            return;
                        }

                        id = Convert.ToInt32(result);
                    }

                    // =========================
                    // GET CAR PRICE
                    // =========================
                    using (NpgsqlCommand cmd1 = new NpgsqlCommand(
                        "SELECT price FROM cars WHERE registration_id = @reg_id", con))
                    {
                        cmd1.Parameters.AddWithValue("@reg_id", car_id);

                        object result1 = cmd1.ExecuteScalar();

                        if (result1 != null)
                            price = Convert.ToDecimal(result1);
                    }

                    // =========================
                    // INSERT BOOKING
                    // =========================
                    string queryBooking = "INSERT INTO bookings (registration_id, buyer_id, booking_date, payment_method, status) " +
                                          "VALUES (@reg, @buy_id, @date, @pay, @status)";

                    using (NpgsqlCommand cmdBooking = new NpgsqlCommand(queryBooking, con))
                    {
                        cmdBooking.Parameters.AddWithValue("@reg", car_id);
                        cmdBooking.Parameters.AddWithValue("@buy_id", id);
                        cmdBooking.Parameters.AddWithValue("@date", date_time);
                        cmdBooking.Parameters.AddWithValue("@pay", payment_method);
                        cmdBooking.Parameters.AddWithValue("@status", status);

                        cmdBooking.ExecuteNonQuery();
                    }

                    // =========================
                    // INSERT SALES
                    // =========================
                    string querySales = "INSERT INTO sales (registration_id, buyer_id, price, sale_date, payment_method) " +
                                        "VALUES (@reg, @buy_id, @prc, @date, @pay)";

                    using (NpgsqlCommand cmdSales = new NpgsqlCommand(querySales, con))
                    {
                        cmdSales.Parameters.AddWithValue("@reg", car_id);
                        cmdSales.Parameters.AddWithValue("@buy_id", id);
                        cmdSales.Parameters.AddWithValue("@prc", price);
                        cmdSales.Parameters.AddWithValue("@date", date_time);
                        cmdSales.Parameters.AddWithValue("@pay", payment_method);

                        cmdSales.ExecuteNonQuery();
                    }

                    // =========================
                    // UPDATE CAR STATUS LAST
                    // =========================
                    string queryUpdateCar = "UPDATE cars SET availability = 'no', status = 'sold' WHERE registration_id = @reg";

                    using (NpgsqlCommand cmdUpdate = new NpgsqlCommand(queryUpdateCar, con))
                    {
                        cmdUpdate.Parameters.AddWithValue("@reg", car_id);
                        cmdUpdate.ExecuteNonQuery();
                    }

                    MessageBox.Show("Booking & Sale Completed Successfully!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
    }
}