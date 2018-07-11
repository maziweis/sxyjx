using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FzSxYJX.Model
{
    public class ResourcePageModel
    {
        /// <summary>
        /// 教材目录列表
        /// </summary>
        public List<StandBookCata> StandBookCataList { get; set; }

        public StandBook Book { get; set; }
        public string ShowMsg { get; set; }
        public string FileUrl { get; set; }
    }
}
