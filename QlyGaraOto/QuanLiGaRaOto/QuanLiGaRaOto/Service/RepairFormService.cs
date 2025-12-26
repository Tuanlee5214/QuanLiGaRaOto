using Microsoft.SqlServer.Server;
using QuanLiGaRaOto.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace QuanLiGaRaOto.Service
{
    public class RepairFormService
    {
        private readonly DatabaseConnection _db = new DatabaseConnection();
        private static RepairFormService instance;
        public static RepairFormService Instance
        {
            get
            {
                if (instance == null)
                    instance = new RepairFormService();
                return instance;
            }
            set { instance = value; }
        }

        public string GetMaPSC()
        {
            using (var conn = _db.GetConnection())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "SELECT MAX(MaPhieuSC) FROM PHIEUSUACHUA";

                object result = cmd.ExecuteScalar();

                int nextNumber = 1; 

                if (result != null && result != DBNull.Value)
                {
                    string lastCode = result.ToString(); 
                    int number = int.Parse(lastCode.Substring(2));
                    nextNumber = number + 1;
                }

                return "SC" + nextNumber.ToString("D5");
            }
        }

        public DataTable GetBSX()
        {

            using (var conn = _db.GetConnection())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "SELECT BienSo FROM XE";
                DataTable dt = new DataTable();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                return dt;
            }
        }

        public DataTable GetNoiDungSC()
        {

            using (var conn = _db.GetConnection())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "SELECT MaNoiDung, MoTa FROM NOIDUNGSUACHUA";
                DataTable dt = new DataTable();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                return dt;
            }
        }

        public DataTable GetPhuTung()
        {

            using (var conn = _db.GetConnection())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "SELECT MaPhuTung, TenPhuTung FROM PHUTUNG";
                DataTable dt = new DataTable();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                return dt;
            }
        }

        public decimal GetTienCong(string matc)
        {
            using (var conn = _db.GetConnection())
            using (var cmd = conn.CreateCommand())
            {
                if (string.IsNullOrWhiteSpace(matc)) return 0;
                cmd.CommandText = "SELECT TienCong FROM NOIDUNGSUACHUA WHERE MaNoiDung = @matc";
                cmd.Parameters.Add("@matc", SqlDbType.Char, 7).Value = matc.Trim();

                var reader = cmd.ExecuteScalar();
                if (reader != null && reader != DBNull.Value)
                {
                    return (decimal)reader;
                }
                else
                    return 0;
            }
        }

        public PhuTung GetPhuTungInfo(string mapt)
        {
            using (var conn = _db.GetConnection())
            using (var cmd = conn.CreateCommand())
            {
                if (string.IsNullOrWhiteSpace(mapt)) return new PhuTung { SoLuongTon = 0, DonGiaBan = 0 }; 
                cmd.CommandText = "SELECT SoLuongTon, DonGiaBan FROM PHUTUNG WHERE MaPhuTung = @mapt";
                cmd.Parameters.Add("@mapt", SqlDbType.Char, 7).Value = mapt.Trim();

                var reader = cmd.ExecuteReader();
                if(reader.Read())
                {
                    return new PhuTung { SoLuongTon = (int)reader["SoLuongTon"], DonGiaBan = (decimal)reader["DonGiaBan"] };
                }
                else
                    return new PhuTung { SoLuongTon = 0, DonGiaBan = 0 };
            }
        }

        public Car GetCarInfo(string bienso)
        {
            using (var conn = _db.GetConnection())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "SELECT TongNo FROM XE WHERE BienSo = @bs";
                cmd.Parameters.Add("@bs", SqlDbType.Char, 20).Value = bienso.Trim();

                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return new Car { TongNo = (decimal)reader["TongNo"] };
                }
                else
                    return new Car { TongNo = 0};
            }
        }

        public InsertOrUpdateResult InsertIntoPSC(string mapsc, string bienso, DateTime ngaysc, decimal tongtien)
        {
            try
            {
                using (var conn = _db.GetConnection())
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO PHIEUSUACHUA (MaPhieuSC, BienSo, NgaySuaChua, TongTien) " +
                                      "VALUES(@mapsc, @bs, @ngaysc, @tongtien)";
                    cmd.Parameters.Add("@mapsc", SqlDbType.Char, 7).Value = mapsc;
                    cmd.Parameters.Add("@bs", SqlDbType.Char, 20).Value = bienso;
                    cmd.Parameters.Add("@ngaysc", SqlDbType.DateTime).Value = ngaysc;
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

        public InsertOrUpdateResult InsertIntoCT_PSC(string mapsc, string mapt, string mand, int sl, decimal dongia, decimal tt, decimal tiencong)
        {
            try
            {
                using (var conn = _db.GetConnection())
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText =
                @"INSERT INTO CT_PHIEUSUACHUA
                  (MaPhieuSC, MaPhuTung, MaNoiDung, SoLuong, DonGia, ThanhTien, TienCong)
                  VALUES
                  (@mapsc, @mapt, @mand, @sl, @dongia, @tt, @tiencong)";

                    cmd.Parameters.Add("@mapsc", SqlDbType.Char, 7).Value = mapsc;
                    cmd.Parameters.Add("@mapt", SqlDbType.Char, 7).Value = mapt;
                    cmd.Parameters.Add("@mand", SqlDbType.Char, 7).Value = mand;
                    cmd.Parameters.Add("@sl", SqlDbType.Int).Value = sl;
                    cmd.Parameters.Add("@dongia", SqlDbType.Money).Value = dongia;
                    cmd.Parameters.Add("@tt", SqlDbType.Money).Value = tt;
                    cmd.Parameters.Add("@tiencong", SqlDbType.Money).Value = tiencong;

                    int row = cmd.ExecuteNonQuery();

                    return new InsertOrUpdateResult
                    {
                        Success = row > 0,
                        SuccessMessage = row > 0 ? "Thêm chi tiết sửa chữa thành công" : null,
                        ErrorMessage = row > 0 ? null : "Thêm chi tiết sửa chữa thất bại"
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

        public InsertOrUpdateResult UpdateTienNoMoi(string bienso, decimal tienmoi)
        {
            try
            {
                using (var conn = _db.GetConnection())
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "UPDATE XE SET TongNo = @tienmoi WHERE BienSo = @bs";


                    cmd.Parameters.Add("@bs", SqlDbType.Char, 20).Value = bienso;
                    cmd.Parameters.Add("@tienmoi", SqlDbType.Money).Value = tienmoi;
                    

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

        public InsertOrUpdateResult UpdateSoLuong(string mavt, decimal slmoi)
        {
            try
            {
                using (var conn = _db.GetConnection())
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "UPDATE PHUTUNG SET SoLuongTon = @slmoi WHERE MaPhuTung = @mavt";


                    cmd.Parameters.Add("@mavt", SqlDbType.Char, 7).Value = mavt;
                    cmd.Parameters.Add("@slmoi", SqlDbType.Money).Value = slmoi;


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
