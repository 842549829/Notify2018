using System;
using System.Collections.Generic;
using System.Linq;
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
        /// <param name="company">Company</param>
        /// <returns>TCompany</returns>
        public static TCompany ToTCompany(this Company company)
        {
            var tCompany = new TCompany
            {
                CompanyId = company.Key,
                CompanyAccountNo = company.CompanyAccountNo,
                CompanyAddress = company.CompanyAddress,
                CompanyName = company.CompanyName,
                CompanyStatus = company.CompanyStatus,
                ContactName = company.ContactName,
                ContactPhone = company.ContactPhone,
                ModifyTime = company.ModifyTime,
                ParenttCompanyId = company.ParenttCompanyId,
                CreateTime = company.CreateTime
            };
            return tCompany;
        }

        /// <summary>
        /// 对象转化
        /// </summary>
        /// <param name="company">Company</param>
        /// <returns>TCompany</returns>
        public static MCompany ToMCompany(this TCompany company)
        {
            var tCompany = new MCompany
            {
                CompanyId = company.CompanyId,
                CompanyAccountNo = company.CompanyAccountNo,
                CompanyAddress = company.CompanyAddress ?? string.Empty,
                CompanyName = company.CompanyName,
                CompanyStatus = company.CompanyStatus,
                ContactName = company.ContactName ?? string.Empty,
                ContactPhone = company.ContactPhone ?? string.Empty,
                ModifyTime = company.ModifyTime,
                ParenttCompanyId = company.ParenttCompanyId,
                CreateTime = company.CreateTime
            };
            return tCompany;
        }

        /// <summary>
        /// 对象转化
        /// </summary>
        /// <param name="company">Company</param>
        /// <returns>MAccount</returns>
        public static RegisterAccount ToRegisterAccount(this MCompany company)
        {
            return new RegisterAccount
            {
                AccountName = company.CompanyName,
                AccountNO = company.CompanyAccountNo,
                Mail = company.CompanyAccountNo,
                Mobile = company.ContactPhone,
                Password = "123456",
                PayPassword = "123456"
            };
        }

        /// <summary>
        /// 对象转化
        /// </summary>
        /// <param name="company">MCompany</param>
        /// <returns>TCompany</returns>
        public static TCompany ToTCompany(this MCompany company)
        {
            var tCompany = new TCompany
            {
                CompanyId = company.CompanyId,
                CompanyAccountNo = company.CompanyAccountNo,
                CompanyAddress = company.CompanyAddress,
                CompanyName = company.CompanyName,
                CompanyStatus = company.CompanyStatus,
                ContactName = company.ContactName,
                ContactPhone = company.ContactPhone,
                ModifyTime = company.ModifyTime,
                ParenttCompanyId = company.ParenttCompanyId,
                CreateTime = company.CreateTime
            };
            return tCompany;
        }

        /// <summary>
        /// 对象转化
        /// </summary>
        /// <param name="mCompany">mCompany</param>
        /// <returns>TCompany</returns>
        public static IEnumerable<TCompany> ToTCompanys(this IEnumerable<MCompany> mCompany)
        {
            return mCompany.Select(ToTCompany);
        }
    }
}