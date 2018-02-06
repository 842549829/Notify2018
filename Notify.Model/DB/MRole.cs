using System;
using Notify.Infrastructure.DomainBase;

namespace Notify.Model.DB
{
    /// <summary>
    /// 角色
    /// </summary>
    public class MRole: IEntity
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName { get; set; }

        /// <summary>
        /// 角色描述
        /// </summary>
        public string RoleDescription { get; set; }

        /// <summary>
        /// 所属公司Id
        /// </summary>
        public Guid CompanyId { get; set; }

        /// <summary>
        /// 是否默认角色
        /// </summary>
        public bool IsDefaultRole { get; set; }

        /// <summary>
        /// 角色类型 0为员工角色 1为公司角色
        /// </summary>
        public int RoleType { get; set; }

        /// <summary>
        /// 主键Id
        /// </summary>
        public object Key => this.Id;
    }
}