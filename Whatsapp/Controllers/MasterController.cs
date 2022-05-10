using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Whatsapp.Interface;
using Whatsapp.Models;
using Whatsapp.Models.Data;
using Whatsapp.Models.ViewModel;

namespace Whatsapp.Controllers
{
    public class MasterController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ApplicationContext _appcontext;
        private IRepository<Users> _users;
        public MasterController(ILogger<HomeController> logger, ApplicationContext appcontext, IRepository<Users> users)
        {
            _logger = logger;
            _appcontext = appcontext;
            this._users = users;

        }
        [Route("Master/MasterServiceList")]
        [Route("MasterServiceList")]
        public IActionResult MasterServiceList()
        {
            return View();
        }
        [Route("Master/GetMasterServiceList")]
        [Route("GetMasterServiceList")]
        public void GetMasterServiceList()
        {
            
        }
    }
}
