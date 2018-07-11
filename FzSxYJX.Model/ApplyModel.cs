using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FzSxYJX.Model
{
    public class ApplyModel
    {
        /// <summary>
        /// 电影课
        /// </summary>
        public List<MovieModel> MoviceList { get; set; }

        public CourseAllModel CourseAllList { get; set; }


    }
    /// <summary>
    /// 电影课
    /// </summary>
  public class MovieModel {
        public string MoviceName { get; set; }
        public string Url { get; set; }
        public string ImageUrl { get; set; }
        public int ID { get; set; }
        public string SubjectID { get; set; }



    }
    public class CourseAllModel {
        /// <summary>
        /// 版本列表
        /// </summary>
        public List<Edition> EditionList { get; set; }
        /// <summary>
        /// 云课堂
        /// </summary>
        public List<CourseModel> CourseList { get; set; }
    }
    /// <summary>
    /// 版本
    /// </summary>
    public class Edition {
        public int EditionID { get; set; }
        public string EditionName { get; set; }
    }
    /// <summary>
    /// 云课堂
    /// </summary>
    public class CourseModel {
        public string CourseName { get; set; }
        public string ImageUrl { get; set; }
        public int EditionID { get; set; }
        public int ID { get; set; }
        public string GradeID { get; set; }   

        public bool IsOwnApply { get; set; }
    }
}
