using System;

namespace Notify.Model.Transfer
{
    /// <summary>
    /// 公司信息
    /// </summary>
    public class TCompany
    {
        /// <summary>
        /// ID
        /// </summary>
        public Guid CompanyId { get; set; }

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
    }
}