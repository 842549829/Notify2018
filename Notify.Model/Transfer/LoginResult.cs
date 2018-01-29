using System.Collections.Generic;
using Notify.Code.Code;

namespace Notify.Model.Transfer
{
    /// <summary>
    /// 登录结果
    /// </summary>
    public class LoginResult 
    {
        /// <summary>
        /// 结果
        /// </summary>
        public Result Result { get; set; }

        /// <summary>
        /// 菜单权限
        /// </summary>
        public IEnumerable<TMenu> Menu { get; set; }

        /// <summary>
        /// 权限
        /// </summary>
        Dictionary<int, IEnumerable<TMenu>> Permission { get; set; }

        /// <summary>
        /// 用户信息
        /// </summary>
        public TAccount Account { get; set; }

        /// <summary>
        /// 公司信息
        /// </summary>
        public TCompany Company { get; set; }
    }
}