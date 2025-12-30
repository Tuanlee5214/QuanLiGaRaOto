using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiGaRaOto.Service
{
    public class PermissionService
    {
        public static bool XemBaoCao()
        {
            return UserSession.CurrentGroup.TenNhomNguoiDung == "Admin";
        }

        public static bool Sua()
        {
            return UserSession.CurrentGroup.TenNhomNguoiDung == "Admin";
        }

        public static bool Xoa()
        {
            return UserSession.CurrentGroup.TenNhomNguoiDung == "Admin";

        }

        public static bool XuatFile()
        {
            return UserSession.CurrentGroup.TenNhomNguoiDung == "Admin";

        }

        public static bool Them()
        {
            return UserSession.CurrentGroup.TenNhomNguoiDung == "Admin";

        }

        public static bool CapNhatNguoiDung()
        {
            return UserSession.CurrentGroup.TenNhomNguoiDung == "Admin";

        }

        public static bool ThayDoiQuyDinh()
        {
            return UserSession.CurrentGroup.TenNhomNguoiDung == "Admin";

        }
    }
}
