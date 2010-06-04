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

        [OperationContract]
        public byte[] 知识面_获取(byte[] 查询)
        {
            return db.题.知识面.Select(new query.题.知识面(查询)).GetBytes();
        }

        #endregion

        #region 题

        [OperationContract]
        public int 题_插入(byte[] 题)
        {
            var row = new db.题.题(题);
            try
            {
                var count = row.Insert();
                if (count < 1) return -1;
                return row.题编号;
            }
            catch
            {
                return -1;
            }
        }

        #endregion
    }
}
