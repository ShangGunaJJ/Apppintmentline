using Ace.Application;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chloe.Application.Models.Appointment
{
    public class AddOrUpdateAdjustAppDataInput : ValidationModel
    {
        public string Id { get; set; }

        public string OldAppointmentID { get; set; }

        public string NewAppointmentID { get; set; }

        public string CreateUser { get; set; }

        public DateTime? CreateTime { get; set; }

        public string remark { get; set; }
    }
    public class AddAdjustAppDataInput : AddOrUpdateAdjustAppDataInput
    {

    }
    public class UpdateAdjustAppDataInput : AddOrUpdateAdjustAppDataInput
    {
        [RequiredAttribute(ErrorMessage = "{0}不能为空")]
        public string Id { get; set; }
    }
}
