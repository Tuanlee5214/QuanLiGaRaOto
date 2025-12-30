using ClosedXML.Excel;
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
            if (!PermissionService.Them())
            {
                MessageBox.Show("Bạn không có quyền truy cập chức năng này", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                return;
            }
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
            if (!PermissionService.Xoa())
            {
                MessageBox.Show("Bạn không có quyền truy cập chức năng này", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                return;
            }
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
                var ws = wb.Worksheets.Add("Danh sách hiệu xe");

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
