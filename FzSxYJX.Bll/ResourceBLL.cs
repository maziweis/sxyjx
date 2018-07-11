using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FzSxYJX.Model;
using DataBase;

namespace FzSxYJX.Bll
{
    /// <summary>
    /// 资源类
    /// </summary>
    public class ResourceBLL
    {
        /// <summary>
        /// 获取资源列表
        /// </summary>
        /// <param name="cataIDs"></param>
        /// <param name="resourceType"></param>
        /// <returns></returns>
        public List<Resource> GetResourceList(string cataIDs, int resourceType, int resourceStyle)
        {
            using (var db = new MODResourceEntities())
            {
                List<Resource> resourceList = new List<Resource>();
                string[] cataArr = cataIDs.Split(',');
                IQueryable<tb_Resource> queryres = db.tb_Resource.Where(m => cataArr.Contains(m.Catalog.ToString()));
                if (resourceType != 0 || resourceStyle != 0)
                {
                    queryres = queryres.Where(m => m.ResourceType == resourceType && m.ResourceStyle == resourceStyle);
                }
                resourceList = queryres.Select(s => new Resource
                {
                    ID = s.ID.ToString(),
                    Title = s.Title,
                    Catalog = s.Catalog,
                    ResourceType = s.ResourceType,
                    ResourceStyle = s.ResourceStyle,
                    KeyWords = s.KeyWords,
                    FileID = s.FileID.ToString(),
                    Description = s.Description,
                    UploadDate = s.UploadDate,
                    FileType = s.FileType
                }).ToList();
                return resourceList;
            }
        }
        /// <summary>
        /// 关键字查询资源列表
        /// </summary>
        /// <param name="bookID"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public List<Resource> GetResourceListByKey(string cataIDs, string keyword)
        {
            using (var db = new MODResourceEntities())
            {
                var catalist = cataIDs.Split(',');
                List<Resource> resourceList = new List<Resource>();
                resourceList = db.tb_Resource.Where(m => catalist.Contains(m.Catalog.ToString()) && m.Title.Contains(keyword)).Select(s=>new Resource {
                    ID = s.ID.ToString(),
                    Title = s.Title,
                    Catalog = s.Catalog,
                    ResourceType = s.ResourceType,
                    ResourceStyle = s.ResourceStyle,
                    KeyWords = s.KeyWords,
                    FileID = s.FileID.ToString(),
                    Description = s.Description,
                    UploadDate = s.UploadDate,
                    FileType = s.FileType
                }).ToList();
                
                return resourceList;
            }
        }
    }
}
