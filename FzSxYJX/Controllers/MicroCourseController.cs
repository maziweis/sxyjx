using Common;
using FzSxYJX.Bll;
using FzSxYJX.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FzSxYJX.Controllers
{
    public class MicroCourseController : Controller
    {
        // GET: MicroCourse
        public ActionResult Index(int subjectId = 2, int stageId = 2)
        {
            MicroCourseBLL mCourseBll = new MicroCourseBLL();
            if (!string.IsNullOrEmpty(CookieHelper.GetCookieValue("StageID")))
            {
                stageId = int.Parse(CookieHelper.GetCookieValue("StageID"));
            }
            MicroCourseModel mCourseModel = mCourseBll.GetMicroCourseInfo(subjectId, stageId);
            ViewBag.StageID = stageId;
            return View(mCourseModel);
        }
        /// <summary>
        /// 通过知识点获取资源
        /// </summary>
        /// <param name="id"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public JsonResult GetMicroCourseByKnowledgeID(string cids, string key="") {
            MicroCourseBLL mCourseBll = new MicroCourseBLL();
            MicroResource microresource = mCourseBll.GetMicroCourseByKnowledgeID(cids, key);
            KingResponse response = KingResponse.GetResponse(null, microresource);
            return Json(response);
        }

        public string GetKnowledgeIDs(int id) {
            MicroCourseBLL mCourseBll = new MicroCourseBLL();
            string cataids = mCourseBll.GetKnowledgeIDs(id);
            return cataids;
        }
        public JsonResult GetMicroCourseByKey(string key = "")
        {
            MicroCourseBLL mCourseBll = new MicroCourseBLL();
            MicroResource microresource = mCourseBll.GetMicroCourseByKey(key);
            KingResponse response = KingResponse.GetResponse(null, microresource);
            return Json(response);
        }

    }
}