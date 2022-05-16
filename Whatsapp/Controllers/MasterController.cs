using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<WhatsappUser> _userManager;
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        public MasterController(ILogger<HomeController> logger, UserManager<WhatsappUser> userManager, ApplicationContext appcontext, IRepository<Users> users, IUnitOfWorkFactory unitOfWorkFactory)
        {
            _logger = logger;
            _appcontext = appcontext;
            this._users = users;
            _unitOfWorkFactory = unitOfWorkFactory;
            _userManager = userManager;
        }
        // Service Region Starts
        #region Service 
        [Route("MasterServiceList")]
        public IActionResult MasterServiceList()
        {
            ViewData["Title"] = "Master Service";
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetMasterServiceList()
        {
            var ms = new MasterServices(_unitOfWorkFactory);
            IEnumerable<MasterService> masterServices = await ms.GetAllService();
            return PartialView("~/Views/Master/PartialView/_MasterServiceList.cshtml", masterServices);
        }
        public async Task<IActionResult> Create(int id)
        {
            var ms = new MasterServices(_unitOfWorkFactory);
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
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var res = new Response()
            {
                StatusCode = (int)ResponseStatus.Failed,
                ResponseText = "Failed"
            };
            if(id != 0)
            {
                var ms = new MasterServices(_unitOfWorkFactory);
                res = await ms.Delete(id);
            }
            return Json(res);
        }
        #endregion 
        // Service Region Ends
        
        
        // Feature Region Starts

        #region Feature
        [Route("MasterServiceFeatureList")]
        public IActionResult MasterServiceFeatureList()
        {
            ViewData["Title"] = "Feature";
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetMasterServiceFeatureList()
        {
            var ms = new MasterFeature(_unitOfWorkFactory);
            IEnumerable<MasterServiceFeatures> mf = await ms.GetFeatureList();
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
        [HttpPost]
        public async Task<IActionResult> DeleteFeature(int id)
        {
            var res = new Response()
            {
                StatusCode = (int)ResponseStatus.Failed,
                ResponseText = "Failed"
            };
            if (id != 0)
            {
                var ms = new MasterFeature(_unitOfWorkFactory);
                res = await ms.Delete(id);
            }
            return Json(res);
        }
        #endregion
        // Feature Region Ends
        // Master Package Region Starts
        #region Master Package
        [Route("MasterPackageList")]
        public IActionResult MasterPackageList()
        {
            ViewData["Title"] = "Master Package";
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetMasterPackageList()
        {
            var ms = new MasterPackageService(_unitOfWorkFactory);
            IEnumerable<MasterPackage> mf = await ms.GetAllMasterPackage();
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
                    res = await ms.InsertMasterPackage(mp);
                }
                else
                {
                    mp.ModifiedDate = DateTime.Now;
                    res = await ms.UpdateMasterPackage(mp);
                }
            }
            return Json(res);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteMasterPackage(int id)
        {
            var res = new Response()
            {
                StatusCode = (int)ResponseStatus.Failed,
                ResponseText = "Failed"
            };
            if (id != 0)
            {
                var ms = new MasterPackageService(_unitOfWorkFactory);
                res = await ms.Delete(id);
            }
            return Json(res);
        }
        #endregion
        // Master Package Region Ends
        // Package Region Starts
        #region Package
        [Route("PackageList")]
        public IActionResult PackageList()
        {
            ViewData["Title"] = "Package";
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetPackageList()
        {
            var ms = new PackageService(_unitOfWorkFactory);
            IEnumerable<Package> mf = await ms.GetPackageList();
            return PartialView("~/Views/Master/PartialView/_PackageList.cshtml", mf);
        }
        public async Task<IActionResult> CreatePackage(int? id)
        {
            Package mf = null;
            if (id != 0)
            {
                mf = await _appcontext.Package
                    .Where(h => h.PackageID == id)
                    .FirstOrDefaultAsync();
            }
            ViewData["ServiceData"] = new SelectList(_appcontext.MasterService, "ServiceID", "ServiceName");
            ViewData["MaspterPackage"] = new SelectList(_appcontext.MasterPackage, "Id", "PackageName");
            return PartialView("~/Views/Master/PartialView/_AddPackage.cshtml", mf);
        }
        [HttpPost]
        public async Task<IActionResult> AddPackage(Package package)
        {
            var res = new Response()
            {
                StatusCode = (int)ResponseStatus.Failed,
                ResponseText = "Failed"
            };
            if (ModelState.IsValid)
            {
                var ms = new PackageService(_unitOfWorkFactory);
                if (package.PackageID == 0)
                {
                    package.CreatedOn = DateTime.Now;
                    res = await ms.InsertPackage(package);
                }
                else
                {
                    package.UpdatedOn = DateTime.Now;
                    res = await ms.UpdatePackage(package);
                }
            }
            return Json(res);
        }
        [HttpPost]
        public async Task<IActionResult> DeletePackage(int id)
        {
            var res = new Response()
            {
                StatusCode = (int)ResponseStatus.Failed,
                ResponseText = "Failed"
            };
            if (id != 0)
            {
                var ms = new PackageService(_unitOfWorkFactory);
                res = await ms.Delete(id);
            }
            return Json(res);
        }
        #endregion
        // Package Region Ends
        // Email Settings Region Starts
        #region Email Region Starts
        [Route("EmailSettingList")]
        public IActionResult EmailSettingList()
        {
            ViewData["Title"] = "Email";
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetEmailSettingList()
        {
            var ms = new EmailSettingService(_unitOfWorkFactory);
            IEnumerable<EmailSetting> mf = await ms.GetEmailSettingList();
            return PartialView("~/Views/Master/PartialView/_EmailList.cshtml", mf);
        }
        public async Task<IActionResult> CreateEmail(int? id)
        {
            EmailSetting mf = new EmailSetting();
            if (id != 0)
            {
                mf = await _appcontext.EmailSetting
                    .Where(h => h.Id == id)
                    .FirstOrDefaultAsync();
            }
            return PartialView("~/Views/Master/PartialView/_AddEmail.cshtml", mf);
        }
        [HttpPost]
        public async Task<IActionResult> AddEmail(EmailSetting email)
        {
            var res = new Response()
            {
                StatusCode = (int)ResponseStatus.Failed,
                ResponseText = "Failed"
            };
            if (ModelState.IsValid)
            {
                var ms = new EmailSettingService(_unitOfWorkFactory);
                var user = _userManager.GetUserAsync(HttpContext.User);
                email.EntryBy = HttpContext.User.Identity.Name;
                if (email.Id == 0)
                {
                    email.CreatedDate = DateTime.Now;
                    res = await ms.InsertEmailSetting(email);
                }
                else
                {
                    email.ModifiedDate = DateTime.Now;
                    email.ModifyBy = HttpContext.User.Identity.Name;
                    res = await ms.UpdateEmailSetting(email);
                }
            }
            return Json(res);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteEmail(int id)
        {
            var res = new Response()
            {
                StatusCode = (int)ResponseStatus.Failed,
                ResponseText = "Failed"
            };
            if (id != 0)
            {
                var ms = new EmailSettingService(_unitOfWorkFactory);
                res = await ms.Delete(id);
            }
            return Json(res);
        }
        #endregion
        // EmailSettings Region Ends
        //Master Api Region Starts
        #region Master API Region Starts
        [Route("MasterAPIList")]
        public IActionResult MasterAPIList()
        {
            ViewData["Title"] = "Master API";
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetMasterAPIList()
        {
            var ms = new MasterAPIService(_unitOfWorkFactory);
            IEnumerable<MasterApi> mf = await ms.GetMasterAPIList();
            return PartialView("~/Views/Master/PartialView/_GetMasterAPIList.cshtml", mf);
        }
        public async Task<IActionResult> CreateMasterAPI(int? id)
        {
            MasterApi mf = new MasterApi();
            if (id != 0)
            {
                mf = await _appcontext.MasterApi
                    .Where(h => h.Id == id)
                    .FirstOrDefaultAsync();
            }
            ViewData["APIType"] = new SelectList(_appcontext.MasterApiType.ToList(), "Id", "ApiTypeName");
            return PartialView("~/Views/Master/PartialView/_AddMasterAPI.cshtml", mf);
        }
        [HttpPost]
        public async Task<IActionResult> AddMasterAPI(MasterApi api)
        {
            var res = new Response()
            {
                StatusCode = (int)ResponseStatus.Failed,
                ResponseText = "Failed"
            };
            if (ModelState.IsValid)
            {
                var ms = new MasterAPIService(_unitOfWorkFactory);
                var user = _userManager.GetUserAsync(HttpContext.User);
                if (api.Id == 0)
                {
                    api.CreatedDate = DateTime.Now;
                    res = await ms.InsertMasterAPI(api);
                }
                else
                {
                    api.ModifiedDate = DateTime.Now;
                    res = await ms.UpdateMasterAPI(api);
                }
            }
            return Json(res);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteMasterAPI(int id)
        {
            var res = new Response()
            {
                StatusCode = (int)ResponseStatus.Failed,
                ResponseText = "Failed"
            };
            if (id != 0)
            {
                var ms = new MasterAPIService(_unitOfWorkFactory);
                res = await ms.Delete(id);
            }
            return Json(res);
        }
        #endregion
        //Master Api Region Ends


        //Sendor Number Region Starts
        #region Sender Number Region Starts
        [Route("SenderNoList")]
        public IActionResult SenderNoList()
        {
            ViewData["Title"] = "Sender No.";
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetSenderNoList()
        {
            var ms = new SenderNumberService(_unitOfWorkFactory);
            IEnumerable<SenderNo> mf = await ms.GetSenderNoList();
            return PartialView("~/Views/Master/PartialView/_SenderNoList.cshtml", mf);
        }
        public async Task<IActionResult> CreateSenderNo(int? id)
        {
            SenderNo mf = new SenderNo();
            if (id != 0)
            {
                mf = await _appcontext.SenderNo
                    .Where(h => h.Id == id)
                    .FirstOrDefaultAsync();
            }
            return PartialView("~/Views/Master/PartialView/_AddSenderNo.cshtml", mf);
        }
        [HttpPost]
        public async Task<IActionResult> AddSenderNo(SenderNo sno)
        {
            var res = new Response()
            {
                StatusCode = (int)ResponseStatus.Failed,
                ResponseText = "Failed"
            };
            if (ModelState.IsValid)
            {
                var ms = new SenderNumberService(_unitOfWorkFactory);
                var user = _userManager.GetUserAsync(HttpContext.User);
                if (sno.Id == 0)
                {
                    sno.CreatedDate = DateTime.Now;
                    res = await ms.InsertSenderNo(sno);
                }
                else
                {
                    sno.ModifiedDate = DateTime.Now;
                    res = await ms.UpdateSenderNo(sno);
                }
            }
            return Json(res);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteSenderNo(int id)
        {
            var res = new Response()
            {
                StatusCode = (int)ResponseStatus.Failed,
                ResponseText = "Failed"
            };
            if (id != 0)
            {
                var ms = new SenderNumberService(_unitOfWorkFactory);
                res = await ms.Delete(id);
            }
            return Json(res);
        }
        #endregion
        //Sendor Number Region Ends
    }
}
