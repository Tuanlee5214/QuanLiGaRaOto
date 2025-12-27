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
                var ws = wb.Worksheets.Add("Danh sách phiếu sửa chữa");

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
