using Ace;
using Ace.IdStrategy;
using Chloe.Application.Interfaces.Appointment;
using Chloe.Application.Models.Appointment;
using Chloe.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chloe.Application.Implements.Appointment
{
    public class MembersAppService : AdminAppService, IMembersAppService
    {
        public int Delete(string Id, int _state)
        {
            if (this.DbContext.Update<MALU_Members>(a => a.Id == Id, a => new MALU_Members()
            {
                state = _state
            }) > 0)
            {
                return 1;
            }
            return 0;
        }
        public MALU_Members Add(AddMembersInput input)
        {
            MALU_Members entity =this.CreateEntity<MALU_Members>();
            entity.IdCard = input.IdCard;
            entity.MobilePhone = input.MobilePhone;
            entity.Name = input.Name;
            // entity.PassWord = input.PassWord;
            entity.WeChatKey = input.WeChatKey;
            entity.WeChatName = input.WeChatName;
            entity.state = 1;
            return this.DbContext.Insert(entity);
        }
        public int Update(AddMembersInput input)
        {

            if (this.DbContext.Update<MALU_Members>(a => a.Id == input.Id, a => new MALU_Members()
            {
                CreateTime = DateTime.Now,
                IdCard = input.IdCard,
                MobilePhone = input.MobilePhone,
                Name = input.Name,
                state=input.state,
                PassWord = input.PassWord,
                WeChatKey = input.WeChatKey,
                WeChatName = input.WeChatName
            }) > 0)
            {
                return 1;
            }
            return 0;
        }

        public int IsReg(string IDCARD) {
            return this.DbContext.Query<MALU_Members>().Where(a => a.IdCard == IDCARD).ToList().Count();
        }
        public MALU_Members GetInfoByID(string ID)
        {
            return this.DbContext.Query<MALU_Members>().Where(a => a.Id == ID).ToList()[0];
        }

        public void SelectByOpenID(string OpenID, ref string Id, ref string Name, ref string IdCard, ref string phone,ref int state)
        {
            List<MALU_Members> mml = this.DbContext.Query<MALU_Members>().Select(a => new MALU_Members()
            {
                IdCard = a.IdCard,
                Id = a.Id,
                MobilePhone = a.MobilePhone,
                Name = a.Name,
                WeChatName = a.WeChatName,
                WeChatKey = a.WeChatKey,
                state=a.state
            }).Where(a => a.WeChatKey == OpenID).ToList();
            if (mml.Count > 0)
            {
                Id = mml[0].Id;
                Name = mml[0].Name;
                IdCard = mml[0].IdCard;
                phone = mml[0].MobilePhone;
                state = mml[0].state;
            }
        }

        public string GetIDCard(string mID) {
            return this.DbContext.Query<MALU_Members>().Where(a => a.Id == mID).Select(a => new { IDCard = a.IdCard }).ToList()[0].IDCard;
        }

        public PagedData<MALU_Members> GetPageData(Pagination page, string keyword,int? state)
        {
            var q = this.DbContext.Query<MALU_Members>();
            q = q.WhereIfNotNullOrEmpty(keyword, a => a.Name.Contains(keyword) || a.MobilePhone.Contains(keyword)).WhereIf(state != 0, a => a.state == state);
            q = q.OrderBy(a => a.CreateTime);
            PagedData<MALU_Members> pagedData = q.TakePageData(page);
            return pagedData;
        }
    }
}
