using DocumentFormat.OpenXml.VariantTypes;
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
    public partial class AddVTPT : Form
    {
        public AddVTPT()
        {
            InitializeComponent();
            textBox1.Text = ImportOrderService.Instance.GetMaPhuTung();
        }

        private void Close(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AddVTPT1(object sender, EventArgs e)
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

            if(string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Vui lòng nhập thông tin", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                return;
            }

            PhuTung pt = new PhuTung();
            pt.MaPhuTung = textBox1.Text;
            pt.TenPhuTung = textBox2.Text;
            var result = ImportOrderService.Instance.AddPhuTung(pt);
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
    }
}
