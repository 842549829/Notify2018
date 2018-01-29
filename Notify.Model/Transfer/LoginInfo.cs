namespace Notify.Model.Transfer
{
    /// <summary>
    /// 登录信息
    /// </summary>
    public class LoginInfo
    {
        /// <summary>
        /// 登录帐号
        /// </summary>
        public string AccountNo { get; set; }

        /// <summary>
        /// 登录密码
        /// </summary>
        public string LoginPassword { get; set; }

        /// <summary>
        /// 登录IP
        /// </summary>
        public string ClinetIp { get; set; }

        /// <summary>
        /// 登录系统类型
        /// </summary>
        public int SysType { get; set; }
    }
}