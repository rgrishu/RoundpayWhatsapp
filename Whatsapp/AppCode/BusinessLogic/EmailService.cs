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
    public class EmailService
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        public EmailService(IUnitOfWorkFactory unitOfWorkFactory)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        //public Response RegistrationEmail(AlertReplacementModel param)
        //{
        //    bool IsSent = false;
        //    var _res = new Response
        //    {
        //        StatusCode = (int)ResponseStatus.Success,
        //        ResponseText =ResponseStatus.Success.ToString()
        //    };
        //    string EmailBody = string.Empty;
        //    var Sqlparam = new CommonReq()
        //    {
        //        LoginID = param.LoginID,
        //        CommonInt = MessageFormat.Registration
        //    };
        //    IProcedure _proc = new ProcGetEmailSettingByFormat(_dal);
        //    var mailSetting = (EmailSettingswithFormat)_proc.Call(Sqlparam);
        //    if (mailSetting.IsEnableEmail)
        //    {
        //        bool IsNoTemplate = true;
        //        if (string.IsNullOrEmpty(mailSetting.EmailTemplate))
        //        {
        //            EmailBody = "No Template Found";
        //            IsNoTemplate = false;
        //        }
        //        if (IsNoTemplate)
        //        {
        //            if (string.IsNullOrEmpty(mailSetting.FromEmail))
        //            {
        //                EmailBody = "No Email Found";
        //            }
        //            FormatedMessages fm = new FormatedMessages();
        //            EmailBody = fm.GetFormatedMessage(mailSetting.EmailTemplate, param);
        //            if (param.WID > 0)
        //            {
        //                if (!string.IsNullOrEmpty(mailSetting.FromEmail))
        //                {
        //                    IEmailML emailManager = new EmailML(_dal);
        //                    string logo = _resourceML.GetLogoURL(param.WID).ToString();
        //                    string Footer = "<p><h4 style='color:#000000;font-family:verdana,sans-serif;margin-bottom:1.5px'><em>{CompanyName}</em></h4><span>{CompanyAddress}</span></p>";
        //                    Footer = Footer.Replace("{CompanyName}", param.Company).Replace("{CompanyAddress}", param.CompanyAddress);
        //                    IsSent = emailManager.SendEMail(mailSetting, param.UserEmailID, null, mailSetting.Subject, EmailBody, param.WID, logo, true, Footer);
        //                }
        //            }
        //        }
        //        SendEmail sendEmail = new SendEmail
        //        {
        //            From = mailSetting.FromEmail,
        //            Body = EmailBody,
        //            Recipients = param.UserEmailID + "," + (param.bccList != null ? (param.bccList.Count > 0 ? String.Join(",", param.bccList) : "") : ""),
        //            Subject = mailSetting.Subject,
        //            IsSent = IsSent,
        //            WID = param.WID
        //        };
        //        EmailDL emailDL = new EmailDL(_dal);
        //        emailDL.SaveMail(sendEmail);
        //    }
        //    return _res;
        //}
    
        //public EmailSetting GetSetting(int WID, int RoleId = 0)
        //{
        //    EmailDL _emailDL = new EmailDL(_dal);
        //    DataTable dt = _emailDL.GetEmailSetting(WID, RoleId);
        //    EmailSetting setting = new EmailSetting();
        //    if (dt.Rows.Count > 0)
        //    {
        //        setting.HostName = dt.Rows[0]["HostName"].ToString();
        //        setting.Password = dt.Rows[0]["Password"].ToString();
        //        setting.Port = dt.Rows[0]["Port"].ToString();
        //        setting.FromEmail = dt.Rows[0]["FromEmail"].ToString();
        //        setting.MailUserID = Convert.ToString(dt.Rows[0]["MailUserID"]);
        //        setting.IsActive = Convert.ToBoolean(dt.Rows[0]["IsActive"]);
        //        setting.IsSSL = dt.Rows[0]["IsSSL"] is DBNull ? false : Convert.ToBoolean(dt.Rows[0]["IsSSL"]);
        //    }
        //    return setting;
        //}
        //public bool SendMail(string ToEmail, List<string> bccList, string Subject, string Body, int WID, string Logo, bool IsHTML = true, string MailFooter = "")
        //{
        //    bool IsSent = false;
        //    EmailSetting setting = new EmailSetting();
        //    if (WID > 0)

        //    {
        //        setting = GetSetting(WID);

        //        if (setting.FromEmail != null && setting.Port != "0" && setting.Password != null && setting.HostName != null)
        //        {
        //            if (IsHTML)
        //            {
        //                Body = GetTemplate(Logo).Replace("{BODY}", Body).Replace("{Footer}", MailFooter).ToString();
        //            }
        //            try
        //            {
        //                MailMessage mailMessage = new MailMessage
        //                {
        //                    From = new MailAddress(setting.FromEmail),
        //                    Subject = Subject,
        //                    Body = Body,
        //                    IsBodyHtml = IsHTML
        //                };
        //                ToEmail = string.IsNullOrEmpty(ToEmail) ? setting.FromEmail : ToEmail;
        //                mailMessage.To.Add(ToEmail);
        //                if (bccList != null)
        //                {
        //                    foreach (string bcc in bccList)
        //                    {
        //                        if (bcc.Contains("@") && bcc.Contains(".") && bcc.Length <= 255)
        //                        {
        //                            mailMessage.Bcc.Add(bcc.ToLower());
        //                        }
        //                    }
        //                }
        //                SmtpClient smtpClient = new SmtpClient(setting.HostName, Convert.ToInt32(setting.Port))
        //                {
        //                    Credentials = new NetworkCredential(string.IsNullOrEmpty(setting.MailUserID) ? setting.FromEmail : setting.MailUserID, setting.Password)
        //                };
        //                if (setting.IsSSL)
        //                {
        //                    smtpClient.EnableSsl = setting.IsSSL;
        //                }
        //                try
        //                {
        //                    smtpClient.Send(mailMessage);
        //                    IsSent = true;
        //                }
        //                catch (Exception ex)
        //                {
        //                }

        //            }
        //            catch (Exception ex)
        //            {

        //            }
        //        }
        //    }
        //    try
        //    {
        //        SendEmail sendEmail = new SendEmail
        //        {
        //            From = setting.FromEmail,
        //            Body = Body,
        //            Recipients = ToEmail + "," + (bccList != null ? (bccList.Count > 0 ? String.Join(",", bccList) : "") : ""),
        //            Subject = Subject,
        //            IsSent = IsSent,
        //            WID = WID
        //        };
        //        //EmailDL emailDL = new EmailDL(_dal);
        //        //emailDL.SaveMail(sendEmail);
        //    }
        //    catch (Exception)
        //    {

        //    }
        //    return IsSent;
        //}
        //public bool SendEMail(EmailSettingswithFormat setting, string ToEmail, List<string> bccList, string Subject, string Body, int WID, string Logo, bool IsHTML = true, string MailFooter = "")
        //{
        //    bool IsSent = false;
        //    if (setting.FromEmail != null && setting.Port != 0 && setting.Password != null && setting.HostName != null)
        //    {
        //        if (IsHTML)
        //        {
        //            Body = GetTemplate(Logo).Replace("{BODY}", Body).Replace("{Footer}", MailFooter).ToString();
        //        }
        //        try
        //        {
        //            MailMessage mailMessage = new MailMessage
        //            {
        //                From = new MailAddress(setting.FromEmail),
        //                Subject = Subject,
        //                Body = Body,
        //                IsBodyHtml = IsHTML
        //            };
        //            ToEmail = string.IsNullOrEmpty(ToEmail) ? setting.FromEmail : ToEmail;
        //            mailMessage.To.Add(ToEmail);
        //            if (bccList != null)
        //            {
        //                foreach (string bcc in bccList)
        //                {
        //                    if (bcc.Contains("@") && bcc.Contains(".") && bcc.Length <= 255)
        //                    {
        //                        mailMessage.Bcc.Add(bcc.ToLower());
        //                    }
        //                }
        //            }
        //            SmtpClient smtpClient = new SmtpClient(setting.HostName, setting.Port)
        //            {
        //                Credentials = new NetworkCredential(string.IsNullOrEmpty(setting.MailUserID) ? setting.FromEmail : setting.MailUserID, setting.Password)
        //            };
        //            if (setting.IsSSL)
        //            {
        //                smtpClient.EnableSsl = setting.IsSSL;
        //            }
        //            try
        //            {
        //                smtpClient.Send(mailMessage);
        //                IsSent = true;
        //            }
        //            catch (Exception ex)
        //            {

        //            }
        //        }
        //        catch (Exception ex)
        //        {

        //        }
        //    }
        //    return IsSent;
        //}
        private StringBuilder GetTemplate(String Logo)
        {
            StringBuilder HtmlTemplate = new StringBuilder(ErrorCodes.HTMLTEMPLATE);
            HtmlTemplate.Replace("{logo}", Logo);
            return HtmlTemplate;
        }
    }
}
