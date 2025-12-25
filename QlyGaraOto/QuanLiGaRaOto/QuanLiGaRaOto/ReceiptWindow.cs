using QuanLiGaRaOto.Service;
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
    public partial class ReceiptWindow : Form
    {
        public ReceiptWindow()
        {
            InitializeComponent();
        }

        private void ExitReceiptForm(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ActivateForm(object sender, EventArgs e)
        {
            this.textBox6.Text = PhieuThuTien.Instance.GetMaPhieuThuTien();
            this.comboBox1.DataSource = RepairFormService.Instance.GetBSX();
            this.comboBox1.DisplayMember = "BienSo";
            this.comboBox1.ValueMember = "BienSo";
            this.comboBox1.SelectedValue = "";
        }

        private void LoadCarInfo(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.comboBox1.Text))
            {
                textBox3.Text = "";
                textBox2.Text = "";
                textBox4.Text = "";
                textBox1.Text = "";
                textBox5.Text = "";
                textBox7.Text = "";
                textBox9.Text = "";
                return;
            }
            
            var item = CarService.Instance.GetCarFromBienSo(this.comboBox1.Text);
            textBox3.Text = item.TenChuXe;
            textBox2.Text = item.HieuXe;
            textBox4.Text = item.SDT;
            textBox1.Text = item.Email;
            textBox5.Text = item.DiaChi;
            textBox7.Text = item.TongNo.ToString();
            textBox9.Text = item.TongNo.ToString();
        }

        private void ChangeTongNo(object sender, EventArgs e)
        {
            decimal tongNo = 0;
            decimal soTienThu = 0;
            decimal output;
            

            decimal.TryParse(textBox7.Text, out tongNo);
            decimal.TryParse(textBox8.Text, out soTienThu);
            if (!Decimal.TryParse(textBox8.Text, out output) && !string.IsNullOrWhiteSpace(textBox8.Text))
            {
                MessageBox.Show("Vui lòng nhập tiền thu thích hợp!");
                textBox8.Clear();
            }

            if (soTienThu > tongNo)
            {
                MessageBox.Show("Số tiền thu không được lớn hơn tổng nợ");
                textBox8.Text = tongNo.ToString();
                soTienThu = tongNo;
                return;
            }

            textBox9.Text = (tongNo - soTienThu).ToString();

        }

        private void LapPhieuThu(object sender, EventArgs e)
        {
            if(string.IsNullOrWhiteSpace(comboBox1.Text) || string.IsNullOrWhiteSpace(textBox8.Text))
            {
                MessageBox.Show("Vui lòng nhập thông tin", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                return;
            }


            var result = PhieuThuTien.Instance.InsertIntoPTT(textBox6.Text, comboBox1.SelectedValue.ToString(), dateTimePicker1.Value, Convert.ToDecimal(textBox8.Text));
            RepairFormService.Instance.UpdateTienNoMoi(comboBox1.SelectedValue.ToString(), Convert.ToDecimal(textBox9.Text));
            if (result.Success)
            {
                MessageBox.Show(result.SuccessMessage);
                this.Close();
            }
            else
                MessageBox.Show(result.ErrorMessage);
        }
    }
}
