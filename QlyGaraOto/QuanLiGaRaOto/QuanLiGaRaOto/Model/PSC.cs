using DocumentFormat.OpenXml.Office2013.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiGaRaOto.Model
{
    public class PSC
    {
        public string MaPhieuSC { get; set; }
        public string BienSo { get; set; }
        public DateTime NgaySuaChua { get; set; }
        public Decimal TongTien { get; set; }
    }

    public class CT_PSC
    {
        public string MoTa { get; set; }
        public string MaPhuTung { get; set; }
        public string TenPhuTung { get; set; }
        public decimal DonGia { get; set; }
        public int SoLuong { get; set; }
        public decimal TienCong { get; set; }
        public decimal ThanhTien { get; set; }
    }
}
