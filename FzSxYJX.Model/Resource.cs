using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FzSxYJX.Model
{
    public class Resource
    {
        public string ID { get; set; }
        public string Title { get; set; }
        public int? Catalog { get; set; }
        public int? ResourceType { get; set; }
        public int? ResourceStyle { get; set; }
        public string KeyWords { get; set; }
        public string FileID { get; set; }
        public string Description { get; set; }
        public DateTime? UploadDate { get; set; }
        public string FileType { get; set; }
    }

    public class ResourceAndType
    {
        public List<Resource> ResourceList { get; set; }
        public List<ResourceType> TypeList { get; set; }
    }

}
