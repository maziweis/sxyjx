using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class AppSetting
    {
        private static string _ReslibUrl;
        public static string ReslibUrl {
            get {
                if (string.IsNullOrEmpty(_ReslibUrl)) {
                    _ReslibUrl = ConfigurationManager.AppSettings["ReslibUrl"];
                }
                return _ReslibUrl;
            }
        }
        private static string _FileUrl;
        public static string FileUrl
        {
            get
            {
                if (string.IsNullOrEmpty(_FileUrl))
                {
                    _FileUrl = ConfigurationManager.AppSettings["File_Url"];
                }
                return _FileUrl;
            }
        }
    }
}
