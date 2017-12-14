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
   public class PlaceInfoService: AdminAppService,IPlaceInfoService
    {
        public string Delete(List<string> id)
        {
            int detailCount = 0;
            int mValue = 0;
            try
            {
                mValue = this.DbContext.Delete<PlaceInfo>(a => id.Contains(a.Id));
                if (mValue > 0)
                {
                    detailCount = this.DbContext.Delete<PlaceInfo>(a => id.Contains(a.Id));
                }
            }
            catch (Exception e)
            {
                return e.ToString();
            }
            return "成功删除" + mValue + detailCount + "条。";
        }
        public PlaceInfo Add(AddPlaceInfoInput input)
        {
            PlaceInfo entity = this.CreateEntity<PlaceInfo>();
            entity.Address = input.Address;
            entity.Code = input.Code;
            entity.Describe = input.Describe;
            entity.CreateUser = input.CreateUser;
            entity.PlaceName = input.PlaceName;
            return this.DbContext.Insert(entity);
        }
        public int Update(UpdatePlaceInfoInput input)
        {

            if (this.DbContext.Update<PlaceInfo>(a => a.Id == input.Id, a => new PlaceInfo()
            {
                Address = input.Address,
                Code = input.Code,
                Describe = input.Describe,
                PlaceName=input.PlaceName,
                UpdateTime= DateTime.Now
        }) > 0)
            {
                return 1;
            }
            return 0;
        }

        public List<SimpleModelcs> GetSimple()
        {
            var models = this.DbContext.Query<PlaceInfo>().Select(a => new SimpleModelcs() { Id = a.Id, Name = a.PlaceName }).ToList();
            return models;
        }
    }
}
