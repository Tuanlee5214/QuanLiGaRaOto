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
    public partial class QuatityReport : Form
    {
        public QuatityReport()
        {
            InitializeComponent();
        }

        private void CloseQuanReport(object sender, EventArgs e)
        {
            this.Close();
        }

        private void OpenVTPTBC(object sender, EventArgs e)
        {
            int thang = Convert.ToInt32(numericUpDown1.Value);
            int nam = Convert.ToInt32(numericUpDown2.Value);
            dataGridView1.DataSource = ReportService.Instance.GetBCTonKho(thang, nam);

        }
    }
}
