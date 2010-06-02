using System;
using System.Collections.Generic;
using SqlLib.Orientations;

namespace DAL.Orientations.Tables.人
{

    public partial class 岗位 : LogicalNode<岗位>
    {
        public ExpNode<岗位> 岗位编号 { get { return this.New_Column(@"岗位编号"); } }
        public ExpNode<岗位> 名称 { get { return this.New_Column(@"名称"); } }
        public ExpNode<岗位> 备注 { get { return this.New_Column(@"备注"); } }
    }
    public partial class 岗位_知识面 : LogicalNode<岗位_知识面>
    {
        public ExpNode<岗位_知识面> 岗位编号 { get { return this.New_Column(@"岗位编号"); } }
        public ExpNode<岗位_知识面> 知识面编号 { get { return this.New_Column(@"知识面编号"); } }
        public ExpNode<岗位_知识面> 深度 { get { return this.New_Column(@"深度"); } }
    }
}
namespace DAL.Orientations.Tables.题
{

    public partial class 附件 : LogicalNode<附件>
    {
        public ExpNode<附件> 附件编号 { get { return this.New_Column(@"附件编号"); } }
        public ExpNode<附件> 题编号 { get { return this.New_Column(@"题编号"); } }
        public ExpNode<附件> 是否为图片 { get { return this.New_Column(@"是否为图片"); } }
        public ExpNode<附件> 数据 { get { return this.New_Column(@"数据"); } }
    }
    public partial class 类型 : LogicalNode<类型>
    {
        public ExpNode<类型> 类型编号 { get { return this.New_Column(@"类型编号"); } }
        public ExpNode<类型> 类型名 { get { return this.New_Column(@"类型名"); } }
        public ExpNode<类型> 是否需要专业阅卷 { get { return this.New_Column(@"是否需要专业阅卷"); } }
    }
    public partial class 题 : LogicalNode<题>
    {
        public ExpNode<题> 题编号 { get { return this.New_Column(@"题编号"); } }
        public ExpNode<题> 新版题编号 { get { return this.New_Column(@"新版题编号"); } }
        public ExpNode<题> 类型编号 { get { return this.New_Column(@"类型编号"); } }
        public ExpNode<题> 知识面编号 { get { return this.New_Column(@"知识面编号"); } }
        public ExpNode<题> 难度系数 { get { return this.New_Column(@"难度系数"); } }
        public ExpNode<题> 显示模板 { get { return this.New_Column(@"显示模板"); } }
        public ExpNode<题> 考核意图 { get { return this.New_Column(@"考核意图"); } }
        public ExpNode<题> 备注 { get { return this.New_Column(@"备注"); } }
        public ExpNode<题> 更新时间 { get { return this.New_Column(@"更新时间"); } }
        public ExpNode<题> 是否启用 { get { return this.New_Column(@"是否启用"); } }
    }
    public partial class 题_连线 : LogicalNode<题_连线>
    {
        public ExpNode<题_连线> 连线编号 { get { return this.New_Column(@"连线编号"); } }
        public ExpNode<题_连线> 题编号 { get { return this.New_Column(@"题编号"); } }
        public ExpNode<题_连线> 组序号 { get { return this.New_Column(@"组序号"); } }
        public ExpNode<题_连线> 显示模板 { get { return this.New_Column(@"显示模板"); } }
    }
    public partial class 题_连线_答案 : LogicalNode<题_连线_答案>
    {
        public ExpNode<题_连线_答案> 题编号 { get { return this.New_Column(@"题编号"); } }
        public ExpNode<题_连线_答案> 连线编号A { get { return this.New_Column(@"连线编号A"); } }
        public ExpNode<题_连线_答案> 连线编号B { get { return this.New_Column(@"连线编号B"); } }
    }
    public partial class 题_判断 : LogicalNode<题_判断>
    {
        public ExpNode<题_判断> 题编号 { get { return this.New_Column(@"题编号"); } }
        public ExpNode<题_判断> 答案 { get { return this.New_Column(@"答案"); } }
    }
    public partial class 题_填空_答案 : LogicalNode<题_填空_答案>
    {
        public ExpNode<题_填空_答案> 题编号 { get { return this.New_Column(@"题编号"); } }
        public ExpNode<题_填空_答案> 格子序号 { get { return this.New_Column(@"格子序号"); } }
        public ExpNode<题_填空_答案> 显示模板 { get { return this.New_Column(@"显示模板"); } }
    }
    public partial class 题_问答 : LogicalNode<题_问答>
    {
        public ExpNode<题_问答> 题编号 { get { return this.New_Column(@"题编号"); } }
        public ExpNode<题_问答> 参考答案 { get { return this.New_Column(@"参考答案"); } }
    }
    public partial class 题_选择_答案 : LogicalNode<题_选择_答案>
    {
        public ExpNode<题_选择_答案> 题编号 { get { return this.New_Column(@"题编号"); } }
        public ExpNode<题_选择_答案> 选项编号 { get { return this.New_Column(@"选项编号"); } }
        public ExpNode<题_选择_答案> 格子序号 { get { return this.New_Column(@"格子序号"); } }
    }
    public partial class 题_选择_选项 : LogicalNode<题_选择_选项>
    {
        public ExpNode<题_选择_选项> 选项编号 { get { return this.New_Column(@"选项编号"); } }
        public ExpNode<题_选择_选项> 题编号 { get { return this.New_Column(@"题编号"); } }
        public ExpNode<题_选择_选项> 显示模板 { get { return this.New_Column(@"显示模板"); } }
        public ExpNode<题_选择_选项> 排序 { get { return this.New_Column(@"排序"); } }
    }
    public partial class 知识面 : LogicalNode<知识面>
    {
        public ExpNode<知识面> 知识面编号 { get { return this.New_Column(@"知识面编号"); } }
        public ExpNode<知识面> 名称 { get { return this.New_Column(@"名称"); } }
    }
}