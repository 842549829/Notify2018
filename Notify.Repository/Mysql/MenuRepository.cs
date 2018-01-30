using System;
using System.Collections.Generic;
using System.Text;
using Notify.DbCommon.Repositroies;
using Notify.Infrastructure.DomainBase;
using Notify.Infrastructure.EntityFactoryFramework;
using Notify.Infrastructure.UnitOfWork;
using Notify.IRepository;
using Notify.Model.DB;
using Notify.Repository.Factory;

namespace Notify.Repository.Mysql
{
    /// <summary>
    /// 菜单仓储
    /// </summary>
    public class MenuRepository : SqlRepositoryBase<Guid, MMenu>, IMenuRepository
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="unitOfWork">工作单元</param>
        /// <param name="name">数据链接</param>
        public MenuRepository(IPowerUnitOfWork unitOfWork, string name) : base(unitOfWork, name)
        {
        }

        /// <summary>
        /// 添加菜单
        /// </summary>
        /// <param name="item">菜单</param>
        public override void PersistNewItem(IEntity item)
        {
            this.ClearParameters();
            var entity = (MMenu)item;
            StringBuilder sql = new StringBuilder();
            sql.Append("INSERT INTO Menu");
            sql.Append(" ( ");
            sql.Append(" Id,");
            sql.Append(" Title,");
            sql.Append(" Description,");
            sql.Append(" ParentId,");
            sql.Append(" Url,");
            sql.Append(" Sort,");
            sql.Append(" Icon,");
            sql.Append(" MenuType");
            sql.Append(" ) VALUES (");
            sql.Append(" @Id,");
            sql.Append(" @Title,");
            sql.Append(" @Description,");
            sql.Append(" @ParentId,");
            sql.Append(" @Url,");
            sql.Append(" @Sort,");
            sql.Append(" @Icon,");
            sql.Append(" @MenuType");
            sql.Append(" ); ");

            this.AddParameter("@Id", entity.Id);
            this.AddParameter("@Title", entity.Title);
            this.AddParameter("@Description", entity.Description);
            this.AddParameter("@ParentId", entity.ParentId);
            this.AddParameter("@Url", entity.Url);
            this.AddParameter("@Sort", entity.Sort);
            this.AddParameter("@Icon", entity.Icon);
            this.AddParameter("@MenuType", entity.MenuType);

            this.ExecuteNonQuery(sql.ToString());
        }

        /// <summary>
        /// 修改菜单
        /// </summary>
        /// <param name="item">菜单</param>
        public override void PersistUpdatedItem(IEntity item)
        {
            this.ClearParameters();
            var entity = (MMenu)item;
            StringBuilder sql = new StringBuilder();
            sql.Append("UPDATE Menu SET");
            sql.Append(" Title = @Title,");
            sql.Append(" Description = @Description,");
            sql.Append(" ParentId = @ParentId,");
            sql.Append(" Url = @Url,");
            sql.Append(" Sort = @Sort,");
            sql.Append(" Icon = @Icon");
            sql.Append(" WHERE Id = @Id;");

            this.AddParameter("@Id", entity.Id);
            this.AddParameter("@Title", entity.Title);
            this.AddParameter("@Description", entity.Description);
            this.AddParameter("@ParentId", entity.ParentId);
            this.AddParameter("@Url", entity.Url);
            this.AddParameter("@Sort", entity.Sort);
            this.AddParameter("@Icon", entity.Icon);

            this.ExecuteNonQuery(sql.ToString());
        }

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="item">菜单</param>
        public override void PersistDeletedItem(IEntity item)
        {
            this.ClearParameters();
            const string sql = "DELETE FROM Menu WHERE Id = @Id;";
            this.AddParameter("@Id", item.Key);
            this.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 创建菜单仓储工作
        /// </summary>
        protected override IEntityFactory<MMenu> BuildEntityFactory()
        {
            return new MenuFactory();
        }

        /// <summary>
        /// 加载菜单子对象
        /// </summary>
        /// <param name="childCallbacks">子对象委托</param>
        protected override void BuildChildCallbacks(Dictionary<string, AppendChildData> childCallbacks)
        {
        }

        /// <summary>
        /// 查询父级默认Id
        /// </summary>
        /// <param name="type">菜单类型</param>
        /// <returns>父级默认Id</returns>
        public Guid QueryDefaultParentId(int type)
        {
            this.ClearParameters();
            const string sql = "SELECT * FROM Menu WHERE ParentId = @ParentId AND MenuType = @MenuType LIMIT 1;";
            this.AddParameter("@MenuType", type);
            this.AddParameter("@ParentId", Guid.Empty.ToString());
            return Guid.Parse(this.ExecuteScalar(sql).ToString());
        }

        /// <summary>
        /// 查询菜单
        /// </summary>
        /// <param name="key">主键</param>
        /// <returns>菜单</returns>
        public override MMenu Query(Guid key)
        {
            this.ClearParameters();
            const string sql = "SELECT * FROM Menu WHERE Id = @Id;";
            this.AddParameter("@Id", key);
            return this.BuildEntityFromSql(sql);
        }

        /// <summary>
        /// 查询所有菜单
        /// </summary>
        /// <param name="type">菜单类型</param>
        /// <returns>菜单集合</returns>
        public IEnumerable<MMenu> QueryMenus(int type)
        {
            this.ClearParameters();
            const string sql = "SELECT * FROM Menu WHERE MenuType = @MenuType ORDER BY Sort;";
            this.AddParameter("@MenuType", type);
            return this.BuildEntitiesFromSql(sql);
        }

        /// <summary>
        /// 菜单查询(根据用户Id查询)
        /// </summary>
        /// <param name="userId">个人用类型</param>
        /// <param name="type">菜单类型</param>
        /// <returns>结果</returns>
        public IEnumerable<MMenu> QueryUserMenus(Guid userId, int type)
        {
            this.ClearParameters();
            const string sql = "SELECT * FROM Menu WHERE MenuType = @MenuType AND Id IN(SELECT MenuId FROM RolePermissions WHERE RoleId IN(SELECT RoleId FROM RoleUserRelationship WHERE UserId = @UserId AND IsDefaultRole = 0)) ORDER BY Sort;";
            this.AddParameter("@MenuType", type);
            this.AddParameter("@UserId", userId);
            return this.BuildEntitiesFromSql(sql);
        }

        /// <summary>
        /// 菜单查询(根据用户Id查询默认权限)
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="type">菜单类型</param>
        /// <returns>结果</returns>
        public IEnumerable<MMenu> QueryUserDefaultMenus(Guid userId, int type)
        {
            this.ClearParameters();
            const string sql = "SELECT * FROM Menu WHERE MenuType = @MenuType AND Id IN(SELECT MenuId FROM RolePermissions WHERE RoleId IN(SELECT RoleId FROM RoleUserRelationship WHERE UserId = @UserId AND IsDefaultRole = 1)) ORDER BY Sort;";
            this.AddParameter("@MenuType", type);
            this.AddParameter("@UserId", userId);
            return this.BuildEntitiesFromSql(sql);
        }
    }
}