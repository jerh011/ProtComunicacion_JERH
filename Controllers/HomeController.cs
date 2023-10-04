using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Twilio;
using Microsoft.Extensions.Configuration;
using Twilio.Rest.Api.V2010.Account;
using ProtComunicacion_JERH.Models;

namespace ProtComunicacion_JERH.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;

        public HomeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            return View(new MessageViewModel());
        }

        [HttpPost]
        public IActionResult SendSms(MessageViewModel model)
        {
            if (ModelState.IsValid)
            {
                var accountSid = _configuration["Twilio:AccountSid"];
                var authToken = _configuration["Twilio:AuthToken"];

                TwilioClient.Init(accountSid, authToken);

                var messageResource = MessageResource.Create(
                    body: model.Message,
                    from: new Twilio.Types.PhoneNumber(""), // Reemplaza con tu número de Twilio, en este caso, "+1 256 661 4023"
                    to: new Twilio.Types.PhoneNumber(model.To)
                );

                // Puedes agregar un mensaje de éxito o manejar redireccionamientos según tus necesidades.
                ViewBag.SuccessMessage = "Mensaje enviado con éxito.";
            }

            return View("Index", model);
        }
        //526221443188
    }
}
