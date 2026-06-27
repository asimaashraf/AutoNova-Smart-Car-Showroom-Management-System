using Npgsql;
using Org.BouncyCastle.Ocsp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace AutoNova_Car_Showroom
{
    public partial class AddCar : Form
    {
        public string conn_string = "Host=localhost;Port=5432;Username=postgres;Password=password;Database=csms1;";
        public AddCar()
        {
            InitializeComponent();
        }

        private void lbllName_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            NpgsqlConnection con = new NpgsqlConnection(conn_string);
            try 
            {
                string brand = txt_brand.Text.ToLower().Replace(" ","_");
                string color = txt_color.Text.ToLower().Replace(" ", "_") ;
                string engine_type = txt_enginet.Text.ToLower();
                string availability = txt_avail.Text.ToLower();
                int year = int.Parse(txt_year.Text);
                decimal price = decimal.Parse(txt_price.Text);
                string reg_id = txt_id.Text.ToLower();
                string model = txt_model.Text.ToLower().Replace(" ", "_");
                con.Open();
                if (brand != "" && model != "" && year != 0 && color != "" && price != 0m && reg_id != "" && engine_type != "")
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("insert into cars(registration_id , color , model , year , brand , engine_type , price , availability) values(@reg,@color,@model,@year,@brand,@engine,@price,@available)", con);
                    cmd.Parameters.AddWithValue("@reg", reg_id);
                    cmd.Parameters.AddWithValue("@color", color);
                    cmd.Parameters.AddWithValue("@model", model);
                    cmd.Parameters.AddWithValue("@year", year); // Ensure this is int or string matching DB
                    cmd.Parameters.AddWithValue("@brand", brand);
                    cmd.Parameters.AddWithValue("@engine", engine_type);
                    cmd.Parameters.AddWithValue("@price", price); // Use decimal for currency precision
                    cmd.Parameters.AddWithValue("@available", availability);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Ride added successfully !!");
                }
                else
                {
                    MessageBox.Show("Fields cannot be empty");
                    return;

                }
            }
            catch(Exception ex) 
            {
                MessageBox.Show("❌ Following Error occured : " + ex);
            }
            

        }

        private void AddCar_Load(object sender, EventArgs e)
        {

        }
    }
}
