using Common;
using DataBase;
using FzSxYJX.Bll;
using FzSxYJX.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FzSxYJX.Web.Controllers
{
    public class HomeController : Controller
    {
        private IndexBLL indexbll = new IndexBLL();
        public ActionResult Index(int id = 2)
        {
            if (!string.IsNullOrEmpty(CookieHelper.GetCookieValue("StageID"))) {
                id = int.Parse(CookieHelper.GetCookieValue("StageID"));
            }
            List<SubjectModel> subModel = indexbll.GetSubjectByStage(id);
            ViewBag.StageID = id;
            return View(subModel);              
        }
        /// <summary>
        /// 通过学段获取学科
        /// </summary>
        /// <param name="stageID"></param>
        /// <returns></returns>
        public JsonResult GetSubjectListByStageID(int stageID) {
            CookieHelper.SetCookie("StageID", stageID.ToString());
            List<SubjectModel> subModel = indexbll.GetSubjectByStage(stageID);
            KingResponse response = KingResponse.GetResponse(null, subModel);
            return Json(response);
        }
    }
}