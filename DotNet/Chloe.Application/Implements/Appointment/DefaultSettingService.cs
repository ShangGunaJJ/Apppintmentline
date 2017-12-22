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
    public class DefaultSettingService : AdminAppService, IDefaultSettingService
    {
        public string Delete(List<string> id)
        {
            int detailCount = 0;
            int mValue = 0;
            try
            {
                mValue = this.DbContext.Delete<MALU_DefaultSetting>(a => id.Contains(a.Id));
            }
            catch (Exception e)
            {
                return e.ToString();
            }
            return "成功删除" + mValue + detailCount + "条。";
        }
        public string DeleteByKey(string KeyName)
        {
            int detailCount = 0;
            int mValue = 0;
            try
            {
                mValue = this.DbContext.Delete<MALU_DefaultSetting>(a => KeyName == a.KeyName);
            }
            catch (Exception e)
            {
                return e.ToString();
            }
            return "成功删除" + mValue + detailCount + "条。";
        }
        public MALU_DefaultSetting Add(AddDefaultSettingInput input)
        {
            MALU_DefaultSetting entity = this.CreateEntity<MALU_DefaultSetting>();
            entity.CreateTime = DateTime.Now;
            entity.Id = input.Id; 
            entity.KeyName = input.KeyName;
            entity.KeyType = input.KeyType;
            entity.Value = input.Value;

            return this.DbContext.Insert(entity);
        }
        public int Update(UpdateDefaultSettingInput input)
        {

            if (this.DbContext.Update<MALU_DefaultSetting>(a => a.Id == input.Id, a => new MALU_DefaultSetting()
            {
                KeyName = input.KeyName,
                KeyType = input.KeyType,
                Value = input.Value,
                UpdateDate = DateTime.Now
            }) > 0)
            {
                return 1;
            }
            return 0;
        }

        public int UpdateByKey(UpdateDefaultSettingInput input)
        {
            if (this.DbContext.Update<MALU_DefaultSetting>(a => a.KeyType == input.KeyType && a.KeyName == input.KeyName, a => new MALU_DefaultSetting()
            {
                KeyName = input.KeyName,
                KeyType = input.KeyType,
                Value = input.Value,
                UpdateDate = DateTime.Now
            }) > 0)
            {
                return 1;
            }
            return 0;
        }
    }
}
