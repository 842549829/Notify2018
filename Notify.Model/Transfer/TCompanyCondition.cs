using System;
using Notify.Code.Code;

namespace Notify.Model.Transfer
{
    /// <summary>
    /// 公司列表
    /// </summary>
    public class TCompanyCondition : EsayUIPaging
    {
        /// <summary>
        /// 帐号
        /// </summary>
        public string AccountNo { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// 上级公司Id
        /// </summary>
        public Guid ParenttCompanyId { get; set; }
    }
}