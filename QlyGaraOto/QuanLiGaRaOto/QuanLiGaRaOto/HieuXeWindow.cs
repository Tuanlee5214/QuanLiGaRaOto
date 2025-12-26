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
    public partial class HieuXeWindow : Form
    {
        public HieuXeWindow()
        {
            InitializeComponent();
        }   

        private void CloseHieuXeWin(object sender, EventArgs e)
        {
            this.Close();
        }

        private void OpenAddHieuXe(object sender, EventArgs e)
        {
            AddHieuXe hx = new AddHieuXe();
            hx.Show();
        }

        private void LoadHieuXe(object sender, EventArgs e)
        {
            dataGridView1.DataSource = CarService.Instance.GetHieuXe();
        }

        private void SearchHieuXe(object sender, EventArgs e)
        {
            if(string.IsNullOrWhiteSpace(textBox8.Text))
            {
                MessageBox.Show("Thông tin tìm kiếm không được để trống", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                return;
            }

            bool IsValidName(string name)
            {
                return Regex.IsMatch(name.Trim(), @"^[a-zA-ZÀ-ỹ\s]+$");
            }

            if (!IsValidName(textBox8.Text) && !string.IsNullOrWhiteSpace(textBox8.Text))
            {
                MessageBox.Show("Thông tin tìm kiếm hiệu xe chỉ được chứa chữ cái", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                return;
            }

            string searchText = textBox8.Text;
            dataGridView1.DataSource = CarService.Instance.SearchHieuXe(searchText);
        }

        private void DeleteHieuXe(object sender, EventArgs e)
        {

            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn hiệu xe cần xóa", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                return;
            }

            string hx = dataGridView1.CurrentRow.Cells["HieuXe"].Value?.ToString();
            DialogResult result1 = MessageBox.Show("Bạn có muốn xóa hiệu xe này không", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result1 == DialogResult.Yes)
            {
                var result = CarService.Instance.DeleteHieuXe(hx);
                if (result.Success)
                {
                    MessageBox.Show(result.SuccessMessage);
                }
                else
                {
                    MessageBox.Show(result.ErrorMessage);
                }
            }

        }

    }
}
