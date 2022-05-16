using System;
using System.Text;
using Whatsapp.Models.StaticModel;
using Whatsapp.Models.ViewModel;

namespace Whatsapp.Models.UtilityModel
{
    public class FormatedMessages
    {
        public string GetFormatedMessage(string Template, AlertReplacementModel Replacements)
        {
            StringBuilder sb = new StringBuilder(Template);

            sb.Replace("{FromUserName}", Replacements.LoginUserName);
            sb.Replace("{FromUserMobile}", Replacements.LoginMobileNo);
            sb.Replace("{FromUserID}", Replacements.LoginPrefix + Replacements.LoginID.ToString());
            sb.Replace("{ToUserMobile}", Replacements.UserMobileNo);
            sb.Replace("{ToUserID}", Replacements.UserPrefix + Replacements.UserID.ToString());
            sb.Replace("{ToUserName}", Replacements.UserName);
            sb.Replace("{UserName}", Replacements.UserName);
            sb.Replace("{Mobile}", Replacements.UserMobileNo);
            sb.Replace("{UserMobile}", Replacements.UserMobileNo);
            sb.Replace("{Amount}", Convert.ToString(Replacements.Amount));
            sb.Replace("{BalanceAmount}", Convert.ToString(Replacements.BalanceAmount));
            sb.Replace("{UserBalanceAmount}", Convert.ToString(Replacements.UserCurrentBalance));
            sb.Replace("{LoginBalanceAmount}", Convert.ToString(Replacements.LoginCurrentBalance));
            sb.Replace("{Operator}", Replacements.Operator);
            sb.Replace("{OperatorName}", Replacements.Operator);
            sb.Replace("{Company}", Replacements.Company);
            sb.Replace("{CompanyName}", Replacements.Company);
            sb.Replace("{CompanyDomain}", Replacements.CompanyDomain);
            sb.Replace("{CompanyAddress}", Replacements.CompanyAddress);
            sb.Replace("{BrandName}", Replacements.BrandName);
            sb.Replace("{OutletName}", Replacements.OutletName);
            sb.Replace("{SupportNumber}", Replacements.SupportNumber);
            sb.Replace("{SupportEmail}", Replacements.SupportEmail);
            sb.Replace("{AccountNumber}", Replacements.AccountNo);
            sb.Replace("{AccountsContactNo}", Replacements.AccountsContactNo);
            sb.Replace("{AccountEmail}", Replacements.AccountEmail);
            sb.Replace("{OTP}", Replacements.OTP);
            sb.Replace("{LoginID}", !String.IsNullOrEmpty(Replacements.CommonStr) ? Replacements.CommonStr : Replacements.LoginPrefix + Replacements.LoginID.ToString());
            sb.Replace("{Password}", Replacements.Password);
            sb.Replace("{PinPassword}", Replacements.PinPassword);
            sb.Replace("{AccountNo}", Replacements.AccountNo);
            sb.Replace("{LiveID}", Replacements.LiveID);
            sb.Replace("{TID}", Convert.ToString(Replacements.TID));
            sb.Replace("{TransactionID}", Replacements.TransactionID);
            sb.Replace("{BankRequestStatus}", Replacements.RequestStatus);
            sb.Replace("{OutletID}", Replacements.OutletID);
            sb.Replace("{OutletMobile}", Replacements.OutletMobile);
            sb.Replace("{RejectReason}", Replacements.KycRejectReason);
            sb.Replace(MessageTemplateKeywords.Message, Replacements.Message);
            sb.Replace(MessageTemplateKeywords.UserEmail, Replacements.EmailID);
            sb.Replace(MessageTemplateKeywords.SenderName, Replacements.SenderName);
            sb.Replace(MessageTemplateKeywords.TransMode, Replacements.TransMode);
            sb.Replace(MessageTemplateKeywords.UTRorRRN, Replacements.UTRorRRN);
            sb.Replace(MessageTemplateKeywords.IFSC, Replacements.IFSC);
            sb.Replace("{DATETIME}", Replacements.DATETIME);
            sb.Replace("{Duration}", Replacements.Duration);
            sb.Replace("{CouponCode}", Replacements.CouponCode);
            sb.Replace("{CouponQty}", Convert.ToString(Replacements.CouponQty));
            sb.Replace("{CouponValidty}", Convert.ToString(Replacements.CouponValdity));

            //sb.Replace(MessageTemplateKeywords.AccountNumber, Replacements.AccountNumber);
            return Convert.ToString(sb);
        }



    }
}
