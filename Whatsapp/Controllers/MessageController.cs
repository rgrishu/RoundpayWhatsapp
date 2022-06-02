using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Whatsapp.Controllers
{
    [Authorize]
    public class MessageController : Controller
    {
        [Route("MessageTemplate")]
        public IActionResult MessageTemplate()
        {
            return View();
        }
    }
}
