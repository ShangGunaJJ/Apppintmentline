using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chloe.Application.Common
{
    public class AppConsts
    {
        const string _AdminUserName = "sysadmin";

        public static string AdminUserName { get { return _AdminUserName; } }


        const string _Admin = "admin";

        public static string Admin { get { return _Admin; } }
        const string _Agent = "agent";

        public static string Agent { get { return _Agent; } }

        const string _AppID_WeChat = "wx704397e6f70bb8d5";

        public static string AppID_WeChat { get { return _AppID_WeChat; } }

        const string _AppSecret_WeChat = "865867c73ac14944ec4eba64d7ebdf52";

        public static string AppSecret_WeChat { get { return _AppSecret_WeChat; } }
    }
}
