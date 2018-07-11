using Common;
using DataBase;
using FzSxYJX.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FzSxYJX.Bll
{
    public class ApplyBLL
    {
        /// <summary>
        /// 云课堂电影课
        /// </summary>
        /// <param name="subjectID"></param>
        /// <param name="stageID"></param>
        /// <returns></returns>
        public ApplyModel GetCourseAndMovice(string subjectID, int stageID)
        {

            using (var db = new ModMetaEntities())
            {
                using (var dbres = new MODResourceEntities())
                {
                    ApplyModel appModel = new ApplyModel();
                    appModel.CourseAllList = new CourseAllModel();
                   // string[] grades = db.tb_Code_ListTable1_Relationship.Where(w => w.End_ID == stageID).Select(s => s.Start_ID.ToString()).ToArray();
                    IQueryable<tb_Course> tempCourse = dbres.tb_Course.Where(w => w.Subject == subjectID && w.SchoolStage == stageID);
                    int[] edi = tempCourse.Where(w => w.Coursetype != "movie").Select(s => s.EditionID).Distinct().ToArray();
                    appModel.CourseAllList.EditionList = db.tb_Code_ListTable3.Where(w => edi.Contains(w.ID)).OrderBy(o => o.Seq).Select(s => new Edition
                    {
                        EditionID = s.ID,
                        EditionName = s.CodeName

                    }).ToList();
                    if (appModel.CourseAllList.EditionList != null && appModel.CourseAllList.EditionList.Count > 0)
                    {
                        int ediID = appModel.CourseAllList.EditionList[0].EditionID;
                        appModel.CourseAllList.CourseList = tempCourse.Where(w => w.EditionID == ediID && w.Coursetype != "movie").OrderBy(o => o.CoursePath).Select(s => new CourseModel
                        {
                            CourseName = s.CourseName,
                            EditionID = s.EditionID,
                            GradeID = s.Grade,
                            ID = s.ID,                        
                            ImageUrl = AppSetting.ReslibUrl + "/DigitalClass/" + s.CoursePath + "/Course.gif",
                            IsOwnApply = ((s.Coursetype != "course") ?false:true)
                        }).ToList();
                        foreach (CourseModel course in appModel.CourseAllList.CourseList) {
                            if (!course.IsOwnApply) {
                                tb_Course tempcourse = dbres.tb_Course.Find(course.ID);
                                int grade = int.Parse(tempcourse.Grade);
                                int subject = int.Parse(tempcourse.Subject);
                                tb_StandardBook book = db.tb_StandardBook.Where(w => w.Edition == course.EditionID && w.Subject == subject && w.Grade == grade && w.Booklet == tempcourse.BookReel && w.Deleted == 0).FirstOrDefault();
                                course.ID = book.ID;
                            }
                        }
                    }
                    appModel.MoviceList = tempCourse.Where(w => w.Coursetype == "movie").OrderBy(o=>o.Grade).Select(s => new MovieModel
                    {
                        MoviceName = s.CourseName,
                        ID = s.ID,
                        Url = AppSetting.ReslibUrl + "/MovieListen/" + s.CoursePath + "/Start.htm",
                        ImageUrl = AppSetting.ReslibUrl + "/MovieListen/" + s.CoursePath + "/Course.gif",
                        SubjectID = s.Subject
                    }).ToList();
                    CacheHelper.Remove("CataIds");
                    return appModel;
                }
            }
        }
        /// <summary>
        /// 获取教材通过版本
        /// </summary>
        /// <param name="ediID"></param>
        /// <param name="subjectId"></param>
        /// <param name="stageId"></param>
        /// <returns></returns>
        public List<CourseModel> GetBookListByEditionID(int ediID, int subjectId, int stageId) {
            using (var db = new ModMetaEntities())
            {
                using (var dbres = new MODResourceEntities())
                {
                    List<CourseModel> courseModelList = new List<CourseModel>();
                    IQueryable<tb_Course> tempCourse = dbres.tb_Course.Where(w => w.Subject == subjectId.ToString() && w.SchoolStage == stageId);
                     courseModelList = tempCourse.Where(w => w.EditionID == ediID && w.Coursetype != "movie").OrderBy(o=>o.CoursePath).Select(s => new CourseModel
                        {
                            CourseName = s.CourseName,
                            EditionID = s.EditionID,
                            GradeID = s.Grade,
                            ID = s.ID,
                            ImageUrl = AppSetting.ReslibUrl + "/DigitalClass/" + s.CoursePath + "/Course.gif",
                            IsOwnApply = ((s.Coursetype != "course") ? false : true)
                        }).ToList();
                        foreach (CourseModel course in courseModelList)
                        {
                            if (!course.IsOwnApply)
                            {
                                tb_Course tempcourse = dbres.tb_Course.Find(course.ID);
                                int grade = int.Parse(tempcourse.Grade);
                                int subject = int.Parse(tempcourse.Subject);
                                tb_StandardBook book = db.tb_StandardBook.Where(w => w.Edition == course.EditionID && w.Subject == subject && w.Grade == grade && w.Booklet == tempcourse.BookReel).FirstOrDefault();
                                course.ID = book.ID;
                            }
                        }
                    return courseModelList;
                }
            }
        }
        /// <summary>
        /// 获取云课堂子应用
        /// </summary>
        /// <param name="subjectID"></param>
        /// <param name="ediID"></param>
        /// <param name="gradeID"></param>
        /// <param name="BookletID"></param>
        /// <returns></returns>
        public ApplyPartModel GetCourseAppList(int CourseID)
        {
            using (var db = new ModMetaEntities())
            {
                using (var dbres = new MODResourceEntities())
                {
                    ApplyPartModel applyModel = new ApplyPartModel();
                    applyModel.appList = new List<AppPartModel>();
                    tb_Course course = dbres.tb_Course.Find(CourseID);
                    tb_StandardBook book = db.tb_StandardBook.Where(w => w.Edition == course.EditionID && w.Subject.ToString() == course.Subject && w.Grade.ToString() ==course.Grade && w.Booklet == course.BookReel&&w.Deleted==0).FirstOrDefault();
                    applyModel.ImageUrl = AppSetting.ReslibUrl + "/DigitalClass/" + course.CoursePath + "/Course.gif";
                    applyModel.BookUrl = AppSetting.ReslibUrl + "/DigitalClass/" + course.CoursePath + "/Start.htm";
                    applyModel.appList = dbres.tb_CourseApp.Where(w => w.CourseID == CourseID).Select(apps => new AppPartModel
                    {
                        AppName = apps.AppName,
                        AppType = apps.AppType,
                        Url = apps.AppType == 0? (apps.Folder+ "?BookID="+ book.ID): (AppSetting.ReslibUrl + "/DigitalClass/" + course.CoursePath + "/" + apps.Folder + "/Start.htm")
                    }).ToList();

                    return applyModel;
                }
            }
        }
    }
}
