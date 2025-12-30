using QuanLiGaRaOto.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLiGaRaOto.Service
{
    public class ReportService
    {
        private readonly DatabaseConnection _db = new DatabaseConnection();

        private static ReportService instance;
        public static ReportService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ReportService();
                }
                return instance;
            }
            set
            {
                instance = value;
            }
        }

        public BindingList<Report> GetBCTonKho(int thang, int nam)
        {
            using (var conn = _db.GetConnection())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "WITH T AS (SELECT CT.MaPhuTung, PT.TenPhuTung, COUNT(CT.SoLuong) AS SoLuong" +
                    " FROM CT_PHIEUSUACHUA CT JOIN PHUTUNG PT ON PT.MaPhuTung = CT.MaPhuTung" +
                    " JOIN PHIEUSUACHUA PSC ON PSC.MaPhieuSC = CT.MaPhieuSC" +
                    " WHERE MONTH(PSC.NgaySuaChua) = @thang AND YEAR(PSC.NgaySuaChua) = @nam" +
                    " GROUP BY CT.MaPhuTung, PT.TenPhuTung)" +
                    " SELECT PT.MaPhuTung, PT.TenPhuTung,(ISNULL(T.SoLuong, 0) + pt.SoLuongTon) AS TonDau ,ISNULL(T.SoLuong, 0) AS PhatSinh, PT.SoLuongTon AS TonCuoi" +
                    " FROM PHUTUNG PT LEFT JOIN T ON T.MaPhuTung = PT.MaPhuTung";

                cmd.Parameters.Add("@thang", System.Data.SqlDbType.Int).Value = thang;
                cmd.Parameters.Add("@nam", System.Data.SqlDbType.Int).Value = nam;
                BindingList<Report> list = new BindingList<Report>();
                using (var read = cmd.ExecuteReader())
                {
                    while (read.Read())
                    {
                        list.Add(new Report
                        {
                            MaPhuTung = read["MaPhuTung"].ToString(),
                            TenPhuTung = read["TenPhuTung"].ToString(),
                            TonDau = Convert.ToInt32(read["TonDau"]),
                            TonCuoi = Convert.ToInt32(read["TonCuoi"]),
                            PhatSinh = Convert.ToInt32(read["PhatSinh"])
                        }
                        );
                    }
                    return list;
                }
            }

        }

        public BindingList<BCDoanhThu> GetBCDoanhThu(int thang, int nam)
        {
            using (var conn = _db.GetConnection())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "WITH DoanhThuTheoHieuXe AS " +
                    "(SELECT X.HieuXe, SUM(PTT.SoTienThu) AS TongTien " +
                    "FROM PHIEUTHUTIEN PTT JOIN XE X ON X.BienSo = PTT.BienSo " +
                    "WHERE MONTH(PTT.NgayThu) = @thang AND YEAR(PTT.NgayThu) = @nam " +
                    "GROUP BY X.HieuXe), " +
                    "LuotSua AS " +
                    "(SELECT X.HieuXe, COUNT(DISTINCT PSC.MaPhieuSC) AS SoLuotSua " +
                    "FROM PHIEUSUACHUA PSC JOIN XE X ON X.BienSo = PSC.BienSo " +
                    "WHERE MONTH(PSC.NgaySuaChua) = @thang AND YEAR(PSC.NgaySuaChua) = @nam " +
                    "GROUP BY X.HieuXe), " +
                    "TongDoanhThu AS " +
                    "(SELECT ISNULL(SUM(TongTien), 0) AS TongTienThang " +
                    "FROM DoanhThuTheoHieuXe) " +
                    "SELECT HX.HieuXe, ISNULL(DT.TongTien, 0) AS TongTien, ISNULL(LS.SoLuotSua, 0) AS SoLuotSua, " +
                    "CASE WHEN TDT.TongTienThang = 0 THEN 0 " +
                    "ELSE ROUND(ISNULL(DT.TongTien,0) * 100.0 / TDT.TongTienThang, 2) " +
                    "END AS TiLePhanTram " +
                    "FROM HIEUXE HX LEFT JOIN DoanhThuTheoHieuXe DT ON DT.HieuXe = HX.HieuXe LEFT JOIN LuotSua LS ON LS.HieuXe = HX.HieuXe " +
                    "CROSS JOIN TongDoanhThu TDT;";

                cmd.Parameters.Add("@thang", System.Data.SqlDbType.Int).Value = thang;
                cmd.Parameters.Add("@nam", System.Data.SqlDbType.Int).Value = nam;
                BindingList<BCDoanhThu> list = new BindingList<BCDoanhThu>();
                using (var read = cmd.ExecuteReader())
                {
                    while (read.Read())
                    {
                        list.Add(new BCDoanhThu
                        {
                            HieuXe = read["HieuXe"].ToString(),
                            TongTien = Math.Round(Convert.ToDecimal(read["TongTien"])),
                            SoLuotSua = Convert.ToInt32(read["SoLuotSua"]),
                            TiLe = Math.Round(Convert.ToDecimal(read["TiLePhanTram"]), 2)
                        }
                        );


                    }
                    if (list.Count == 0)
                    {
                        return list;
                    }
                    return list;
                }
            }

        }

        public decimal GetTongTienThang(int thang, int nam)
        {
            using (var conn = _db.GetConnection())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "SELECT ISNULL(SUM(SoTienThu), 0) AS TongTien FROM PHIEUTHUTIEN " +
                    "WHERE MONTH(NgayThu) = @thang AND YEAR(NgayThu) = @nam";

                cmd.Parameters.Add("@thang", System.Data.SqlDbType.Int).Value = thang;
                cmd.Parameters.Add("@nam", System.Data.SqlDbType.Int).Value = nam;

                var reader = cmd.ExecuteScalar();
                decimal tongtien = 0;
                tongtien = Math.Round(Convert.ToDecimal(reader));
                return tongtien;
            }

        }
    }


}
