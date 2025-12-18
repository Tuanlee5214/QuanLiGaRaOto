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
    public partial class ReceiptWindow : Form
    {
        public ReceiptWindow()
        {
            InitializeComponent();
        }

        private void ExitReceiptForm(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
