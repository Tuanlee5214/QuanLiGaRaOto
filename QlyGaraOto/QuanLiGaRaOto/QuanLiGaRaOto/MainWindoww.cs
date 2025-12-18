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
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void báoCáoThốngKếToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void thôngTinTàiKhoảnHiệnToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void MainWindoww_Load(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

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
    }
}
