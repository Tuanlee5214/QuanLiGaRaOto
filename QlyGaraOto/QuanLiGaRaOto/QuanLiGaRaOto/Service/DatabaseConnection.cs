using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiGaRaOto.Service
{
    public class DatabaseConnection
    {
        private readonly string _connectString = "Server=TUANLEE\\SQLEXPRESS;Database=QUANLYGARA111;Trusted_Connection=True;";

        public SqlConnection GetConnection()
        {
            var conn = new SqlConnection(_connectString);
            conn.Open();
            return conn;
        }
    }
}
