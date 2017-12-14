﻿using Ace.IdStrategy;
using Chloe.Application.Interfaces;
using Chloe.Application.Interfaces.Appointment;
using Chloe.Application.Models.Appointment;
using Chloe.Entities;
using Chloe.Admin.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;

namespace Chloe.Admin.Areas.Appointment.Controllers
{
    [LoginAuthorizeAttribute]
    public class PlaceInfoController : WebController
    {
        // GET: Appointment/PlaceInfo
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddPeriod(AddPlaceInfoInput ti)
        {
            if (this.CreateService<IPlaceInfoService>().Add(ti).Id != "")
            {
                return this.AddSuccessMsg();
            }
            return this.AddSuccessMsg();
        } 
    }
}