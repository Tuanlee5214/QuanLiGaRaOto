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
    public partial class AddHieuXe : Form
    {
        public AddHieuXe()
        {
            InitializeComponent();
        }

        private void AddHieuXe_Load(object sender, EventArgs e)
        {

        }


        private void CloseAddHieuXe(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AddHieuXeInfo(object sender, EventArgs e)
        {
            bool IsValidName(string name)
            {
                return Regex.IsMatch(name.Trim(), @"^[a-zA-ZÀ-ỹ\s]+$");
            }

            if (!IsValidName(textBox6.Text) && !string.IsNullOrWhiteSpace(textBox6.Text))
            {
                MessageBox.Show("Tên hiệu xe chỉ được chứa chữ cái", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(textBox6.Text))
            {
                MessageBox.Show("Tên hiệu xe không được để trống", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                return;
            }

            string hx = textBox6.Text;
            var result = CarService.Instance.AddHieuXe(hx);
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
