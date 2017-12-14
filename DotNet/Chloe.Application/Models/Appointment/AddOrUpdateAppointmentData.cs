using Ace.Application;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Chloe.Application.Models.Appointment
{
   public class AddOrUpdateAppointmentData: ValidationModel
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
        /// 预约状态 -1 失效 0 未开始 1 正在受理 
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
    public class AddAppointmentDataInput : AddOrUpdateAppointmentData
    {

    }
    public class UpdateAppointmentDataInput : AddOrUpdateAppointmentData
    {
        [RequiredAttribute(ErrorMessage = "{0}不能为空")]
        public string Id { get; set; }
    }
}
