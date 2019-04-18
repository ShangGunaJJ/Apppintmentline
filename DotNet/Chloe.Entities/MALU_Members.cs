using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chloe.Entities
{
    public class MALU_Members
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string PassWord { get; set; }
        public string WeChatKey { get; set; }
        public string WeChatName { get; set; }
        public string MobilePhone { get; set; }
        public string IdCard { get; set; }
        /// <summary>
        /// 用户状态 1是启动 -1 停用
        /// </summary>
        public int state { get; set; }
        public DateTime? CreateTime { get; set; }

        public string CreateUser { get; set; }
    }

}
