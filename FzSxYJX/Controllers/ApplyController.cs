using Common;
using Fz.Common;
using FzSxYJX.Bll;
using FzSxYJX.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace FzSxYJX.Controllers
{
    public class ApplyController : Controller
    {
        private ApplyBLL appbll = new ApplyBLL();
        // GET: Apply
        public ActionResult Index(int subjectId = 3, int stageId = 2)
        {
            if (!string.IsNullOrEmpty(CookieHelper.GetCookieValue("StageID")))
            {
                stageId = int.Parse(CookieHelper.GetCookieValue("StageID"));
            }
            ApplyModel appModel =  appbll.GetCourseAndMovice(subjectId.ToString(), stageId);
            ViewBag.StageID = stageId;
            ViewBag.SubjetID = subjectId;           
            return View(appModel);         
        }
        /// <summary>
        /// 切换学段获取教材
        /// </summary>
        /// <param name="subjectId"></param>
        /// <param name="stageId"></param>
        /// <returns></returns>
        public JsonResult GetBookListAndMovice(int subjectId, int stageId) {
            CookieHelper.SetCookie("StageID", stageId.ToString());
            ApplyModel appModel = appbll.GetCourseAndMovice(subjectId.ToString(), stageId);
            KingResponse response = KingResponse.GetResponse(null, appModel);
            return Json(response);
        }
        public JsonResult GetBookListByEditionID(int ediID, int subjectId, int stageId) {
            List<CourseModel> appModel = appbll.GetBookListByEditionID(ediID, subjectId, stageId);
            KingResponse response = KingResponse.GetResponse(null, appModel);
            return Json(response);
        }
        /// <summary>
        /// 获取云课堂子集
        /// </summary>
        /// <param name="CourseID"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetCourseAppList(int CourseID) {
            ApplyPartModel appList =  appbll.GetCourseAppList(CourseID);
            KingResponse response = KingResponse.GetResponse(null,appList);
            return Json(response);
        }

        /// <summary>
        /// 接收返回结果
        /// </summary>
        public class Result
        {
            public int code { get; set; }
            public string msg { get; set; }
        }
        [HttpPost]
        public JsonResult CourseAppTransfer(string Url)
        {
            string token = System.Configuration.ConfigurationManager.AppSettings["Token"];
            string key = "";
            key = UrlEcode.EncryptUrl(token);
            Url += "?" + key;
            Result result = new Result();
            result.msg = "服务器拒绝访问";
            try
            {
                WebRequest request = WebRequest.Create(Url);
                WebResponse response = request.GetResponse();
                Stream s = response.GetResponseStream();
                StreamReader sr = new StreamReader(s, Encoding.Default);
                string test = sr.ReadToEnd();
                result = JsonHelper.DeepDecodeJson<Result>(test);
                if (result.code >= 0)
                    return Json(result);
                else
                    return Json(result);
            }
            catch (Exception ex)
            {
                result.msg = Url;
                return Json(result);
            }
        }
    }
}