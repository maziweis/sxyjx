using DataBase;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ExportModResourceTool
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Resource> resourceList = new List<Resource>();
        private List<Resource> tempresourceList;
        private string srcbaseUrl;
        private string desbaseUrl;
        private string savaUrl;
        public MainWindow()
        {
            InitializeComponent();
            using (var db = new ModMetaEntities())
            {
                List<tb_Code_ListTable2> subjectList = db.tb_Code_ListTable2.Where(w => w.Deleted == 0).OrderBy(o => o.Seq).ToList();
                comboSubject.ItemsSource = subjectList;
                comboSubject.SelectedValuePath = "ID";
                comboSubject.DisplayMemberPath = "CodeName";
                List<tb_Code_SchoolStage> stageList = db.tb_Code_SchoolStage.Where(w => w.Deleted == 0).OrderBy(o => o.Seq).ToList();
                comboStage.ItemsSource = stageList;
                comboStage.SelectedValuePath = "ID";
                comboStage.DisplayMemberPath = "CodeName";
                List<tb_Code_BookReel> breelList = db.tb_Code_BookReel.Where(w => w.Deleted == 0).OrderBy(o => o.Seq).ToList();
                breelList.Insert(0, new tb_Code_BookReel { ID = 0, CodeName = "--全部--" });
                comboBrlee.ItemsSource = breelList;
                comboBrlee.SelectedValuePath = "ID";
                comboBrlee.DisplayMemberPath = "CodeName";
            }

        }

        private void comboSubject_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            using (var db = new ModMetaEntities())
            {
                int[] edis = db.tb_Code_ListTable3_Relationship.Where(w => w.End_ID == (int)comboSubject.SelectedValue).Select(s => s.Start_ID).ToArray();
                List<tb_Code_ListTable3> editionList = db.tb_Code_ListTable3.Where(w => w.Deleted == 0 && edis.Contains(w.ID)).OrderBy(o => o.Seq).ToList();
                comboEdition.ItemsSource = editionList;
                comboEdition.SelectedValuePath = "ID";
                comboEdition.DisplayMemberPath = "CodeName";
            }
            TotalResource();
        }

        private void comboStage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            using (var db = new ModMetaEntities())
            {
                int[] grades = db.tb_Code_ListTable1_Relationship.Where(w => w.End_ID == (int)comboStage.SelectedValue).Select(s => s.Start_ID).ToArray();
                List<tb_Code_ListTable1> gradeList = db.tb_Code_ListTable1.Where(w => w.Deleted == 0 && grades.Contains(w.ID)).OrderBy(o => o.Seq).ToList();
                gradeList.Insert(0, new tb_Code_ListTable1 { ID = 0, CodeName = "--全部--" });
                comboGrade.ItemsSource = gradeList;
                comboGrade.SelectedValuePath = "ID";
                comboGrade.DisplayMemberPath = "CodeName";
            }
            TotalResource();
        }

        private void comboEdition_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TotalResource();
        }

        private void comboGrade_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TotalResource();
        }

        private void comboBrlee_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TotalResource();
        }
        /// <summary>
        /// 统计资源
        /// </summary>
        public void TotalResource()
        {
            using (var db = new MODResourceEntities())
            {
                IQueryable<tb_Resource> query = db.tb_Resource.Where(w => w.IsDelete == 0);
                if (comboSubject.SelectedValue != null)
                {
                    query = query.Where(w => w.Subject == (int)comboSubject.SelectedValue);
                }
                if (comboStage.SelectedValue != null)
                {
                    query = query.Where(w => w.SchoolStage == (int)comboStage.SelectedValue);
                }
                if (comboGrade.SelectedValue != null && (int)comboGrade.SelectedValue != 0)
                {
                    query = query.Where(w => w.Grade == (int)comboGrade.SelectedValue);
                }
                if (comboEdition.SelectedValue != null)
                {
                    query = query.Where(w => w.Edition == (int)comboEdition.SelectedValue);
                }
                if (comboBrlee.SelectedValue != null && (int)comboBrlee.SelectedValue != 0)
                {
                    query = query.Where(w => w.BookReel == (int)comboBrlee.SelectedValue);
                }
                resCount.Text = query.Count().ToString();
                tempresourceList = query.Select(s => new Resource
                {
                    Applicable = s.Applicable,
                    AppraisedCounts = s.AppraisedCounts,
                    BookReel = s.BookReel,
                    BreviaryImgUrl = s.BreviaryImgUrl,
                    Catalog = s.Catalog,
                    CollectCounts = s.CollectCounts,
                    ComeFrom = s.ComeFrom,
                    Copyright = s.Copyright,
                    CopyrightName = s.CopyrightName,
                    Description = s.Description,
                    DownCounts = s.DownCounts,
                    Edition = s.Edition,
                    ScanCounts = s.ScanCounts,
                    FileID = s.FileID,
                    FileType = s.FileType,
                    Grade = s.Grade,
                    ID = s.ID,
                    IsDelete = s.IsDelete,
                    IsRecommend = s.IsRecommend,
                    IsVerify = s.IsVerify,
                    KeyWords = s.KeyWords,
                    MaterialID = s.MaterialID,
                    ModifyDate = s.ModifyDate,
                    Number = s.Number,
                    Purview = s.Purview,
                    ResourceClass = s.ResourceClass,
                    ResourceLevel = s.ResourceLevel,
                    ResourceSize = s.ResourceSize,
                    ResourceStyle = s.ResourceStyle,
                    ResourceType = s.ResourceType,
                    SchoolStage = s.SchoolStage,
                    Sort = s.Sort,
                    Subject = s.Subject,
                    TeachingModules = s.TeachingModules,
                    TeachingStep = s.TeachingStep,
                    TeachingStyle = s.TeachingStyle,
                    TimeSpan = s.TimeSpan,
                    Title = s.Title,
                    UploadDate = s.UploadDate,
                    UploadUser = s.UploadUser
                }).ToList();
            }

        }
        /// <summary>
        /// 导出资源
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExportResource_Click(object sender, RoutedEventArgs e)
        {
            ExportResourceByLoading();
        }
        private string ExportResourceByLoading() {
            using (var db = new MODkingfilesEntities())
            {
                srcbaseUrl = ConfigurationManager.AppSettings["srcPath"];
                desbaseUrl = ConfigurationManager.AppSettings["desPath"];
                savaUrl = desbaseUrl + "/Temp/" + DateTime.Now.ToString("yyyyMMdd") + System.Guid.NewGuid().ToString();
                string imgurl = srcbaseUrl + "/Img/";
                string desimgUrl = savaUrl + "/Img/";
                if (!Directory.Exists(savaUrl))
                {
                    Directory.CreateDirectory(savaUrl);
                }
                if (!Directory.Exists(desimgUrl))
                {
                    Directory.CreateDirectory(desimgUrl);
                }               
                List<tb_Files> fileList = new List<tb_Files>();
                List<Resource> resList = new List<Resource>();
                foreach (Resource res in resourceList)
                {
                    tb_Files f = db.tb_Files.Find(res.FileID);
                    resList.Add(res);
                    fileList.Add(f);
                    string realfile = srcbaseUrl + "\\" + f.FilePath + "\\" + f.ID;
                    string desfile = savaUrl + "\\" + f.FilePath + "\\";
                    try
                    {
                        if (!Directory.Exists(desfile))
                        {
                            Directory.CreateDirectory(desfile);
                        }
                        if (File.Exists(imgurl + f.ID + ".jpg"))
                        {
                            File.Copy(imgurl + f.ID + ".jpg", desimgUrl + f.ID + ".jpg", true);
                        }
                        desfile = desfile + f.ID;
                        CopyFileToDestion(realfile, desfile, f.FileExtension);

                    }
                    catch (Exception ex)
                    {
                        OutErrorLog(realfile + ex.Message, savaUrl + "\\ErrorLog.txt");
                    }
                }
                OutLog(JsonHelper.EncodeJson(resList), savaUrl + "\\Tb_Resource.txt");
                OutLog(JsonHelper.EncodeJson(fileList), savaUrl + "\\Tb_File.txt");
               return savaUrl;
            }
        }
        private void CopyFileToDestion(string srcPath, string destPath, string FileType)
        {
            if (File.Exists(srcPath + "." + FileType))
            {
                File.Copy(srcPath + "." + FileType, destPath + "." + FileType, true);      //true表示可以覆盖同名文件
            }
            CopyDirectory(srcPath, destPath);
        }
        public void CopyDirectory(string srcPath, string destPath)
        {

            if (Directory.Exists(srcPath))
            {
                DirectoryInfo dir = new DirectoryInfo(srcPath);
                FileSystemInfo[] fileinfo = dir.GetFileSystemInfos();  //获取目录下（不包含子目录）的文件和子目录
                if (fileinfo.Length > 0)
                {
                    if (!Directory.Exists(destPath))
                    {
                        Directory.CreateDirectory(destPath);
                    }
                }
                foreach (FileSystemInfo i in fileinfo)
                {
                    if (i is DirectoryInfo)     //判断是否文件夹
                    {
                        if (!Directory.Exists(destPath + "\\" + i.Name))
                        {
                            Directory.CreateDirectory(destPath + "\\" + i.Name);   //目标目录下不存在此文件夹即创建子文件夹
                        }
                        CopyDirectory(i.FullName, destPath + "\\" + i.Name);    //递归调用复制子文件夹
                    }
                    else
                    {
                        File.Copy(i.FullName, destPath + "\\" + i.Name, true);      //不是文件夹即复制文件，true表示可以覆盖同名文件
                    }
                }
            }

        }
        /// <summary>
        /// 写入日志
        /// </summary>
        /// <param name="put"></param>
        public void OutLog(string info, string Path)
        {
            using (FileStream fsWrite = new FileStream(Path, FileMode.Append))
            {
                StreamWriter write = new StreamWriter(fsWrite, Encoding.UTF8);
                write.Write(info);
                write.Flush();
                write.Dispose();
                write.Close();
            };
        }
        /// <summary>
        /// 写入错误日志
        /// </summary>
        /// <param name="put"></param>
        public void OutErrorLog(string info, string Path)
        {
            using (FileStream fsWrite = new FileStream(Path, FileMode.Append))
            {
                StreamWriter write = new StreamWriter(fsWrite, Encoding.UTF8);
                write.WriteLine(info);
                write.Flush();
                write.Dispose();
                write.Close();
            };


        }
        /// <summary>
        /// 添加资源到导出队列
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddResourceToQueue_Click(object sender, RoutedEventArgs e)
        {
            if (tempresourceList == null || tempresourceList.Count == 0)
            {
                MessageBox.Show("没有资源加入到队列！");
                return;
            }
            else {
                ExportResource.IsEnabled = true;
                resourceList.AddRange(tempresourceList);
                AddResourceToQueue.Content = "加入队列("+ resourceList.Count + ")";

            }

        }
        /// <summary>
        /// 清除队列
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RemoveResourceToQueue_Click(object sender, RoutedEventArgs e)
        {
            resourceList.Clear();
            ExportResource.IsEnabled = false;
            AddResourceToQueue.Content = "加入队列(0)";
        }
    }
}
