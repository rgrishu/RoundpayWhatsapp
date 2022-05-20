using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Whatsapp.Models.Data;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Whatsapp.AppCode.HelperClass;

namespace Whatsapp.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        const string SessionOtp = "OTP";
        private readonly UserManager<WhatsappUser> _userManager;
        private readonly SignInManager<WhatsappUser> _signInManager;
        private readonly ILogger<LoginModel> _logger;
        private readonly ApplicationContext _appcontext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public LoginModel(SignInManager<WhatsappUser> signInManager,
            ILogger<LoginModel> logger,
            UserManager<WhatsappUser> userManager, ApplicationContext appcontext, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _appcontext = appcontext;
            _httpContextAccessor = httpContextAccessor;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }
            public int OTP { get; set; }

            [Display(Name = "Remember me?")]
            public bool RememberMe { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }
            returnUrl = returnUrl ?? Url.Content("~/");
            string host = _httpContextAccessor.HttpContext.Request.Host.Value;
            var WebSite = _appcontext.MasterWebsite.Where(x => x.WebsiteName == host);
            CookieOptions option = new CookieOptions();
            option.Expires = DateTime.Now.AddDays(30);
            //Create a Cookie with a suitable Key and add the Cookie to Browser.
            Response.Cookies.Append("WID", HashEncryption.O.Encrypt(WebSite.FirstOrDefault().Id.ToString()), option);
            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            ReturnUrl = returnUrl;
            LoginModel.InputModel inputModel = new InputModel();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                WhatsappUser user = null;
                //using (ApplicationContext db = new ApplicationContext())
                //{
                //    user = db.Users.Single(u => u.Email.Equals(Input.Email));
                //}
                var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, Input.RememberMe, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    user = _userManager.Users.Where(x => x.Email == Input.Email).FirstOrDefault();                    if (user.IsOtp)
                    {
                        if (Input.OTP > 0)
                        {
                            string sotp = HttpContext.Session.GetString(SessionOtp);
                            if (sotp == Input.OTP.ToString())
                            {
                                HttpContext.Session.Remove(SessionOtp);
                            }
                        }
                        else
                        {
                            var otp = HashEncryption.O.CreatePasswordNumeric(6);
                            HttpContext.Session.SetString(SessionOtp, otp);
                            return Page();
                        }
                    }
                    await _userManager.AddClaimAsync(user, new Claim("WID", user.WID.ToString()));
                    _logger.LogInformation("User logged in.");
                    return LocalRedirect(returnUrl);
                }
                if (result.RequiresTwoFactor)
                {
                    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account locked out.");
                    return RedirectToPage("./Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return Page();
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
