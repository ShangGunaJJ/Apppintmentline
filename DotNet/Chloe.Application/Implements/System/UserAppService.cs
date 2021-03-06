﻿using Ace;
using Ace.Exceptions;
using Ace.IdStrategy;
using Ace.Security;
using Chloe.Application.Common;
using Chloe.Application.Interfaces;
using Chloe.Application.Interfaces.System;
using Chloe.Application.Models.User;
using Chloe.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chloe.Application.Implements.System
{
    public class UserAppService : AdminAppService, IUserAppService
    {
        public void EnableAccount(string userId)
        {
            this.DbContext.Enable<MALU_Users>(userId);
        }
        public void DisableAccount(string userId)
        {
            this.DbContext.Disable<MALU_Users>(userId);
        }
        public void RevisePassword(string userId, string pwdText)
        {
            userId.NotNullOrEmpty("用户 Id 不能为空");
            PasswordHelper.EnsurePasswordLegal(pwdText);

            var user = this.DbContext.QueryByKey<MALU_Users>(userId);
            if (user == null || user.username.ToLower() == AppConsts.AdminUserName)
                throw new Ace.Exceptions.InvalidDataException("用户不存在");

            string userSecretkey = UserHelper.GenUserSecretkey();
            string encryptedPassword = PasswordHelper.Encrypt(pwdText, "invtax");

            this.DbContext.DoWithTransaction(() =>
            {
                this.DbContext.Update<MALU_Users>(a => a.Id == userId, a => new MALU_Users() { password = encryptedPassword });
                this.Log(Entities.Enums.LogType.Update, "User", true, "重置用户[{0}]密码".ToFormat(userId));
            });
        }
        public List<SimpleUserModel> GetSimpleModels()
        {
            var models = this.DbContext.Query<MALU_Users>().Select(a => new SimpleUserModel() { Id = a.Id, Name = a.RealName }).ToList();
            return models;
        }

        public void DeleteAccount(string userId)
        {
            userId.NotNullOrEmpty("用户 Id 不能为空");

            this.DbContext.DeleteByKey<MALU_Users>(userId);
        }

        public void AddUser(AddUserInput input)
        {
            input.Validate();

            string userName = input.UserName.ToLower();

            bool exists = this.DbContext.GetInv_users().Where(a => a.username == userName).Any();
            if (exists)
                throw new InvalidDataException("用户名[{0}]已存在".ToFormat(input.UserName));

            MALU_Users user = this.CreateEntity<MALU_Users>();
            user.username = userName;
            user.RoleId = input.RoleId;
            user.DutyId = input.DutyId;
            user.RealName = input.RealName;
            user.Gender = input.Gender;
            user.MobilePhone = input.MobilePhone;
            user.Birthday = input.Birthday;
            user.IsEnabled = 1;
            user.companyguid = input.companyguid;
            user.IsFirst = 1;
            string userSecretkey = UserHelper.GenUserSecretkey();
            string encryptedPassword = PasswordHelper.Encrypt(input.Password, "invtax");

            user.password = encryptedPassword;

            //Sys_UserLogOn logOnEntity = new Sys_UserLogOn();
            //logOnEntity.Id = IdHelper.CreateGuid();
            //logOnEntity.UserId = user.Id;
            //logOnEntity.UserSecretkey = userSecretkey;
            //logOnEntity.UserPassword = encryptedPassword;

            this.DbContext.DoWithTransaction(() =>
            {
                this.DbContext.Insert(user);
            });
        }
        public void UpdateUser(UpdateUserInput input)
        {
            input.Validate();

            this.DbContext.Update<MALU_Users>(a => a.Id == input.Id, a => new MALU_Users()
            {
                //OrganizeId = input.OrganizeId,
                RoleId = input.RoleId,
                DutyId = input.DutyId,
                RealName = input.RealName,
                Gender = input.Gender,
                MobilePhone = input.MobilePhone,
                Birthday = input.Birthday,
                IsEnabled = input.IsEnabled,
                companyguid = input.companyguid
            });
        }
        public int UpdateUserTheme(string t)
        {
            return this.DbContext.Update<MALU_Users>(a => a.Id == this.Session.UserId, a => new MALU_Users()
            {
                Theme = t
            });
        }
        public PagedData<MALU_Users> GetPageData(Pagination page, string keyword)
        {
            var q = this.DbContext.GetInv_users();

            q = q.WhereIfNotNullOrEmpty(keyword, a => a.username.Contains(keyword) || a.RealName.Contains(keyword) || a.MobilePhone.Contains(keyword));
            q = q.Where(a => a.username != AppConsts.AdminUserName);
            q = q.OrderBy(a => a.companyname).ThenByDesc(a => a.CreateTime);

            PagedData<MALU_Users> pagedData = q.TakePageData(page);

            return pagedData;
        }
    }
}
