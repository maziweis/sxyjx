using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FzSxYJX.Model
{
    public class MicroCourseModel
    {
        public int subjectID { get; set; }
        public string subjectName { get; set; }

        public List<KnowledgeModel> knowledgeList { get; set; }
    }
}
