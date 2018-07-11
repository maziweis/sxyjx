using Common;
using DataBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SyncFileResource
{
    public partial class Form1 : Form
    {
        private List<tb_Files> filelist;

        private List<tb_Resource> resourcelist;

        public Form1()
        {
            InitializeComponent();
        }

        private void SelectFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "文本文件(*.txt)|*.txt|所有文件(*.*)|*.*";
            dialog.RestoreDirectory = true;
            dialog.FilterIndex = 1;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                Filetext.Text = dialog.FileName;
                string text = File.ReadAllText(Filetext.Text);
                filelist = JsonHelper.JosnDeserializer<tb_Files>(text);
            }

        }

        private void SelectResource_Click(object sender, EventArgs e)
        {

            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "文本文件(*.txt)|*.txt|所有文件(*.*)|*.*";
            dialog.RestoreDirectory = true;
            dialog.FilterIndex = 1;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                Resourcetext.Text = dialog.FileName;
                string text = File.ReadAllText(Resourcetext.Text);
                resourcelist = JsonHelper.JosnDeserializer<tb_Resource>(text);
            }
        }

        private void UploadDataBase_Click(object sender, EventArgs e)
        {
            using (var db = new ModMetaEntities())
            {
                using (var dbres = new MODResourceEntities())
                {
                    using (var dbfile = new MODkingfilesEntities())
                    {
                        /////////////先增加文件信息//////////////
                        foreach (tb_Files file in filelist)
                        {
                            tb_Files tempfile = dbfile.tb_Files.Find(file.ID);
                            if (tempfile == null)
                            {
                                dbfile.tb_Files.Add(file);
                            }
                        }
                        dbfile.SaveChanges();
                        /////////////再增加资源信息//////////////
                        foreach (tb_Resource resource in resourcelist)
                        {
                            tb_Resource tempresource = dbres.tb_Resource.Find(resource.ID);
                            if (tempresource == null)
                            {
                                if (resource.Edition == 66 && resource.Grade == 3 && resource.BookReel == 2 && resource.Subject == 1) {
                                    ViewMod_Trunt_Branch tbmodel = db.ViewMod_Trunt_Branch.Where(w => w.tID == resource.Catalog).FirstOrDefault();
                                    resource.Catalog = tbmodel.bID;                                   
                                }
                                resource.ResourceClass = 1;
                                dbres.tb_Resource.Add(resource);

                            }
                        }
                        dbres.SaveChanges();
                        MessageBox.Show("操作成功");
                    }
                }
            }            
        }
    }
}
