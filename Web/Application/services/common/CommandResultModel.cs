using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.services.common
{
    public class CommandResultModel
    {
        public bool success { get; set; }
        public List<string> messages { get; set; }
        public int id { get; set; }
        public object model { get; set; }
        public CommandResultModel()
        {
            messages = new List<string>();
        }

    }
}
