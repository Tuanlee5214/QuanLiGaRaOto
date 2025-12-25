using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLiGaRaOto.Model;
using System.Windows.Forms;
using System.ComponentModel;

namespace QuanLiGaRaOto.Service
{
    public class CarService
    {
        private readonly DatabaseConnection _db = new DatabaseConnection();

        private static CarService instance;
        public static CarService Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new CarService();
                }
                    return instance;
            }
            set
            {
                instance = value;
            }
        }
        public DataTable GetTypeOfCarInfo()
        {

            using (var conn = _db.GetConnection())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "SELECT HieuXe FROM HIEUXE";
                DataTable dt = new DataTable();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                return dt;
            }
        }

        public int GetCarInADay()
        {
            using (var conn = _db.GetConnection())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "SELECT COUNT(BienSo) FROM XE WHERE NgayTiepNhan >= @today AND NgayTiepNhan < @tomorrow";
                cmd.Parameters.Add("@today", SqlDbType.DateTime).Value = DateTime.Today;
                cmd.Parameters.Add("@tomorrow", SqlDbType.DateTime).Value = DateTime.Today.AddDays(1);

                var result = cmd.ExecuteScalar();
                int CarInADay = Convert.ToInt32(result);
                return CarInADay;
            }
        }

        public GetCarsResult GetCars(DateTime today, DateTime tomorrow)
        {
            try
            {
                using (var conn = _db.GetConnection())
                using (var cmd = conn.CreateCommand())
                {

                    cmd.CommandText =
                        "SELECT BienSo, HieuXe, TenChuXe, DiaChi, SDT, Email, NgayTiepNhan, TongNo " +
                        "FROM XE WHERE NgayTiepNhan >= @today AND NgayTiepNhan < @tomorrow";

                    cmd.Parameters.Add("@today", SqlDbType.DateTime).Value = today;
                    cmd.Parameters.Add("@tomorrow", SqlDbType.DateTime).Value = tomorrow;

                    var list = new BindingList<Car>();

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new Car
                            {
                                BienSo = reader["BienSo"].ToString(),
                                HieuXe = reader["HieuXe"].ToString(),
                                TenChuXe = reader["TenChuXe"].ToString(),
                                DiaChi = reader["DiaChi"].ToString(),
                                SDT = reader["SDT"].ToString(),
                                Email = reader["Email"].ToString(),
                                NgayTiepNhan = Convert.ToDateTime(reader["NgayTiepNhan"]),
                                TongNo = Convert.ToDecimal(reader["TongNo"])
                            });
                        }
                    }

                    if (list.Count == 0)
                    {
                        return new GetCarsResult
                        {
                            Success = true,
                            ListCar = list
                        };
                    }

                    return new GetCarsResult
                    {
                        Success = true,
                        ListCar = list
                    };
                }
            }
            catch (SqlException)
            {
                return new GetCarsResult
                {
                    Success = false,
                    ErrorMessage = "Lỗi kết nối tới server"
                };
            }
        }

        public InsertOrUpdateResult UpdateCar(Car item)
        {
            try
            {
                using (var conn = _db.GetConnection())
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "UPDATE XE SET TenChuXe = @tcx, HieuXe = @hx, DiaChi = @dc, SDT = @sdt, Email = @email WHERE BienSo = @bs";
                    cmd.Parameters.Add("@hx", SqlDbType.VarChar, 30).Value = item.HieuXe;
                    cmd.Parameters.Add("@tcx", SqlDbType.NVarChar, 50).Value = item.TenChuXe;
                    cmd.Parameters.Add("@dc", SqlDbType.NVarChar, 200).Value = item.DiaChi;
                    cmd.Parameters.Add("@sdt", SqlDbType.Char, 20).Value = item.SDT;
                    cmd.Parameters.Add("@email", SqlDbType.VarChar, 50).Value = item.Email;
                    cmd.Parameters.Add("@bs", SqlDbType.Char, 20).Value = item.BienSo;

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

        public InsertOrUpdateResult AddCar(Car item)
        {
            try
            {
                using (var conn = _db.GetConnection())
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO XE (BienSo, HieuXe, TenChuXe, DiaChi, SDT, Email, NgayTiepNhan, TongNo)" +
                                      "VALUES(@bs, @hx, @tcx, @dc, @sdt, @email, @ntn, 0)";
                    cmd.Parameters.Add("@bs", SqlDbType.Char, 20).Value = item.BienSo;
                    cmd.Parameters.Add("@hx", SqlDbType.VarChar, 30).Value = item.HieuXe;
                    cmd.Parameters.Add("@tcx", SqlDbType.NVarChar, 50).Value = item.TenChuXe;
                    cmd.Parameters.Add("@dc", SqlDbType.NVarChar, 200).Value = item.DiaChi;
                    cmd.Parameters.Add("@sdt", SqlDbType.Char, 20).Value = item.SDT;
                    cmd.Parameters.Add("@email", SqlDbType.VarChar, 50).Value = item.Email;
                    cmd.Parameters.Add("@ntn", SqlDbType.DateTime).Value = item.NgayTiepNhan;

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
    }

    public class InsertOrUpdateResult
    {
        public bool Success { get; set; }
        public string SuccessMessage { get; set; }
        public string ErrorMessage { get; set; }
    }

    public class GetCarsResult
    {
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
        public string SuccessMessage { get; set; }

        public BindingList<Car> ListCar;
    }
}
