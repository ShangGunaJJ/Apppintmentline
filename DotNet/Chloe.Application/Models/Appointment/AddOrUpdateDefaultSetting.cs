using Ace.Application;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chloe.Application.Models.Appointment
{
    public class AddOrUpdateDefaultSetting : ValidationModel
    {
        public string Id { get; set; }
        /// <summary>
        /// Key类型
        /// </summary>
        public int KeyType { get; set; }
        /// <summary>
        /// Key名称
        /// </summary>
        public string KeyName { get; set; }
        /// <summary>
        /// 键值
        /// </summary>
        public string Value { get; set; }

        public DateTime? CreateTime { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string CreateUser { get; set; }
    }
    public class AddDefaultSettingInput : AddOrUpdateDefaultSetting
    {

    }
    public class UpdateDefaultSettingInput : AddOrUpdateDefaultSetting
    {
        [RequiredAttribute(ErrorMessage = "{0}不能为空")]
        public string Id { get; set; }
    }
}
