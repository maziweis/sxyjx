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
    public class IndexBLL
    {
        /// <summary>
        /// 根据学段id取出已有教材从而动态获取到学段所有学科
        /// </summary>
        /// <param name="stageid">学段id</param>
        /// <returns></returns>
        public List<SubjectModel> GetSubjectByStage(int stageid)
        {
            using (var db = new ModMetaEntities())
            {
                using (var dbres = new MODResourceEntities())
                {
                    string[] subIdlist = dbres.tb_Course.Where(w => w.SchoolStage == stageid).Select(s => s.Subject).Distinct().ToArray();
                    List<SubjectModel> subModel = db.tb_Code_ListTable2.Where(w => subIdlist.Contains(w.ID.ToString())).OrderBy(o => o.Seq).Select(s => new SubjectModel
                    {
                        SubjectID = s.ID,
                        SubjectName = s.CodeName

                    }).ToList();
                    return subModel;
                }
            }
        }
        /// <summary>
        /// 获取学段
        /// </summary>
        /// <returns></returns>
        public static List<StageModel> GetStage()
        {
            using (var db = new ModMetaEntities())
            {
                using (var dbres = new MODResourceEntities())
                {
                    int[] stage = dbres.tb_Course.Select(s =>s.SchoolStage!=null?(int)s.SchoolStage:2).Distinct().ToArray();
                    List<StageModel> stageList = db.tb_Code_SchoolStage.Where(w => w.Deleted == 0 && stage.Contains(w.ID)).Select(s => new StageModel
                    {
                        stageID = s.ID,
                        stageName = s.CodeName,
                        IsCurrent = false

                    }).ToList();
                    if (string.IsNullOrEmpty(CookieHelper.GetCookieValue("StageID")))
                    {
                        CookieHelper.SetCookie("StageID", "2");
                    }
                    int stageID = int.Parse(CookieHelper.GetCookieValue("StageID"));
                    foreach (StageModel smodel in stageList)
                    {
                        if (smodel.stageID == stageID)
                        {
                            smodel.IsCurrent = true;
                        }
                    }
                    return stageList;
                }
            }
        }
    }
}
