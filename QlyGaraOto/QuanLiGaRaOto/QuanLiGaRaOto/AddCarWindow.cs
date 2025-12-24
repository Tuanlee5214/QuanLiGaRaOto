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
    public partial class AddCarWindow : Form
    {
        public AddCarWindow()
        {
            InitializeComponent();
            comboBox1.DataSource = CarService.Instance.GetTypeOfCarInfo();
            comboBox1.DisplayMember = "HieuXe";
            comboBox1.ValueMember = "HieuXe";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void CloseAddCar(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AddCarIntoSystem(object sender, EventArgs e)
        {

        }
    }
}
