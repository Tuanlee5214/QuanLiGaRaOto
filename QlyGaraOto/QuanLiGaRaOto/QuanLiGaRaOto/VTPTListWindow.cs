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
    public partial class VTPTListWindow : Form
    {
        public VTPTListWindow()
        {
            InitializeComponent();
        }

        private void CloseVTPTListWindow(object sender, EventArgs e)
        {
            this.Close();
        }

        private void OpenUpdateVTPTWindow(object sender, EventArgs e)
        {
            UpdateVTPT ud = new UpdateVTPT();
            ud.Show();
        }

        private void OpenAddVTPTWin(object sender, EventArgs e)
        {
            AddVTPT a = new AddVTPT();
            a.Show();
        }
    }
}
