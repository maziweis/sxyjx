//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataBase
{
    using System;
    using System.Collections.Generic;
    
    public partial class tb_StandardBook
    {
        public int ID { get; set; }
        public int Stage { get; set; }
        public int Subject { get; set; }
        public int Grade { get; set; }
        public int Booklet { get; set; }
        public int Edition { get; set; }
        public string BooKName { get; set; }
        public string Remark { get; set; }
        public Nullable<System.DateTime> CreateDateTime { get; set; }
        public Nullable<System.DateTime> ModifyDateTime { get; set; }
        public int Deleted { get; set; }
        public int BookType { get; set; }
        public string AgeRange { get; set; }
    }
}
