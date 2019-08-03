using BPMSApi;
using BPMSWebApplication.Configs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BPMSWebApplication.Controllers
{
    public class BaseController : Controller
    {
        protected IBPMSSettings Settings { get; set; }
        protected BPMSApiInstance BPMSApiInstance { get; set; }

        public BaseController(IBPMSSettings settings)
        {
            Settings = settings;
            BPMSApiInstance = new BPMSApiInstance(new BPMSConfig
            {
                ApiKey = settings.ApiKey,
                Credentials = settings.Credentials
            });
        }

        public BaseController()
        {
            //Settings = settings;
            //BPMSApiInstance = new BPMSApiInstance(new BPMSConfig
            //{
            //    ApiKey = settings.ApiKey,
            //    Credentials = settings.Credentials
            //});
        }

    }
}
