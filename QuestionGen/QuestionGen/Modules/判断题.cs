using System.Collections.Generic;
using 题 = DAL.Database.Tables.题;

namespace QuestionGen.Modules
{
    public partial class 判断题
    {
        public 题.题 题 { get; set; }
        public 题.题_判断_答案 答案 { get; set; }
    }
}