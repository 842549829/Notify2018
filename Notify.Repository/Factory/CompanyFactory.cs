using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Notify.Code.Extension;
using Notify.Infrastructure.EntityFactoryFramework;
using Notify.Model.DB;

namespace Notify.Repository.Factory
{
    /// <summary>
    /// 公司仓储工厂
    /// </summary>
    public class CompanyFactory : IEntityFactory<MCompany>
    {
        /// <summary>
        /// 创建公司
        /// </summary>
        /// <param name="reader">IDataReader</param>
        /// <returns>用户</returns>
        public MCompany BuildEntity(IDataReader reader)
        {
            return new MCompany
            {
                CompanyId = Guid.Parse(reader["CompanyId"].ToString()),
                CompanyAccountNo = reader["CompanyAccountNo"].ToString(),
                CompanyAddress = reader["CompanyAddress"].ToString(),
                CreateTime = Convert.ToDateTime(reader["CreateTime"]),
                ModifyTime = Convert.ToDateTime(reader["ModifyTime"]),
                CompanyName = reader["CompanyName"].ToString(),
                ContactName = reader["ContactName"].ToString(),
                ContactPhone = reader["ContactPhone"].ToString(),
                ParenttCompanyId = Guid.Parse(reader["ParenttCompanyId"].ToString()),
                CompanyStatus = (Model.CompanyStatus)(Convert.ToUInt32(reader["CompanyStatus"]))
            };
        }

        /// <summary>
        /// 创建公司
        /// </summary>
        /// <param name="table">DataSet</param>
        /// <returns>用户</returns>
        public MCompany BuildEntity(DataSet table)
        {
            return table.ToModel<MCompany>();
        }
    }
}