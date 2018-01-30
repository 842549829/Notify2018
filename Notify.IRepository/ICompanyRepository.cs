using System;
using System.Collections.Generic;
using Notify.Infrastructure.RepositoryFramework;
using Notify.Model.DB;
using Notify.Model.Transfer;

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

        /// <summary>
        /// 公司分页查询
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns>结果</returns>
        IEnumerable<MCompany> QueryCompanyByPagings(TCompanyCondition condition);
    }
}