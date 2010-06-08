using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

using SqlLib;

namespace DAL.Database.Tables
{
    public static partial class ___Extensions
    {
        #region 人.岗位

        #region Insert

		public static int Insert(this Database.Tables.人.岗位 o, ColumnEnums.Tables.人.岗位.Handler insertCols = null, ColumnEnums.Tables.人.岗位.Handler fillCols = null, bool isFillAfterInsert = true)
		{
            return Database.Tables.人.岗位.Insert(o, insertCols, fillCols, isFillAfterInsert);
		}
        #endregion

        #region Update

		public static int Update(this Database.Tables.人.岗位 o, Expressions.Tables.人.岗位.Handler eh = null, ColumnEnums.Tables.人.岗位.Handler updateCols = null, ColumnEnums.Tables.人.岗位.Handler fillCols = null, bool isFillAfterUpdate = true)
		{
            if (eh == null) eh = new Expressions.Tables.人.岗位.Handler(a => a.岗位编号 == o.岗位编号);
            return Database.Tables.人.岗位.Update(o, eh, updateCols, fillCols, isFillAfterUpdate);
		}
        #endregion

        #region Delete

		public static int Delete(this Database.Tables.人.岗位 o, ColumnEnums.Tables.人.岗位.Handler conditionCols = null)
		{
            if(conditionCols == null) return Database.Tables.人.岗位.Delete(t =>
                t.岗位编号 == o.岗位编号
            );
            var cols = conditionCols(new DAL.ColumnEnums.Tables.人.岗位());
            var exp = new DAL.Expressions.Tables.人.岗位();
            if(cols.Contains(0)) exp.And(t => t.岗位编号 == o.岗位编号);
            if(cols.Contains(1)) exp.And(t => t.名称 == o.名称);
            if(cols.Contains(2)) exp.And(t => t.备注 == o.备注);
            return Database.Tables.人.岗位.Delete(exp);
		}

        #endregion

        #endregion

        #region 人.岗位_知识面

        #region Insert

		public static int Insert(this Database.Tables.人.岗位_知识面 o, ColumnEnums.Tables.人.岗位_知识面.Handler insertCols = null, ColumnEnums.Tables.人.岗位_知识面.Handler fillCols = null, bool isFillAfterInsert = true)
		{
            return Database.Tables.人.岗位_知识面.Insert(o, insertCols, fillCols, isFillAfterInsert);
		}
        #endregion

        #region Update

		public static int Update(this Database.Tables.人.岗位_知识面 o, Expressions.Tables.人.岗位_知识面.Handler eh = null, ColumnEnums.Tables.人.岗位_知识面.Handler updateCols = null, ColumnEnums.Tables.人.岗位_知识面.Handler fillCols = null, bool isFillAfterUpdate = true)
		{
            if (eh == null) eh = new Expressions.Tables.人.岗位_知识面.Handler(a => a.岗位编号 == o.岗位编号 & a.知识面编号 == o.知识面编号);
            return Database.Tables.人.岗位_知识面.Update(o, eh, updateCols, fillCols, isFillAfterUpdate);
		}
        #endregion

        #region Delete

		public static int Delete(this Database.Tables.人.岗位_知识面 o, ColumnEnums.Tables.人.岗位_知识面.Handler conditionCols = null)
		{
            if(conditionCols == null) return Database.Tables.人.岗位_知识面.Delete(t =>
                t.岗位编号 == o.岗位编号 &
                t.知识面编号 == o.知识面编号
            );
            var cols = conditionCols(new DAL.ColumnEnums.Tables.人.岗位_知识面());
            var exp = new DAL.Expressions.Tables.人.岗位_知识面();
            if(cols.Contains(0)) exp.And(t => t.岗位编号 == o.岗位编号);
            if(cols.Contains(1)) exp.And(t => t.知识面编号 == o.知识面编号);
            if(cols.Contains(2)) exp.And(t => t.深度 == o.深度);
            return Database.Tables.人.岗位_知识面.Delete(exp);
		}

        #endregion

        #endregion

        #region 题.附件

        #region Insert

		public static int Insert(this Database.Tables.题.附件 o, ColumnEnums.Tables.题.附件.Handler insertCols = null, ColumnEnums.Tables.题.附件.Handler fillCols = null, bool isFillAfterInsert = true)
		{
            return Database.Tables.题.附件.Insert(o, insertCols, fillCols, isFillAfterInsert);
		}
        #endregion

