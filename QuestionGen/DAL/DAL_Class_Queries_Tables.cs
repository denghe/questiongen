using System;
using System.Collections.Generic;
using SqlLib.Queries;

namespace DAL.Queries.Tables.人
{

    public partial class 岗位 : Query<岗位, Expressions.Tables.人.岗位, Orientations.Tables.人.岗位, ColumnEnums.Tables.人.岗位>
    {
        public override string ToSqlString(string schema = null, string name = null, List<string> columns = null)
        {
            return base.ToSqlString(schema ?? @"人", name ?? @"岗位", columns);
        }
    }
    public partial class 岗位_知识面 : Query<岗位_知识面, Expressions.Tables.人.岗位_知识面, Orientations.Tables.人.岗位_知识面, ColumnEnums.Tables.人.岗位_知识面>
    {
        public override string ToSqlString(string schema = null, string name = null, List<string> columns = null)
        {
            return base.ToSqlString(schema ?? @"人", name ?? @"岗位_知识面", columns);
        }
    }
}
namespace DAL.Queries.Tables.题
{

    public partial class 附件 : Query<附件, Expressions.Tables.题.附件, Orientations.Tables.题.附件, ColumnEnums.Tables.题.附件>
    {
        public override string ToSqlString(string schema = null, string name = null, List<string> columns = null)
        {
            return base.ToSqlString(schema ?? @"题", name ?? @"附件", columns);
        }
    }
    public partial class 类型 : Query<类型, Expressions.Tables.题.类型, Orientations.Tables.题.类型, ColumnEnums.Tables.题.类型>
    {
        public override string ToSqlString(string schema = null, string name = null, List<string> columns = null)
        {
            return base.ToSqlString(schema ?? @"题", name ?? @"类型", columns);
        }
    }
    public partial class 题 : Query<题, Expressions.Tables.题.题, Orientations.Tables.题.题, ColumnEnums.Tables.题.题>
    {
        public override string ToSqlString(string schema = null, string name = null, List<string> columns = null)
        {
            return base.ToSqlString(schema ?? @"题", name ?? @"题", columns);
        }
    }
    public partial class 题_连线 : Query<题_连线, Expressions.Tables.题.题_连线, Orientations.Tables.题.题_连线, ColumnEnums.Tables.题.题_连线>
    {
        public override string ToSqlString(string schema = null, string name = null, List<string> columns = null)
        {
            return base.ToSqlString(schema ?? @"题", name ?? @"题_连线", columns);
        }
    }
    public partial class 题_连线_答案 : Query<题_连线_答案, Expressions.Tables.题.题_连线_答案, Orientations.Tables.题.题_连线_答案, ColumnEnums.Tables.题.题_连线_答案>
    {
        public override string ToSqlString(string schema = null, string name = null, List<string> columns = null)
        {
            return base.ToSqlString(schema ?? @"题", name ?? @"题_连线_答案", columns);
        }
    }
    public partial class 题_判断 : Query<题_判断, Expressions.Tables.题.题_判断, Orientations.Tables.题.题_判断, ColumnEnums.Tables.题.题_判断>
    {
        public override string ToSqlString(string schema = null, string name = null, List<string> columns = null)
        {
            return base.ToSqlString(schema ?? @"题", name ?? @"题_判断", columns);
        }
    }
    public partial class 题_填空_答案 : Query<题_填空_答案, Expressions.Tables.题.题_填空_答案, Orientations.Tables.题.题_填空_答案, ColumnEnums.Tables.题.题_填空_答案>
    {
        public override string ToSqlString(string schema = null, string name = null, List<string> columns = null)
        {
            return base.ToSqlString(schema ?? @"题", name ?? @"题_填空_答案", columns);
        }
    }
    public partial class 题_问答 : Query<题_问答, Expressions.Tables.题.题_问答, Orientations.Tables.题.题_问答, ColumnEnums.Tables.题.题_问答>
    {
        public override string ToSqlString(string schema = null, string name = null, List<string> columns = null)
        {
            return base.ToSqlString(schema ?? @"题", name ?? @"题_问答", columns);
        }
    }
    public partial class 题_选择_答案 : Query<题_选择_答案, Expressions.Tables.题.题_选择_答案, Orientations.Tables.题.题_选择_答案, ColumnEnums.Tables.题.题_选择_答案>
    {
        public override string ToSqlString(string schema = null, string name = null, List<string> columns = null)
        {
            return base.ToSqlString(schema ?? @"题", name ?? @"题_选择_答案", columns);
        }
    }
    public partial class 题_选择_选项 : Query<题_选择_选项, Expressions.Tables.题.题_选择_选项, Orientations.Tables.题.题_选择_选项, ColumnEnums.Tables.题.题_选择_选项>
    {
        public override string ToSqlString(string schema = null, string name = null, List<string> columns = null)
        {
            return base.ToSqlString(schema ?? @"题", name ?? @"题_选择_选项", columns);
        }
    }
    public partial class 知识面 : Query<知识面, Expressions.Tables.题.知识面, Orientations.Tables.题.知识面, ColumnEnums.Tables.题.知识面>
    {
        public override string ToSqlString(string schema = null, string name = null, List<string> columns = null)
        {
            return base.ToSqlString(schema ?? @"题", name ?? @"知识面", columns);
        }
    }
}