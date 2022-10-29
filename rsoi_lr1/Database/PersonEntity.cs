using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rsoi_lr1.Database
{
    public class PersonEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Job { get; set; }
        public int? Age { get; set; }
    }
}