        #region Update

		public static int Update(this Database.Tables.题.附件 o, Expressions.Tables.题.附件.Handler eh = null, ColumnEnums.Tables.题.附件.Handler updateCols = null, ColumnEnums.Tables.题.附件.Handler fillCols = null, bool isFillAfterUpdate = true)
		{
            if (eh == null) eh = new Expressions.Tables.题.附件.Handler(a => a.附件编号 == o.附件编号);
            return Database.Tables.题.附件.Update(o, eh, updateCols, fillCols, isFillAfterUpdate);
		}
        #endregion

        #region Delete

		public static int Delete(this Database.Tables.题.附件 o, ColumnEnums.Tables.题.附件.Handler conditionCols = null)
		{
            if(conditionCols == null) return Database.Tables.题.附件.Delete(t =>
                t.附件编号 == o.附件编号
            );
            var cols = conditionCols(new DAL.ColumnEnums.Tables.题.附件());
            var exp = new DAL.Expressions.Tables.题.附件();
            if(cols.Contains(0)) exp.And(t => t.附件编号 == o.附件编号);
            if(cols.Contains(1)) exp.And(t => t.题编号 == o.题编号);
            if(cols.Contains(2)) exp.And(t => t.是否为图片 == o.是否为图片);
            if(cols.Contains(3)) exp.And(t => t.数据 == o.数据);
            return Database.Tables.题.附件.Delete(exp);
		}

        #endregion

        #endregion

        #region 题.类型

        #region Insert

		public static int Insert(this Database.Tables.题.类型 o, ColumnEnums.Tables.题.类型.Handler insertCols = null, ColumnEnums.Tables.题.类型.Handler fillCols = null, bool isFillAfterInsert = true)
		{
            return Database.Tables.题.类型.Insert(o, insertCols, fillCols, isFillAfterInsert);
		}
        #endregion

        #region Update

		public static int Update(this Database.Tables.题.类型 o, Expressions.Tables.题.类型.Handler eh = null, ColumnEnums.Tables.题.类型.Handler updateCols = null, ColumnEnums.Tables.题.类型.Handler fillCols = null, bool isFillAfterUpdate = true)
		{
            if (eh == null) eh = new Expressions.Tables.题.类型.Handler(a => a.类型编号 == o.类型编号);
            return Database.Tables.题.类型.Update(o, eh, updateCols, fillCols, isFillAfterUpdate);
		}
        #endregion

        #region Delete

		public static int Delete(this Database.Tables.题.类型 o, ColumnEnums.Tables.题.类型.Handler conditionCols = null)
		{
            if(conditionCols == null) return Database.Tables.题.类型.Delete(t =>
                t.类型编号 == o.类型编号
            );
            var cols = conditionCols(new DAL.ColumnEnums.Tables.题.类型());
            var exp = new DAL.Expressions.Tables.题.类型();
            if(cols.Contains(0)) exp.And(t => t.类型编号 == o.类型编号);
            if(cols.Contains(1)) exp.And(t => t.类型名 == o.类型名);
            if(cols.Contains(2)) exp.And(t => t.是否需要专业阅卷 == o.是否需要专业阅卷);
            return Database.Tables.题.类型.Delete(exp);
		}

        #endregion

        #endregion

        #region 题.题

        #region Insert

		public static int Insert(this Database.Tables.题.题 o, ColumnEnums.Tables.题.题.Handler insertCols = null, ColumnEnums.Tables.题.题.Handler fillCols = null, bool isFillAfterInsert = true)
		{
            return Database.Tables.题.题.Insert(o, insertCols, fillCols, isFillAfterInsert);
		}
        #endregion

        #region Update

		public static int Update(this Database.Tables.题.题 o, Expressions.Tables.题.题.Handler eh = null, ColumnEnums.Tables.题.题.Handler updateCols = null, ColumnEnums.Tables.题.题.Handler fillCols = null, bool isFillAfterUpdate = true)
		{
            if (eh == null) eh = new Expressions.Tables.题.题.Handler(a => a.题编号 == o.题编号);
            return Database.Tables.题.题.Update(o, eh, updateCols, fillCols, isFillAfterUpdate);
		}
        #endregion

