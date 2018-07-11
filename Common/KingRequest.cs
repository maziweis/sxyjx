using System;

namespace Common
{
    /// <summary>
    /// 服务请求
    /// </summary>
    [Serializable]
    public class KingRequest
    {
        /// <summary>
        /// 请求ID
        /// </summary>
        public string ID
        {
            get;
            set;
        }
        /// <summary>
        /// 请求方法
        /// </summary>
        public string Function
        {
            get;
            set;
        }
        /// <summary>
        /// 业务数据
        /// </summary>
        public string Data
        {
            get;
            set;
        }
    }
}