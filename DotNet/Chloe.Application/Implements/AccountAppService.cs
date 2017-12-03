using Ace.Security;
using Chloe.Application.Common;
using Chloe.Application.Interfaces;
using Chloe.Application.Models.Account;
using Chloe.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chloe.Application.Implements
{
    public class AccountAppService : AdminAppService, IAccountAppService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password">前端传过来的是经过md5加密后的密码</param>
        /// <param name="user"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public bool CheckLogin(string userName, string password, out MALU_Users user, out Sys_Role role, out string msg)
        {
            userName.NotNullOrEmpty();
            password.NotNullOrEmpty();

            user = null;
            msg = null;
            role = null;
            var Role = this.DbContext.GetSys_Role();
            var users = this.DbContext.GetInv_users().LeftJoin(Role, (u, r) => u.RoleId == r.Id);
            var view = users.Select((u, r) => new { User = u, Role = r });


            var viewEntity = view.FirstOrDefault(a => a.User.username == userName);

            if (viewEntity == null)
            {
                msg = "账户不存在，请重新输入";
                return false;
            }

            //if (viewEntity.User.IsEnabled == 0)
            //{
            //    msg = "账户被系统锁定,请联系管理员";
            //    return false;
            //}

            MALU_Users userEntity = viewEntity.User;
            string dbPassword = PasswordHelper.EncryptMD5Password(password, "invtax");
            if (dbPassword != userEntity.password)
            {
                msg = "密码不正确，请重新输入";
                return false;
            }
            user = userEntity;
            role = viewEntity.Role;
            return true;
        }

        public List<string> GetRolePeople(List<string> GetCompanysID)
        {
            if (this.Session._IsAdmin)
            {
                return this.DbContext.Query<MALU_Users>().Where(a => GetCompanysID.Contains(a.companyguid)).Select(a => a.Id).ToList();
            }
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="oldPassword">明文</param>
        /// <param name="newPassword">明文</param>
        public void ChangePassword(string oldPassword, string newPassword)
        {
            PasswordHelper.EnsurePasswordLegal(newPassword);

            AdminSession session = this.Session;

            MALU_Users userLogOn = this.DbContext.Query<MALU_Users>().Where(a => a.Id == session.UserId).First();

            string encryptedOldPassword = PasswordHelper.Encrypt(oldPassword, "invtax");

            if (encryptedOldPassword != userLogOn.password)
                throw new Ace.Exceptions.InvalidDataException("旧密码不正确");

            string newEncryptedPassword = PasswordHelper.Encrypt(newPassword, "invtax");

            this.DbContext.DoWithTransaction(() =>
            {
                this.DbContext.Update<MALU_Users>(a => a.Id == session.UserId, a => new MALU_Users() { password = newEncryptedPassword });
                // this.Log(Entities.Enums.LogType.Update, "Account", true, "用户[{0}]修改密码".ToFormat(session.UserId));
            });
        }

        public void ModifyInfo(ModifyAccountInfoInput input)
        {
            input.Validate();

            var session = this.Session;

            this.DbContext.Update<MALU_Users>(a => a.Id == session.UserId, a => new MALU_Users()
            {
                RealName = input.RealName,
                Gender = input.Gender,
                MobilePhone = input.MobilePhone,
                Birthday = input.Birthday
            });
        }
    }
}