        #region Delete

		public static int Delete(this Database.Tables.题.题 o, ColumnEnums.Tables.题.题.Handler conditionCols = null)
		{
            if(conditionCols == null) return Database.Tables.题.题.Delete(t =>
                t.题编号 == o.题编号
            );
            var cols = conditionCols(new DAL.ColumnEnums.Tables.题.题());
            var exp = new DAL.Expressions.Tables.题.题();
            if(cols.Contains(0)) exp.And(t => t.题编号 == o.题编号);
            if(cols.Contains(1)) exp.And(t => t.新版题编号 == o.新版题编号);
            if(cols.Contains(2)) exp.And(t => t.类型编号 == o.类型编号);
            if(cols.Contains(3)) exp.And(t => t.知识面编号 == o.知识面编号);
            if(cols.Contains(4)) exp.And(t => t.难度系数 == o.难度系数);
            if(cols.Contains(5)) exp.And(t => t.显示模板 == o.显示模板);
            if(cols.Contains(6)) exp.And(t => t.考核意图 == o.考核意图);
            if(cols.Contains(7)) exp.And(t => t.备注 == o.备注);
            if(cols.Contains(8)) exp.And(t => t.更新时间 == o.更新时间);
            if(cols.Contains(9)) exp.And(t => t.是否启用 == o.是否启用);
            return Database.Tables.题.题.Delete(exp);
		}

        #endregion

        #endregion

        #region 题.题_连线

        #region Insert

		public static int Insert(this Database.Tables.题.题_连线 o, ColumnEnums.Tables.题.题_连线.Handler insertCols = null, ColumnEnums.Tables.题.题_连线.Handler fillCols = null, bool isFillAfterInsert = true)
		{
            return Database.Tables.题.题_连线.Insert(o, insertCols, fillCols, isFillAfterInsert);
		}
        #endregion

        #region Update

		public static int Update(this Database.Tables.题.题_连线 o, Expressions.Tables.题.题_连线.Handler eh = null, ColumnEnums.Tables.题.题_连线.Handler updateCols = null, ColumnEnums.Tables.题.题_连线.Handler fillCols = null, bool isFillAfterUpdate = true)
		{
            if (eh == null) eh = new Expressions.Tables.题.题_连线.Handler(a => a.题编号 == o.题编号 & a.连线序号 == o.连线序号);
            return Database.Tables.题.题_连线.Update(o, eh, updateCols, fillCols, isFillAfterUpdate);
		}
        #endregion

        #region Delete

		public static int Delete(this Database.Tables.题.题_连线 o, ColumnEnums.Tables.题.题_连线.Handler conditionCols = null)
		{
            if(conditionCols == null) return Database.Tables.题.题_连线.Delete(t =>
                t.题编号 == o.题编号 &
                t.连线序号 == o.连线序号
            );
            var cols = conditionCols(new DAL.ColumnEnums.Tables.题.题_连线());
            var exp = new DAL.Expressions.Tables.题.题_连线();
            if(cols.Contains(0)) exp.And(t => t.题编号 == o.题编号);
            if(cols.Contains(1)) exp.And(t => t.连线序号 == o.连线序号);
            if(cols.Contains(2)) exp.And(t => t.组序号 == o.组序号);
            if(cols.Contains(3)) exp.And(t => t.显示模板 == o.显示模板);
            return Database.Tables.题.题_连线.Delete(exp);
		}

        #endregion

        #endregion

        #region 题.题_连线_答案

        #region Insert

		public static int Insert(this Database.Tables.题.题_连线_答案 o, ColumnEnums.Tables.题.题_连线_答案.Handler insertCols = null, ColumnEnums.Tables.题.题_连线_答案.Handler fillCols = null, bool isFillAfterInsert = true)
		{
            return Database.Tables.题.题_连线_答案.Insert(o, insertCols, fillCols, isFillAfterInsert);
		}
        #endregion

        #region Update

