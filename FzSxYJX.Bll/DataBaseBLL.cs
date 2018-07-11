using DataBase;
using FzSxYJX.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FzSxYJX.Bll
{
    /// <summary>
    /// 基础数据类
    /// </summary>
    public class DataBaseBLL
    {
        public static StandBook GetStandBookByID(int bookID)
        {
            StandBook book = new StandBook();
            using (var db=new ModMetaEntities())
            {
                var list = db.tb_StandardBook.Where(m=>m.ID==bookID&&m.Deleted==0).ToList();
                book.ID = list[0].ID;
                book.BookName = list[0].BooKName;

                book.SubjectID = list[0].Subject;
                var subject=db.tb_Code_ListTable2.Where(m=>m.ID== book.SubjectID && m.Deleted==0).ToList();
                book.SubjectName = subject[0].CodeName;

                book.StageID = list[0].Stage;
                var stage = db.tb_Code_SchoolStage.Where(m => m.ID == book.StageID && m.Deleted == 0).ToList();
                book.StageName = stage[0].CodeName;

                book.EditionID = list[0].Edition;
                var edition = db.tb_Code_ListTable3.Where(m => m.ID == book.EditionID && m.Deleted == 0).ToList();
                book.EditionName = edition[0].CodeName;
            }
            return book;
        }
        /// <summary>
        /// 通过教材ID获取教材目录列表
        /// </summary>
        public static List<StandBookCata> GetCataByBookID(int bookID)
        {
            using (var db=new ModMetaEntities())
            {
                List<tb_StandardCatalog> list = new List<tb_StandardCatalog>();
                List<StandBookCata> treeList = new List<StandBookCata>();
                list= db.tb_StandardCatalog.Where(m => m.BookID == bookID && m.Deleted == 0).ToList();
                foreach (var item in list)
                {
                    if (item.ParentID==0)
                    {
                        StandBookCata model = new StandBookCata();
                        model.ID = item.ID;
                        model.BookID = item.BookID;
                        model.CataName = item.FolderName;
                        model.ParentID = item.ParentID;
                        model.PageStart = item.PageStart;
                        model.PageStart = item.PageStart;
                        treeList.Add(model);
                        ///递归获取目录子集列表///
                        LoopToAppendChildrenForCata(list, model);
                    }
                }
                return treeList;
            }
        }
        /// <summary>
        /// 获取目录的子集目录
        /// </summary>
        /// <param name="cataID"></param>
        /// <returns></returns>
        public static string GetChildCataByCataID(int bookID, int cataID)
        {
            string cataIDs = cataID.ToString();
            using (var db = new ModMetaEntities())
            {
                List<tb_StandardCatalog> list = new List<tb_StandardCatalog>();
                List<StandBookCata> treeList = new List<StandBookCata>();
                list = db.tb_StandardCatalog.Where(m => m.BookID == bookID && m.Deleted == 0).ToList();
                foreach (var item in list)
                {
                    if (item.ParentID == cataID)
                    {
                        StandBookCata model = new StandBookCata();
                        model.ID = item.ID;
                        model.BookID = item.BookID;
                        model.CataName = item.FolderName;
                        model.ParentID = item.ParentID;
                        model.PageStart = item.PageStart;
                        model.PageStart = item.PageStart;
                        treeList.Add(model);
                        ///递归获取目录子集列表///
                        LoopToAppendChildrenForCata(list, model);
                    }
                }
                if (treeList.Count != 0)
                {
                    cataIDs += GetCataID(treeList);
                    
                }
            }
            return cataIDs;
        }
        public static string GetCataID(List<StandBookCata> list) {
            string cataIDs = "";
            foreach (var item in list)
            {
                cataIDs += "," + item.ID;
                if (item.ChildCata.Count!=0)
                    cataIDs += GetCataID(item.ChildCata);
            }
            return cataIDs;
        }

        public static string GetChildCataByCataID(int cataID)
        {
            throw new NotImplementedException();
        }
        #region 递归获取目录子集列表
        public static void LoopToAppendChildrenForCata(List<tb_StandardCatalog> list, StandBookCata cata)
        {
            List<tb_StandardCatalog> child = list.Where(m => m.ParentID == cata.ID).ToList();
            cata.ChildCata = new List<StandBookCata>();
            if (child.Count!=0)
            {
                foreach (var item in child)
                {
                    StandBookCata model = new StandBookCata();
                    model.ID = item.ID;
                    model.BookID = item.BookID;
                    model.CataName = item.FolderName;
                    model.ParentID = item.ParentID;
                    model.PageStart = item.PageStart;
                    model.PageStart = item.PageStart;
                    cata.ChildCata.Add(model);
                    ///递归获取目录子集列表///
                    LoopToAppendChildrenForCata(list, model);
                }
            }
        }
        #endregion
        /// <summary>
        /// 获取资源类型列表
        /// </summary>
        public static List<ResourceType> GetResourceTypeList()
        {
            using (var db=new ModMetaEntities())
            {
                List<tb_Code_TreeTable2> list = new List<tb_Code_TreeTable2>();
                List<ResourceType> treeList = new List<ResourceType>();
                list = db.tb_Code_TreeTable2.Where(m=>m.Deleted==0).ToList();
                foreach (var item in list)
                {
                    if (item.ParentID == 0)
                    {
                        ResourceType model = new ResourceType();
                        model.ID = item.ID;
                        model.CodeName = item.CodeName;
                        model.ParentID = item.ParentID;
                        model.Child = new List<ResourceType>();
                        treeList.Add(model);
                        ///递归获取子集列表///
                        LoopToAppendChildrenForResourceType(list, model);
                    }
                }
                return treeList;
            }
        }

        #region 递归获取资源类型的子集
        private static void LoopToAppendChildrenForResourceType(List<tb_Code_TreeTable2> list, ResourceType resourceType)
        {
            foreach (var item in list)
            {
                if (item.ParentID== resourceType.ID)
                {
                    ResourceType model = new ResourceType();
                    model.ID = item.ID;
                    model.CodeName = item.CodeName;
                    model.ParentID = item.ParentID;
                    model.Child = new List<ResourceType>();
                    resourceType.Child.Add(model);
                    ///递归获取子集列表///
                    LoopToAppendChildrenForResourceType(list, model);
                }
            }
        } 
        #endregion
    }
}
