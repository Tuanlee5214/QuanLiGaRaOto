using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiGaRaOto.Model
{
    public class Report
    {
        public string MaPhuTung { get; set; }
        public string TenPhuTung { get; set; }
        public int TonDau { get; set; }
        public int TonCuoi { get; set; }
        public int PhatSinh { get; set; }
    }

    public class BCDoanhThu
    {
        public string HieuXe { get; set; }
        public decimal TongTien { get; set; }
        public int SoLuotSua { get; set; }
        public decimal TiLe { get; set; }
    }
}
