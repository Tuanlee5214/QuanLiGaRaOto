using QuanLiGaRaOto.Model;
using QuanLiGaRaOto.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLiGaRaOto
{
    public partial class UpdateContentRepair : Form
    {
        public UpdateContentRepair()
        {
            InitializeComponent();
        }

        private void CloseUpdateContentRepair(object sender, EventArgs e)
        {
            this.Close();
        }

        private void UpdateContentInfo(object sender, EventArgs e)
        {
            bool IsValidName(string name)
            {
                return Regex.IsMatch(name.Trim(), @"^[a-zA-ZÀ-ỹ\s]+$");
            }

            if (!IsValidName(textBox2.Text) && !string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Tên chỉ được chứa chữ cái", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                return;
            }

            decimal output;
            if (!Decimal.TryParse(textBox3.Text, out output) && !string.IsNullOrWhiteSpace(textBox3.Text))
            {
                MessageBox.Show("Tiền công chỉ được chứa kí tự số", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(textBox2.Text) || string.IsNullOrWhiteSpace(textBox3.Text))
            {
                MessageBox.Show("Vui lòng nhập thông tin", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                return;
            }

            NoiDung pt = new NoiDung();
            pt.MaNoiDung = textBox1.Text;
            pt.MoTa = textBox2.Text;
            pt.TienCong = Convert.ToDecimal(textBox3.Text);
            var result = ContentService.Instance.UpdateContent(pt);
            if (result.Success)
            {
                MessageBox.Show(result.SuccessMessage);
                this.Close();
            }
            else
            {
                MessageBox.Show(result.ErrorMessage);
            }

        }

        private void UpdateContentRepair_Load(object sender, EventArgs e)
        {

        }
    }
}
