﻿using Ace.Application;
using Chloe.Entities;
using Chloe.Application.Models.Appointment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chloe.Application.Interfaces.System
{
    public interface IFileDataService: IAppService
    {
        string Delete(List<string> id);
        FileData Add(AddFileDataInput input);
        int Update(AddFileDataInput input);
    }
}
