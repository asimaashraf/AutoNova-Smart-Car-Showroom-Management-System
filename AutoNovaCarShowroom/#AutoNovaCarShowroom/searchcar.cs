using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Security.Cryptography;
using System.Windows.Forms;
namespace AutoNova_Car_Showroom
{
    public partial class searchcar : Form
    {
        public string con_string = "Host=localhost;Port=5432;Username=postgres;Password=password;Database=csms1;";
        List<string> brand = new List<string>();
        List<string> model_obj = new List<string>();
        public searchcar()
        {
            InitializeComponent();

            // Form Load
            this.Load += searchcars_Load;

            // Attach Events in Code
            textmodel.Enter += textmodel_Enter;
            textmodel.Leave += textmodel_Leave;

            textbrand.Enter += textbrand_Enter;
            textbrand.Leave += textbrand_Leave;


            
        }

        private void textmodel_TextChanged(object sender, EventArgs e)
        {

        }
        private void textbrand_TextChanged(object sender, EventArgs e)
        {

        }

        private void textprice_TextChanged(object sender, EventArgs e)
        {

        }

        private void searchcar_Load(object sender, EventArgs e)
        {
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

            dataGridView1.BackgroundColor = Color.FromArgb(154, 160, 166);
            dataGridView1.BorderStyle = BorderStyle.None;

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.RowHeadersVisible = false;

            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersHeight = 40;

            // HEADER STYLE
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(154, 160, 166);
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // ROW STYLE
            dataGridView1.DefaultCellStyle.BackColor = Color.FromArgb(154, 160, 166);
            dataGridView1.DefaultCellStyle.ForeColor = Color.Black;

            dataGridView1.RowTemplate.Height = 35;
        }





        private void searchcars_Load(object sender, EventArgs e)
        {
            textmodel.Text = "Model";
            textmodel.ForeColor = Color.Gray;

            textbrand.Text = "Brand";
            textbrand.ForeColor = Color.Gray;


  
        }

        // MODEL
        private void textmodel_Enter(object sender, EventArgs e)
        {
            if (textmodel.Text == "Model")
            {
                textmodel.Text = "";
                textmodel.ForeColor = Color.White;
            }
        }

        private void textmodel_Leave(object sender, EventArgs e)
        {
            if (textmodel.Text == "")
            {
                textmodel.Text = "Model";
                textmodel.ForeColor = Color.Gray;
            }
        }

        // BRAND
        private void textbrand_Enter(object sender, EventArgs e)
        {
            if (textbrand.Text == "Brand")
            {
                textbrand.Text = "";
                textbrand.ForeColor = Color.White;
            }
        }

        private void textbrand_Leave(object sender, EventArgs e)
        {
            if (textbrand.Text == "")
            {
                textbrand.Text = "Brand";
                textbrand.ForeColor = Color.Gray;
            }
        }



        private void button1_Click(object sender, EventArgs e)
        {
            // 1. Inputs ko variables mein store karein aur clean karein
            string brandInput = textbrand.Text.ToLower().Replace(" ","_");
            string modelInput = textmodel.Text.ToLower().Replace(" ","_");

            // 'using' block use karne se connection automatically close ho jata hai
            using (NpgsqlConnection con = new NpgsqlConnection(con_string))
            {
                try
                {
                    // Check karein ke kam az kam ek field bhari ho
                    if (!string.IsNullOrEmpty(brandInput) || !string.IsNullOrEmpty(modelInput))
                    {
                        con.Open();

                        // 2. Query mein brackets ka istemal zaroori hai taake 'is_active' lazmi check ho
                        string query = @"SELECT 
                                    brand AS ""Brand"",
                                    model AS ""Model"", 
                                    color AS ""Color"", 
                                    engine_type AS ""Engine"", 
                                    price AS ""Price"", 
                                    availability AS ""Status"" 
                                 FROM cars
                                 WHERE is_active = TRUE 
                                 AND (LOWER(brand) = @brand OR LOWER(model) = @model)";

                        using (NpgsqlCommand cmd = new NpgsqlCommand(query, con))
                        {
                            // 3. FIX: Parameters ko value assign karna (Yahan aapka code ruk raha tha)
                            cmd.Parameters.AddWithValue("@brand", brandInput);
                            cmd.Parameters.AddWithValue("@model", modelInput);

                            NpgsqlDataAdapter sda = new NpgsqlDataAdapter(cmd);
                            DataTable dt = new DataTable();
                            sda.Fill(dt);

                            // 4. DataGrid update karna
                            dataGridView1.DataSource = dt;

                            if (dt.Rows.Count == 0)
                            {
                                MessageBox.Show("Car Not Found!!");
                            }
                            else
                            {
                                MessageBox.Show("Loading Results");
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please enter Brand or Model name.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }
    }
}