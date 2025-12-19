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
    public partial class ListImportOrder : Form
    {
        public ListImportOrder()
        {
            InitializeComponent();
        }

        private void CloseImportOrderList(object sender, EventArgs e)
        {
            this.Close();
        }

        private void OpenPNKDetail(object sender, EventArgs e)
        {
            PNKDetail p = new PNKDetail();
            p.Show();
        }
    }
}
