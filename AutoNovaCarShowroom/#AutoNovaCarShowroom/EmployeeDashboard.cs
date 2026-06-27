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
    public partial class EmployeeDashboard : Form
    {
        public EmployeeDashboard()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            panelmain2.Controls.Clear();

            ViewInventry obj = new ViewInventry();
            obj.TopLevel = false;
            obj.Dock = DockStyle.Fill;

            panelmain2.Controls.Add(obj);
            obj.Show();
        }

        private void EmployeeDashboard_Load(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            LoginChoice obj = new LoginChoice();
            obj.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panelmain2.Controls.Clear();

            AddCar obj = new AddCar();
            obj.TopLevel = false;
            obj.Dock = DockStyle.Fill;

            panelmain2.Controls.Add(obj);
            obj.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panelmain2.Controls.Clear();

            UpdateCar obj = new UpdateCar();
            obj.TopLevel = false;
            obj.Dock = DockStyle.Fill;

            panelmain2.Controls.Add(obj);
            obj.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panelmain2.Controls.Clear();

            DeleteCar obj = new DeleteCar();
            obj.TopLevel = false;
            obj.Dock = DockStyle.Fill;

            panelmain2.Controls.Add(obj);
            obj.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            panelmain2.Controls.Clear();

            AICarAdvisor obj = new AICarAdvisor();
            obj.TopLevel = false;
            obj.Dock = DockStyle.Fill;

            panelmain2.Controls.Add(obj);
            obj.Show();
        }

        private void panelmain2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
