using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chloe.Entities
{
    public class AppointmentData
    {
        public string Id { get; set; }

        /// <summary>
        /// 业务项ID
        /// </summary>
        public string BusinessID { get; set; }

        /// <summary>
        /// 会员ID
        /// </summary>
        public string MamberID { get; set; }

        /// <summary>
        /// 预约日期
        /// </summary>
        public DateTime? AppointmentDate { get; set; }

        /// <summary>
        /// 预约状态
        /// </summary>
        public int State { get; set; }

        /// <summary>
        /// 预约号
        /// </summary>
        public string MALU_Code { get; set; }
        /// <summary>
        /// 文件ID
        /// </summary>
        public string FileID { get; set; }


        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime? CreateTime { get; set; }
    }
}
