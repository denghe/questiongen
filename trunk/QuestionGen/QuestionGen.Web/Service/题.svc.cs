using System;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;

using SqlLib;
using db = DAL.Database.Tables;
using query = DAL.Queries.Tables;

namespace QuestionGen.Web.Service
{
    [ServiceContract(Namespace = "")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class 题
    {
        #region 知识面

        [OperationContract]
        public int 知识面_插入(string 名称)
        {
            var row = new db.题.知识面 { 名称 = 名称 };
            try
            {
                db.题.知识面.Insert(row);
                return row.知识面编号;
            }
            catch
            {
                return -1;
            }
        }

        [OperationContract]
        public int 知识面_删除(int 知识面编号)
        {
            return db.题.知识面.Delete(o => o.知识面编号.Equal(知识面编号));
        }


        [OperationContract]
        public int 知识面_更新(int 知识面编号, string 名称)
        {
            return db.题.知识面.Update(new db.题.知识面 { 知识面编号 = 知识面编号, 名称 = 名称 });
        }

        [OperationContract]
        public byte[] 知识面_获取(byte[] 查询)
        {
            var q = new query.题.知识面(查询);
            return db.题.知识面.Select(q).GetBytes();
        }

        #endregion

        #region 题

        [OperationContract]
        public int 题_插入(byte[] 题)
        {
            var row = new db.题.题(题);
            db.题.题.Insert(row);
            return row.题编号;
        }

        #endregion
    }
}
