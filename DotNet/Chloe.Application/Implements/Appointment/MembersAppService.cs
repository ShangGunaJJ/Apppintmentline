using Ace.IdStrategy;
using Chloe.Application.Interfaces.System;
using Chloe.Application.Models.Appointment;
using Chloe.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chloe.Application.Implements.Appointment
{
  public  class MembersAppService: AdminAppService, IMembersAppService
    {
        public string Delete(List<string> id)
        {
            int detailCount = 0;
            int mValue = 0;
            try
            {
                mValue = this.DbContext.Delete<MALU_Members>(a => id.Contains(a.Id));
                if (mValue > 0)
                {
                    detailCount = this.DbContext.Delete<MALU_Members>(a => id.Contains(a.Id));
                }
            }
            catch (Exception e)
            {
                return e.ToString();
            }
            return "成功删除" + mValue+ detailCount + "条。";
        }
        public MALU_Members Add(MALU_Members input)
        {
            MALU_Members entity = new MALU_Members();
            entity.CreateTime = DateTime.Now;
            entity.Id = input.Id;
            entity.IdCard = input.IdCard;
            entity.MobilePhone = input.MobilePhone;
            entity.Name = input.Name;
            entity.PassWord = input.PassWord;
            entity.WeChatKey = input.WeChatKey;
            entity.WeChatName = input.WeChatName;
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
                PassWord = input.PassWord,
                WeChatKey = input.WeChatKey,
                WeChatName = input.WeChatName
            }) > 0)
            {
                return 1;
            }
            return 0;
        }
    }
}
