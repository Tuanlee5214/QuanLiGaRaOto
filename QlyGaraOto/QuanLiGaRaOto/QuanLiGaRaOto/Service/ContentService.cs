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
    public class ContentService
    {
        private readonly DatabaseConnection _db = new DatabaseConnection();

        private static ContentService instance;
        public static ContentService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ContentService();
                }
                return instance;
            }
            set
            {
                instance = value;
            }
        }

        public string GetMaNoiDungSC()
        {
            using (var conn = _db.GetConnection())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "SELECT MAX(MaNoiDung) FROM NOIDUNGSUACHUA";

                object result = cmd.ExecuteScalar();

                int nextNumber = 1;

                if (result != null && result != DBNull.Value)
                {
                    string lastCode = result.ToString();
                    int number = int.Parse(lastCode.Substring(2));
                    nextNumber = number + 1;
                }

                return "ND" + nextNumber.ToString("D5");
            }
        }


        public GetContentResult GetContentFull()
        {
            try
            {
                using (var conn = _db.GetConnection())
                using (var cmd = conn.CreateCommand())
                {

                    cmd.CommandText =
                        "SELECT MaNoiDung, MoTa, TienCong " +
                        "FROM NOIDUNGSUACHUA";


                    var list = new BindingList<NoiDung>();

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new NoiDung
                            {
                                MaNoiDung = reader["MaNoiDung"].ToString(),
                                MoTa = reader["MoTa"].ToString(),
                                TienCong = Convert.ToDecimal(reader["TienCong"]),
                            });
                        }
                    }

                    if (list.Count == 0)
                    {
                        return new GetContentResult
                        {
                            Success = true,
                            ListContent = list
                        };
                    }

                    return new GetContentResult
                    {
                        Success = true,
                        ListContent = list
                    };
                }
            }
            catch (SqlException)
            {
                return new GetContentResult
                {
                    Success = false,
                    ErrorMessage = "Lỗi kết nối tới server"
                };
            }
        }

        public GetContentResult SearchContent(string searchText, int type)
        {
            try
            {
                using (var conn = _db.GetConnection())
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText =
                        "SELECT MaNoiDung, MoTa, TienCong " +
                        "FROM NOIDUNGSUACHUA ";

                    switch (type)
                    {
                        case 1: // Mã noidung
                            cmd.CommandText += "WHERE MaNoiDung LIKE @ma";
                            cmd.Parameters.Add("@ma", SqlDbType.VarChar, 20)
                                          .Value = "%" + searchText.Trim() + "%";
                            break;

                        case 2: // Tên noidung
                            cmd.CommandText += "WHERE MoTa LIKE @ten";
                            cmd.Parameters.Add("@ten", SqlDbType.NVarChar, 1000)
                                          .Value = "%" + searchText.Trim() + "%";
                            break;

                        default:
                            return new GetContentResult
                            {
                                Success = false,
                                ErrorMessage = "Loại tìm kiếm không hợp lệ"
                            };
                    }

                    var list = new BindingList<NoiDung>();

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new NoiDung
                            {
                                MaNoiDung = reader["MaNoiDung"].ToString(),
                                MoTa = reader["MoTa"].ToString(),
                                TienCong = Convert.ToDecimal(reader["TienCong"]),
                            });
                        }
                    }

                    return new GetContentResult
                    {
                        Success = true,
                        ListContent = list
                    };
                }
            }
            catch (SqlException)
            {
                return new GetContentResult
                {
                    Success = false,
                    ErrorMessage = "Lỗi kết nối tới server"
                };
            }
        }

        public InsertOrUpdateResult AddContent(NoiDung item)
        {
            try
            {
                using (var conn = _db.GetConnection())
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO NOIDUNGSUACHUA (MaNoiDung, MoTa, TienCong) " +
                                      "VALUES(@mand, @ten, @tc)";
                    cmd.Parameters.Add("@mand", SqlDbType.Char, 7).Value = item.MaNoiDung;
                    cmd.Parameters.Add("@ten", SqlDbType.NVarChar, 50).Value = item.MoTa;
                    cmd.Parameters.Add("@tc", SqlDbType.Decimal).Value = item.TienCong;

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

        public InsertOrUpdateResult DeleteContent(string mand)
        {
            try
            {
                using (var conn = _db.GetConnection())
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM NOIDUNGSUACHUA WHERE MaNoiDung = @mand";
                    cmd.Parameters.Add("@mand", SqlDbType.Char, 7).Value = mand;


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

        public InsertOrUpdateResult UpdateContent(NoiDung item)
        {
            try
            {
                using (var conn = _db.GetConnection())
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "UPDATE NOIDUNGSUACHUA SET MoTa = @ten, TienCong = @tc WHERE MaNoiDung = @mand";
                    cmd.Parameters.Add("@mand", SqlDbType.Char, 7).Value = item.MaNoiDung;
                    cmd.Parameters.Add("@ten", SqlDbType.NVarChar, 50).Value = item.MoTa;
                    cmd.Parameters.Add("@tc", SqlDbType.Decimal).Value = item.TienCong;

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


    }

    public class GetContentResult
    {
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
        public string SuccessMessage { get; set; }

        public BindingList<NoiDung> ListContent;
    }
}
