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
    public partial class MainWindoww : Form
    {
        
        public MainWindoww()
        {
            InitializeComponent();
            this.textBox9.Text = UserSession.CurrentUser.TenNguoiDung;
            var fday = new DateTime(2000, 1, 1, 0, 0, 0);
            var sday = new DateTime(2100, 1, 1, 0, 0, 0);
            dataGridView1.DataSource = CarService.Instance.GetCars(fday, sday).ListCar;
        }

        

        private void OpenUserInfo_Click(object sender, EventArgs e)
        {
            UserInfoWindow user = new UserInfoWindow();
            user.Show();
        }

        private void RevenueReport_Click(object sender, EventArgs e)
        {
            RevenueReportWindow rvn = new RevenueReportWindow();
            rvn.Show();
        }

        private void LogOut_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn đăng xuất!", "Xác nhận", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (result == DialogResult.OK)
            {
                this.Close();
            }
        }

        private void OpenQuantityReport(object sender, EventArgs e)
        {
            QuatityReport qr = new QuatityReport();
            qr.Show();
        }

        private void OpenSoftWareInfo(object sender, EventArgs e)
        {
            SoftWareInfoWindow sw = new SoftWareInfoWindow();
            sw.Show();
        }

        private void OpenContactWindow(object sender, EventArgs e)
        {
            ContactWindow ct = new ContactWindow();
            ct.Show();
        }

        private void OpenAddCar(object sender, EventArgs e)
        {
            AddCarWindow ac = new AddCarWindow();
            ac.Show();
        }

        private void OpenReceiptForm(object sender, EventArgs e)
        {
            ReceiptWindow rc = new ReceiptWindow();
            rc.Show();
        }

        private void OpenRepairForm(object sender, EventArgs e)
        {
            RepairForm rp = new RepairForm();
            rp.Show();
        }

        private void OpenImportOrder(object sender, EventArgs e)
        {
            ImportOrder ip = new ImportOrder();
            ip.Show();
        }

        private void OpenVTPTListWindow(object sender, EventArgs e)
        {
            VTPTListWindow vt = new VTPTListWindow();
            vt.Show();
        }

        private void OpenContentRepairWin(object sender, EventArgs e)
        {
            ContentRepairListWindow cr = new ContentRepairListWindow();
            cr.Show();
        }

        private void OpenHieuXeWin(object sender, EventArgs e)
        {
            HieuXeWindow hx = new HieuXeWindow();
            hx.Show();
        }

        private void OpenMaxCarADay(object sender, EventArgs e)
        {
            MaxCarADay mc = new MaxCarADay();
            mc.Show();
        }

        private void OpenTiLeLaiWin(object sender, EventArgs e)
        {
            TiLeLaiWindow tl = new TiLeLaiWindow();
            tl.Show();
        }

        private void OpenUpdateCar(object sender, EventArgs e)
        {
            string a;
            a = this.textBox2.Text;
            UpdateCar ud = new UpdateCar(a);
            ud.textBox1.Text = this.textBox1.Text;
            ud.textBox2.Text = this.textBox4.Text;
            ud.textBox3.Text = this.textBox5.Text;
            ud.textBox4.Text = this.textBox3.Text.Trim();
            ud.textBox5.Text = this.textBox6.Text;
            if (string.IsNullOrWhiteSpace(ud.textBox1.Text) || string.IsNullOrWhiteSpace(ud.textBox2.Text) ||
               string.IsNullOrWhiteSpace(ud.textBox3.Text) || string.IsNullOrWhiteSpace(ud.textBox4.Text) ||
               string.IsNullOrWhiteSpace(ud.textBox5.Text))
            {
                MessageBox.Show("Vui lòng chọn thông tin xe", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                return;
            }

            ud.Show();
        }

        private void OpenListPSC(object sender, EventArgs e)
        {
            ListRepairForm ip = new ListRepairForm();
            ip.Show();
        }

        private void OpenListImportOrder(object sender, EventArgs e)
        {
            ListImportOrder l = new ListImportOrder();
            l.Show();
        }

        private void OpenListPTT(object sender, EventArgs e)
        {
            ListPTT L = new ListPTT();
            L.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void ActivateForm(object sender, EventArgs e)
        {
            var fday = new DateTime(2000, 1, 1, 0, 0, 0);
            var sday = new DateTime(2100, 1, 1, 0, 0, 0);
            dataGridView1.DataSource = CarService.Instance.GetCars(fday, sday).ListCar;
        }

        private void GetCars(object sender, EventArgs e)
        {
            var fday = new DateTime(2000, 1, 1, 0, 0, 0);
            var sday = new DateTime(2100, 1, 1, 0, 0, 0);
            dataGridView1.DataSource = CarService.Instance.GetCars(fday, sday).ListCar;
        }

        private void GetCarInADay(object sender, EventArgs e)
        {
            var fday = DateTime.Today;
            var sday = DateTime.Today.AddDays(1);
            dataGridView1.DataSource = CarService.Instance.GetCars(fday, sday).ListCar;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return; 

            DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

            textBox1.Text = row.Cells["BienSo"].Value?.ToString();
            textBox2.Text = row.Cells["HieuXe"].Value?.ToString();
            textBox4.Text = row.Cells["TenChuXe"].Value?.ToString();
            textBox5.Text = row.Cells["DiaChi"].Value?.ToString();
            textBox3.Text = row.Cells["SDT"].Value?.ToString();
            textBox6.Text = row.Cells["Email"].Value?.ToString();

            if (row.Cells["NgayTiepNhan"].Value != null)
                dateTimePicker1.Value = (DateTime)row.Cells["NgayTiepNhan"].Value;

            if (row.Cells["TongNo"].Value != null)
                textBox7.Text = row.Cells["TongNo"].Value.ToString();
        }
    }
}
