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
    public partial class TiLeLaiWindow : Form
    {
        public TiLeLaiWindow()
        {
            InitializeComponent();
            this.numericUpDown1.Value = (decimal)RuleService.Instance.GetTiLeLai();
        }

        private void CloseTiLeWin(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ActivateForm(object sender, EventArgs e)
        {
            numericUpDown1.Value = (decimal)RuleService.Instance.GetTiLeLai();

        }

        private void UpdateTiLeLai(object sender, EventArgs e)
        {
            double tilelai = (double)numericUpDown1.Value;
            var result = RuleService.Instance.UpdateTiLeLai(tilelai);
            if (result.Success)
            {
                MessageBox.Show(result.SuccesMessage);
            }
            else
            {
                MessageBox.Show(result.ErrorMessage);
            }
        }
    }
}
