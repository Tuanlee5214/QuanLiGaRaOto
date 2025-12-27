using ClosedXML.Excel;
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
    public partial class VTPTListWindow : Form
    {
        public VTPTListWindow()
        {
            InitializeComponent();
        }

        private void CloseVTPTListWindow(object sender, EventArgs e)
        {
            this.Close();
        }

        private void OpenUpdateVTPTWindow(object sender, EventArgs e)
        {
            if(string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Vui lòng chọn vật tư phụ tùng cần cập nhật", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                return;
            }
            UpdateVTPT ud = new UpdateVTPT();
            ud.textBox1.Text = this.textBox3.Text;
            ud.textBox2.Text = this.textBox4.Text;
            ud.textBox3.Text = dataGridView1.CurrentRow.Cells["DonGiaNhap"].Value.ToString();
            ud.textBox4.Text = dataGridView1.CurrentRow.Cells["DonGiaBan"].Value.ToString();
            ud.Show();
        }

        private void OpenAddVTPTWin(object sender, EventArgs e)
        {
            AddVTPT a = new AddVTPT();
            a.Show();
        }

        private void LoadDSVTPT(object sender, EventArgs e)
        {
            dataGridView1.DataSource = ImportOrderService.Instance.GetPhuTungFull().ListPhuTung;
        }



        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

            textBox3.Text = row.Cells["MaVTPT"]?.Value.ToString();
            textBox4.Text = row.Cells["TenVTPT"]?.Value.ToString();
            textBox2.Text = row.Cells["DonGiaBan"]?.Value.ToString();
            textBox1.Text = row.Cells["SoLuongTon"]?.Value.ToString();
        }

        private void SearchVTPT(object sender, EventArgs e)
        {
            int type = 0;
            string searchText = textBox8.Text;
            if(string.IsNullOrWhiteSpace(searchText))
            {
                MessageBox.Show("Thông tin tìm kiếm không được bỏ trống", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                return;
            }

            if (radioButton1.Checked)
                type = 1;
            else if (radioButton2.Checked)
                type = 2;

            if(type == 0)
            {
                MessageBox.Show("Vui lòng chọn kiểu tìm kiếm phụ tùng", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                return;
            }

            var result = ImportOrderService.Instance.SearchPhuTung(searchText, type);
            if(result.Success)
            {
                dataGridView1.DataSource = result.ListPhuTung;
            }
            else
            {
                MessageBox.Show(result.ErrorMessage);
            }
        }

        private void DeleteVTPT(object sender, EventArgs e)
        {
            if(string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Vui lòng chọn vật tư phụ tùng cần xóa", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                return;
            }

            string mapt = textBox3.Text;
            DialogResult result1 = MessageBox.Show("Bạn có muốn xóa phụ tùng này không", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if(result1 == DialogResult.OK)
            {
                var result = ImportOrderService.Instance.DeletePhuTung(mapt);
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

        private void ExportFile(object sender, EventArgs e)
        {
            // Không có dữ liệu để xuất
            if (dataGridView1.Rows.Count == 0 ||
                (dataGridView1.Rows.Count == 1 && dataGridView1.Rows[0].IsNewRow))
            {
                MessageBox.Show(
                    "Không có dữ liệu để xuất",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "Excel Files|*.xlsx";
                sfd.Title = "Xuất file Excel";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    ExportDataGridViewToExcel(dataGridView1, sfd.FileName);
                    MessageBox.Show(
                        "Xuất Excel thành công",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                }
            }
        }


        private void ExportDataGridViewToExcel(DataGridView dgv, string filePath)
        {
            using (XLWorkbook wb = new XLWorkbook())
            {
                var ws = wb.Worksheets.Add("Danh sách vật tư phụ tùng");

                // Header
                for (int i = 0; i < dgv.Columns.Count; i++)
                {
                    ws.Cell(1, i + 1).Value = dgv.Columns[i].HeaderText;
                    ws.Cell(1, i + 1).Style.Font.Bold = true;
                }

                // Data
                for (int i = 0; i < dgv.Rows.Count; i++)
                {
                    for (int j = 0; j < dgv.Columns.Count; j++)
                    {
                        ws.Cell(i + 2, j + 1).Value =
                            dgv.Rows[i].Cells[j].Value?.ToString();
                    }
                }

                ws.Columns().AdjustToContents();
                wb.SaveAs(filePath);
            }
        }
    }
}
