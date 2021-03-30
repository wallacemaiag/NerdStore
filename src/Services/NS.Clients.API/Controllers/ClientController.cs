using Microsoft.AspNetCore.Mvc;
using NS.Clients.API.Application.Commands;
using NS.Core.MediatR;
using NS.WebApi.Core.Controllers;
using System;
using System.Threading.Tasks;

namespace NS.Clients.API.Controllers
{
    public class ClientController : MainController
    {
        private readonly IMediatrHandler _mediatrHandler;

        public ClientController(IMediatrHandler mediatrHandler)
        {
            _mediatrHandler = mediatrHandler;
        }

        [HttpGet("clients")]
        public async Task<IActionResult> Index()
        {
            var result = await _mediatrHandler.SendCommand(new RegisterClientCommand(Guid.NewGuid(), "Wallace Maia", "wallacemaiag@gmail.com", "13310731626"));

            return CustomResponse(result);
        }
    }
}
