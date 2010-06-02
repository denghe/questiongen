using System;
using System.Collections.Generic;
using SqlLib.ColumnEnums;

namespace DAL.ColumnEnums.Tables.人
{

    public partial class 岗位 : ColumnList<岗位>
    {
        public 岗位 岗位编号 { get { __columns.Add(0); return this; } }
        public 岗位 名称 { get { __columns.Add(1); return this; } }
        public 岗位 备注 { get { __columns.Add(2); return this; } }
        protected static string[] __cns = new string[]
        {
            @"岗位编号",
            @"名称",
            @"备注"
        };
        public override string GetColumnName(int i) {
            return __cns[i];
        }
    }
    public partial class 岗位_知识面 : ColumnList<岗位_知识面>
    {
        public 岗位_知识面 岗位编号 { get { __columns.Add(0); return this; } }
        public 岗位_知识面 知识面编号 { get { __columns.Add(1); return this; } }
        public 岗位_知识面 深度 { get { __columns.Add(2); return this; } }
        protected static string[] __cns = new string[]
        {
            @"岗位编号",
            @"知识面编号",
            @"深度"
        };
        public override string GetColumnName(int i) {
            return __cns[i];
        }
    }
}
namespace DAL.ColumnEnums.Tables.题
{

    public partial class 附件 : ColumnList<附件>
    {
        public 附件 附件编号 { get { __columns.Add(0); return this; } }
        public 附件 题编号 { get { __columns.Add(1); return this; } }
        public 附件 是否为图片 { get { __columns.Add(2); return this; } }
        public 附件 数据 { get { __columns.Add(3); return this; } }
        protected static string[] __cns = new string[]
        {
            @"附件编号",
            @"题编号",
            @"是否为图片",
            @"数据"
        };
        public override string GetColumnName(int i) {
            return __cns[i];
        }
    }
    public partial class 类型 : ColumnList<类型>
    {
        public 类型 类型编号 { get { __columns.Add(0); return this; } }
        public 类型 类型名 { get { __columns.Add(1); return this; } }
        public 类型 是否需要专业阅卷 { get { __columns.Add(2); return this; } }
        protected static string[] __cns = new string[]
        {
            @"类型编号",
            @"类型名",
            @"是否需要专业阅卷"
        };
        public override string GetColumnName(int i) {
            return __cns[i];
        }
    }
    public partial class 题 : ColumnList<题>
    {
        public 题 题编号 { get { __columns.Add(0); return this; } }
        public 题 新版题编号 { get { __columns.Add(1); return this; } }
        public 题 类型编号 { get { __columns.Add(2); return this; } }
        public 题 知识面编号 { get { __columns.Add(3); return this; } }
        public 题 难度系数 { get { __columns.Add(4); return this; } }
        public 题 显示模板 { get { __columns.Add(5); return this; } }
        public 题 考核意图 { get { __columns.Add(6); return this; } }
        public 题 备注 { get { __columns.Add(7); return this; } }
        public 题 更新时间 { get { __columns.Add(8); return this; } }
        public 题 是否启用 { get { __columns.Add(9); return this; } }
        protected static string[] __cns = new string[]
        {
            @"题编号",
            @"新版题编号",
            @"类型编号",
            @"知识面编号",
            @"难度系数",
            @"显示模板",
            @"考核意图",
            @"备注",
            @"更新时间",
            @"是否启用"
        };
        public override string GetColumnName(int i) {
            return __cns[i];
        }
    }
    public partial class 题_连线 : ColumnList<题_连线>
    {
        public 题_连线 连线编号 { get { __columns.Add(0); return this; } }
        public 题_连线 题编号 { get { __columns.Add(1); return this; } }
        public 题_连线 组序号 { get { __columns.Add(2); return this; } }
        public 题_连线 显示模板 { get { __columns.Add(3); return this; } }
        protected static string[] __cns = new string[]
        {
            @"连线编号",
            @"题编号",
            @"组序号",
            @"显示模板"
        };
        public override string GetColumnName(int i) {
            return __cns[i];
        }
    }
    public partial class 题_连线_答案 : ColumnList<题_连线_答案>
    {
        public 题_连线_答案 题编号 { get { __columns.Add(0); return this; } }
        public 题_连线_答案 连线编号A { get { __columns.Add(1); return this; } }
        public 题_连线_答案 连线编号B { get { __columns.Add(2); return this; } }
        protected static string[] __cns = new string[]
        {
            @"题编号",
            @"连线编号A",
            @"连线编号B"
        };
        public override string GetColumnName(int i) {
            return __cns[i];
        }
    }
    public partial class 题_判断 : ColumnList<题_判断>
    {
        public 题_判断 题编号 { get { __columns.Add(0); return this; } }
        public 题_判断 答案 { get { __columns.Add(1); return this; } }
        protected static string[] __cns = new string[]
        {
            @"题编号",
            @"答案"
        };
        public override string GetColumnName(int i) {
            return __cns[i];
        }
    }
    public partial class 题_填空_答案 : ColumnList<题_填空_答案>
    {
        public 题_填空_答案 题编号 { get { __columns.Add(0); return this; } }
        public 题_填空_答案 格子序号 { get { __columns.Add(1); return this; } }
        public 题_填空_答案 显示模板 { get { __columns.Add(2); return this; } }
        protected static string[] __cns = new string[]
        {
            @"题编号",
            @"格子序号",
            @"显示模板"
        };
        public override string GetColumnName(int i) {
            return __cns[i];
        }
    }
    public partial class 题_问答 : ColumnList<题_问答>
    {
        public 题_问答 题编号 { get { __columns.Add(0); return this; } }
        public 题_问答 参考答案 { get { __columns.Add(1); return this; } }
        protected static string[] __cns = new string[]
        {
            @"题编号",
            @"参考答案"
        };
        public override string GetColumnName(int i) {
            return __cns[i];
        }
    }
    public partial class 题_选择_答案 : ColumnList<题_选择_答案>
    {
        public 题_选择_答案 题编号 { get { __columns.Add(0); return this; } }
        public 题_选择_答案 选项编号 { get { __columns.Add(1); return this; } }
        public 题_选择_答案 格子序号 { get { __columns.Add(2); return this; } }
        protected static string[] __cns = new string[]
        {
            @"题编号",
            @"选项编号",
            @"格子序号"
        };
        public override string GetColumnName(int i) {
            return __cns[i];
        }
    }
    public partial class 题_选择_选项 : ColumnList<题_选择_选项>
    {
        public 题_选择_选项 选项编号 { get { __columns.Add(0); return this; } }
        public 题_选择_选项 题编号 { get { __columns.Add(1); return this; } }
        public 题_选择_选项 显示模板 { get { __columns.Add(2); return this; } }
        public 题_选择_选项 排序 { get { __columns.Add(3); return this; } }
        protected static string[] __cns = new string[]
        {
            @"选项编号",
            @"题编号",
            @"显示模板",
            @"排序"
        };
        public override string GetColumnName(int i) {
            return __cns[i];
        }
    }
    public partial class 知识面 : ColumnList<知识面>
    {
        public 知识面 知识面编号 { get { __columns.Add(0); return this; } }
        public 知识面 名称 { get { __columns.Add(1); return this; } }
        protected static string[] __cns = new string[]
        {
            @"知识面编号",
            @"名称"
        };
        public override string GetColumnName(int i) {
            return __cns[i];
        }
    }
}