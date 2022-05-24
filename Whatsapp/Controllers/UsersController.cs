using EmailService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WAEFCore22.AppCode.BusinessLogic;
using WAEFCore22.AppCode.Interface.Repos;
using Whatsapp.AppCode.Extensions;
using Whatsapp.Interface;
using Whatsapp.Models;
using Whatsapp.Models.Data;
using Whatsapp.Models.UtilityModel;
using Whatsapp.Models.ViewModel;
using Whatsapp.Services.Interface;

namespace Whatsapp.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        private ApplicationContext _appcontext;
        private IRepository<Users> _users;
        private readonly UserManager<WhatsappUser> _userManager;
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private readonly IEmailService _emailService;
       private readonly IMasterWebsiteService _masterWebsiteService;
        
        public UsersController(ILogger<HomeController> logger, UserManager<WhatsappUser> userManager, ApplicationContext appcontext, IRepository<Users> users, IUnitOfWorkFactory unitOfWorkFactory, IEmailService emailService, IMasterWebsiteService masterWebsiteService)
        {
            _logger = logger;
            _appcontext = appcontext;
            _users = users;
            _userManager = userManager;
            _unitOfWorkFactory = unitOfWorkFactory;
            _emailService = emailService;
            _masterWebsiteService = masterWebsiteService;
        }
        

    }
}
