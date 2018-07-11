using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExportModResourceTool
{
    public class Resource
    {
        public System.Guid ID { get; set; }
        public double? Number { get; set; }
        public string Title { get; set; }
        public int? SchoolStage { get; set; }
        public int? Grade { get; set; }
        public int? Subject { get; set; }
        public int? Edition { get; set; }
        public int? BookReel { get; set; }
        public int? Catalog { get; set; }
        public int? ResourceType { get; set; }
        public int? ResourceStyle { get; set; }
        public int? ResourceLevel { get; set; }
        public string KeyWords { get; set; }
        public int? TeachingStep { get; set; }
        public int? TeachingStyle { get; set; }
        public System.Guid FileID { get; set; }
        public string Description { get; set; }
        public int? IsVerify { get; set; }
        public int? Purview { get; set; }
        public System.DateTime? UploadDate { get; set; }
        public string UploadUser { get; set; }
        public int? IsDelete { get; set; }
        public int? DownCounts { get; set; }
        public int? ScanCounts { get; set; }
        public string FileType { get; set; }
        public string BreviaryImgUrl { get; set; }
        public decimal? ResourceSize { get; set; }
        public int? IsRecommend { get; set; }
        public System.Guid? MaterialID { get; set; }
        public string ComeFrom { get; set; }
        public int? AppraisedCounts { get; set; }
        public int? CollectCounts { get; set; }
        public string TeachingModules { get; set; }
        public string Applicable { get; set; }
        public System.DateTime? ModifyDate { get; set; }
        public int? Copyright { get; set; }
        public string CopyrightName { get; set; }
        public long Sort { get; set; }
        public int? TimeSpan { get; set; }
        public int? ResourceClass { get; set; }
    }
}
