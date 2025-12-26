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
    public partial class UpdateVTPT : Form
    {
        public UpdateVTPT()
        {
            InitializeComponent();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void CloseUpdateVTPT(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }



        private void UpdateVTPTInfo(object sender, EventArgs e)
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
            if(!Decimal.TryParse(textBox3.Text, out output)  && !string.IsNullOrWhiteSpace(textBox3.Text))
            {
                MessageBox.Show("Đơn giá nhập chỉ được chứa kí tự số", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(textBox2.Text) || string.IsNullOrWhiteSpace(textBox3.Text))
            {
                MessageBox.Show("Vui lòng nhập thông tin", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                return;
            }

            PhuTung pt = new PhuTung();
            pt.MaPhuTung = textBox1.Text;
            pt.TenPhuTung = textBox2.Text;
            pt.DonGiaNhap = Convert.ToDecimal(textBox3.Text);
            pt.DonGiaBan = Convert.ToDecimal(textBox4.Text);
            var result = ImportOrderService.Instance.UpdatePhuTung(pt);
            if(result.Success)
            {
                MessageBox.Show(result.SuccessMessage);
                this.Close();
            }
            else
            {
                MessageBox.Show(result.ErrorMessage);
            }

        }

        private void TextChange(object sender, EventArgs e)
        {
            double tilelai = RuleService.Instance.GetTiLeLai();
            if(string.IsNullOrEmpty(textBox3.Text))
            {
                textBox4.Text = "";
                return;
            }
            textBox4.Text = (Math.Round((Convert.ToDecimal(textBox3.Text) * (decimal)tilelai), 0)).ToString();
        }
    }
}
