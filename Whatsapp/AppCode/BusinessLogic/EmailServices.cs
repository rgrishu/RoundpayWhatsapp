using EmailService;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using WAEFCore22.AppCode.Interface;
using WAEFCore22.AppCode.Interface.Repos;
using Whatsapp.Models;
using Whatsapp.Models.Data;
using Whatsapp.Models.StaticModel;
using Whatsapp.Models.UtilityModel;
using Whatsapp.Models.ViewModel;

namespace WAEFCore22.AppCode.BusinessLogic
{
    public class EmailServices
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private readonly IEmailService _emailService;
        public EmailServices(IUnitOfWorkFactory unitOfWorkFactory, IEmailService emailService)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
            _emailService = emailService;
        }


        public async Task<bool> SendMail(AlertReplacementModel param,string template)
        {
            bool IsSent = true;
            using (var unitofwork = _unitOfWorkFactory.Create())
            {
                var data = await unitofwork.Repository().SingleOrDefaultAsync<EmailSetting>(x => x.IsDefault && x.IsActive);
                FormatedMessages fm = new FormatedMessages();
                string msg = fm.GetFormatedMessage(template, param);
                _emailService.Send(
                             new EmailMessage
                             {
                                 Content = msg,
                                 Subject = param.Subject,

                                 FromAddresses = new List<EmailAddress> {
                            new EmailAddress{
                                    Address = data.FromEmail,
                                    Name = "",
                                }
                             },
                                 ToAddresses = new List<EmailAddress> {
                            new EmailAddress{
                                    Address =param.EmailID,
                                    Name =param.UserName,
                                }
                             }
                             }, new EmailConfiguration
                             {
                                 SmtpServer = data.HostName,
                                 SmtpPassword = data.Password,
                                 SmtpUsername = data.FromEmail,
                                 SmtpPort = data.Port,
                             });
            }
            return IsSent;
        }
    }
}
