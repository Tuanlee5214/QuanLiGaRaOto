using QuanLiGaRaOto.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiGaRaOto.Service
{
    public class UserSession
    {
        public static User CurrentUser { get; private set; }
        public static Group CurrentGroup { get; private set; }


        public static void SetUser(User user, Group group)
        {
            CurrentUser = user;
            CurrentGroup = group;
        }



        public static void Logout()
        {
            CurrentUser = null;
        }
    }
}