		public static int Update(this Database.Tables.题.题_连线_答案 o, Expressions.Tables.题.题_连线_答案.Handler eh = null, ColumnEnums.Tables.题.题_连线_答案.Handler updateCols = null, ColumnEnums.Tables.题.题_连线_答案.Handler fillCols = null, bool isFillAfterUpdate = true)
		{
            if (eh == null) eh = new Expressions.Tables.题.题_连线_答案.Handler(a => a.题编号 == o.题编号 & a.连线序号A == o.连线序号A & a.连线序号B == o.连线序号B);
            return Database.Tables.题.题_连线_答案.Update(o, eh, updateCols, fillCols, isFillAfterUpdate);
		}
        #endregion

        #region Delete

		public static int Delete(this Database.Tables.题.题_连线_答案 o, ColumnEnums.Tables.题.题_连线_答案.Handler conditionCols = null)
		{
            if(conditionCols == null) return Database.Tables.题.题_连线_答案.Delete(t =>
                t.题编号 == o.题编号 &
                t.连线序号A == o.连线序号A &
                t.连线序号B == o.连线序号B
            );
            var cols = conditionCols(new DAL.ColumnEnums.Tables.题.题_连线_答案());
            var exp = new DAL.Expressions.Tables.题.题_连线_答案();
            if(cols.Contains(0)) exp.And(t => t.题编号 == o.题编号);
            if(cols.Contains(1)) exp.And(t => t.连线序号A == o.连线序号A);
            if(cols.Contains(2)) exp.And(t => t.连线序号B == o.连线序号B);
            return Database.Tables.题.题_连线_答案.Delete(exp);
		}

        #endregion

        #endregion

        #region 题.题_判断

        #region Insert

		public static int Insert(this Database.Tables.题.题_判断 o, ColumnEnums.Tables.题.题_判断.Handler insertCols = null, ColumnEnums.Tables.题.题_判断.Handler fillCols = null, bool isFillAfterInsert = true)
		{
            return Database.Tables.题.题_判断.Insert(o, insertCols, fillCols, isFillAfterInsert);
		}
        #endregion

        #region Update

		public static int Update(this Database.Tables.题.题_判断 o, Expressions.Tables.题.题_判断.Handler eh = null, ColumnEnums.Tables.题.题_判断.Handler updateCols = null, ColumnEnums.Tables.题.题_判断.Handler fillCols = null, bool isFillAfterUpdate = true)
		{
            if (eh == null) eh = new Expressions.Tables.题.题_判断.Handler(a => a.题编号 == o.题编号);
            return Database.Tables.题.题_判断.Update(o, eh, updateCols, fillCols, isFillAfterUpdate);
		}
        #endregion

        #region Delete

		public static int Delete(this Database.Tables.题.题_判断 o, ColumnEnums.Tables.题.题_判断.Handler conditionCols = null)
		{
            if(conditionCols == null) return Database.Tables.题.题_判断.Delete(t =>
                t.题编号 == o.题编号
            );
            var cols = conditionCols(new DAL.ColumnEnums.Tables.题.题_判断());
            var exp = new DAL.Expressions.Tables.题.题_判断();
            if(cols.Contains(0)) exp.And(t => t.题编号 == o.题编号);
            if(cols.Contains(1)) exp.And(t => t.答案 == o.答案);
            return Database.Tables.题.题_判断.Delete(exp);
		}

        #endregion

        #endregion

        #region 题.题_填空_答案

        #region Insert

		public static int Insert(this Database.Tables.题.题_填空_答案 o, ColumnEnums.Tables.题.题_填空_答案.Handler insertCols = null, ColumnEnums.Tables.题.题_填空_答案.Handler fillCols = null, bool isFillAfterInsert = true)
		{
            return Database.Tables.题.题_填空_答案.Insert(o, insertCols, fillCols, isFillAfterInsert);
		}
        #endregion

        #region Update

		public static int Update(this Database.Tables.题.题_填空_答案 o, Expressions.Tables.题.题_填空_答案.Handler eh = null, ColumnEnums.Tables.题.题_填空_答案.Handler updateCols = null, ColumnEnums.Tables.题.题_填空_答案.Handler fillCols = null, bool isFillAfterUpdate = true)
		{
            if (eh == null) eh = new Expressions.Tables.题.题_填空_答案.Handler(a => a.题编号 == o.题编号 & a.格子序号 == o.格子序号);
            return Database.Tables.题.题_填空_答案.Update(o, eh, updateCols, fillCols, isFillAfterUpdate);
		}
        #endregion

        #region Delete

