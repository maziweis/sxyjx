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
    public class MicroCourseBLL
    {
        private StringBuilder sbcata = new StringBuilder();
        private string scatalist = "";

        public MicroCourseModel GetMicroCourseInfo(int subjectId, int stageId)
        {
            using (var metadb = new ModMetaEntities()) {
                MicroCourseModel micrcomodel = new MicroCourseModel();
                tb_Code_ListTable2 subject =  metadb.tb_Code_ListTable2.Where(w=>w.ID == subjectId && w.Deleted == 0).FirstOrDefault();
                micrcomodel.subjectID = subject.ID;
                micrcomodel.subjectName = subject.CodeName;
                micrcomodel.knowledgeList = new List<KnowledgeModel>();
                List<tb_Code_Knowledge> knowlist = metadb.tb_Code_Knowledge.Where(w => w.SubjectID == subjectId && w.StageID == stageId && w.Deleted == 0 && w.ParentID == 0).OrderBy(o=>o.Seq).ToList();
                foreach (tb_Code_Knowledge konwledge in knowlist) {
                    KnowledgeModel knowledgemodel = new KnowledgeModel();
                    knowledgemodel.ID = konwledge.ID;
                    knowledgemodel.stage = konwledge.StageID;
                    knowledgemodel.subject = konwledge.SubjectID;
                    knowledgemodel.CodeName = konwledge.CodeName;                   
                    knowledgemodel.Children = GetKnowlegeList(metadb, knowledgemodel.ID);
                    micrcomodel.knowledgeList.Add(knowledgemodel);
                }
                if (CacheHelper.Get("CataIds") == null) {
                    List<tb_Code_Knowledge> listcatas = metadb.tb_Code_Knowledge.Where(w => w.Deleted == 0 && w.SubjectID == subjectId && w.StageID == stageId).OrderBy(o => o.Seq).ToList();
                    CacheHelper.Insert("CataIds", listcatas);
                }
                return micrcomodel;
            }             
        }
        /// <summary>
        /// 递归目录节点
        /// </summary>
        /// <param name="db"></param>
        /// <param name="parentId"></param>
        /// <returns></returns>
        private List<KnowledgeModel> GetKnowlegeList(ModMetaEntities db, int parentId) {
            List<KnowledgeModel> knowledgeList = new List<KnowledgeModel>();
            List<tb_Code_Knowledge> tempkonwledgeList = db.tb_Code_Knowledge.Where(w => w.Deleted == 0 && w.ParentID == parentId).OrderBy(o => o.Seq).ToList();
            foreach (tb_Code_Knowledge konwledge in tempkonwledgeList)
            {
                KnowledgeModel knowledgemodel = new KnowledgeModel();
                knowledgemodel.ID = konwledge.ID;
                knowledgemodel.stage = konwledge.StageID;
                knowledgemodel.subject = konwledge.SubjectID;
                knowledgemodel.CodeName = konwledge.CodeName;
                knowledgemodel.Children = null;
                knowledgeList.Add(knowledgemodel);
             }
            return knowledgeList;
        }

        public MicroResource GetMicroCourseByKnowledgeID(string ids,  string key)
        {
                using (var dbres = new MODResourceEntities())
                {
                    MicroResource microresource = new MicroResource();
                    microresource.FileUrl = AppSetting.FileUrl;
                    microresource.microResourceList = new List<Resource>();
                    string[] cataids = ids.Split(',');                  
                    IQueryable<tb_Resource> qresource = dbres.tb_Resource.Where(w => w.tb_ResourceKnowledge.Where(wrk => cataids.Contains(wrk.KnowledgeID.ToString())).Select(s1 => s1.ResourceID).Contains(w.ID) && w.IsDelete == 0);
                    if (!string.IsNullOrEmpty(key)) {
                        qresource = qresource.Where(w => w.Title.Contains(key));
                    }                                 
                    microresource.microResourceList = qresource.OrderBy(o => o.UploadDate).Select(s => new Resource
                    {
                        ID = s.ID.ToString(),
                        FileID = s.FileID.ToString(),
                        Title = s.Title,
                        Description = s.Description,
                        FileType = s.FileType,
                        KeyWords = s.KeyWords,
                        UploadDate = s.UploadDate
                    }).ToList();
                    microresource.resourceCount = microresource.microResourceList.Count;
                    return microresource;
                }
        }
        /// <summary>
        /// 采用循环
        /// </summary>
        /// <param name="db"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetcataidListbyXH(ModMetaEntities db, int id) {
            sbcata.Append(id);
            List<tb_Code_Knowledge> CataAllids = CacheHelper.Get("CataIds") as List<tb_Code_Knowledge>;
            int[] Secondcatas = CataAllids.Where(w => w.ParentID == id).Select(s=>s.ID).ToArray();
            foreach (int scata in Secondcatas)
            {
                if (sbcata != null) {
                    sbcata.Append(",");
                }
                sbcata.Append(scata);
                int[] lcatas = CataAllids.Where(w => w.ParentID == scata).Select(s => s.ID).ToArray();
                foreach (int lcata in lcatas)
                {
                    if (sbcata != null)
                    {
                        sbcata.Append(",");
                    }
                    sbcata.Append(lcata);
                }

            }
            return sbcata.ToString();
        }
        /// <summary>
        /// 采用递归
        /// </summary>
        /// <param name="db"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetcataidListbyDG(ModMetaEntities db, int id)
        {
            int[] catas = db.tb_Code_Knowledge.Where(w => w.Deleted == 0 && w.ParentID == id).Select(s => s.ID).ToArray();
            foreach (int cata in catas)
            {
                if (!string.IsNullOrEmpty(scatalist))
                {
                    scatalist +=",";
                }
                scatalist += cata;
                scatalist = GetcataidListbyDG(db, cata);

            }
            return scatalist;
        }
        public string GetKnowledgeIDs(int id)
        {
            using (var metadb = new ModMetaEntities())
            {
                string tempcataids = id.ToString();
                tempcataids += ",";
                tempcataids += GetcataidListbyXH(metadb, id);
                return tempcataids;
            }
        }
        public MicroResource GetMicroCourseByKey(string key) {
            using (var dbres = new MODResourceEntities())
            {
                MicroResource microresource = new MicroResource();
                microresource.FileUrl = AppSetting.FileUrl;
                microresource.microResourceList = new List<Resource>();
                List<tb_Code_Knowledge> CataAllids = CacheHelper.Get("CataIds") as List<tb_Code_Knowledge>;
                int[] konwledges = CataAllids.Select(s=>s.ID).ToArray();
                IQueryable<tb_Resource> qresource = dbres.tb_Resource.Where(w => w.tb_ResourceKnowledge.Where(wrk => konwledges.Contains(wrk.KnowledgeID)).Select(s1 => s1.ResourceID).Contains(w.ID) && w.IsDelete == 0);
                if (!string.IsNullOrEmpty(key))
                {
                    qresource = qresource.Where(w => w.Title.Contains(key));
                }
                microresource.microResourceList = qresource.OrderBy(o=>o.UploadDate).Select(s => new Resource
                {
                    ID = s.ID.ToString(),
                    FileID = s.FileID.ToString(),
                    Title = s.Title,
                    Description = s.Description,
                    FileType = s.FileType,
                    KeyWords = s.KeyWords,
                    UploadDate = s.UploadDate
                }).ToList();
                microresource.resourceCount = qresource.Count();
                return microresource;
            }

        }
    }
}
