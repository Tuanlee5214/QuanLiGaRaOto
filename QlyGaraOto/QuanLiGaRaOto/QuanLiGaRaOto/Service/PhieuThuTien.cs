using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLiGaRaOto.Model;
using System.ComponentModel;
using System.Windows.Forms;

namespace QuanLiGaRaOto.Service
{
    public class PhieuThuTien
    {
        private readonly DatabaseConnection _db = new DatabaseConnection();
        private static PhieuThuTien instance;
        public static PhieuThuTien Instance
        {
            get
            {
                if (instance == null)
                    instance = new PhieuThuTien();
                return instance;
            }
            set { instance = value; }
        }
        public InsertOrUpdateResult InsertIntoPTT(string mapt, string bienso, DateTime ngaythu, decimal tongtien)
        {
            try
            {
                using (var conn = _db.GetConnection())
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO PHIEUTHUTIEN (MaPhieuThu, BienSo, NgayThu, SoTienThu) " +
                                      "VALUES(@mapT, @bs, @ngaythu, @tongtien)";
                    cmd.Parameters.Add("@mapt", SqlDbType.Char, 7).Value = mapt;
                    cmd.Parameters.Add("@bs", SqlDbType.Char, 20).Value = bienso;
                    cmd.Parameters.Add("@ngaythu", SqlDbType.DateTime).Value = ngaythu;
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

        public string GetMaPhieuThuTien()
        {
            using (var conn = _db.GetConnection())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "SELECT MAX(MaPhieuThu) FROM PHIEUTHUTIEN";

                object result = cmd.ExecuteScalar();

                int nextNumber = 1;

                if (result != null && result != DBNull.Value)
                {
                    string lastCode = result.ToString();
                    int number = int.Parse(lastCode.Substring(2));
                    nextNumber = number + 1;
                }

                return "TT" + nextNumber.ToString("D5");
            }
        }

        public BindingList<PTT> GetPTT()
        {
            using (var conn = _db.GetConnection())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "SELECT MaPhieuThu, BienSo, NgayThu, SoTienThu FROM PHIEUTHUTIEN";
                var list = new BindingList<PTT>();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new PTT
                        {
                            MaPhieuThu = reader["MaPhieuThu"].ToString(),
                            BienSo = reader["BienSo"].ToString(),
                            NgayThu = Convert.ToDateTime(reader["NgayThu"]),
                            SoTienThu = Convert.ToDecimal(reader["SoTienThu"])
                        });
                    }
                }
                return list;
            }
        }

        public BindingList<PTT> SearchPTT(string searchText, int type, int ngay, int thang, int nam)
        {
            using (var conn = _db.GetConnection())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "SELECT MaPhieuThu, BienSo, NgayThu, SoTienThu FROM PHIEUTHUTIEN ";
                switch (type)
                {
                    case 1:
                        cmd.CommandText += "WHERE MaPhieuThu LIKE @text";
                        cmd.Parameters.Add("@text", SqlDbType.VarChar, 20).Value = "%" + searchText.Trim() + "%";
                        break;
                    case 2:
                        cmd.CommandText +=
                            "WHERE DAY(NgayThu) = @ngay " +
                            "AND MONTH(NgayThu) = @thang " +
                            "AND YEAR(NgayThu) = @nam";
                        cmd.Parameters.AddWithValue("@ngay", ngay);
                        cmd.Parameters.AddWithValue("@thang", thang);
                        cmd.Parameters.AddWithValue("@nam", nam);
                        break;
                    default:
                        break;
                }
                var list = new BindingList<PTT>();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new PTT
                        {
                            MaPhieuThu = reader["MaPhieuThu"].ToString(),
                            BienSo = reader["BienSo"].ToString(),
                            NgayThu = Convert.ToDateTime(reader["NgayThu"]),
                            SoTienThu = Convert.ToDecimal(reader["SoTienThu"])
                        });
                    }
                }
                return list;
            }
        }
    }
}
