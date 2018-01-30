using System;
using System.Collections.Generic;
using System.Text;
using Notify.DbCommon.Repositroies;
using Notify.Infrastructure.DomainBase;
using Notify.Infrastructure.EntityFactoryFramework;
using Notify.Infrastructure.UnitOfWork;
using Notify.IRepository;
using Notify.Model.DB;
using Notify.Model.Transfer;
using Notify.Repository.Factory;

namespace Notify.Repository.Mysql
{
    /// <summary>
    /// 公司仓储
    /// </summary>
    public class CompanyRepository : SqlRepositoryBase<Guid, MCompany>, ICompanyRepository
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="unitOfWork">工作单元</param>
        /// <param name="name">数据连接</param>
        public CompanyRepository(IPowerUnitOfWork unitOfWork, string name) : base(unitOfWork, name)
        {
        }

        /// <summary>
        /// 改为由子类创建实体，不使用工厂
        /// </summary>
        /// <returns>TValue</returns>
        protected override IEntityFactory<MCompany> BuildEntityFactory()
        {
            return new CompanyFactory();
        }

        /// <summary>
        /// 创建子对象回调
        /// </summary>
        /// <param name="childCallbacks">子对象集</param>
        protected override void BuildChildCallbacks(Dictionary<string, AppendChildData> childCallbacks)
        {
            childCallbacks.Add(
                "CompanyId",
                (account, obj) =>
                {
                    //using (AddressRepository repository = new AddressRepository(this.UnitOfWork, this.Name))
                    //{
                    //}
                    //string sql = "SELECT * FROM Address WHERE AccountId = @AccountId";
                    //this.ClearParameters();
                    //this.AddParameter("@AccountId", a.Id);
                    //a.Address = this.ExecuteReader(sql).ToModel<Address>();
                });
        }

        /// <summary>
        /// 根据主键查询实体
        /// </summary>
        /// <param name="key">主键</param>
        /// <returns>实体</returns>
        public override MCompany Query(Guid key)
        {
            string sql = "SELECT * FROM Company WHERE CompanyId = @CompanyId";
            this.ClearParameters();
            this.AddParameter("@CompanyId", key);
            return this.BuildEntityFromSql(sql);
        }

        /// <summary>
        /// 根据帐号查询用户
        /// </summary>
        /// <param name="accountNo">用户帐号</param>
        /// <returns>用户</returns>
        public MCompany Query(string accountNo)
        {
            string sql = "SELECT * FROM Company WHERE CompanyAccountNo = @CompanyAccountNo";
            this.ClearParameters();
            this.AddParameter("@CompanyAccountNo", accountNo);
            return this.BuildEntityFromSql(sql);
        }

        /// <summary>
        /// 公司分页查询
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns>结果</returns>
        public IEnumerable<MCompany> QueryCompanyByPagings(TCompanyCondition condition)
        {
            this.ClearParameters();
            StringBuilder sqlCondition = new StringBuilder("ParenttCompanyId = @ParenttCompanyId");
            this.AddParameter("@ParenttCompanyId", condition.ParenttCompanyId);
            if (!string.IsNullOrWhiteSpace(condition.AccountNo))
            {
                sqlCondition.Append(" AND AccountNo = @AccountNo ");
                this.AddParameter("@AccountNo", condition.AccountNo);
            }
            if (!string.IsNullOrWhiteSpace(condition.CompanyName))
            {
                sqlCondition.Append(" AND AccountName = @AccountName ");
                this.AddParameter("@AccountName", condition.CompanyName);
            }

            if (condition.GetRowsCount)
            {
                string sqlCount = "SELECT COUNT(0) FROM Company WHERE  " + sqlCondition + ";";
                object obj = this.ExecuteScalar(sqlCount);
                condition.RowsCount = obj == null ? 0 : Convert.ToInt32(obj);
            }

            string sqlData = "SELECT * FROM Company WHERE " + sqlCondition + " ORDER BY CreateTime DESC LIMIT @StratRows, @PageSize;";
            this.AddParameter("@StratRows", condition.StratRows);
            this.AddParameter("@PageSize", condition.PageSize);
            return this.BuildEntitiesFromSql(sqlData);
        }

        /// <summary>
        /// 添加实体(持久化)
        /// </summary>
        /// <param name="item">实体</param>
        public override void PersistNewItem(IEntity item)
        {
            var entity = (MCompany)item;
            this.ClearParameters();
            StringBuilder sql = new StringBuilder();
            sql.Append(" INSERT INTO Company ");
            sql.Append(" ( ");
            sql.Append("CompanyId,");
            sql.Append("CompanyAccountNo,");
            sql.Append("CompanyName,");
            sql.Append("ContactName,");
            sql.Append("ContactPhone,");
            sql.Append("CompanyAddress,");
            sql.Append("CreateTime,");
            sql.Append("ModifyTime,");
            sql.Append("ParenttCompanyId,");
            sql.Append("CompanyStatus");
            sql.Append(" ) VALUES ( ");
            sql.Append("@CompanyId,");
            sql.Append("@CompanyAccountNo,");
            sql.Append("@CompanyName,");
            sql.Append("@ContactName,");
            sql.Append("@ContactPhone,");
            sql.Append("@CompanyAddress,");
            sql.Append("@CreateTime,");
            sql.Append("@ModifyTime,");
            sql.Append("@ParenttCompanyId,");
            sql.Append("@CompanyStatus");
            sql.Append(" ); ");

            this.AddParameter("@CompanyId", entity.Key);
            this.AddParameter("@CompanyAccountNo", entity.CompanyAccountNo);
            this.AddParameter("@CompanyName", entity.CompanyName);
            this.AddParameter("@ContactName", entity.ContactName);
            this.AddParameter("@ContactPhone", entity.ContactPhone);
            this.AddParameter("@CompanyAddress", entity.CompanyAddress);
            this.AddParameter("@CreateTime", entity.CreateTime);
            this.AddParameter("@ModifyTime", entity.ModifyTime);
            this.AddParameter("@ParenttCompanyId", entity.ParenttCompanyId);
            this.AddParameter("@CompanyStatus", entity.CompanyStatus);

            this.ExecuteNonQuery(sql.ToString());
        }

        /// <summary>
        /// 修改实体(持久化)
        /// </summary>
        /// <param name="item">实体</param>
        public override void PersistUpdatedItem(IEntity item)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 删除实体(持久化)
        /// </summary>
        /// <param name="item">实体</param>
        public override void PersistDeletedItem(IEntity item)
        {
            throw new NotImplementedException();
        }
    }
}