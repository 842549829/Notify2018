using System;
using System.Collections.Generic;
using Notify.Code.Code;
using Notify.Code.Constant;
using Notify.Controller.Base.Filters;
using Notify.Model.Transfer;
using Notify.Domain.MenuDomain;
using MenuService = Notify.Service.MenuService;

namespace Notify.Controller.Base
{
    /// <summary>
    /// 基础控制器
    /// </summary>
    [UserAuthorize]
    public class BaseController : System.Web.Mvc.Controller
    {
        /// <summary>
        /// 操作信息
        /// </summary>
        public static Operational GetOperational()
        {
            return new Operational
            {
                Operator = LogonUser.AccountNo,
                OperationDateTime = DateTime.Now
            };
        }

        /// <summary>
        /// 当前登录用户
        /// </summary>
        public static TAccount LogonUser
        {
            get
            {
                if (System.Web.HttpContext.Current.Session == null || System.Web.HttpContext.Current.Session[Const.UserSessionKey] == null)
                {
                    return null;
                }
                return System.Web.HttpContext.Current.Session[Const.UserSessionKey] as TAccount;
            }
        }

        /// <summary>
        /// 权限菜单
        /// </summary>
        public static IEnumerable<TMenu> Menu
        {
            get
            {
                if (System.Web.HttpContext.Current.Session == null || System.Web.HttpContext.Current.Session[Const.MenuSessionKey] == null)
                {
                    return null;
                }
                return System.Web.HttpContext.Current.Session[Const.MenuSessionKey] as IEnumerable<TMenu>;
            }
        }

        /// <summary>
        /// EsayUIMenu
        /// </summary>
        public static IEnumerable<EsayUIMenu> EsayUIMenu => Menu.ToEsayUIMenus(0);

        /// <summary>
        /// 公司信息
        /// </summary>
        public static TCompany Company
        {
            get
            {
                if (System.Web.HttpContext.Current.Session == null || System.Web.HttpContext.Current.Session[Const.CompanySessionKey] == null)
                {
                    return null;
                }
                return System.Web.HttpContext.Current.Session[Const.CompanySessionKey] as TCompany;
            }
        }

        /// <summary>
        /// 登录状态
        /// </summary>
        public static bool Logoned => LogonUser != null && LogonUser.Id != Guid.Empty;

        /// <summary>
        /// 验证权限
        /// </summary>
        /// <param name="address">菜单地址</param>
        /// <returns>结果</returns>
        public static bool HasPermission(string address)
        {
            return MenuService.HasPermission(Menu, address);
        }
    }
}