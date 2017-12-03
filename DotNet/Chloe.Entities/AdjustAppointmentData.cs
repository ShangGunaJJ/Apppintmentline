using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chloe.Entities
{
   public class AdjustAppointmentData
    {
        public string Id { get; set; }

        public string OldAppointmentID { get; set; }

        public string NewAppointmentID { get; set; }

        public string CreateUser { get; set; }

        public DateTime? CreateTime { get; set; }

        public string remark { get; set; }
    }
}
