using Ace.Application;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chloe.Application.Models.Appointment
{
    public class AddOrUpdateBusinessInput : ValidationModel
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
        public int IsWeekEnd { get; set; }
        /// <summary>
        /// 可预约数量
        /// </summary>
        public int AppointmentNum { get; set; }
    }
    public class AddBusinessInput : AddOrUpdateBusinessInput
    {

    }
    public class SelBusinessInput : AddOrUpdateBusinessInput
    {
        public string PlaceName { get; set; }
        /// <summary>
        /// 业务名称
        /// </summary>
        public string TranName { get; set; }
        public string PeriodTime { get; set; }
        /// <summary>
        /// 当前排队号
        /// </summary>
        public string NowAppCode { get; set; }
        /// <summary>
        /// 排队人数
        /// </summary>
        public int LineUpNumber { get; set; }
    }
    public class UpdateBusinessInput : AddOrUpdateBusinessInput
    {
        [RequiredAttribute(ErrorMessage = "{0}不能为空")]
        public string Id { get; set; }
    }
}
