using System;
using System.Collections.Generic;
using Notify.DbCommon.Repositroies;
using Notify.Model.DB;

namespace Notify.Domain.MenuDomain
{
    /// <summary>
    /// 菜单服务
    /// </summary>
    public class MenuService
    {
        /// <summary>
        /// 当前上下文
        /// </summary>
        public static IDbFactory.IDbFactory DbContext { get; } = Factory.GetFactory<IDbFactory.IDbFactory>();

        /// <summary>
        /// 菜单查询(所有)
        /// </summary>
        /// <param name="type">菜单类型</param>
        /// <returns>结果</returns>
        public static IEnumerable<Menu> QueryMenus(int type)
        {
            using (var menuRepository = DbContext.CreateIMenuRepository())
            {
                var data = menuRepository.QueryMenus(type).ToMenus();
                return data;
            }
        }

        /// <summary>
        /// 菜单查询(根据用户Id查询)
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="type">菜单类型</param>
        /// <returns>结果</returns>
        public static IEnumerable<Menu> QueryUserMenus(Guid userId, int type)
        {
            using (var menuRepository = DbContext.CreateIMenuRepository())
            {
                var data = menuRepository.QueryUserMenus(userId, type).ToMenus();
                return data;
            }
        }

        /// <summary>
        /// 菜单查询(根据用户Id查询默认权限)
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="type">菜单类型</param>
        /// <returns>结果</returns>
        public static IEnumerable<Menu> QueryUserDefaultMenus(Guid userId, int type)
        {
            using (var menuRepository = DbContext.CreateIMenuRepository())
            {
                var data = menuRepository.QueryUserDefaultMenus(userId, type).ToMenus();
                return data;
            }
        }

        /// <summary>
        /// 查询父级默认Id
        /// </summary>
        /// <param name="type">菜单类型</param>
        /// <returns>父级默认Id</returns>
        public static Guid QueryDefaultParentId(int type)
        {
            using (var menuRepository = DbContext.CreateIMenuRepository())
            {
                var data = menuRepository.QueryDefaultParentId(type);
                return data;
            }
        }
    }
}