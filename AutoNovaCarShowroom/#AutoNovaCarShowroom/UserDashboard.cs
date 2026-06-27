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
    public partial class UserDashboard : Form
    {
        public UserDashboard()
        {
            InitializeComponent();
        }

        private void panelmain_Paint(object sender, PaintEventArgs e)
        {

        }

        public void LoadForm(Form f)
        {
            panelmain.Controls.Clear();

            f.TopLevel = false;
            f.Dock = DockStyle.Fill;
            f.FormBorderStyle = FormBorderStyle.None;

            panelmain.Controls.Add(f);
            f.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            LoginChoice obj = new LoginChoice();
            obj.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {


            panelmain.Controls.Clear();

            availablecar obj = new availablecar();
            obj.TopLevel = false;
            obj.Dock = DockStyle.Fill;

            panelmain.Controls.Add(obj);
            obj.Show();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            panelmain.Controls.Clear();

            searchcar obj = new searchcar();
            obj.TopLevel = false;
            obj.Dock = DockStyle.Fill;

            panelmain.Controls.Add(obj);
            obj.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panelmain.Controls.Clear();

            carbooking obj = new carbooking();
            obj.TopLevel = false;
            obj.Dock = DockStyle.Fill;

            panelmain.Controls.Add(obj);
            obj.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {

            panelmain.Controls.Clear();

            viewBookinguserform obj = new viewBookinguserform();
            obj.TopLevel = false;
            obj.Dock = DockStyle.Fill;

            panelmain.Controls.Add(obj);
            obj.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            panelmain.Controls.Clear();

            AICarAdvisor obj = new AICarAdvisor();
            obj.TopLevel = false;
            obj.Dock = DockStyle.Fill;

            panelmain.Controls.Add(obj);
            obj.Show();
        }
    }
}
