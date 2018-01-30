using System.Web.Mvc;
using Common.Code;
using MvcPager;
using Notify.Code.Code;
using Notify.Controller.Base;
using Notify.Model.Transfer;
using Notify.Service;

namespace Notify.Controller.Account
{
    /// <summary>
    /// 公司控制器
    /// </summary>
    public class CompanyController : BaseController
    {
        /// <summary>
        /// 用户列表
        /// </summary>
        /// <returns>结果</returns>
        public ActionResult CompanyList(TCompanyCondition condition)
        {
            if (condition != null)
            {
                condition.ParenttCompanyId = Company.CompanyId;
                var data = AccountService.QueryCompanyByPagings(condition);
                PagedList<TCompany> pageList = new PagedList<TCompany>(data, condition.PageIndex, condition.PageSize, condition.RowsCount);
                ViewModel<TCompanyCondition, PagedList<TCompany>> result = new ViewModel<TCompanyCondition, PagedList<TCompany>>
                {
                    Condition = condition,
                    Data = pageList
                };
                return this.View(result);
            }
            else
            {
                return this.View();
            }
        }
    }
}