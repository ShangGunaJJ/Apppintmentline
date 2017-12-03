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
    public class FileDataService: AdminAppService,IFileDataService
    {
        public string Delete(List<string> id)
        {
            int detailCount = 0;
            int mValue = 0;
            try
            {
                mValue = this.DbContext.Delete<FileData>(a => id.Contains(a.Id));
                if (mValue > 0)
                {
                    detailCount = this.DbContext.Delete<FileData>(a => id.Contains(a.Id));
                }
            }
            catch (Exception e)
            {
                return e.ToString();
            }
            return "成功删除" + mValue + detailCount + "条。";
        }
        public FileData Add(AddFileDataInput input)
        {
            FileData entity = new FileData();
            entity.CreateTime = DateTime.Now;
            entity.Id = input.Id;
            entity.CreateUser = input.CreateUser;
            entity.FileName = input.FileName;
            entity.FileUrl = input.FileUrl;
            return this.DbContext.Insert(entity);
        }
        public int Update(AddFileDataInput input)
        {

            if (this.DbContext.Update<FileData>(a => a.Id == input.Id, a => new FileData()
            {
                CreateTime = DateTime.Now,
                FileName=input.FileName,
                FileUrl=input.FileUrl
            }) > 0)
            {
                return 1;
            }
            return 0;
        }
    }
}
