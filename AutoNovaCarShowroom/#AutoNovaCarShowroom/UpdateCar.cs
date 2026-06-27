using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;

namespace AutoNova_Car_Showroom
{
    public partial class UpdateCar : Form
    {
        public string conn_string = "Host=localhost;Port=5432;Username=postgres;Password=password;Database=csms1;";
        public List<string> list = new List<string>();
        string reg = "";
        string color = "";
        string model = "";
        int year = 0;
        string brand = "";
        string engine = "";
        decimal price = 0m;
        string isAvailable = "";
        string reg_id = "";
        public UpdateCar()
        {
            InitializeComponent();


        }

        private void UpdateCar_Load(object sender, EventArgs e)
        {
            load_reg();
            foreach (var item in list)
            {
                comboBox_reg.Items.Add(item);
            }

            //    // ADD COLUMNS
            //    if (dataGridView1.Columns.Count == 0)
            //    {
            //        dataGridView1.Columns.Add("carid", "Car ID");
            //        dataGridView1.Columns.Add("model", "Model");
            //        dataGridView1.Columns.Add("brand", "Brand / Name");
            //        dataGridView1.Columns.Add("price", "Price");
            //        dataGridView1.Columns.Add("color", "Color");
            //        dataGridView1.Columns.Add("year", "Year");
            //        dataGridView1.Columns.Add("engine", "Engine Type");
            //        dataGridView1.Columns.Add("availability", "Availability");
            //    }

            //    // GRID STYLE
            //    dataGridView1.BackgroundColor = Color.FromArgb(154, 160, 166);
            //    dataGridView1.BorderStyle = BorderStyle.None;
            //    dataGridView1.GridColor = Color.White;

            //    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            //    dataGridView1.RowHeadersVisible = false;

            //    dataGridView1.EnableHeadersVisualStyles = false;
            //    dataGridView1.ColumnHeadersHeight = 40;

            //    // HEADER STYLE
            //    dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(154, 160, 166);
            //    dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            //    dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            //    dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            //    // ROW STYLE
            //    dataGridView1.DefaultCellStyle.BackColor = Color.FromArgb(154, 160, 166);
            //    dataGridView1.DefaultCellStyle.ForeColor = Color.Black;
            //    dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            //    dataGridView1.RowTemplate.Height = 35;

            //    // SELECTION STYLE
            //    dataGridView1.DefaultCellStyle.SelectionBackColor = Color.Black;
            //    dataGridView1.DefaultCellStyle.SelectionForeColor = Color.White;

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        public void load_reg()
        {
            NpgsqlConnection con = new NpgsqlConnection(conn_string);
            try
            {
                con.Open();
                NpgsqlCommand cmd = new NpgsqlCommand("Select registration_id from cars where availability = 'yes'", con);
                NpgsqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(reader.GetString(0));
                }
                reader.Close();
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred" + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
             reg_id = comboBox_reg.Text;
            try
            {
                using (NpgsqlConnection con = new NpgsqlConnection(conn_string))
                {
                    con.Open();
                    string sql = "SELECT * FROM cars WHERE registration_id = @reg";
                    NpgsqlCommand cmd = new NpgsqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@reg", reg_id);

                    using (NpgsqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            // --- 2. Assign values to the global variables ---
                            reg = dr["registration_id"].ToString();
                            color = dr["color"].ToString();
                            model = dr["model"].ToString();
                            year = Convert.ToInt32(dr["year"]);
                            brand = dr["brand"].ToString();
                            engine = dr["engine_type"].ToString();
                            price = decimal.Parse(dr["price"].ToString());
                            isAvailable = dr["availability"].ToString();

                            MessageBox.Show($"Data for {reg} loaded into memory!");
                        }
                    }
                    con.Close();
                }

                txt_model.Text = model.ToString();
                txt_year.Text = year.ToString();
                txt_price.Text = price.ToString();
                txt_brand.Text = brand.ToString();
                txt_color.Text = color.ToString();
                txt_enginet.Text = engine.ToString();
                txt_avail.Text = isAvailable.ToString();
            }
            catch (Exception ex) 
            { MessageBox.Show("Error occured " + ex.Message); }
    }

        private void button9_Click(object sender, EventArgs e)
        {
            reg_id = comboBox_reg.Text;
            model = txt_model.Text;
            year = int.Parse(txt_year.Text);
            price = decimal.Parse(txt_price.Text);
            brand = txt_brand.Text;
            color = txt_color.Text;
            engine = txt_enginet.Text;
            isAvailable = txt_avail.Text;

            try
            {
                using (NpgsqlConnection con = new NpgsqlConnection(conn_string))
                {
                    con.Open();

                    // 2. The SQL UPDATE statement
                    string sql = @"UPDATE cars 
                           SET color = @color, 
                               model = @model, 
                               year = @year, 
                               brand = @brand, 
                               engine_type = @engine, 
                               price = @price, 
                               availability = @available 
                           WHERE registration_id = @reg";

                    NpgsqlCommand cmd = new NpgsqlCommand(sql, con);

                    // 3. Map the parameters to your variables
                    cmd.Parameters.AddWithValue("@color", color);
                    cmd.Parameters.AddWithValue("@model", model);
                    cmd.Parameters.AddWithValue("@year", year);
                    cmd.Parameters.AddWithValue("@brand", brand);
                    cmd.Parameters.AddWithValue("@engine", engine);
                    cmd.Parameters.AddWithValue("@price", price);
                    cmd.Parameters.AddWithValue("@available", isAvailable);
                    cmd.Parameters.AddWithValue("@reg", reg); // The original ID we searched for

                    // 4. Execute
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Record updated successfully!");
                    }
                    else
                    {
                        MessageBox.Show("Update failed. Record not found.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Update Error: " + ex.Message);
            }
        }
    }
}
