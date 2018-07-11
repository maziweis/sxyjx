namespace SyncFileResource
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.Filetext = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SelectFile = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.Resourcetext = new System.Windows.Forms.TextBox();
            this.SelectResource = new System.Windows.Forms.Button();
            this.UploadDataBase = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Filetext
            // 
            this.Filetext.Location = new System.Drawing.Point(102, 28);
            this.Filetext.Name = "Filetext";
            this.Filetext.Size = new System.Drawing.Size(410, 21);
            this.Filetext.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(33, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "文件地址";
            // 
            // SelectFile
            // 
            this.SelectFile.Location = new System.Drawing.Point(593, 21);
            this.SelectFile.Name = "SelectFile";
            this.SelectFile.Size = new System.Drawing.Size(98, 33);
            this.SelectFile.TabIndex = 2;
            this.SelectFile.Text = "选择文件";
            this.SelectFile.UseVisualStyleBackColor = true;
            this.SelectFile.Click += new System.EventHandler(this.SelectFile_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(33, 92);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "资源地址";
            // 
            // Resourcetext
            // 
            this.Resourcetext.Location = new System.Drawing.Point(102, 91);
            this.Resourcetext.Name = "Resourcetext";
            this.Resourcetext.Size = new System.Drawing.Size(410, 21);
            this.Resourcetext.TabIndex = 0;
            // 
            // SelectResource
            // 
            this.SelectResource.Location = new System.Drawing.Point(590, 82);
            this.SelectResource.Name = "SelectResource";
            this.SelectResource.Size = new System.Drawing.Size(98, 33);
            this.SelectResource.TabIndex = 2;
            this.SelectResource.Text = "选择资源";
            this.SelectResource.UseVisualStyleBackColor = true;
            this.SelectResource.Click += new System.EventHandler(this.SelectResource_Click);
            // 
            // UploadDataBase
            // 
            this.UploadDataBase.Location = new System.Drawing.Point(82, 169);
            this.UploadDataBase.Name = "UploadDataBase";
            this.UploadDataBase.Size = new System.Drawing.Size(116, 41);
            this.UploadDataBase.TabIndex = 3;
            this.UploadDataBase.Text = "上传数据库";
            this.UploadDataBase.UseVisualStyleBackColor = true;
            this.UploadDataBase.Click += new System.EventHandler(this.UploadDataBase_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(783, 385);
            this.Controls.Add(this.UploadDataBase);
            this.Controls.Add(this.SelectResource);
            this.Controls.Add(this.SelectFile);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Resourcetext);
            this.Controls.Add(this.Filetext);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox Filetext;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button SelectFile;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox Resourcetext;
        private System.Windows.Forms.Button SelectResource;
        private System.Windows.Forms.Button UploadDataBase;
    }
}

