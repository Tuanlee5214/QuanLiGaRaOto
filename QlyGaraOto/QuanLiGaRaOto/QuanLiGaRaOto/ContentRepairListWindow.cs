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
    public partial class ContentRepairListWindow : Form
    {
        public ContentRepairListWindow()
        {
            InitializeComponent();
        }

        private void CloseContentRepairWin(object sender, EventArgs e)
        {
            this.Close();
        }

        private void OpenUpdateContentRepair(object sender, EventArgs e)
        {
            if (!PermissionService.Sua())
            {
                MessageBox.Show("Bạn không có quyền truy cập chức năng này", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(textBox3.Text))
            {
                MessageBox.Show("Vui lòng chọn nội dung cần cập nhật", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                return;
            }
            UpdateContentRepair ud = new UpdateContentRepair();
            ud.textBox1.Text = textBox3.Text;
            ud.textBox2.Text = textBox4.Text;
            ud.textBox3.Text = textBox2.Text;
            ud.Show();
        }

        private void OpenAddContentRepair(object sender, EventArgs e)
        {
            if (!PermissionService.Them())
            {
                MessageBox.Show("Bạn không có quyền truy cập chức năng này", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                return;
            }
            AddConRepair ct = new AddConRepair();
            ct.Show();
        }

        private void LoadContent(object sender, EventArgs e)
        {
            dataGridView1.DataSource = ContentService.Instance.GetContentFull().ListContent;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
            textBox3.Text = row.Cells["MaNoiDung"]?.Value.ToString();
            textBox4.Text = row.Cells["MoTa"]?.Value.ToString();
            textBox2.Text = row.Cells["TienCong"]?.Value.ToString();
        }

        private void SearchContent(object sender, EventArgs e)
        {
            int type = 0;
            string searchText = textBox8.Text;
            if (string.IsNullOrWhiteSpace(searchText))
            {
                MessageBox.Show("Thông tin tìm kiếm không được bỏ trống", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                return;
            }

            if (radioButton1.Checked)
                type = 1;
            else if (radioButton2.Checked)
                type = 2;

            if (type == 0)
            {
                MessageBox.Show("Vui lòng chọn kiểu tìm kiếm nội dung", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                return;
            }

            var result = ContentService.Instance.SearchContent(searchText, type);
            if (result.Success)
            {
                dataGridView1.DataSource = result.ListContent;
            }
            else
            {
                MessageBox.Show(result.ErrorMessage);
            }
        }

        private void DeleteContent(object sender, EventArgs e)
        {
            if (!PermissionService.Xoa())
            {
                MessageBox.Show("Bạn không có quyền truy cập chức năng này", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(textBox3.Text))
            {
                MessageBox.Show("Vui lòng chọn nội dung cần xóa", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                return;
            }

            string mand = textBox3.Text;
            DialogResult result1 = MessageBox.Show("Bạn có muốn xóa nội dung này không", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (result1 == DialogResult.OK)
            {
                var result = ContentService.Instance.DeleteContent(mand);
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
            if (!PermissionService.XuatFile())
            {
                MessageBox.Show("Bạn không có quyền truy cập chức năng này", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                return;
            }
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
                var ws = wb.Worksheets.Add("Danh sách nội dung sửa chữa");

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
