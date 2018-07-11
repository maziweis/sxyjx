using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FzSxYJX.Model
{
    public class StandBookCata
    {
        public int ID { get; set; }
        public int BookID { get; set; }
        public string CataName { get; set; }
        public int ParentID { get; set; }
        public int? PageStart { get; set; }
        public int? PageEnd { get; set; }
        public List<StandBookCata> ChildCata { get; set; }
    }
}
