using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FzSxYJX.Model
{
    public class MicroResource
    {
         public int resourceCount { get; set; }

         public string FileUrl { get; set; }
        public List<Resource> microResourceList { get; set; }
    }
}
