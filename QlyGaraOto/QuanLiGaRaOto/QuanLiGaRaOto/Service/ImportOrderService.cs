using QuanLiGaRaOto.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiGaRaOto.Service
{
    public class ImportOrderService
    {
        private readonly DatabaseConnection _db = new DatabaseConnection();

        private static ImportOrderService instance;
        public static ImportOrderService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ImportOrderService();
                }
                return instance;
            }
            set
            {
                instance = value;
            }
        }

        public string GetMaPhieuNhap()
        {
            using (var conn = _db.GetConnection())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "SELECT MAX(MaPhieuNhap) FROM PHIEUNHAPPHUTUNG";

                object result = cmd.ExecuteScalar();

                int nextNumber = 1;

                if (result != null && result != DBNull.Value)
                {
                    string lastCode = result.ToString();
                    int number = int.Parse(lastCode.Substring(2));
                    nextNumber = number + 1;
                }

                return "PN" + nextNumber.ToString("D5");
            }
        }

        public InsertOrUpdateResult InsertIntoPhieuNhap(string mapn, DateTime ngaynhap, decimal tongtien)
        {
            try
            {
                using (var conn = _db.GetConnection())
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO PHIEUNHAPPHUTUNG (MaPhieuNhap, NgayNhap, TongTien) " +
                                      "VALUES(@mapn, @ngaynhap, @tongtien)";
                    cmd.Parameters.Add("@mapn", SqlDbType.Char, 7).Value = mapn;
                    cmd.Parameters.Add("@ngaynhap", SqlDbType.DateTime).Value = ngaynhap;
                    cmd.Parameters.Add("@tongtien", SqlDbType.Money).Value = tongtien;


                    int row = cmd.ExecuteNonQuery();
                    if (row != 0)
                    {
                        return new InsertOrUpdateResult
                        {
                            Success = true,
                            SuccessMessage = "Thêm thông tin thành công"
                        };
                    }
                    else
                    {
                        return new InsertOrUpdateResult
                        {
                            Success = false,
                            ErrorMessage = "Thêm thông tin thất bại"
                        };
                    }

                }

            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error: " + ex.Message);
                return new InsertOrUpdateResult
                {
                    Success = false,
                    ErrorMessage = "Lỗi kết nối tới máy chủ"
                };
            }
        }

        public InsertOrUpdateResult InsertIntoCT_PhieuNhap(string mapn, string mapt, int sl, decimal dongia, decimal tt)
        {
            try
            {
                using (var conn = _db.GetConnection())
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText =
                @"INSERT INTO CT_PHIEUNHAPPHUTUNG
                  (MaPhieuNhap, MaPhuTung, SoLuongNhap, DonGiaNhap, ThanhTien)
                  VALUES
                  (@mapn, @mapt, @sl, @dongia, @tt)";

                    cmd.Parameters.Add("@mapn", SqlDbType.Char, 7).Value = mapn;
                    cmd.Parameters.Add("@mapt", SqlDbType.Char, 7).Value = mapt;
                    cmd.Parameters.Add("@sl", SqlDbType.Int).Value = sl;
                    cmd.Parameters.Add("@dongia", SqlDbType.Money).Value = dongia;
                    cmd.Parameters.Add("@tt", SqlDbType.Money).Value = tt;

                    int row = cmd.ExecuteNonQuery();

                    return new InsertOrUpdateResult
                    {
                        Success = row > 0,
                        SuccessMessage = row > 0 ? "Thêm ct phiếu nhập thành công" : null,
                        ErrorMessage = row > 0 ? null : "Thêm ct phiếu nhập thất bại"
                    };
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error: " + ex.Message);
                return new InsertOrUpdateResult
                {
                    Success = false,
                    ErrorMessage = "Lỗi kết nối tới máy chủ"
                };
            }
        }

        public InsertOrUpdateResult UpdateDonGiaPT(string mapt, decimal dgnhapmoi, decimal dgbanmoi)
        {
            try
            {
                using (var conn = _db.GetConnection())
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "UPDATE PHUTUNG SET DonGiaNhap = @dgn, DonGiaBan = @dgb WHERE MaPhuTung = @mapt";


                    cmd.Parameters.Add("@mapt", SqlDbType.Char, 7).Value = mapt;
                    cmd.Parameters.Add("@dgn", SqlDbType.Money).Value = dgnhapmoi;
                    cmd.Parameters.Add("@dgb", SqlDbType.Money).Value = dgbanmoi;



                    int row = cmd.ExecuteNonQuery();

                    return new InsertOrUpdateResult
                    {
                        Success = row > 0,
                        SuccessMessage = row > 0 ? "Sửa chữa thành công" : null,
                        ErrorMessage = row > 0 ? null : "Sửa chữa thất bại"
                    };
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error: " + ex.Message);
                return new InsertOrUpdateResult
                {
                    Success = false,
                    ErrorMessage = "Lỗi kết nối tới máy chủ"
                };
            }
        }
    }
}
