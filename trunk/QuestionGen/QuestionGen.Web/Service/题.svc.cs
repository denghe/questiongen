using System;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;

using SqlLib;
using DAL.Database.Tables;
using db = DAL.Database.Tables;
using query = DAL.Queries.Tables;
using exp = DAL.Expressions.Tables;

namespace QuestionGen.Web.Service
{
    [ServiceContract(Namespace = "")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class 题
    {
        /**
            潜规则：通常情况下, 
            插入操作 成功返回 主键值, 失败 返回 负数
            更新 删除 操作, 成功返回 受影响行数, 失败返回 负数
         */

        #region 知识面

        [OperationContract]
        public int 知识面_插入(byte[] 知识面)
        {
            var row = new db.题.知识面(知识面);
            try
            {
                var count = row.Insert();
                if (count < 1) return -1;
                return row.知识面编号;
            }
            catch
            {
                return -1;
            }
        }

        [OperationContract]
        public int 知识面_更新(byte[] 知识面)
        {
            return new db.题.知识面(知识面).Update();
        }

        #endregion

        #region 题

        [OperationContract]
        public int 题_插入(byte[] 题, byte[] 选项, byte[] 答案)
        {
            var question = new db.题.题(题);
            var options = 选项.ToList<db.题.题_选择_选项>();
            var answers = 答案.ToList<db.题.题_选择_答案>();

            // 预处理
            question.更新时间 = DateTime.Now;

            using (var tran = SqlHelper.NewTransaction())
            {
                try
                {
                    // 插入 题
                    var affected = question.Insert(fillCols: o => o.题编号);
                    if (affected < 1)
                    {
                        tran.Rollback();
                        return -1;
                    }

                    // 插入 选项
                    foreach (var option in options)
                    {
                        // 预处理
                        option.题编号 = question.题编号;

                        // 插入选项
                        affected = option.Insert(isFillAfterInsert: false);
                        if (affected < 1)
                        {
                            tran.Rollback();
                            return -1;
                        }
                    }
                    // 插入 答案
                    foreach (var answer in answers)
                    {
                        // 预处理
                        answer.题编号 = question.题编号;

                        // 插入答案
                        affected = answer.Insert(isFillAfterInsert: false);
                        if (affected < 1)
                        {
                            tran.Rollback();
                            return -1;
                        }
                    }

                    tran.Commit();
                    return 1;
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    return -1;
                }
            }
        }


        [OperationContract]
        public int 题_修改(byte[] 题, byte[] 选项, byte[] 答案)
        {
            var question = new db.题.题(题);
            var options = 选项.ToList<db.题.题_选择_选项>();
            var answers = 答案.ToList<db.题.题_选择_答案>();

            // 预处理
            question.更新时间 = DateTime.Now;

            using (var tran = SqlHelper.NewTransaction())
            {
                try
                {
                    // 更新 题
                    var affected = question.Update();
                    if (affected < 1)
                    {
                        tran.Rollback();
                        return -1;
                    }

                    // 删除原 选项,答案
                    affected = db.题.题_选择_答案.Delete(o => o.题编号 == question.题编号);
                    if (affected < 1)
                    {
                        tran.Rollback();
                        return -1;
                    }
                    affected = db.题.题_选择_选项.Delete(o => o.题编号 == question.题编号);
                    if (affected < 1)
                    {
                        tran.Rollback();
                        return -1;
                    }

                    // 插入 选项
                    foreach (var option in options)
                    {
                        // 预处理
                        option.题编号 = question.题编号;

                        // 插入选项
                        affected = option.Insert(isFillAfterInsert: false);
                        if (affected < 1)
                        {
                            tran.Rollback();
                            return -1;
                        }
                    }
                    // 插入 答案
                    foreach (var answer in answers)
                    {
                        // 预处理
                        answer.题编号 = question.题编号;

                        // 插入答案
                        affected = answer.Insert(isFillAfterInsert: false);
                        if (affected < 1)
                        {
                            tran.Rollback();
                            return -1;
                        }
                    }

                    tran.Commit();
                    return 1;
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    return -1;
                }
            }
        }

        #endregion

        #region 各种 按查询 获取 数据行

        [OperationContract]
        public byte[] 知识面_获取(byte[] 查询)
        {
            return db.题.知识面.Select(new query.题.知识面(查询)).GetBytes();
        }

        [OperationContract]
        public byte[] 题_获取(byte[] 查询)
        {
            return db.题.题.Select(new query.题.题(查询)).GetBytes();
        }

        [OperationContract]
        public byte[] 题_选择_选项_获取(byte[] 查询)
        {
            return db.题.题_选择_选项.Select(new query.题.题_选择_选项(查询)).GetBytes();
        }

        [OperationContract]
        public byte[] 题_选择_答案_获取(byte[] 查询)
        {
            return db.题.题_选择_答案.Select(new query.题.题_选择_答案(查询)).GetBytes();
        }

        [OperationContract]
        public byte[] 类型_获取(byte[] 查询)
        {
            return db.题.类型.Select(new query.题.类型(查询)).GetBytes();
        }

        #endregion

        #region 各种 按条件 获取 行数

        [OperationContract]
        public int 知识面_获取行数(byte[] 条件)
        {
            return db.题.知识面.Count(new exp.题.知识面(条件));
        }

        [OperationContract]
        public int 题_获取行数(byte[] 条件)
        {
            return db.题.题.Count(new exp.题.题(条件));
        }

        [OperationContract]
        public int 题_选择_选项_获取行数(byte[] 条件)
        {
            return db.题.题_选择_选项.Count(new exp.题.题_选择_选项(条件));
        }

        [OperationContract]
        public int 题_选择_答案_获取行数(byte[] 条件)
        {
            return db.题.题_选择_答案.Count(new exp.题.题_选择_答案(条件));
        }

        [OperationContract]
        public int 类型_获取行数(byte[] 条件)
        {
            return db.题.类型.Count(new exp.题.类型(条件));
        }

        #endregion

        #region 各种 按条件 删除

        [OperationContract]
        public int 知识面_删除(byte[] 条件)
        {
            return db.题.知识面.Delete(new exp.题.知识面(条件));
        }

        [OperationContract]
        public int 题_删除(byte[] 条件)
        {
            return db.题.题.Delete(new exp.题.题(条件));
        }

        [OperationContract]
        public int 类型_删除(byte[] 条件)
        {
            return db.题.类型.Delete(new exp.题.类型(条件));
        }


        #endregion
    }
}
