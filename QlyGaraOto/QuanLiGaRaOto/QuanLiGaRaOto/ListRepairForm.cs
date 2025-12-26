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
    public partial class ListRepairForm : Form
    {
        public ListRepairForm()
        {
            InitializeComponent();
        }

        private void CloseListPSC(object sender, EventArgs e)
        {
            this.Close();
        }

        private void OpenPSCDetail(object sender, EventArgs e)
        {
            if(dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn phiếu sửa chữa để xem chi tiết", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                return;
            }
            PSCDetail ps = new PSCDetail();
            ps.textBox1.Text = dataGridView1.CurrentRow.Cells["MaPhieuSC"].Value.ToString();
            ps.textBox2.Text = dataGridView1.CurrentRow.Cells["BienSo"].Value.ToString();
            ps.textBox3.Text = dataGridView1.CurrentRow.Cells["TongTien"].Value.ToString();
            ps.dateTimePicker1.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells["NgaySuaChua"].Value);
            ps.Show();
        }

        private void LoadPSCInfo(object sender, EventArgs e)
        {
            dataGridView1.DataSource = RepairFormService.Instance.GetPSC();
        }

        private void SearchPSC(object sender, EventArgs e)
        {
            int type = 0;
            string searchText = "";
            int ngay = 1; int thang = 1; int nam = 1;
            if(string.IsNullOrWhiteSpace(textBox8.Text) && !radioButton2.Checked)
            {
                MessageBox.Show("Thông tin tìm kiếm không được bỏ trống", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                return;
            }

            if (!radioButton1.Checked && !radioButton2.Checked)
            {
                MessageBox.Show("Vui lòng chọn kiểu tìm kiếm");
                return;
            }

            if (radioButton1.Checked)
            {
                type = 1;
                searchText = textBox8.Text;

            }
            else if (radioButton2.Checked)
            {
                type = 2;
                ngay = dateTimePicker1.Value.Day;
                thang = dateTimePicker1.Value.Month;
                nam = dateTimePicker1.Value.Year;
                
            }

            if (type == 0)
            {
                MessageBox.Show("Vui lòng chọn kiểu tìm kiếm phiếu sửa chữa", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                return;
            }

            if(string.IsNullOrWhiteSpace(textBox8.Text) && radioButton2.Checked)
            {
                dataGridView1.DataSource = RepairFormService.Instance.SearchPSC(searchText, type, ngay, thang, nam);
                return;
            }

            dataGridView1.DataSource = RepairFormService.Instance.SearchPSC(searchText, type, ngay, thang, nam);
        }
    }
}
