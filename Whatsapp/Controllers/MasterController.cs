using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WAEFCore22.AppCode.BusinessLogic;
using WAEFCore22.AppCode.Interface.Repos;
using Whatsapp.AppCode.BusinessLogic;
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
        // Service Region Starts
        #region Service 
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
        #endregion // Service Region Ends
        // Feature Region Starts

        #region Feature
        [Route("MasterServiceFeatureList")]
        public IActionResult MasterServiceFeatureList()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetMasterServiceFeatureList()
        {
            List<MasterServiceFeatures> mf = new List<MasterServiceFeatures>();
            mf = await _appcontext.MasterServiceFeatures.Include(a => a.MasterService).ToListAsync().ConfigureAwait(false);
            return PartialView("~/Views/Master/PartialView/_MasterServiceFeatureList.cshtml", mf);
        }
        public async Task<IActionResult> CreateServiceFeature(int? id)
        {
            MasterServiceFeatures mf = null;
            if (id != 0)
            {
                mf = await _appcontext.MasterServiceFeatures
                    .Where(h => h.FeatureID == id)
                    .FirstOrDefaultAsync();
            }
            ViewData["ServiceData"] = new SelectList(_appcontext.MasterService, "ServiceID", "ServiceName");
            return PartialView("~/Views/Master/PartialView/_AddFeature.cshtml", mf);
        }
        [HttpPost]
        public async Task<IActionResult> AddFeature(MasterServiceFeatures masterService)
        {
            var res = new Response()
            {
                StatusCode = (int)ResponseStatus.Failed,
                ResponseText = "Failed"
            };
            var ms = new MasterFeature(_unitOfWorkFactory);
            if (ModelState.IsValid)
            {
                if (masterService.FeatureID == 0)
                {
                    res = await ms.InsertMasterFeature(masterService);
                }
                else
                {
                    res = await ms.UpdateMasterFeature(masterService);
                }
            }
            return Json(res);
        }
        [HttpGet]
        public async Task<IActionResult> DeleteFeature(int id)
        {
            MasterServiceFeatures mf = new MasterServiceFeatures();
            mf = await _appcontext.MasterServiceFeatures.Where(a => a.FeatureID == id).FirstOrDefaultAsync().ConfigureAwait(false);
            _appcontext.MasterServiceFeatures.Remove(mf);
            await _appcontext.SaveChangesAsync();
            return View("MasterServiceFeatureList");
        }
        #endregion
        // Feature Region Ends
        // Master Package Region Starts
        #region Master Package
        [Route("MasterPackageList")]
        public IActionResult MasterPackageList()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetMasterPackageList()
        {
            List<MasterPackage> mf = new List<MasterPackage>();
            mf = await _appcontext.MasterPackage.ToListAsync().ConfigureAwait(false);
            return PartialView("~/Views/Master/PartialView/_MasterPackageList.cshtml", mf);
        }
        public async Task<IActionResult> CreateMasterPackage(int? id)
        {
            MasterPackage mf = null;
            if (id != 0)
            {
                mf = await _appcontext.MasterPackage
                    .Where(h => h.Id == id)
                    .FirstOrDefaultAsync();
            }
            ViewData["ServiceData"] = new SelectList(_appcontext.MasterService, "ServiceID", "ServiceName");
            return PartialView("~/Views/Master/PartialView/_AddMasterPackage.cshtml", mf);
        }
        [HttpPost]
        public async Task<IActionResult> AddMasterPackage(MasterPackage mp)
        {
            var res = new Response()
            {
                StatusCode = (int)ResponseStatus.Failed,
                ResponseText = "Failed"
            };
            var ms = new MasterPackageService(_unitOfWorkFactory);
            if (ModelState.IsValid)
            {
                if (mp.Id == 0)
                {
                    mp.CreatedDate = DateTime.Now;
                    res = await ms.InsertMasterFeature(mp);
                }
                else
                {
                    mp.ModifiedDate = DateTime.Now;
                    res = await ms.UpdateMasterFeature(mp);
                }
            }
            return Json(res);
        }
        [HttpGet]
        public async Task<IActionResult> DeleteMasterPackage(int id)
        {
            MasterPackage mf = new MasterPackage();
            mf = await _appcontext.MasterPackage.Where(a => a.Id == id).FirstOrDefaultAsync().ConfigureAwait(false);
            _appcontext.MasterPackage.Remove(mf);
            await _appcontext.SaveChangesAsync();
            return View("MasterPackageList");
        }
        #endregion
        // Master Package Region Ends
    }
}
