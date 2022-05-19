using ApiRequestUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WAEFCore22.AppCode.Interface;
using WAEFCore22.AppCode.Interface.Repos;
using Whatsapp.Models;
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
        public async Task<Response> RegistrationSMS(AlertReplacementModel param)
        {
            var _res = new Response
            {
                StatusCode = (int)ResponseStatus.Success,
                ResponseText = ResponseStatus.Success.ToString(),
            };
            string sendRes = string.Empty;
            string SMS = string.Empty;
            //var Sqlparam = new CommonReq()
            //{
            //    LoginID = param.LoginID,
            //    CommonInt = MessageFormat.Registration
            //};
            //IProcedure _proc = new ProcGetSMSSettingByFormat(_dal);
            //  var smsSetting = (SMSSetting)_proc.Call(Sqlparam);
            using (var unitofwork = _unitOfWorkFactory.Create())
            {
                var smsSetting = await unitofwork.Repository().SingleOrDefaultAsync<MessageTemplate>();
                var ApiSetting = await unitofwork.Repository().SingleOrDefaultAsync<MasterApi>(x => x.IsDefault && x.IsActive);
                if (smsSetting.IsEnableSMS)
                {
                    bool IsNoTemplate = true;
                    StringBuilder sbUrl = new StringBuilder(ApiSetting.BaseUrl);
                    if (string.IsNullOrEmpty(smsSetting.SMSTemplate))
                    {
                        sendRes = "No Template Found";
                        IsNoTemplate = false;
                    }
                    if (IsNoTemplate)
                    {
                        if (ApiSetting.Id == 0)
                        {
                            sendRes = "No API Found";
                        }
                        FormatedMessages fm = new FormatedMessages();
                        SMS = fm.GetFormatedMessage(smsSetting.SMSTemplate, param);
                        if (ApiSetting.Id > 0 && !string.IsNullOrEmpty(ApiSetting.BaseUrl))
                        {
                            sbUrl.Replace("{SENDERID}", "");
                            sbUrl.Replace("{TO}", param.UserMobileNo);
                            sbUrl.Replace("{MESSAGE}", SMS);
                            var p = new SendSMSRequest
                            {
                                APIMethod = ApiSetting.Method,
                                SmsURL = sbUrl.ToString()
                            };
                            sendRes = CallSendSMSAPI(p);
                        }
                    }
                    var _Response = new SMSResponse
                    {
                        ReqURL = sbUrl.ToString(),
                        Response = Convert.ToString(sendRes),
                        ResponseID = "",
                        Status = SMSResponseTYPE.SEND,
                        SMSID = smsSetting.FormatID,
                        MobileNo = param.UserMobileNo,
                        TransactionID = "",
                        SMS = SMS,
                        WID = param.WID
                    };
                    //SaveSMSResponse(_Response);

                }
                return _res;
            }
        }
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
