using Microsoft.AspNetCore.Mvc;
using NS.WebApp.MVC.Models;
using System.Linq;

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

        protected void AddErrorValidation(string message)
        {
            ModelState.AddModelError(string.Empty, message);
        }

        protected bool ValidOperation()
        {
            return ModelState.ErrorCount == 0;
        }
    }
}
