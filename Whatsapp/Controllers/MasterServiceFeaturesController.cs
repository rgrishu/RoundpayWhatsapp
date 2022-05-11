using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Whatsapp.Interface;
using Whatsapp.Models;
using Whatsapp.Models.Data;
using Whatsapp.Models.ViewModel;

namespace Whatsapp.Controllers
{
    public class MasterServiceFeaturesController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ApplicationContext _appcontext;
        private IRepository<Users> _users;
        public MasterServiceFeaturesController(ILogger<HomeController> logger, ApplicationContext appcontext, IRepository<Users> users)
        {
            _logger = logger;
            _appcontext = appcontext;
            this._users = users;
        }
        public IActionResult MasterServiceFeatureList()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetMasterServiceFeatureList()
        {
            List<MasterServiceFeatures> mf = new List<MasterServiceFeatures>();
            mf = await _appcontext.MasterServiceFeatures.ToListAsync().ConfigureAwait(false);
            return PartialView("~/Views/MasterServiceFeatures/PartialView/_MasterServiceFeatureList.cshtml", mf);
        }
        [HttpGet]
        public async Task<IActionResult> Create(int? id)
        {
            MasterServiceFeatures mf = null;
            if (id != 0)
            {
                mf = await _appcontext.MasterServiceFeatures
                    .Where(h => h.ServiceID == id)
                    .FirstOrDefaultAsync();
            }
            ViewData["ServiceData"] = new SelectList(_appcontext.MasterService.ToList(), "ServiceID", "ServiceName");
            return PartialView("~/Views/MasterServiceFeatures/PartialView/_Create.cshtml", mf);
        }
        [HttpPost]
        public async Task<IActionResult> Create(MasterServiceFeatures mf)
        {
            if (ModelState.IsValid)
            {
                if (mf.FeatureID == 0)
                {
                    _appcontext.Add(mf);
                }
                else
                {
                    _appcontext.Update(mf);
                }
                await _appcontext.SaveChangesAsync();
                return View("MasterServiceFeatureList");
            }
            return Json(mf);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            MasterServiceFeatures mf = new MasterServiceFeatures();
            mf = await _appcontext.MasterServiceFeatures.Where(a => a.FeatureID == id).FirstOrDefaultAsync().ConfigureAwait(false);
            _appcontext.MasterServiceFeatures.Remove(mf);
            await _appcontext.SaveChangesAsync();
            return View("MasterServiceFeatureList");
        }
    }
}
