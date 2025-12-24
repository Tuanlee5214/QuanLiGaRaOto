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
    public partial class ChangePass : Form
    {
        public ChangePass()
        {
            InitializeComponent();
        }

        private void ExitCB(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ChangePassword(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text)
               || string.IsNullOrWhiteSpace(textBox3.Text))
            {
                MessageBox.Show("Vui lòng nhập thông tin đầy đủ", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                return;
            }

            var oldPass = UserSession.CurrentUser.MatKhauBam;
            if(PasswordHasher.Hash(textBox1.Text.Trim()) != oldPass)
            {
                MessageBox.Show("Mật khẩu cũ không đúng", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                return;
            }

            var newPass = textBox2.Text.Trim();
            if(PasswordHasher.Hash(newPass) == oldPass)
            {
                MessageBox.Show("Mật khẩu mới không được giống mật khẩu cũ", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                return;
            }

            var verifyPass = textBox3.Text.Trim();
            if(PasswordHasher.Hash(verifyPass) != PasswordHasher.Hash(newPass))
            {
                MessageBox.Show("Xác nhận mật khẩu không đúng với mật khẩu mới", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                return;
            }

            var result = UserService.Instance.UpdatePass(PasswordHasher.Hash(newPass));
            if(result.Success)
            {
                MessageBox.Show(result.SuccesMessage);
                this.Close();
            }
            else
            {
                MessageBox.Show(result.ErrorMessage);
                this.Close();
            }

        }

        private void CheckOldPassDisplay(object sender, EventArgs e)
        {
            textBox1.UseSystemPasswordChar = !checkBox1.Checked;
        }

        private void CheckNewPassChange(object sender, EventArgs e)
        {
            textBox2.UseSystemPasswordChar = !checkBox2.Checked;
        }

        private void CheckVerifyPassChange(object sender, EventArgs e)
        {
            textBox3.UseSystemPasswordChar = !checkBox3.Checked;
        }
    }
}
