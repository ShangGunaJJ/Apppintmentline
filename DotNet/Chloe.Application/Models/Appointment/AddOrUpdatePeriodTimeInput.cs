using Ace.Application;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chloe.Application.Models.Appointment
{
    public class AddOrUpdatePeriodTimeInput : ValidationModel
    {
        public string Id { get; set; }
        /// <summary>
        /// 星期几
        /// </summary>
        public int SeveraWeeks { get; set; }

        public string StratTime { get; set; }
        public string EndTime { get; set; }
        public DateTime? CreateTime { get; set; }

        public string CreateUser { get; set; }
        public DateTime? UpdateTime { get; set; }
    }
    public class AddPeriodTimeInput : AddOrUpdatePeriodTimeInput
    {

    }
    public class UpdatePeriodTimeInput : AddOrUpdatePeriodTimeInput
    {
        [RequiredAttribute(ErrorMessage = "{0}不能为空")]
        public string Id { get; set; }
    }
}
