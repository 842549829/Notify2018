using System;
using Notify.Domain.AccountDomain;
using Notify.Model.DB;
using Notify.Model.Transfer;

namespace Notify.Domain.CompanyDomain
{
    /// <summary>
    /// 公司数据转换
    /// </summary>
    public static class CompanyBuilder
    {
        /// <summary>
        /// 对象转化
        /// </summary>
        /// <param name="mCompany">mCompany</param>
        /// <returns>Account</returns>
        public static Company ToCompany(this MCompany mCompany)
        {
            var company = new Company(mCompany.CompanyId)
            {
                CompanyAccountNo = mCompany.CompanyAccountNo,
                CompanyAddress = mCompany.CompanyAddress,
                CompanyName = mCompany.CompanyName,
                CompanyStatus = mCompany.CompanyStatus,
                ContactName = mCompany.ContactName,
                ContactPhone = mCompany.ContactPhone,
                ModifyTime = mCompany.ModifyTime,
                ParenttCompanyId = mCompany.ParenttCompanyId,
                CreateTime = mCompany.CreateTime
            };
            return company;
        }

        /// <summary>
        /// 对象转化
        /// </summary>
        /// <param name="mAccount">MAccount</param>
        /// <returns>TAccount</returns>
        public static TAccount ToTAccount(this MAccount mAccount)
        {
            var account = new TAccount
            {
                Id = mAccount.Id,
                AccountName = mAccount.AccountName,
                AccountNo = mAccount.AccountNo,
                Mail = mAccount.Mail,
                Mobile = mAccount.Mobile,
                CreateTime = mAccount.CreateTime,
                IsAdmin = mAccount.IsAdmin,
                Status = mAccount.Status
            };
            return account;
        }

        /// <summary>
        /// 对象转化
        /// </summary>
        /// <param name="account">Account</param>
        /// <returns>TAccount</returns>
        public static TAccount ToTAccount(this Account account)
        {
            var tAccount = new TAccount
            {
                Id = account.Key,
                AccountName = account.AccountName,
                AccountNo = account.AccountNO,
                Mail = account.Mail,
                Mobile = account.Mobile,
                CreateTime = account.CreateTime
            };
            return tAccount;
        }

        /// <summary>
        /// 对象转化
        /// </summary>
        /// <param name="account">Account</param>
        /// <returns>MAccount</returns>
        public static MAccount ToMAccount(this Account account)
        {
            return new MAccount
            {
                Id = account.Key,
                AccountName = account.AccountName,
                AccountNo = account.AccountNO,
                Mail = account.Mail,
                Mobile = account.Mobile,
                Password = account.Password,
                PayPassword = account.PayPassword,
                CreateTime = account.CreateTime,
                IsAdmin = account.IsAdmin,
                Status = account.Status
            };
        }

        /// <summary>
        /// 对象转化
        /// </summary>
        /// <param name="accountId">accountId</param>
        /// <returns>MAccount</returns>
        public static MAccount ToMAccount(this Guid accountId)
        {
            return new MAccount
            {
                Id = accountId
            };
        }
    }
}