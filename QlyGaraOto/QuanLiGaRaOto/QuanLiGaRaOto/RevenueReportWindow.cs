using QuanLiGaRaOto.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLiGaRaOto
{
    public partial class RevenueReportWindow : Form
    {
        public RevenueReportWindow()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void ExitRev_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DisplayReportRevenue(object sender, EventArgs e)
        {
            int thang = Convert.ToInt32(numericUpDown1.Value);
            int nam = Convert.ToInt32(numericUpDown2.Value);
            var result = ReportService.Instance.GetBCDoanhThu(thang, nam);
            dataGridView1.DataSource = result;
            textBox1.Text = ReportService.Instance.GetTongTienThang(thang, nam).ToString();
        }
    }
}
