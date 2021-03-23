﻿using Microsoft.AspNetCore.Mvc;
using NS.WebApp.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NS.WebApp.MVC.Controllers
{
    public class MainController : Controller
    {
        protected bool HasErrorsResponse(ResponseResult response)
        {
            if (response != null && response.Errors.Messages.Any())
            {
                foreach (var message in response.Errors.Messages)
                {
                    ModelState.AddModelError(string.Empty, message);
                }
                return true;
            }

            return false;
        }
    }
}
