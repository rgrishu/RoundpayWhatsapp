using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WAEFCore22.AppCode.BusinessLogic;
using WAEFCore22.AppCode.Interface.Repos;
using Whatsapp.Interface;
using Whatsapp.Models;
using Whatsapp.Models.Data;
using Whatsapp.Models.UtilityModel;
using Whatsapp.Models.ViewModel;

namespace Whatsapp.Controllers
{
    public class MasterController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ApplicationContext _appcontext;
        private IRepository<Users> _users;
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        public MasterController(ILogger<HomeController> logger, ApplicationContext appcontext, IRepository<Users> users, IUnitOfWorkFactory unitOfWorkFactory)
        {
            _logger = logger;
            _appcontext = appcontext;
            this._users = users;
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        [Route("MasterServiceList")]
        public IActionResult MasterServiceList()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetMasterServiceList()
        {
            List<MasterService> masterServices = new List<MasterService>();
            masterServices = await _appcontext.MasterService.ToListAsync().ConfigureAwait(false);
            return PartialView("~/Views/Master/PartialView/_MasterServiceList.cshtml", masterServices);
        }
        public async Task<IActionResult> Create(int? id)
        {
            MasterService masterService = null;
            if (id != 0)
            {
                masterService = await _appcontext.MasterService
                    .Where(h => h.ServiceID == id)
                    .FirstOrDefaultAsync();
            }
            return PartialView("~/Views/Master/PartialView/_AddService.cshtml", masterService);
        }
        [HttpPost]
        public async Task<IActionResult> AddService(MasterService masterService)
        {
            var res = new Response()
            {
                StatusCode = (int)ResponseStatus.Failed,
                ResponseText = "Failed"
            };
            var ms = new MasterServices(_unitOfWorkFactory);
            if (ModelState.IsValid)
            {
                if (masterService.ServiceID == 0)
                {
                    res = await ms.InsertMasterService(masterService);
                }
                else
                {
                    res = await ms.UpdateMasterService(masterService);
                }
            }
            return Json(res);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            MasterService masterService = new MasterService();
            masterService = await _appcontext.MasterService.Where(a => a.ServiceID == id).FirstOrDefaultAsync().ConfigureAwait(false);
            _appcontext.MasterService.Remove(masterService);
            await _appcontext.SaveChangesAsync();
            return View("MasterServiceList");
        }
    }
}
