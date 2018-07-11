using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FzSxYJX.Model
{
   public class KnowledgeModel
    {
        public int ID { get; set; }

        public string CodeName { get; set; }

        public int stage { get; set; }

        public int subject { get; set; }

        public string cataids { get; set; }

        public List<KnowledgeModel> Children { get; set; }
    }
}
