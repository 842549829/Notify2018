using Notify.Code.Exception;

namespace Notify.Domain.CompanyDomain
{
    /// <summary>
    /// 公司验证
    /// </summary>
    public class CompanyValidate
    {
        /// <summary>
        /// 验证公司信息
        /// </summary>
        /// <param name="company">公司信息</param>
        public static void ValdateCompany(Company company)
        {
            if (company == null)
            {
                throw new CustomException("登录信息为空");
            }

            company.ValidateCompanyStatus();
        }
    }
}