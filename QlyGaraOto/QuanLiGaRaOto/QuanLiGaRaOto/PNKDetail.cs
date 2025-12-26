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
    public partial class PNKDetail : Form
    {
        public PNKDetail()
        {
            InitializeComponent();
        }

        private void ClosePNKDetail(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LoadPNKCT(object sender, EventArgs e)
        {
            dataGridView1.DataSource = ImportOrderService.Instance.GetCT_PN(textBox1.Text);
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
