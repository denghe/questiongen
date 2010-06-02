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
        public int 插入_题(byte[] buff)
        {
            var row = new db.题.题(buff);
            db.题.题.Insert(row);
            return row.题编号;
        }


    }
}
