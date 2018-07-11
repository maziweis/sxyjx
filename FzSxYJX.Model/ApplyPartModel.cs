using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FzSxYJX.Model
{
    public class ApplyPartModel {
        public string BookUrl { get; set; }
        public string ImageUrl { get; set; }
        public List<AppPartModel> appList { get; set; }

    }
    /// <summary>
    /// 云课堂应用
    /// </summary>
    public class AppPartModel
    {
        public string AppName { get; set; }

        public int AppType { get; set; }

        public string Url { get; set; }
    }
}
