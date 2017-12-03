using Ace.Application;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chloe.Application.Models.Appointment
{
   public class AddOrUpdateFileDataInput: ValidationModel
    {
        public string Id { get; set; }

        public string FileName { get; set; }

        public string FileUrl { get; set; }
        public string CreateUser { get; set; }
        public DateTime CreateTime { get; set; }
    }

    public class AddFileDataInput : AddOrUpdateFileDataInput
    {

    }
    public class UpdateFileDataInput : AddOrUpdateFileDataInput
    {
        [RequiredAttribute(ErrorMessage = "{0}不能为空")]
        public string Id { get; set; }
    }
}