		public static int Delete(this Database.Tables.题.题_填空_答案 o, ColumnEnums.Tables.题.题_填空_答案.Handler conditionCols = null)
		{
            if(conditionCols == null) return Database.Tables.题.题_填空_答案.Delete(t =>
                t.题编号 == o.题编号 &
                t.格子序号 == o.格子序号
            );
            var cols = conditionCols(new DAL.ColumnEnums.Tables.题.题_填空_答案());
            var exp = new DAL.Expressions.Tables.题.题_填空_答案();
            if(cols.Contains(0)) exp.And(t => t.题编号 == o.题编号);
            if(cols.Contains(1)) exp.And(t => t.格子序号 == o.格子序号);
            if(cols.Contains(2)) exp.And(t => t.显示模板 == o.显示模板);
            return Database.Tables.题.题_填空_答案.Delete(exp);
		}

        #endregion

        #endregion

        #region 题.题_问答

        #region Insert

		public static int Insert(this Database.Tables.题.题_问答 o, ColumnEnums.Tables.题.题_问答.Handler insertCols = null, ColumnEnums.Tables.题.题_问答.Handler fillCols = null, bool isFillAfterInsert = true)
		{
            return Database.Tables.题.题_问答.Insert(o, insertCols, fillCols, isFillAfterInsert);
		}
        #endregion

        #region Update

		public static int Update(this Database.Tables.题.题_问答 o, Expressions.Tables.题.题_问答.Handler eh = null, ColumnEnums.Tables.题.题_问答.Handler updateCols = null, ColumnEnums.Tables.题.题_问答.Handler fillCols = null, bool isFillAfterUpdate = true)
		{
            if (eh == null) eh = new Expressions.Tables.题.题_问答.Handler(a => a.题编号 == o.题编号);
            return Database.Tables.题.题_问答.Update(o, eh, updateCols, fillCols, isFillAfterUpdate);
		}
        #endregion

        #region Delete

		public static int Delete(this Database.Tables.题.题_问答 o, ColumnEnums.Tables.题.题_问答.Handler conditionCols = null)
		{
            if(conditionCols == null) return Database.Tables.题.题_问答.Delete(t =>
                t.题编号 == o.题编号
            );
            var cols = conditionCols(new DAL.ColumnEnums.Tables.题.题_问答());
            var exp = new DAL.Expressions.Tables.题.题_问答();
            if(cols.Contains(0)) exp.And(t => t.题编号 == o.题编号);
            if(cols.Contains(1)) exp.And(t => t.参考答案 == o.参考答案);
            return Database.Tables.题.题_问答.Delete(exp);
		}

        #endregion

        #endregion

        #region 题.题_选择_答案

        #region Insert

		public static int Insert(this Database.Tables.题.题_选择_答案 o, ColumnEnums.Tables.题.题_选择_答案.Handler insertCols = null, ColumnEnums.Tables.题.题_选择_答案.Handler fillCols = null, bool isFillAfterInsert = true)
		{
            return Database.Tables.题.题_选择_答案.Insert(o, insertCols, fillCols, isFillAfterInsert);
		}
        #endregion

        #region Update

		public static int Update(this Database.Tables.题.题_选择_答案 o, Expressions.Tables.题.题_选择_答案.Handler eh = null, ColumnEnums.Tables.题.题_选择_答案.Handler updateCols = null, ColumnEnums.Tables.题.题_选择_答案.Handler fillCols = null, bool isFillAfterUpdate = true)
		{
            if (eh == null) eh = new Expressions.Tables.题.题_选择_答案.Handler(a => a.题编号 == o.题编号 & a.选项序号 == o.选项序号 & a.格子序号 == o.格子序号);
            return Database.Tables.题.题_选择_答案.Update(o, eh, updateCols, fillCols, isFillAfterUpdate);
		}
        #endregion

        #region Delete

