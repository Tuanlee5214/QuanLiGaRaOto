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
    public partial class RepairForm : Form
    {
        public RepairForm()
        {
            InitializeComponent();
        }


        private void CloseRepairForm(object sender, EventArgs e)
        {
            this.Close();
        }



        private void ActivateForm(object sender, EventArgs e)
        {
            this.textBox1.Text = RepairFormService.Instance.GetMaPSC();
            this.comboBox1.DataSource = RepairFormService.Instance.GetBSX();
            this.comboBox1.DisplayMember = "BienSo";
            this.comboBox1.ValueMember = "BienSo";
            this.comboBox1.SelectedValue = "";
            this.comboBox2.DataSource = RepairFormService.Instance.GetNoiDungSC();
            this.comboBox2.DisplayMember = "MoTa";
            this.comboBox2.ValueMember = "MaNoiDung";
            this.comboBox2.SelectedValue = "";
            this.comboBox3.DataSource = RepairFormService.Instance.GetPhuTung();
            this.comboBox3.DisplayMember = "TenPhuTung";
            this.comboBox3.ValueMember = "MaPhuTung";
            this.comboBox3.SelectedValue = "";

        }

        private void SelectedChangeTc(object sender, EventArgs e)
        {
            if (comboBox2.SelectedValue == null) return;
            this.textBox5.Text = Convert.ToString(RepairFormService.Instance.GetTienCong(this.comboBox2.SelectedValue.ToString()));
        }

        private void SelectedChangePT(object sender, EventArgs e)
        {
            if(comboBox3.SelectedValue == null) return;
            this.textBox3.Text = Convert.ToString(RepairFormService.Instance.GetPhuTungInfo(this.comboBox3.SelectedValue.ToString()).DonGiaBan);
            this.textBox4.Text = Convert.ToString(RepairFormService.Instance.GetPhuTungInfo(this.comboBox3.SelectedValue.ToString()).SoLuongTon);
            textBox2.Text = Convert.ToString(Convert.ToDecimal(textBox3.Text.Trim()) * numericUpDown1.Value);
        }

        private void ValueChange(object sender, EventArgs e)
        {
            textBox2.Text = Convert.ToString(Convert.ToDecimal(textBox3.Text.Trim()) * numericUpDown1.Value);
        }

        private void AddInfoIntoDatagrid(object sender, EventArgs e)
        {
            if(string.IsNullOrWhiteSpace(comboBox1.Text))
            {
                MessageBox.Show("Biển số xe không được để trống", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                return;
            }

            string nd = comboBox2.Text;
            string mand = comboBox2.SelectedValue.ToString();
            string mavt = comboBox3.SelectedValue.ToString();
            string tenvt = comboBox3.Text;
            string dongia = textBox3.Text;
            string sl = numericUpDown1.Value.ToString();
            string tc = textBox5.Text;
            string ttpt = textBox2.Text;
            string slt = textBox4.Text;
            if(Convert.ToInt32(slt) - Convert.ToInt32(sl) >= 0)
            {
                dataGridView1.Rows.Add(nd, mand, mavt, tenvt, dongia, sl, tc, ttpt);
                TinhTongTien();
            }
            else
            {
                MessageBox.Show("Không đủ số lượng để thêm", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            }
        }

        private void XoaDongDangChon(object sender, EventArgs e)
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
            decimal tongTienCong = 0;

            HashSet<string> noiDungDaTinhTienCong = new HashSet<string>();

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.IsNewRow) continue;

                // 1. Cộng thành tiền phụ tùng
                decimal thanhTienPT = Convert.ToDecimal(row.Cells["ThanhTien"].Value);
                tongThanhTienPhuTung += thanhTienPT;

                // 2. Cộng tiền công (chỉ 1 lần / nội dung)
                string noiDung = row.Cells["NoiDung"].Value.ToString();

                if (!noiDungDaTinhTienCong.Contains(noiDung))
                {
                    decimal tienCong = Convert.ToDecimal(row.Cells["TienCong"].Value);
                    tongTienCong += tienCong;
                    noiDungDaTinhTienCong.Add(noiDung);
                }
            }

            decimal tongTien = tongThanhTienPhuTung + tongTienCong;
            textBox6.Text = tongTien.ToString("N0");
        }



        private void LapPhieuSC(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("Vui lòng thêm thông tin", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                return;
            }

            string masc = textBox1.Text;
            string bienso = comboBox1.SelectedValue.ToString();
            DateTime ngaynhap = dateTimePicker1.Value;
            decimal tongno = Convert.ToDecimal(textBox6.Text);
            RepairFormService.Instance.InsertIntoPSC(masc, bienso, ngaynhap, tongno);

            foreach (DataGridViewRow dataRow in dataGridView1.Rows)
            {
                if (dataRow.IsNewRow) continue;

                string mavt = dataRow.Cells["MaVTPT"].Value?.ToString();
                string mand = dataRow.Cells["MaNoiDung"].Value?.ToString();

                int sl = dataRow.Cells["SoLuong"].Value == null
                    ? 0
                    : Convert.ToInt32(dataRow.Cells["SoLuong"].Value);

                decimal dg = dataRow.Cells["DonGia"].Value == null
                    ? 0
                    : Convert.ToDecimal(dataRow.Cells["DonGia"].Value);

                decimal tc = dataRow.Cells["TienCong"].Value == null
                    ? 0
                    : Convert.ToDecimal(dataRow.Cells["TienCong"].Value);

                decimal tt = dataRow.Cells["ThanhTien"].Value == null
                    ? 0
                    : Convert.ToDecimal(dataRow.Cells["ThanhTien"].Value);

                RepairFormService.Instance.InsertIntoCT_PSC(masc, mavt, mand, sl, dg, tt, tc);
                int slcu = RepairFormService.Instance.GetPhuTungInfo(mavt).SoLuongTon;
                int slmoi = slcu - sl;
                RepairFormService.Instance.UpdateSoLuong(mavt, slmoi);
            }
            decimal tiennocu = RepairFormService.Instance.GetCarInfo(bienso).TongNo;
            decimal tiennomoi = tiennocu + tongno;
            var result = RepairFormService.Instance.UpdateTienNoMoi(bienso, tiennomoi);
            if (result.Success) MessageBox.Show(result.SuccessMessage);
            this.Close();
        }
    }
}
