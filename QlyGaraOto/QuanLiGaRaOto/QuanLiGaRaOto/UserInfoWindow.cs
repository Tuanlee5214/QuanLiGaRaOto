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
    public partial class UserInfoWindow : Form
    {
        public UserInfoWindow()
        {
            InitializeComponent();
        }

        private void InfoClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ChangPass_Click(object sender, EventArgs e)
        {
            ChangePass cb = new ChangePass();
            cb.Show();
        }

        private void OpenUpdateWin(object sender, EventArgs e)
        {
            UpdateUserWindow ud = new UpdateUserWindow();
            ud.Show();
        }
    }
}
