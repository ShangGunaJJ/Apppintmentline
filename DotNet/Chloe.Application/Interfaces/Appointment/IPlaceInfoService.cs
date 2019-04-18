using Ace.Application;
using Chloe.Entities;
using Chloe.Application.Models.Appointment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ace;

namespace Chloe.Application.Interfaces.Appointment
{
    public interface IPlaceInfoService: IAppService
    {
        string Delete(string id);
        PlaceInfo Add(AddPlaceInfoInput input);
        int Update(UpdatePlaceInfoInput input);
        List<SimpleModelcs> GetSimple();
        PagedData<PlaceInfo> GetPageData(Pagination page, string keyword);
        bool IsAdd(string Name, string code);
    }
}
