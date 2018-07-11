using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FzSxYJX.Model
{
    public class ResourceType
    {
        public int ID { get; set; }
        public string CodeName { get; set; }
        public int ParentID { get; set; }

        public List<ResourceType> Child { get; set; }
    }
}
