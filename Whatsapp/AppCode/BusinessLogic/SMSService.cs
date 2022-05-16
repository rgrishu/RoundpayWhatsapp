using ApiRequestUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WAEFCore22.AppCode.Interface;
using WAEFCore22.AppCode.Interface.Repos;
using Whatsapp.Models.Data;
using Whatsapp.Models.UtilityModel;
using Whatsapp.Models.ViewModel;

namespace WAEFCore22.AppCode.BusinessLogic
{
    public class SMSService
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        public SMSService(IUnitOfWorkFactory unitOfWorkFactory)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        #region Registration
        //public Response RegistrationSMS(AlertReplacementModel param)
        //{
        //    var _res = new Response
        //    {
        //        StatusCode = (int)ResponseStatus.Success,
        //        ResponseText = ResponseStatus.Success.ToString(),
        //    };
        //    string sendRes = string.Empty;
        //    string SMS = string.Empty;
        //    var Sqlparam = new CommonReq()
        //    {
        //        LoginID = param.LoginID,
        //        CommonInt = MessageFormat.Registration
        //    };
        //    IProcedure _proc = new ProcGetSMSSettingByFormat(_dal);
        //    var smsSetting = (SMSSetting)_proc.Call(Sqlparam);
        //    if (smsSetting.IsEnableSMS)
        //    {
        //        bool IsNoTemplate = true;
        //        StringBuilder sbUrl = new StringBuilder(smsSetting.URL);
        //        if (string.IsNullOrEmpty(smsSetting.Template))
        //        {
        //            sendRes = "No Template Found";
        //            IsNoTemplate = false;
        //        }
        //        if (IsNoTemplate)
        //        {
        //            if (smsSetting.SMSID == 0)
        //            {
        //                sendRes = "No API Found";
        //            }
        //            FormatedMessages fm = new FormatedMessages();
        //            SMS = fm.GetFormatedMessage(smsSetting.Template, param);
        //            if (smsSetting.SMSID > 0 && !string.IsNullOrEmpty(smsSetting.URL))
        //            {
        //                sbUrl.Replace("{SENDERID}", smsSetting.SenderID);
        //                sbUrl.Replace("{TO}", param.UserMobileNo);
        //                sbUrl.Replace("{MESSAGE}", SMS);
        //                var p = new SendSMSRequest
        //                {
        //                    APIMethod = smsSetting.APIMethod,
        //                    SmsURL = sbUrl.ToString()
        //                };
        //                sendRes = CallSendSMSAPI(p);
        //            }
        //        }
        //        var _Response = new SMSResponse
        //        {
        //            ReqURL = sbUrl.ToString(),
        //            Response = Convert.ToString(sendRes),
        //            ResponseID = "",
        //            Status = SMSResponseTYPE.SEND,
        //            SMSID = smsSetting.SMSID,
        //            MobileNo = param.UserMobileNo,
        //            TransactionID = "",
        //            SMS = SMS,
        //            WID = param.WID
        //        };
        //        //SaveSMSResponse(_Response);
        //    }
        //    return _res;
        //}
        public string CallSendSMSAPI(SendSMSRequest _req)
        {
            string ApiResp = "";
            try
            {
                if (_req.APIMethod == "GET")
                {
                    ApiResp = AppWebRequest.O.CallUsingWebClient_GET(_req.SmsURL, 0);
                    _req.IsSend = true;
                }
                if (_req.APIMethod == "POST")
                {

                }
            }
            catch (Exception ex)
            {
                ApiResp = "Exception Occured! " + ex.Message;
            }
            return ApiResp;
        }

        #endregion
    }
}
