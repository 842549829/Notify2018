using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Notify.Code.Exception;
using Notify.Domain.AccountDomain;
using Notify.Infrastructure.DomainBase;
using Notify.Model;

namespace Notify.Domain.CompanyDomain
{
    /// <summary>
    /// 公司
    /// </summary>
    public class Company : EntityBase<Guid>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public Company()
            : base(Guid.NewGuid())
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="id">公司Id</param>
        public Company(Guid id)
            : base(id)
        {
        }

        /// <summary>
        /// 公司帐号
        /// </summary>
        public string CompanyAccountNo { get; set; }

        /// <summary>
        /// 公司名称
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// 公司联系人
        /// </summary>
        public string ContactName { get; set; }

        /// <summary>
        /// 公司电话
        /// </summary>
        public string ContactPhone { get; set; }

        /// <summary>
        /// 公司地址
        /// </summary>
        public string CompanyAddress { get; set; }

        /// <summary>
        /// 注册时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime ModifyTime { get; set; }

        /// <summary>
        /// 上级公司Id
        /// </summary>
        public Guid ParenttCompanyId { get; set; }

        /// <summary>
        /// 公司状态
        /// </summary>
        public CompanyStatus CompanyStatus { get; set; }

        /// <summary>
        /// 是否是总公司
        /// </summary>
        /// <returns>是否是总公司</returns>
        public bool IsAdminCompany => this.ParenttCompanyId == Guid.Empty;

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="account">用户信息</param>
        /// <param name="company">上级公司</param>
        public void Register(Account account, Company company)
        {
            this.CompanyAccountNo = account.AccountNO;
            this.CompanyName = account.AccountName;
            this.ContactName = account.AccountName;
            this.ContactPhone = string.Empty;
            this.CompanyAddress = string.Empty;
            this.CreateTime = account.CreateTime;
            this.ModifyTime = account.CreateTime;
            this.CompanyStatus = CompanyStatus.Enabled;
            if (company != null)
            {
                this.ParenttCompanyId = company.Key;
            }
        }

        /// <summary>
        /// 验证公司状态
        /// </summary>
        public void ValidateCompanyStatus()
        {
            if (this.CompanyStatus == CompanyStatus.Disable)
            {
                throw new CustomException("公司已经被禁用");
            }
        }
    }
}