		public static int Delete(this Database.Tables.题.题_选择_答案 o, ColumnEnums.Tables.题.题_选择_答案.Handler conditionCols = null)
		{
            if(conditionCols == null) return Database.Tables.题.题_选择_答案.Delete(t =>
                t.题编号 == o.题编号 &
                t.选项序号 == o.选项序号 &
                t.格子序号 == o.格子序号
            );
            var cols = conditionCols(new DAL.ColumnEnums.Tables.题.题_选择_答案());
            var exp = new DAL.Expressions.Tables.题.题_选择_答案();
            if(cols.Contains(0)) exp.And(t => t.题编号 == o.题编号);
            if(cols.Contains(1)) exp.And(t => t.选项序号 == o.选项序号);
            if(cols.Contains(2)) exp.And(t => t.格子序号 == o.格子序号);
            return Database.Tables.题.题_选择_答案.Delete(exp);
		}

        #endregion

        #endregion

        #region 题.题_选择_选项

        #region Insert

		public static int Insert(this Database.Tables.题.题_选择_选项 o, ColumnEnums.Tables.题.题_选择_选项.Handler insertCols = null, ColumnEnums.Tables.题.题_选择_选项.Handler fillCols = null, bool isFillAfterInsert = true)
		{
            return Database.Tables.题.题_选择_选项.Insert(o, insertCols, fillCols, isFillAfterInsert);
		}
        #endregion

        #region Update

		public static int Update(this Database.Tables.题.题_选择_选项 o, Expressions.Tables.题.题_选择_选项.Handler eh = null, ColumnEnums.Tables.题.题_选择_选项.Handler updateCols = null, ColumnEnums.Tables.题.题_选择_选项.Handler fillCols = null, bool isFillAfterUpdate = true)
		{
            if (eh == null) eh = new Expressions.Tables.题.题_选择_选项.Handler(a => a.题编号 == o.题编号 & a.选项序号 == o.选项序号);
            return Database.Tables.题.题_选择_选项.Update(o, eh, updateCols, fillCols, isFillAfterUpdate);
		}
        #endregion

        #region Delete

		public static int Delete(this Database.Tables.题.题_选择_选项 o, ColumnEnums.Tables.题.题_选择_选项.Handler conditionCols = null)
		{
            if(conditionCols == null) return Database.Tables.题.题_选择_选项.Delete(t =>
                t.题编号 == o.题编号 &
                t.选项序号 == o.选项序号
            );
            var cols = conditionCols(new DAL.ColumnEnums.Tables.题.题_选择_选项());
            var exp = new DAL.Expressions.Tables.题.题_选择_选项();
            if(cols.Contains(0)) exp.And(t => t.题编号 == o.题编号);
            if(cols.Contains(1)) exp.And(t => t.选项序号 == o.选项序号);
            if(cols.Contains(2)) exp.And(t => t.显示模板 == o.显示模板);
            return Database.Tables.题.题_选择_选项.Delete(exp);
		}

        #endregion

        #endregion

        #region 题.知识面

        #region Insert

		public static int Insert(this Database.Tables.题.知识面 o, ColumnEnums.Tables.题.知识面.Handler insertCols = null, ColumnEnums.Tables.题.知识面.Handler fillCols = null, bool isFillAfterInsert = true)
		{
            return Database.Tables.题.知识面.Insert(o, insertCols, fillCols, isFillAfterInsert);
		}
        #endregion

        #region Update

		public static int Update(this Database.Tables.题.知识面 o, Expressions.Tables.题.知识面.Handler eh = null, ColumnEnums.Tables.题.知识面.Handler updateCols = null, ColumnEnums.Tables.题.知识面.Handler fillCols = null, bool isFillAfterUpdate = true)
		{
            if (eh == null) eh = new Expressions.Tables.题.知识面.Handler(a => a.知识面编号 == o.知识面编号);
            return Database.Tables.题.知识面.Update(o, eh, updateCols, fillCols, isFillAfterUpdate);
		}
        #endregion

        #region Delete

		public static int Delete(this Database.Tables.题.知识面 o, ColumnEnums.Tables.题.知识面.Handler conditionCols = null)
		{
            if(conditionCols == null) return Database.Tables.题.知识面.Delete(t =>
                t.知识面编号 == o.知识面编号
            );
            var cols = conditionCols(new DAL.ColumnEnums.Tables.题.知识面());
            var exp = new DAL.Expressions.Tables.题.知识面();
            if(cols.Contains(0)) exp.And(t => t.知识面编号 == o.知识面编号);
            if(cols.Contains(1)) exp.And(t => t.名称 == o.名称);
            return Database.Tables.题.知识面.Delete(exp);
		}

        #endregion

        #endregion

    }
}