using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FzSxYJX.Model
{
    public class StandBook
    {
        public int ID { get; set; }
        public string BookName { get; set; }
        public int SubjectID { get; set; }
        public string SubjectName { get; set; }
        public int StageID { get; set; }
        public string StageName { get; set; }
        public int EditionID { get; set; }
        public string EditionName { get; set; }
    }
}
