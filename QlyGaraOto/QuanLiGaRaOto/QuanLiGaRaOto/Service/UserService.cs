using QuanLiGaRaOto.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace QuanLiGaRaOto.Service
{
    public class UserService
    {
        private readonly DatabaseConnection _db = new DatabaseConnection();

        private static UserService instance;

        public static UserService Instance
        {
            get
            {
                if (instance == null)
                    instance = new UserService();
                return instance;
            }
            set { instance = value; }
        }

        public LoginResult Login(string username, string password)
        {
            try
            {
                using (var conn = _db.GetConnection())
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText =
                        "SELECT * FROM NGUOIDUNG ND JOIN NHOMNGUOIDUNG GR ON GR.MaNhomND = ND.MaNhomND " +
                        "WHERE ND.TenDangNhap = @u AND ND.MatKhauBam = @p";

                    cmd.Parameters.Add("@u", System.Data.SqlDbType.VarChar, 30).Value = username;
                    cmd.Parameters.Add("@p", System.Data.SqlDbType.VarChar, 250).Value = PasswordHasher.Hash(password);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (!reader.Read())
                        {
                            return new LoginResult
                            {
                                Success = false,
                                ErrorMessage = "Tài khoản hoặc mật khẩu không đúng"
                            };
                        }

                        var user = new User
                        {
                            MaNguoiDung = reader["MaNguoiDung"].ToString(),
                            TenNguoiDung = reader["TenNguoiDung"].ToString(),
                            NgaySinh = (DateTime)reader["NgaySinh"],
                            TenDangNhap = reader["TenDangNhap"].ToString(),
                            MatKhauBam = reader["MatKhauBam"].ToString(),
                            MaNhomND = reader["MaNhomND"].ToString()
                        };

                        var group = new Group
                        {
                            MaNhomND = reader["MaNhomND"].ToString(),
                            TenNhomNguoiDung = reader["TenNhomNguoiDung"].ToString()
                        };

                        UserSession.SetUser(user, group);

                        return new LoginResult
                        {
                            Success = true,
                            SuccesMessage = "Đăng nhập thành công",
                            user = user
                        };
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error: " + ex.Message);
                return new LoginResult
                {
                    Success = false,
                    ErrorMessage = "Lỗi kết nối tới máy chủ"
                };
            }
        }

        public DataTable GetGroupInfo()
        {
            
             using (var conn = _db.GetConnection())
             using (var cmd = conn.CreateCommand())
             {
                cmd.CommandText = "SELECT MaNhomND, TenNhomNguoiDung FROM NHOMNGUOIDUNG";
                DataTable dt = new DataTable();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                return dt;
             }
        }

        public bool LoadUserInfo()
        {
            try
            {
                using (var conn = _db.GetConnection())
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText =
                        "SELECT * FROM NGUOIDUNG ND JOIN NHOMNGUOIDUNG GR ON GR.MaNhomND = ND.MaNhomND " +
                    "WHERE ND.TenDangNhap = @u AND ND.MatKhauBam = @p";

                    cmd.Parameters.Add("@u", System.Data.SqlDbType.VarChar, 30).Value = UserSession.CurrentUser.TenDangNhap;
                    cmd.Parameters.Add("@p", System.Data.SqlDbType.VarChar, 250).Value = UserSession.CurrentUser.MatKhauBam;

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (!reader.Read())
                        {
                            return false;
                        }

                        var user = new User
                        {
                            MaNguoiDung = reader["MaNguoiDung"].ToString(),
                            TenNguoiDung = reader["TenNguoiDung"].ToString(),
                            NgaySinh = (DateTime)reader["NgaySinh"],
                            TenDangNhap = reader["TenDangNhap"].ToString(),
                            MatKhauBam = reader["MatKhauBam"].ToString(),
                            MaNhomND = reader["MaNhomND"].ToString()
                        };

                        var group = new Group
                        {
                            MaNhomND = reader["MaNhomND"].ToString(),
                            TenNhomNguoiDung = reader["TenNhomNguoiDung"].ToString()
                        };

                        UserSession.SetUser(user, group);

                        return true;
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error: " + ex.Message);
                return false;
            }
        }

        public UpdateResult UpdateUser(string tnd, DateTime ngSinh, string maNhomND)
        {
            try
            {
                using (var conn = _db.GetConnection())
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText =
                        "UPDATE NGUOIDUNG SET TenNguoiDung = @tnd, NgaySinh = @ngsinh, MaNhomND = @maNhomND";

                    cmd.Parameters.Add("@tnd", System.Data.SqlDbType.NVarChar, 30).Value = tnd;
                    cmd.Parameters.Add("@ngSinh", System.Data.SqlDbType.DateTime).Value = ngSinh;
                    cmd.Parameters.Add("@maNhomND", System.Data.SqlDbType.Char, 7).Value = maNhomND;

                    int row = cmd.ExecuteNonQuery();

                    if (row != 0)
                    {
                        return new UpdateResult
                        {
                            Success = true,
                            SuccesMessage = "Cập nhật thành công"
                        };
                    }
                    else
                    {
                        return new UpdateResult
                        {
                            Success = false,
                            ErrorMessage = "Cập nhật thất bại"
                        };
                    }

                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error: " + ex.Message);
                return new UpdateResult
                {
                    Success = false,
                    ErrorMessage = "Lỗi kết nối tới máy chủ"
                };
            }
        }


        public UpdateResult UpdatePass(string hashPassword)
        {
            try
            {
                using (var conn = _db.GetConnection())
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText =
                        "UPDATE NGUOIDUNG SET MatKhauBam = @hashPassword";

                    cmd.Parameters.Add("@hashPassword", System.Data.SqlDbType.VarChar, 250).Value = hashPassword;
                  

                    int row = cmd.ExecuteNonQuery();

                    if (row != 0)
                    {
                        return new UpdateResult
                        {
                            Success = true,
                            SuccesMessage = "Cập nhật thành công"
                        };
                    }
                    else
                    {
                        return new UpdateResult
                        {
                            Success = false,
                            ErrorMessage = "Cập nhật thất bại"
                        };
                    }

                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error: " + ex.Message);
                return new UpdateResult
                {
                    Success = false,
                    ErrorMessage = "Lỗi kết nối tới máy chủ"
                };
            }
        }


    }

    public class LoginResult
    {
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
        public string SuccesMessage { get; set; }
        public User user { get; set; }
    }

    public class UpdateResult
    {
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
        public string SuccesMessage { get; set; }
    }
}
