using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chloe.Entities
{
    /// <summary>
    /// 预约业务项
    /// </summary>
    public class MALU_Business
    {
        public string Id { get; set; }
        /// <summary>
        /// 地点ID
        /// </summary>
        public string PlaceId { get; set; }
        /// <summary>
        /// 业务类型ID
        /// </summary>
        public string TransactionID { get; set; }
        /// <summary>
        /// 时间段ID
        /// </summary>
        public string PeriodTimeID { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime { get; set; }
        public string CreateUser { get; set; }
        public DateTime? UpdateTime { get; set; }
        /// <summary>
        /// 是否是周末
        /// </summary>
        public IsWeekEnd? IsWeekEnd { get; set; }
        /// <summary>
        /// 可预约数量
        /// </summary>
        public int AppointmentNum { get; set; }

        
    }
    public enum IsWeekEnd {
        Yes=1,
        No=-1
    }
}
