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
    public partial class ImportOrder : Form
    {
        public ImportOrder()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void CloseImportOrder(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LoadForm(object sender, EventArgs e)
        {
            this.textBox1.Text = ImportOrderService.Instance.GetMaPhieuNhap();
            this.comboBox3.DataSource = RepairFormService.Instance.GetPhuTung();
            this.comboBox3.DisplayMember = "TenPhuTung";
            this.comboBox3.ValueMember = "MaPhuTung";

            this.comboBox3.SelectedValue = "";
        }

        private void TextChange(object sender, EventArgs e)
        {
            decimal output;
            if (string.IsNullOrEmpty(textBox2.Text))
            {
                textBox4.Text = "";
                return;
            }
            if (!Decimal.TryParse(textBox2.Text, out output) && !string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Vui lòng chỉ nhập số", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                return;
            }
            if(!string.IsNullOrWhiteSpace(textBox2.Text))
            {
                textBox4.Text = Convert.ToString(numericUpDown1.Value * Convert.ToDecimal(textBox2.Text));
            }
        }

        private void ValueChange(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(textBox2.Text))
            {
                textBox4.Text = "";
                return;
            }
            if (!string.IsNullOrWhiteSpace(textBox2.Text))
            {
                textBox4.Text = Convert.ToString(numericUpDown1.Value * Convert.ToDecimal(textBox2.Text));
            }
        }

        private void AddInfoIntoDatagrid(object sender, EventArgs e)
        {
            if(string.IsNullOrWhiteSpace(comboBox3.Text) || string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Tên phụ tùng và đơn giá không được bỏ trống", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                return;
            }

            string mavt = comboBox3.SelectedValue.ToString();
            string tenvt = comboBox3.Text;
            string sl = numericUpDown1.Value.ToString();
            string dg = textBox2.Text;
            string tt = textBox4.Text;
            dataGridView1.Rows.Add(mavt, tenvt, sl, dg, tt);
            TinhTongTien();
        }

        private void DeleteFromDatagrid(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null || dataGridView1.CurrentRow.IsNewRow)
            {
                MessageBox.Show("Vui lòng chọn dòng cần xóa", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show(
                "Bạn có chắc muốn xóa dòng này?",
                "Xác nhận",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Question);

            if (confirm != DialogResult.OK) return;

            dataGridView1.Rows.Remove(dataGridView1.CurrentRow);

            // Tính lại tổng tiền sau khi xóa
            TinhTongTien();
        }
        private void TinhTongTien()
        {
            decimal tongThanhTienPhuTung = 0;


            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.IsNewRow) continue;

                // 1. Cộng thành tiền phụ tùng
                decimal thanhTienPT = Convert.ToDecimal(row.Cells["ThanhTien"].Value);
                tongThanhTienPhuTung += thanhTienPT;
            }

            decimal tongTien = tongThanhTienPhuTung;
            textBox6.Text = tongTien.ToString("N0");
        }

        private void LapPhieuNhap(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("Vui lòng thêm thông tin", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                return;
            }

            string mapn = textBox1.Text;
            DateTime ngaynhap = dateTimePicker1.Value;
            decimal tongtien = Convert.ToDecimal(textBox6.Text);
            ImportOrderService.Instance.InsertIntoPhieuNhap(mapn, ngaynhap, tongtien);

            foreach (DataGridViewRow dataRow in dataGridView1.Rows)
            {
                if (dataRow.IsNewRow) continue;

                string mavt = dataRow.Cells["MaVTPT"].Value?.ToString();

                int sl = dataRow.Cells["SoLuong"].Value == null
                    ? 0
                    : Convert.ToInt32(dataRow.Cells["SoLuong"].Value);

                decimal dg = dataRow.Cells["DonGia"].Value == null
                    ? 0
                    : Convert.ToDecimal(dataRow.Cells["DonGia"].Value);

                decimal tt = dataRow.Cells["ThanhTien"].Value == null
                    ? 0
                    : Convert.ToDecimal(dataRow.Cells["ThanhTien"].Value);

                ImportOrderService.Instance.InsertIntoCT_PhieuNhap(mapn, mavt, sl, dg, tt);
                var pt = RepairFormService.Instance.GetPhuTungInfo(mavt);
                int slcu = pt.SoLuongTon;
                decimal dgnhapcu = pt.DonGiaNhap;
                int slmoi = slcu + sl;
                RepairFormService.Instance.UpdateSoLuong(mavt, slmoi);
                if(dgnhapcu != dg)
                {
                    double tilelai = RuleService.Instance.GetTiLeLai();
                    decimal dgmoi = (decimal)tilelai * dg;
                    ImportOrderService.Instance.UpdateDonGiaPT(mavt, dg, dgmoi);
                }
            }
            MessageBox.Show("Thêm thông tin thành công");
            this.Close();
        }
    }
    
}
