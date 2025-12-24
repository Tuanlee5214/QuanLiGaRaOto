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
    public partial class AddCarWindow : Form
    {
        public AddCarWindow()
        {
            InitializeComponent();
            comboBox1.DataSource = CarService.Instance.GetTypeOfCarInfo();
            comboBox1.DisplayMember = "HieuXe";
            comboBox1.ValueMember = "HieuXe";
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void CloseAddCar(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AddCarIntoSystem(object sender, EventArgs e)
        {
            if(string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text) 
               || string.IsNullOrWhiteSpace(textBox3.Text) || string.IsNullOrWhiteSpace(textBox4.Text) ||
               string.IsNullOrWhiteSpace(textBox5.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                return;
            }

            int CarInADay = CarService.Instance.GetCarInADay();
            int MaxCarInADay = RuleService.Instance.GetMaxCarOfDay();
            if(CarInADay + 1 > MaxCarInADay)
            {
                MessageBox.Show("Vượt quá số xe tiếp nhận trong ngày", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                return;
            }

            if (!textBox1.Text.All(char.IsDigit))
            {
                MessageBox.Show("Số điện thoại chỉ được chứa chữ số");
                return;
            }

            bool IsValidName(string name)
            {
                return Regex.IsMatch(name.Trim(), @"^[a-zA-ZÀ-ỹ\s]+$");
            }

            if (!IsValidName(textBox3.Text))
            {
                MessageBox.Show("Tên chỉ được chứa chữ cái");
                return;
            }

            Car item = new Car();
            item.BienSo = textBox2.Text;
            item.TenChuXe = textBox3.Text;
            item.HieuXe = comboBox1.Text;
            item.DiaChi = textBox4.Text;
            item.SDT = textBox1.Text;
            item.Email = textBox5.Text;
            item.NgayTiepNhan = dateTimePicker1.Value;

            var result = CarService.Instance.AddCar(item);
            if(result.Success)
            {
                MessageBox.Show(result.SuccessMessage);
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
