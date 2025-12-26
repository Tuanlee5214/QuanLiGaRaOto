using QuanLiGaRaOto.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        public string GetMaPhuTung()
        {
            using (var conn = _db.GetConnection())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "SELECT MAX(MaPhuTung) FROM PHUTUNG";

                object result = cmd.ExecuteScalar();

                int nextNumber = 1;

                if (result != null && result != DBNull.Value)
                {
                    string lastCode = result.ToString();
                    int number = int.Parse(lastCode.Substring(2));
                    nextNumber = number + 1;
                }

                return "PT" + nextNumber.ToString("D5");
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

        public InsertOrUpdateResult AddPhuTung(PhuTung item)
        {
            try
            {
                using (var conn = _db.GetConnection())
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO PHUTUNG (MaPhuTung, TenPhuTung, DonGiaNhap, DonGiaBan, SoLuongTon) " +
                                      "VALUES(@mapt, @ten, 0, 0, 0)";
                    cmd.Parameters.Add("@mapt", SqlDbType.Char, 7).Value = item.MaPhuTung;
                    cmd.Parameters.Add("@ten", SqlDbType.NVarChar, 50).Value = item.TenPhuTung;
   
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
                            ErrorMessage = "Biển số xe đã tồn tại"
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

        public InsertOrUpdateResult DeletePhuTung(string mapt)
        {
            try
            {
                using (var conn = _db.GetConnection())
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM PHUTUNG WHERE MaPhuTung = @mapt";
                    cmd.Parameters.Add("@mapt", SqlDbType.Char, 7).Value = mapt;


                    int row = cmd.ExecuteNonQuery();
                    if (row != 0)
                    {
                        return new InsertOrUpdateResult
                        {
                            Success = true,
                            SuccessMessage = "Xóa thông tin thành công"
                        };
                    }
                    else
                    {
                        return new InsertOrUpdateResult
                        {
                            Success = false,
                            ErrorMessage = "Xóa thông tin không thành công"
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

        public GetPhuTungResult GetPhuTungFull()
        {
            try
            {
                using (var conn = _db.GetConnection())
                using (var cmd = conn.CreateCommand())
                {

                    cmd.CommandText =
                        "SELECT MaPhuTung, TenPhuTung, DonGiaNhap, DonGiaBan, SoLuongTon " +
                        "FROM PHUTUNG";


                    var list = new BindingList<PhuTung>();

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new PhuTung
                            {
                                MaPhuTung = reader["MaPhuTung"].ToString(),
                                TenPhuTung = reader["TenPhuTung"].ToString(),
                                DonGiaNhap = Convert.ToInt32(reader["DonGiaNhap"]),
                                DonGiaBan = Convert.ToInt32(reader["DonGiaBan"]),
                                SoLuongTon = Convert.ToInt32(reader["SoLuongTon"])

                            });
                        }
                    }

                    if (list.Count == 0)
                    {
                        return new GetPhuTungResult
                        {
                            Success = true,
                            ListPhuTung = list
                        };
                    }

                    return new GetPhuTungResult
                    {
                        Success = true,
                        ListPhuTung = list
                    };
                }
            }
            catch (SqlException)
            {
                return new GetPhuTungResult
                {
                    Success = false,
                    ErrorMessage = "Lỗi kết nối tới server"
                };
            }
        }

        public GetPhuTungResult SearchPhuTung(string searchText, int type)
        {
            try
            {
                using (var conn = _db.GetConnection())
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText =
                        "SELECT MaPhuTung, TenPhuTung, DonGiaNhap, DonGiaBan, SoLuongTon " +
                        "FROM PHUTUNG ";

                    switch (type)
                    {
                        case 1: // Mã vật tư 
                            cmd.CommandText += "WHERE MaPhuTung LIKE @ma";
                            cmd.Parameters.Add("@ma", SqlDbType.VarChar, 20)
                                          .Value = "%" + searchText.Trim() + "%";
                            break;

                        case 2: // Tên vật tư
                            cmd.CommandText += "WHERE TenPhuTung LIKE @ten";
                            cmd.Parameters.Add("@ten", SqlDbType.NVarChar, 50)
                                          .Value = "%" + searchText.Trim() + "%";
                            break;

                        default:
                            return new GetPhuTungResult
                            {
                                Success = false,
                                ErrorMessage = "Loại tìm kiếm không hợp lệ"
                            };
                    }

                    var list = new BindingList<PhuTung>();

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new PhuTung
                            {
                                MaPhuTung = reader["MaPhuTung"].ToString(),
                                TenPhuTung = reader["TenPhuTung"].ToString(),
                                DonGiaNhap = Convert.ToInt32(reader["DonGiaNhap"]),
                                DonGiaBan = Convert.ToInt32(reader["DonGiaBan"]),
                                SoLuongTon = Convert.ToInt32(reader["SoLuongTon"])

                            });
                        }
                    }

                    return new GetPhuTungResult
                    {
                        Success = true,
                        ListPhuTung = list
                    };
                }
            }
            catch (SqlException)
            {
                return new GetPhuTungResult
                {
                    Success = false,
                    ErrorMessage = "Lỗi kết nối tới server"
                };
            }
        }

        public InsertOrUpdateResult UpdatePhuTung(PhuTung item)
        {
            try
            {
                using (var conn = _db.GetConnection())
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "UPDATE PHUTUNG SET TenPhuTung = @ten, DonGiaNhap = @dgn, DonGiaBan = @dgb WHERE MaPhuTung = @mapt";
                    cmd.Parameters.Add("@ten", SqlDbType.VarChar, 30).Value = item.TenPhuTung;
                    cmd.Parameters.Add("@dgn", SqlDbType.Money).Value = item.DonGiaNhap;
                    cmd.Parameters.Add("@dgb", SqlDbType.Money).Value = item.DonGiaBan;
                    cmd.Parameters.Add("mapt", SqlDbType.Char, 7).Value = item.MaPhuTung;

                    int row = cmd.ExecuteNonQuery();
                    if (row != 0)
                    {
                        return new InsertOrUpdateResult
                        {
                            Success = true,
                            SuccessMessage = "Cập nhật thông tin thành công"
                        };
                    }
                    else
                    {
                        return new InsertOrUpdateResult
                        {
                            Success = false,
                            ErrorMessage = "Cập nhật thông tin thất bại"
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
        public BindingList<PN> GetPN()
        {
            using (var conn = _db.GetConnection())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "SELECT MaPhieuNhap, NgayNhap, TongTien FROM PHIEUNHAPPHUTUNG";
                var list = new BindingList<PN>();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new PN
                        {
                            MaPhieuNhap = reader["MaPhieuNhap"].ToString(),
                            NgayNhap = Convert.ToDateTime(reader["NgayNhap"]),
                            TongTien = Convert.ToDecimal(reader["TongTien"])
                        });
                    }
                }
                return list;
            }
        }

        public BindingList<PN> SearchPN(string searchText, int type, int ngay, int thang, int nam)
        {
            using (var conn = _db.GetConnection())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "SELECT MaPhieuNhap, NgayNhap, TongTien FROM PHIEUNHAPPHUTUNG ";
                switch (type)
                {
                    case 1:
                        cmd.CommandText += "WHERE MaPhieuNhap LIKE @text";
                        cmd.Parameters.Add("@text", SqlDbType.VarChar, 20).Value = "%" + searchText.Trim() + "%";
                        break;
                    case 2:
                        cmd.CommandText +=
                            "WHERE DAY(NgayNhap) = @ngay " +
                            "AND MONTH(NgayNhap) = @thang " +
                            "AND YEAR(NgayNhap) = @nam";
                        cmd.Parameters.AddWithValue("@ngay", ngay);
                        cmd.Parameters.AddWithValue("@thang", thang);
                        cmd.Parameters.AddWithValue("@nam", nam);
                        break;
                    default:
                        break;
                }
                var list = new BindingList<PN>();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new PN
                        {
                            MaPhieuNhap = reader["MaPhieuNhap"].ToString(),
                            NgayNhap = Convert.ToDateTime(reader["NgayNhap"]),
                            TongTien = Convert.ToDecimal(reader["TongTien"])
                        });
                    }
                }
                return list;
            }
        }


        public BindingList<CT_PN> GetCT_PN(string mapn)
        {
            using (var conn = _db.GetConnection())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "SELECT PT.MaPhuTung, PT.TenPhuTung, CT.SoLuongNhap, CT.DonGiaNhap, CT.ThanhTien " +
                                  "FROM CT_PHIEUNHAPPHUTUNG CT JOIN PHUTUNG PT ON PT.MaPhuTung = CT.MaPhuTung" +
                                  " WHERE CT.MaPhieuNhap = @mapn";


                cmd.Parameters.Add("@mapn", SqlDbType.Char, 7).Value = mapn;
                var list = new BindingList<CT_PN>();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new CT_PN
                        {
                            MaPhuTung = reader["MaPhuTung"].ToString(),
                            TenPhuTung = reader["TenPhuTung"].ToString(),
                            DonGiaNhap = Convert.ToDecimal(reader["DonGiaNhap"]),
                            SoLuongNhap = Convert.ToInt32(reader["SoLuongNhap"]),
                            ThanhTien = Convert.ToDecimal(reader["ThanhTien"]),

                        });
                    }
                }
                return list;
            }
        }
    }

    public class GetPhuTungResult
    {
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
        public string SuccessMessage { get; set; }

        public BindingList<PhuTung> ListPhuTung;
    }
}
