using Npgsql;
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
    public partial class DeleteCar : Form
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
        public DeleteCar()
        {
            InitializeComponent();
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (dataGridView1.Columns[e.ColumnIndex].Name == "delete" && e.RowIndex >= 0)
            //{
            //    DialogResult result = MessageBox.Show(
            //        "Are you sure you want to delete this car?",
            //        "Confirm Delete",
            //        MessageBoxButtons.YesNo
            //    );

            //    if (result == DialogResult.Yes)
            //    {
            //        dataGridView1.Rows.RemoveAt(e.RowIndex);
            //    }
            //}
        }

        private void dataGridView1_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.ColumnIndex >= 0 && dataGridView1.Columns[e.ColumnIndex].Name == "delete")
            //{
            //    dataGridView1.Cursor = Cursors.Hand;
            //}
        }


        private void DeleteCar_Load(object sender, EventArgs e)
        {
            load_reg();
            foreach (var item in list)
            {
                comboBox_reg.Items.Add(item);
            }
            //    if (dataGridView1.Columns.Count == 0)
            //    {
            //        // COLUMNS
            //        dataGridView1.Columns.Add("carid", "Car ID");
            //        dataGridView1.Columns.Add("model", "Model");
            //        dataGridView1.Columns.Add("brand", "Brand");
            //        dataGridView1.Columns.Add("price", "Price");
            //        dataGridView1.Columns.Add("color", "Color");
            //        dataGridView1.Columns.Add("year", "Year");
            //        dataGridView1.Columns.Add("engine", "Engine Type");
            //        dataGridView1.Columns.Add("availability", "Availability");

            //        // DELETE BUTTON COLUMN
            //        DataGridViewButtonColumn deleteBtn = new DataGridViewButtonColumn();
            //        deleteBtn.Name = "delete";
            //        deleteBtn.HeaderText = "Action";
            //        deleteBtn.Text = "Delete";
            //        deleteBtn.UseColumnTextForButtonValue = true;

            //        dataGridView1.Columns.Add(deleteBtn);

            //        // SAMPLE DATA
            //        dataGridView1.Rows.Add("1", "Civic", "Honda", "5000000", "Black", "2022", "1800cc", "Available");
            //        dataGridView1.Rows.Add("2", "Corolla", "Toyota", "4500000", "White", "2021", "1600cc", "Available");
            //        dataGridView1.Rows.Add("3", "Sportage", "KIA", "6500000", "Red", "2023", "2000cc", "Not Available");
            //    }

            //    // GRID STYLE
            //    dataGridView1.BackgroundColor = Color.FromArgb(154, 160, 166);
            //    dataGridView1.BorderStyle = BorderStyle.None;

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

            //    // DELETE BUTTON STYLE
            //    dataGridView1.Columns["delete"].DefaultCellStyle.BackColor = Color.Red;
            //    dataGridView1.Columns["delete"].DefaultCellStyle.ForeColor = Color.White;
            //    dataGridView1.Columns["delete"].DefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
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
            {
                MessageBox.Show("Error occured " + ex.Message);
            }
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

        private void button9_Click(object sender, EventArgs e)
        {
            reg = comboBox_reg.Text;
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(conn_string);
                NpgsqlCommand cmd = new NpgsqlCommand("UPDATE cars SET is_active = FALSE WHERE registration_id = @reg;", con);
                con.Open();
                cmd.Parameters.AddWithValue("@reg", reg);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show($"Record with {reg} deleted");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred " + ex.Message);
            }
        }
    }
}
