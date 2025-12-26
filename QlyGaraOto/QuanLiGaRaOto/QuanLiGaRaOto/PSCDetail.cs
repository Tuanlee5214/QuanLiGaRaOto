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
    public partial class PSCDetail : Form
    {
        public PSCDetail()
        {
            InitializeComponent();
        }

        private void ClosePSCDetail(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LoadCT_PSC(object sender, EventArgs e)
        {
            dataGridView1.DataSource = RepairFormService.Instance.GetCT_PSC(textBox1.Text);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
