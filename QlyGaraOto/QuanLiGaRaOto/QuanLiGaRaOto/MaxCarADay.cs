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
    public partial class MaxCarADay : Form
    {
        public MaxCarADay()
        {
            InitializeComponent();
            numericUpDown1.Value = RuleService.Instance.GetMaxCarOfDay();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void CloseMaxCarADay(object sender, EventArgs e)
        {
            this.Close();
        }



        private void UpdateInfoMaxCar(object sender, EventArgs e)
        {
            int maxCar = (int)numericUpDown1.Value;
            var result = RuleService.Instance.UpdateMaxCarOfDay(maxCar);
            if(result.Success)
            {
                MessageBox.Show(result.SuccesMessage);
            }
            else
            {
                MessageBox.Show(result.ErrorMessage);
            }
        }

        private void ActivateForm(object sender, EventArgs e)
        {
            numericUpDown1.Value = RuleService.Instance.GetMaxCarOfDay();
        }
    }
}
