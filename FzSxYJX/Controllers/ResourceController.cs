using FzSxYJX.Bll;
using FzSxYJX.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FzSxYJX.Controllers
{
    public class ResourceController : Controller
    {
        /// <summary>
        /// 初始化页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Index(int BookID)
        {
            ResourcePageModel model = new ResourcePageModel();
            model.Book = DataBaseBLL.GetStandBookByID(BookID);
            //////////////通过教材ID加载教材目录列表///////////////////
            model.StandBookCataList = DataBaseBLL.GetCataByBookID(BookID);
            model.ShowMsg = "义务教育教科书" + model.Book.EditionName + model.Book.StageName + model.Book.SubjectName;
            model.FileUrl = ConfigurationManager.AppSettings["File_Url"];
            return View(model);
        }
        /// <summary>
        /// 获取资源类型列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetResourceTypeList(FormCollection form)
        {
            string bookID = form["BookID"];
            string parentID = form["ParentID"];
            List<ResourceType> list = new List<ResourceType>();

            List<ResourceType> type_list = DataBaseBLL.GetResourceTypeList();

            ResourceBLL resourceBLL = new ResourceBLL();
            string cataIDs = DataBaseBLL.GetChildCataByCataID(Int32.Parse(bookID), Int32.Parse(parentID));
            List<Resource> resource_list = resourceBLL.GetResourceList(cataIDs, 0, 0);
            foreach (var type in type_list)
            {
                var result = false;
                foreach (var resource in resource_list)
                {
                    if (type.ID == resource.ResourceType && resource.ResourceStyle == 0)
                    {
                        list.Add(type);
                        result = true;
                        break;
                    }
                }
                if (!result)
                {
                    foreach (var child in type.Child)
                    {
                        foreach (var resource in resource_list)
                        {
                            if (child.ID == resource.ResourceStyle && child.ParentID == resource.ResourceType)
                            {
                                list.Add(child);
                                result = true;
                                break;
                            }
                        }
                    }
                }
            }
            ResourceAndType modellist = new ResourceAndType();
            modellist.ResourceList = resource_list;
            modellist.TypeList = list;
            return Json(modellist);
        }
        /// <summary>
        /// 获取资源列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetResource(FormCollection form)
        {
            ResourceBLL resourceBLL = new ResourceBLL();
            string bookID = form["BookID"];
            string cataID = form["CataID"];
            int resourceType = Int32.Parse(form["ResourceType"]);
            int resourceStyle = Int32.Parse(form["ResourceStyle"]);
            string cataIDs = DataBaseBLL.GetChildCataByCataID(Int32.Parse(bookID), Int32.Parse(cataID));
            List<Resource> list = new List<Resource>();
            if (resourceStyle== 0)
                list = resourceBLL.GetResourceList(cataIDs, resourceType, resourceStyle);
            else
                list = resourceBLL.GetResourceList(cataIDs, resourceStyle,resourceType);
            return Json(list);
        }
        /// <summary>
        /// 关键字查询资源列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetResourceByKey(FormCollection form)
        {
            ResourceBLL resourceBLL = new ResourceBLL();
            int bookID = Int32.Parse(form["BookID"]);
            string keyword = form["KeyWord"];


            List<StandBookCata> catalist = DataBaseBLL.GetCataByBookID(bookID);
            string cataIDs = DataBaseBLL.GetCataID(catalist);

            List<Resource> list = resourceBLL.GetResourceListByKey(cataIDs, keyword);
            return Json(list);
        }

        [HttpPost]
        public ActionResult GetChildID(FormCollection form)
        {
            string bookID = form["BookID"];
            string cataID = form["CataID"];
            string cataIDs = DataBaseBLL.GetChildCataByCataID(Int32.Parse(bookID), Int32.Parse(cataID));
            return Json(cataIDs);
        }
    }
}