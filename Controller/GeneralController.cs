﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Utilities.Enums;
using Utilities.GlobalManagers;
using Utilities.GlobalManagers.CRM;
using Utilities.GlobalViewModels;
using Utilities.Helpers;
namespace Utilities.Controller
{

    [RoutePrefix("{lang}/api/General")]
    public class GeneralController : BaseApiController
    {
        [HttpGet]
        [Route("FirstVisitExpireAfter")]
        public HttpResponseMessage FirstVisitExpireAfter()
        {
            using (GeneralManager _mngr = new GeneralManager(RequestUtility))
            {
                var result = _mngr.GetFirstVisitExpiryDate();
                return Response<string>(result);
            }
        }
    }
}

