using Ace.Application;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chloe.Application.Models.Appointment
{
    public class AddEvaluate: ValidationModel
    {
        public string Id { get; set; }
        public string ApponitID { get; set; }

        public int Star { get; set; }

        public string Evaluation { get; set; }
        public string MenberID { get; set; }

        public DateTime CreateTime { get; set;}
    }

    public class SelEvaluate : AddEvaluate {
        /// <summary>
        /// 业务名称
        /// </summary>
        public string TranName { get; set; }
        public string MName { get; set; }

        public string Phone { get; set; }

        public string IdCard { get; set; }
        public string TransactionID { get; set; }
        public DateTime? AppointmentDate { get; set; }

    }
}
