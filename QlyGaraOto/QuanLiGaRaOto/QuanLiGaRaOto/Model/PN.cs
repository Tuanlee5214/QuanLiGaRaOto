using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiGaRaOto.Model
{
    public class PN
    {
        public string MaPhieuNhap { get; set; }
        public DateTime NgayNhap { get; set; }
        public Decimal TongTien { get; set; }
    }

    public class CT_PN
    {
        public string TenPhuTung { get; set; }
        public string MaPhuTung { get; set; }
        public int SoLuongNhap { get; set; }
        public decimal DonGiaNhap { get; set; }
        public decimal ThanhTien { get; set; }
    }
}
