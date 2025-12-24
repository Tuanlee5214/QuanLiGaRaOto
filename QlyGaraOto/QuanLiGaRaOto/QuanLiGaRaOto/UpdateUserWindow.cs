using QuanLiGaRaOto.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLiGaRaOto
{
    public partial class UpdateUserWindow : Form
    {
        public UpdateUserWindow()
        {
            InitializeComponent();
            this.textBox1.Text = UserSession.CurrentUser.TenDangNhap;
            this.textBox2.Text = UserSession.CurrentUser.TenNguoiDung;
            this.textBox3.Text = UserSession.CurrentUser.NgaySinh.ToShortDateString();
            this.comboBox1.DataSource = UserService.Instance.GetGroupInfo();
            this.comboBox1.DisplayMember = "TenNhomNguoiDung";
            this.comboBox1.ValueMember = "MaNhomND";
        }

        private void UpdateUserWindow_Load(object sender, EventArgs e)
        {

        }

        private void ExitUpDateUser_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void UpdateUserInfo(object sender, EventArgs e)
        {
            DateTime dtNgaySinh;

            bool hopLe = DateTime.TryParseExact(
                textBox3.Text.Trim(),
                "dd/MM/yyyy",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out dtNgaySinh
            );

            if (!hopLe)
            {
                MessageBox.Show("Ngày sinh không hợp lệ (dd/MM/yyyy)");
                return;
            }

            var result = UserService.Instance.UpdateUser(textBox2.Text, dtNgaySinh, comboBox1.SelectedValue.ToString());
            if(result.Success)
            {
                MessageBox.Show(result.SuccesMessage);
                this.Close();
            }
            else
            {
                MessageBox.Show(result.ErrorMessage);
                this.Close();
            }
        }
    }
}
