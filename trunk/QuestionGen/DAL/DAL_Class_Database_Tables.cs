using System;
using System.Collections.Generic;

namespace DAL.Database.Tables.人
{

	/// <summary>
	/// 
	/// </summary>
    public partial class 岗位
    {
		/// <summary>
		/// 
		/// </summary>
        public int        岗位编号 { get; set; }
		/// <summary>
		/// 
		/// </summary>
        public string     名称   { get; set; }
		/// <summary>
		/// 
		/// </summary>
        public string     备注   { get; set; }
    }
	/// <summary>
	/// 
	/// </summary>
    public partial class 岗位_知识面
    {
		/// <summary>
		/// 
		/// </summary>
        public int        岗位编号  { get; set; }
		/// <summary>
		/// 
		/// </summary>
        public int        知识面编号 { get; set; }
		/// <summary>
		/// 
		/// </summary>
        public int        深度    { get; set; }
    }
}
namespace DAL.Database.Tables.题
{

	/// <summary>
	/// 
	/// </summary>
    public partial class 附件
    {
		/// <summary>
		/// 
		/// </summary>
        public Guid       附件编号  { get; set; }
		/// <summary>
		/// 
		/// </summary>
        public int        题编号   { get; set; }
		/// <summary>
		/// 是为图片,否为视频
		/// </summary>
        public bool       是否为图片 { get; set; }
		/// <summary>
		/// 
		/// </summary>
        public byte[]     数据    { get; set; }
    }
	/// <summary>
	/// 
	/// </summary>
    public partial class 类型
    {
		/// <summary>
		/// 
		/// </summary>
        public int        类型编号     { get; set; }
		/// <summary>
		/// 
		/// </summary>
        public string     类型名      { get; set; }
		/// <summary>
		/// 
		/// </summary>
        public bool       是否需要专业阅卷 { get; set; }
    }
	/// <summary>
	/// 
	/// </summary>
    public partial class 题
    {
		/// <summary>
		/// 
		/// </summary>
        public int        题编号   { get; set; }
		/// <summary>
		/// 
		/// </summary>
        public int?       新版题编号 { get; set; }
		/// <summary>
		/// 
		/// </summary>
        public int        类型编号  { get; set; }
		/// <summary>
		/// 
		/// </summary>
        public int        知识面编号 { get; set; }
		/// <summary>
		/// 1 - 100
		/// </summary>
        public int        难度系数  { get; set; }
		/// <summary>
		/// 
		/// </summary>
        public string     显示模板  { get; set; }
		/// <summary>
		/// 
		/// </summary>
        public string     考核意图  { get; set; }
		/// <summary>
		/// 
		/// </summary>
        public string     备注    { get; set; }
		/// <summary>
		/// 
		/// </summary>
        public DateTime   更新时间  { get; set; }
		/// <summary>
		/// 
		/// </summary>
        public bool       是否启用  { get; set; }
    }
	/// <summary>
	/// 
	/// </summary>
    public partial class 题_连线_答案
    {
		/// <summary>
		/// 
		/// </summary>
        public int        题编号   { get; set; }
		/// <summary>
		/// 
		/// </summary>
        public int        连线序号A { get; set; }
		/// <summary>
		/// 
		/// </summary>
        public int        连线序号B { get; set; }
    }
	/// <summary>
	/// 
	/// </summary>
    public partial class 题_连线_选项
    {
		/// <summary>
		/// 
		/// </summary>
        public int        题编号  { get; set; }
		/// <summary>
		/// 
		/// </summary>
        public int        连线序号 { get; set; }
		/// <summary>
		/// 
		/// </summary>
        public int        组序号  { get; set; }
		/// <summary>
		/// 
		/// </summary>
        public string     显示模板 { get; set; }
    }
	/// <summary>
	/// 
	/// </summary>
    public partial class 题_判断_答案
    {
		/// <summary>
		/// 
		/// </summary>
        public int        题编号 { get; set; }
		/// <summary>
		/// 
		/// </summary>
        public bool       答案  { get; set; }
    }
	/// <summary>
	/// 
	/// </summary>
    public partial class 题_填空_答案
    {
		/// <summary>
		/// 
		/// </summary>
        public int        题编号  { get; set; }
		/// <summary>
		/// 由显示模板中 <c/> 的出现顺序而定, 从 1 开始
		/// </summary>
        public int        格子序号 { get; set; }
		/// <summary>
		/// 
		/// </summary>
        public string     显示模板 { get; set; }
    }
	/// <summary>
	/// 
	/// </summary>
    public partial class 题_问答_答案
    {
		/// <summary>
		/// 
		/// </summary>
        public int        题编号  { get; set; }
		/// <summary>
		/// 
		/// </summary>
        public string     参考答案 { get; set; }
    }
	/// <summary>
	/// 
	/// </summary>
    public partial class 题_选择_答案
    {
		/// <summary>
		/// 
		/// </summary>
        public int        题编号  { get; set; }
		/// <summary>
		/// 
		/// </summary>
        public int        选项序号 { get; set; }
		/// <summary>
		/// 
		/// </summary>
        public int        格子序号 { get; set; }
    }
	/// <summary>
	/// 
	/// </summary>
    public partial class 题_选择_选项
    {
		/// <summary>
		/// 
		/// </summary>
        public int        题编号  { get; set; }
		/// <summary>
		/// 
		/// </summary>
        public int        选项序号 { get; set; }
		/// <summary>
		/// 
		/// </summary>
        public string     显示模板 { get; set; }
    }
	/// <summary>
	/// 
	/// </summary>
    public partial class 知识面
    {
		/// <summary>
		/// 
		/// </summary>
        public int        知识面编号 { get; set; }
		/// <summary>
		/// 
		/// </summary>
        public string     名称    { get; set; }
    }
}