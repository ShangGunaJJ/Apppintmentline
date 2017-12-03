using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chloe.Entities
{
    public class PlaceInfo
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
}
