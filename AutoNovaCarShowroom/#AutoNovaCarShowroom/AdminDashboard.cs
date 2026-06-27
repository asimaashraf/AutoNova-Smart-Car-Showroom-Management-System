using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoNova_Car_Showroom
{
    public partial class AdminDashboard : Form
    {
        public AdminDashboard()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
           panelmain1.Controls.Clear();

            availablecar obj = new availablecar();
            obj.TopLevel = false;
            obj.Dock = DockStyle.Fill;

            panelmain1.Controls.Add(obj);
            obj.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            LoginChoice obj = new LoginChoice();
            obj.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panelmain1.Controls.Clear();

            AddCar obj = new AddCar();
            obj.TopLevel = false;
            obj.Dock = DockStyle.Fill;

            panelmain1.Controls.Add(obj);
            obj.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panelmain1.Controls.Clear();

            UpdateCar obj = new UpdateCar();
            obj.TopLevel = false;
            obj.Dock = DockStyle.Fill;

            panelmain1.Controls.Add(obj);
            obj.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panelmain1.Controls.Clear();

            DeleteCar obj = new DeleteCar();
            obj.TopLevel = false;
            obj.Dock = DockStyle.Fill;

            panelmain1.Controls.Add(obj);
            obj.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            panelmain1.Controls.Clear();

            ViewInventry obj = new ViewInventry();
            obj.TopLevel = false;
            obj.Dock = DockStyle.Fill;

            panelmain1.Controls.Add(obj);
            obj.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            panelmain1.Controls.Clear();

            ViewSales obj = new ViewSales();
            obj.TopLevel = false;
            obj.Dock = DockStyle.Fill;

            panelmain1.Controls.Add(obj);
            obj.Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            panelmain1.Controls.Clear();

            HandleEmployes obj = new HandleEmployes();
            obj.TopLevel = false;
            obj.Dock = DockStyle.Fill;

            panelmain1.Controls.Add(obj);
            obj.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            panelmain1.Controls.Clear();

            AICarAdvisor obj = new AICarAdvisor();
            obj.TopLevel = false;
            obj.Dock = DockStyle.Fill;

            panelmain1.Controls.Add(obj);
            obj.Show();
        }

        private void panelmain1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
