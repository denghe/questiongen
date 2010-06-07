using System;
using System.Collections.Generic;
using SqlLib.Expressions;

namespace DAL.Expressions.Tables.人
{

    public partial class 岗位 : LogicalNode<岗位>
    {
        public ExpNode_Int32<岗位> 岗位编号 { get { return this.New_Int32(@"岗位编号"); } }
        public ExpNode_String<岗位> 名称 { get { return this.New_String(@"名称"); } }
        public ExpNode_String<岗位> 备注 { get { return this.New_String(@"备注"); } }
    }
    public partial class 岗位_知识面 : LogicalNode<岗位_知识面>
    {
        public ExpNode_Int32<岗位_知识面> 岗位编号 { get { return this.New_Int32(@"岗位编号"); } }
        public ExpNode_Int32<岗位_知识面> 知识面编号 { get { return this.New_Int32(@"知识面编号"); } }
        public ExpNode_Int32<岗位_知识面> 深度 { get { return this.New_Int32(@"深度"); } }
    }
}
namespace DAL.Expressions.Tables.题
{

    public partial class 附件 : LogicalNode<附件>
    {
        public ExpNode_Guid<附件> 附件编号 { get { return this.New_Guid(@"附件编号"); } }
        public ExpNode_Int32<附件> 题编号 { get { return this.New_Int32(@"题编号"); } }
        public ExpNode_Boolean<附件> 是否为图片 { get { return this.New_Boolean(@"是否为图片"); } }
        public ExpNode_Bytes<附件> 数据 { get { return this.New_Bytes(@"数据"); } }
    }
    public partial class 类型 : LogicalNode<类型>
    {
        public ExpNode_Int32<类型> 类型编号 { get { return this.New_Int32(@"类型编号"); } }
        public ExpNode_String<类型> 类型名 { get { return this.New_String(@"类型名"); } }
        public ExpNode_Boolean<类型> 是否需要专业阅卷 { get { return this.New_Boolean(@"是否需要专业阅卷"); } }
    }
    public partial class 题 : LogicalNode<题>
    {
        public ExpNode_Int32<题> 题编号 { get { return this.New_Int32(@"题编号"); } }
        public ExpNode_Nullable_Int32<题> 新版题编号 { get { return this.New_Nullable_Int32(@"新版题编号"); } }
        public ExpNode_Int32<题> 类型编号 { get { return this.New_Int32(@"类型编号"); } }
        public ExpNode_Int32<题> 知识面编号 { get { return this.New_Int32(@"知识面编号"); } }
        public ExpNode_Int32<题> 难度系数 { get { return this.New_Int32(@"难度系数"); } }
        public ExpNode_String<题> 显示模板 { get { return this.New_String(@"显示模板"); } }
        public ExpNode_String<题> 考核意图 { get { return this.New_String(@"考核意图"); } }
        public ExpNode_String<题> 备注 { get { return this.New_String(@"备注"); } }
        public ExpNode_DateTime<题> 更新时间 { get { return this.New_DateTime(@"更新时间"); } }
        public ExpNode_Boolean<题> 是否启用 { get { return this.New_Boolean(@"是否启用"); } }
    }
    public partial class 题_连线 : LogicalNode<题_连线>
    {
        public ExpNode_Int32<题_连线> 连线编号 { get { return this.New_Int32(@"连线编号"); } }
        public ExpNode_Int32<题_连线> 题编号 { get { return this.New_Int32(@"题编号"); } }
        public ExpNode_Int32<题_连线> 组序号 { get { return this.New_Int32(@"组序号"); } }
        public ExpNode_String<题_连线> 显示模板 { get { return this.New_String(@"显示模板"); } }
    }
    public partial class 题_连线_答案 : LogicalNode<题_连线_答案>
    {
        public ExpNode_Int32<题_连线_答案> 题编号 { get { return this.New_Int32(@"题编号"); } }
        public ExpNode_Int32<题_连线_答案> 连线编号A { get { return this.New_Int32(@"连线编号A"); } }
        public ExpNode_Int32<题_连线_答案> 连线编号B { get { return this.New_Int32(@"连线编号B"); } }
    }
    public partial class 题_判断 : LogicalNode<题_判断>
    {
        public ExpNode_Int32<题_判断> 题编号 { get { return this.New_Int32(@"题编号"); } }
        public ExpNode_Boolean<题_判断> 答案 { get { return this.New_Boolean(@"答案"); } }
    }
    public partial class 题_填空_答案 : LogicalNode<题_填空_答案>
    {
        public ExpNode_Int32<题_填空_答案> 题编号 { get { return this.New_Int32(@"题编号"); } }
        public ExpNode_Int32<题_填空_答案> 格子序号 { get { return this.New_Int32(@"格子序号"); } }
        public ExpNode_String<题_填空_答案> 显示模板 { get { return this.New_String(@"显示模板"); } }
    }
    public partial class 题_问答 : LogicalNode<题_问答>
    {
        public ExpNode_Int32<题_问答> 题编号 { get { return this.New_Int32(@"题编号"); } }
        public ExpNode_String<题_问答> 参考答案 { get { return this.New_String(@"参考答案"); } }
    }
    public partial class 题_选择_答案 : LogicalNode<题_选择_答案>
    {
        public ExpNode_Int32<题_选择_答案> 题编号 { get { return this.New_Int32(@"题编号"); } }
        public ExpNode_Int32<题_选择_答案> 选项编号 { get { return this.New_Int32(@"选项编号"); } }
        public ExpNode_Int32<题_选择_答案> 格子序号 { get { return this.New_Int32(@"格子序号"); } }
    }
    public partial class 题_选择_选项 : LogicalNode<题_选择_选项>
    {
        public ExpNode_Int32<题_选择_选项> 选项编号 { get { return this.New_Int32(@"选项编号"); } }
        public ExpNode_Int32<题_选择_选项> 题编号 { get { return this.New_Int32(@"题编号"); } }
        public ExpNode_String<题_选择_选项> 显示模板 { get { return this.New_String(@"显示模板"); } }
        public ExpNode_Int32<题_选择_选项> 排序 { get { return this.New_Int32(@"排序"); } }
    }
    public partial class 知识面 : LogicalNode<知识面>
    {
        public ExpNode_Int32<知识面> 知识面编号 { get { return this.New_Int32(@"知识面编号"); } }
        public ExpNode_String<知识面> 名称 { get { return this.New_String(@"名称"); } }
    }
}