using Infrastructure.common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.Entity
{
    [Table("Employee")]
    public class Employee : BaseEntity
    {
        public string name { get; set; }
        public int age { get; set; }
        public string position { get; set; }
        public string salary { get; set; }
        public string company { get; set; }


    }
}
