using System;
using Notify.Infrastructure.RepositoryFramework;
using Notify.Model.DB;

namespace Notify.IRepository
{
    /// <summary>
    /// 公司仓储层接口
    /// </summary>
    public interface ICompanyRepository : IRepository<Guid, MCompany>
    {
        /// <summary>
        /// 根据帐号查询用户
        /// </summary>
        /// <param name="accountNo">用户帐号</param>
        /// <returns>用户</returns>
        MCompany Query(string accountNo);
    }
}
