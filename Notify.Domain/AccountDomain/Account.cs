using System;
using System.Collections.Generic;
using Notify.Code.Exception;
using Notify.Code.Extension;
using Notify.Domain.CompanyDomain;
using Notify.Domain.PermissionDomain;
using Notify.Domain.VerificationCodeDomain;
using Notify.Infrastructure.DomainBase;
using Notify.Model;
using Notify.Model.Transfer;
using Notify.Domain.MenuDomain;
using Notify.Model.DB;

namespace Notify.Domain.AccountDomain
{
    /// <summary>
    /// 账户
    /// </summary>
    public class Account : EntityBase<Guid>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="registerAccount">注册信息</param>
        public Account(RegisterAccount registerAccount)
            : base(Guid.NewGuid())
        {
            this.AccountName = registerAccount.AccountName;
            this.AccountNO = registerAccount.AccountNO;
            this.Mail = registerAccount.Mail;
            this.Mobile = registerAccount.Mobile;
            this.Password = registerAccount.Password;
            this.PayPassword = registerAccount.PayPassword;
            this.Status = AccountStatus.Enabled;
            this.CreateTime = DateTime.Now;
            this.IsAdmin = false;
            this.VerificationCode = new VerificationCode(this);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="id">用户Id</param>
        public Account(Guid id)
            : base(id)
        {
        }

        /// <summary>
        /// 帐号
        /// </summary>
        public string AccountNO { get; set; }

        /// <summary>
        /// 账户名(昵称)
        /// </summary>
        public string AccountName { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Mail { get; set; }

        /// <summary>
        /// 手机
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 登录密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 支付密码
        /// </summary>
        public string PayPassword { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 是否是管理员
        /// </summary>
        public bool IsAdmin { get; set; }

        /// <summary>
        /// 帐号状态
        /// </summary>
        public AccountStatus Status { get; set; }

        /// <summary>
        /// 验证码
        /// </summary>
        public VerificationCode VerificationCode { get; set; }

        /// <summary>
        /// 公司Id
        /// </summary>
        public Guid CompanyId { get; set; }

        /// <summary>
        /// 公司
        /// </summary>
        private Lazy<Company> companyLazy;

        /// <summary>
        /// 公司
        /// </summary>
        public Lazy<Company> Company
        {
            get
            {
                if (this.companyLazy != null)
                {
                    return companyLazy;
                }
                this.companyLazy = new Lazy<Company>(() =>
                {
                    MCompany mCompany = CompanyService.QueryMCompanyByComapnyId(this.CompanyId);
                    if (mCompany != null)
                    {
                        return mCompany.ToCompany();
                    }
                    else
                    {
                        return new Company();
                    }
                });
                return this.companyLazy;
            }
        }

        /// <summary>
        /// 权限
        /// </summary>
        public Dictionary<int, PermissionCollection> Permission { get; set; }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="type">系统类型</param>
        /// <param name="password">登录密码</param>
        public LoginResult Login(string password, int type)
        {
            this.ValidateLogin(password);

            // 加载菜单
            LoginResult result = new LoginResult();
            var permissionCollection = PermissionService.QueryPermissionOfUser(this, type);
            result.Account = this.ToTAccount();
            result.Menu = permissionCollection.Menus;
            result.Result.IsSucceed = true;
            result.Result.Message = "登录成功";
            if (this.Permission == null)
            {
                this.Permission = new Dictionary<int, PermissionCollection>();
            }
            if (this.Permission.ContainsKey(type))
            {
                this.Permission[type] = permissionCollection;
            }
            else
            {
                this.Permission.Add(type, permissionCollection);
            }
            return result;
        }

        /// <summary>
        /// 注册
        /// </summary>
        public void Register()
        {
            this.ValidateRegister();
            this.EncryptPassword();
            this.EncryptPayPassword();
            this.VerificationCode.CreateMailCode(this.Mail);
            this.Company.Value.Register(this, null);
        }

        /// <summary>
        /// 添加用户
        /// </summary>
        public void AddUser()
        {
            this.ValidateRegister();
            this.EncryptPassword();
            this.EncryptPayPassword();
        }

        /// <summary>
        /// 验证注册信息
        /// </summary>
        public void ValidateRegister()
        {
            if (string.IsNullOrWhiteSpace(this.AccountNO))
            {
                throw new CustomException("帐号为空");
            }
            if (string.IsNullOrWhiteSpace(this.AccountName))
            {
                throw new CustomException("帐号名称为空");
            }
            if (string.IsNullOrWhiteSpace(this.Password))
            {
                throw new CustomException("帐号密码为空");
            }
            if (string.IsNullOrWhiteSpace(this.Mail))
            {
                throw new CustomException("帐号绑定邮箱为空");
            }
        }

        /// <summary>
        /// 验证登录
        /// </summary>
        /// <param name="password">登录密码</param>
        public void ValidateLogin(string password)
        {
            if (this.Key == Guid.Empty)
            {
                throw new CustomException("帐号不存在");
            }
            if (this.Password != password.ToMd5())
            {
                throw new CustomException("帐号登录密码错误");
            }
        }

        /// <summary>
        /// 登录密码加密
        /// </summary>
        public void EncryptPassword()
        {
            this.Password = this.Password.ToMd5();
        }

        /// <summary>
        /// 支付密码加密
        /// </summary>
        public void EncryptPayPassword()
        {
            if (!string.IsNullOrEmpty(this.PayPassword))
            {
                this.PayPassword = this.PayPassword.ToMd5();
            }
        }
    }
}