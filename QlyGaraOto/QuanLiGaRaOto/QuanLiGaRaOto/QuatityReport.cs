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
    public partial class QuatityReport : Form
    {
        public QuatityReport()
        {
            InitializeComponent();
        }

        private void CloseQuanReport(object sender, EventArgs e)
        {
            this.Close();
        }

        private void OpenVTPTBC(object sender, EventArgs e)
        {
            int thang = Convert.ToInt32(numericUpDown1.Value);
            int nam = Convert.ToInt32(numericUpDown2.Value);
            dataGridView1.DataSource = ReportService.Instance.GetBCTonKho(thang, nam);

        }

        private void ExportFile(object sender, EventArgs e)
        {
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
