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
    public partial class UpdateCar : Form
    {
        private string HieuXe;
        public UpdateCar(string HieuXe)
        {
            InitializeComponent();
            this.HieuXe = HieuXe;
        }

        private void CloseUpdateCar(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ActivateForm(object sender, EventArgs e)
        {
            comboBox1.DataSource = CarService.Instance.GetTypeOfCarInfo();
            comboBox1.DisplayMember = "HieuXe";
            comboBox1.ValueMember = "HieuXe";

            comboBox1.SelectedValue = this.HieuXe;
        }

        private void UpdateCarsys(object sender, EventArgs e)
        {
            if (!textBox4.Text.All(char.IsDigit))
            {
                MessageBox.Show("Số điện thoại chỉ được chứa chữ số");
                return;
            }

            bool IsValidName(string name)
            {
                return Regex.IsMatch(name.Trim(), @"^[a-zA-ZÀ-ỹ\s]+$");
            }

            if (!IsValidName(textBox2.Text))
            {
                MessageBox.Show("Tên chỉ được chứa chữ cái");
                return;
            }

            Car c = new Car();
            c.BienSo = this.textBox1.Text;
            c.TenChuXe = this.textBox2.Text;
            c.DiaChi = this.textBox3.Text;
            c.SDT = this.textBox4.Text;
            c.Email = this.textBox5.Text;
            c.HieuXe = this.comboBox1.SelectedValue.ToString().Trim();
            var result = CarService.Instance.UpdateCar(c);
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
