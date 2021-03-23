﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NS.WebApp.MVC.Models;
using System.Diagnostics;

namespace NS.WebApp.MVC.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [Route("erro/{id:length(3,3)}")]
        public IActionResult Error(int id)
        {
            var modelError = new ErrorViewModel();

            if(id == 500)
            {
                modelError.Message = "Ocorreu um erro! Tente novamente mais tarde ou entre em contato com nosso suporte";
                modelError.Title = "Ocorreu um erro!";
                modelError.ErrorCode = id;
            }
            else if(id == 404)
            {
                modelError.Message = "A página que esta tentando acessar não foi encontrada < /br> " +
                                        "Em caso de dúvidas entre em contato com o nosso suporte";
                modelError.Title = "Ops! Página não encontrada :(";
                modelError.ErrorCode = id;
            }
            else if(id == 403)
            {
                modelError.Message = "Você não tem permissão para fazer isso!";
                modelError.Title = "Acesso negado!";
                modelError.ErrorCode = id;
            }
            else
            {
                return StatusCode(404);
            }

            return View("Error", modelError);
        }
    }
}
