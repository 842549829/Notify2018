using System;
using Notify.DbCommon.Repositroies;
using Notify.Model.DB;

namespace Notify.Domain.CompanyDomain
{
    /// <summary>
    /// 公司信息
    /// </summary>
    public static class CompanyService
    {
        /// <summary>
        /// 当前上下文
        /// </summary>
        public static IDbFactory.IDbFactory DbContext { get; } = Factory.GetFactory<IDbFactory.IDbFactory>();

        /// <summary>
        /// 获取公司信息
        /// </summary>
        /// <param name="companyId">公司Id</param>
        /// <returns>公司信息</returns>
        public static MCompany QueryMCompanyByComapnyId(Guid companyId)
        {
            using (var accountesRepository = DbContext.CreateICompanyRepository())
            {
                return accountesRepository.Query(companyId);
            }
        }
    }
}