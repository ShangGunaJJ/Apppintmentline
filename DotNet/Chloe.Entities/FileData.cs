using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chloe.Entities
{
    public class FileData
    {
        public string Id { get; set; }

        public string FileName { get; set; }

        public string FileUrl { get; set; }
        public string CreateUser { get; set; }
        public DateTime? CreateTime { get; set; }
    }
}
