using Ace.Application;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chloe.Application.Models.Appointment
{
    public class AddOrUpdatePlaceInfoInput : ValidationModel
    {
        public string Id { get; set; }
        public string PlaceName { get; set; }
        public string Describe { get; set; }
        public string Code { get; set; }
        public string Address { get; set; }
        public DateTime? CreateTime { get; set; }
        public string CreateUser { get; set; }
        public DateTime? UpdateTime { get; set; }
    }
    public class AddPlaceInfoInput : AddOrUpdatePlaceInfoInput
    {

    }
    public class UpdatePlaceInfoInput : AddOrUpdatePlaceInfoInput
    {
        [RequiredAttribute(ErrorMessage = "{0}不能为空")]
        public string Id { get; set; }
    }
}
