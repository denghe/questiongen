using System.Collections.Generic;
using 题 = DAL.Database.Tables.题;

namespace QuestionGen.Modules
{
    public partial class 填空题
    {
        public 题.题 题 { get; set; }
        public List<题.题_填空_答案> 答案 { get; set; }
    }
}