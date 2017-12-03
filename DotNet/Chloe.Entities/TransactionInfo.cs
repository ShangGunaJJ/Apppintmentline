using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chloe.Entities
{
   public class TransactionInfo
    {
        public string Id { get; set; }
        public string TransactionName { get; set; }
        public string Describe { get; set; }
        public string Code { get; set; }
        public string CreateUser { get; set; }
        public DateTime? CreateTime { get; set; }
        public DateTime? UpdateTime { get; set; }

        public int IsUploadFile { get; set; }
        public int IsApproval { get; set; }

        public int IsAutoCode { get; set; }
    }
}
