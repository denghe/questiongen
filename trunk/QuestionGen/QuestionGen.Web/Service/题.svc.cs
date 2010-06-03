using System;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using db = DAL.Database.Tables;

namespace QuestionGen.Web.Service
{
    [ServiceContract(Namespace = "")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class 题
    {
        [OperationContract]
        public void DoWork()
        {
            // Add your operation implementation here
            return;
        }

        // Add more operations here and mark them with [OperationContract]

        [OperationContract]
        public int 题_插入(byte[] 题)
        {
            var row = new db.题.题(题);
            db.题.题.Insert(row);
            return row.题编号;
        }

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

    }
}
