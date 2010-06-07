using System.Collections.Generic;
using 题 = DAL.Database.Tables.题;

namespace QuestionGen.Modules
{
    public partial class 选择题
    {
        public 题.题 题 { get; set; }
        public List<题.题_选择_选项> 选项 { get; set; }
        public List<题.题_选择_答案> 答案 { get; set; }
    }
}