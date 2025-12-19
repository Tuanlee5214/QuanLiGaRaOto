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
    public partial class ContentRepairListWindow : Form
    {
        public ContentRepairListWindow()
        {
            InitializeComponent();
        }

        private void CloseContentRepairWin(object sender, EventArgs e)
        {
            this.Close();
        }

        private void OpenUpdateContentRepair(object sender, EventArgs e)
        {
            UpdateContentRepair ud = new UpdateContentRepair();
            ud.Show();
        }

        private void OpenAddContentRepair(object sender, EventArgs e)
        {
            AddConRepair ct = new AddConRepair();
            ct.Show();
        }
    }
}
