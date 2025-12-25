using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiGaRaOto.Service
{
    public class RuleService
    {
        private readonly DatabaseConnection _db = new DatabaseConnection();
        private static RuleService instance;
        public static RuleService Instance
        {
            get
            {
                if (instance == null)
                    instance = new RuleService();
                return instance;
            }
            set
            {
                instance = value;
            }
        }

        public int GetMaxCarOfDay()
        {
            using (var conn = _db.GetConnection())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "SELECT TOP 1 SoXeSuaChuaToiDa FROM QUYDINH";

                object result = cmd.ExecuteScalar();

                if (result == null || result == DBNull.Value)
                    return -1;
                return Convert.ToInt32(result);
            }
        }


        public double GetTiLeLai()
        {
            using (var conn = _db.GetConnection())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "SELECT TiLeLai FROM QUYDINH";

                var result = cmd.ExecuteScalar();
                double tiLeLai = Convert.ToDouble(result);
                return tiLeLai;
            }
        }

        public UpdateResult UpdateMaxCarOfDay(int MaxCar)
        {
            try
            {
                using (var conn = _db.GetConnection())
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText =
                        "UPDATE QUYDINH SET SoXeSuaChuaToiDa = @MaxCar";

                    cmd.Parameters.Add("@MaxCar", System.Data.SqlDbType.Int).Value = MaxCar;


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

        public UpdateResult UpdateTiLeLai(double tiLeLai)
        {
            try
            {
                using (var conn = _db.GetConnection())
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText =
                        "UPDATE QUYDINH SET TiLeLai = @tiLeLai";

                    cmd.Parameters.Add("@tiLeLai", System.Data.SqlDbType.Float).Value = (float)tiLeLai;


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
    

}
