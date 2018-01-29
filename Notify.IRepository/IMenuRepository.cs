using System;
using System.Collections.Generic;
using Notify.Infrastructure.RepositoryFramework;
using Notify.Model.DB;

namespace Notify.IRepository
{
    /// <summary>
    /// 菜单仓储接口
    /// </summary>
    public interface IMenuRepository : IRepository<Guid, MMenu>
    {
        /// <summary>
        /// 查询父级默认Id
        /// </summary>
        /// <param name="type">菜单类型</param>
        /// <returns>父级默认Id</returns>
        Guid QueryDefaultParentId(int type);
        
        /// <summary>
        /// 查询所有菜单
        /// </summary>
        /// <param name="type">菜单类型</param>
        /// <returns>菜单集合</returns>
        IEnumerable<MMenu> QueryMenus(int type);

        /// <summary>
        /// 菜单查询(根据用户Id查询)
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="type">菜单类型</param>
        /// <returns>结果</returns>
        IEnumerable<MMenu> QueryUserMenus(Guid userId, int type);

        /// <summary>
        /// 菜单查询(根据用户Id查询默认权限)
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="type">菜单类型</param>
        /// <returns>结果</returns>
        IEnumerable<MMenu> QueryUserDefaultMenus(Guid userId, int type);
    }
}