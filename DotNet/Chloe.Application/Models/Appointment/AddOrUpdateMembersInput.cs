using Ace.Application;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chloe.Application.Models.Appointment
{
    public class AddOrUpdateMembersInput:ValidationModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string PassWord { get; set; }
        public string WeChatKey { get; set; }
        public string WeChatName { get; set; }
        public string MobilePhone { get; set; }
        public string IdCard { get; set; }

        public DateTime? CreateTime { get; set; }
    }
    public class AddMembersInput : AddOrUpdateMembersInput
    {
        public string PhoneVcode { get; set; }
        public string VCode { get; set; }
    }
    public class UpdateMainInput : AddOrUpdateMembersInput
    {
        [RequiredAttribute(ErrorMessage = "{0}不能为空")]
        public string Id { get; set; }
    }
}
