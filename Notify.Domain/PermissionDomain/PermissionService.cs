using System;
using System.Collections.Generic;
using System.Linq;
using Notify.DbCommon.Repositroies;
using Notify.Domain.AccountDomain;
using Notify.Domain.MenuDomain;

namespace Notify.Domain.PermissionDomain
{
    /// <summary>
    /// 权限服务
    /// </summary>
    public class PermissionService
    {
        /// <summary>
        /// 当前上下文
        /// </summary>
        public static IDbFactory.IDbFactory DbContext { get; } = Factory.GetFactory<IDbFactory.IDbFactory>();

        /// <summary>
        /// 查询用户的权限
        /// </summary>
        /// <param name="type">系统类型</param>
        /// <param name="account">用户</param>
        /// <returns>权限</returns>
        public static PermissionCollection QueryPermissionOfUser(Account account, int type)
        {
            // 超级管理员
            if (account.Company.Value.IsAdminCompany)
            {
                return QueryAllPermission(type);
            }
            else
            {
                // 是否是公司管理员
                if (account.IsAdmin)
                {
                    return QueryCompanyPermission(account.Company.Value.Key, type);
                }
                else
                {
                    return QueryAccountPermission(account, type);
                }
            }
        }

        /// <summary>
        /// 查询权限(员工个人权限)
        /// </summary>
        /// <param name="account">账户信息</param>
        /// <param name="type">系统类型</param>
        /// <returns>权限</returns>
        private static PermissionCollection QueryAccountPermission(Account account, int type)
        {
            IEnumerable<Menu> menus;
            var userPermission = MenuService.QueryUserMenus(account.Key, type).ToList();
            if (!userPermission.Any())
            {
                // 查询公司默认权限
                var companyDefaultPermission = MenuService.QueryUserDefaultMenus(account.Company.Value.Key, type).ToList();
                menus = !companyDefaultPermission.Any() ? new List<Menu>() : companyDefaultPermission;
            }
            else
            {
                menus = userPermission;
            }
            var userPermissions = PermissionCollection.Union(menus);
            return new PermissionCollection(userPermissions);
        }

        /// <summary>
        /// 查询权限(公司权限)
        /// </summary>
        /// <param name="companyId">公司Id</param>
        /// <param name="type">系统类型</param>
        /// <returns>权限</returns>
        private static PermissionCollection QueryCompanyPermission(Guid companyId, int type)
        {
            var userPermissionRoles = MenuService.QueryUserMenus(companyId, type);
            var userPermissions = PermissionCollection.Union(userPermissionRoles);
            return new PermissionCollection(userPermissions);
        }

        /// <summary>
        ///  查询权限(超级管理员) 
        /// <param name="type">系统类型</param>
        /// </summary>
        private static PermissionCollection QueryAllPermission(int type)
        {
            var userPermissionRoles = MenuService.QueryMenus(type);
            var userPermissions = PermissionCollection.Union(userPermissionRoles);
            return new PermissionCollection(userPermissions);
        }
    }
}