using System;
using System.Collections.Generic;

namespace Notify.Model.Transfer
{
    /// <summary>
    /// EsayUI框架菜单
    /// </summary>
    public class EsayUIMenu
    {
        /// <summary>
        /// 菜单Id
        /// </summary>
        public Guid MenuId { get; set; }

        /// <summary>
        /// 菜单名称
        /// </summary>
        public string MenuName { get; set; }

        /// <summary>
        /// 菜单图标
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// url
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 子菜单
        /// </summary>
        public IEnumerable<EsayUIMenu> Menus { get; set; }
    }
}