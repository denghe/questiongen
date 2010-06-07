using System;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;

using SqlLib;
using DAL.Database.Tables;
using db = DAL.Database.Tables;
using query = DAL.Queries.Tables;

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
        public int 知识面_删除(byte[] 知识面)
        {
            var row = new db.题.知识面(知识面);
            return row.Delete();
        }


        [OperationContract]
        public int 知识面_更新(byte[] 知识面)
        {
            var row = new db.题.知识面(知识面);
            return row.Update();
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
                    var affected = question.Insert(isFillAfterInsert: false);
                    if (affected < 1)
                    {
                        tran.Rollback();
                        return -1;
                    }

                    // 插入 题 选项
                    foreach (var option in options)
                    {
                        // 预处理
                        option.题编号 = question.题编号;
                        option.排序 = option.选项编号;

                        // 插入选项 回填 选项编号
                        affected = option.Insert(
                            o => o.排序.题编号.显示模板,
                            o => o.选项编号
                        );
                        if (affected < 1)
                        {
                            tran.Rollback();
                            return -1;
                        }

                        // 插入 题 答案 (与当前选项相关的)
                        var option_answers = answers.Where(o => o.选项编号 == option.排序);
                        foreach (var answer in option_answers)
                        {
                            // 预处理
                            answer.题编号 = question.题编号;
                            answer.选项编号 = option.选项编号;

                            affected = answer.Insert(isFillAfterInsert: false);
                            if (affected < 1)
                            {
                                tran.Rollback();
                                return -1;
                            }
                        }
                    }

                    tran.Commit();
                    return 1;
                }
                catch(Exception ex)
                {
                    tran.Rollback();
                    return -1;
                }
            }
        }

        #endregion

        #region 各种获取

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

        #endregion
    }
}
