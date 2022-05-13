using ApiRequestUtility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WAEFCore22.AppCode.Interface;
using WAEFCore22.AppCode.Interface.Repos;
using Whatsapp.Models;
using Whatsapp.Models.Data;
using Whatsapp.Models.ViewModel;

namespace WAEFCore22.AppCode.BusinessLogic.ApiService
{
    public class ApiWhatsappService
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        public ApiWhatsappService(IUnitOfWorkFactory unitOfWorkFactory)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        #region MyRegion
        public async Task<SendSessionMessageResponse> AlertHub_SendSessionMessage(WhatsappAPIAlertHub _ObjAlertHub)
        {
            var _Request = string.Empty;
            var _Response = string.Empty;
            var reserr = new SendSessionMessageResponse
            {
                statuscode = -1,
                result = "failed",
                info = "failed"
            };
            try
            {
                StringBuilder sbDetailUrl = new StringBuilder(_ObjAlertHub.APIURL);
                Random rn = new Random();
                var requestiddetail = DateTime.Now.ToString("yyyymmddMMss") + rn.Next(0000, 9999);
                sbDetailUrl.Replace("{RequestID}", requestiddetail);
                sbDetailUrl.Replace("{COUNTRY}{TO}", _ObjAlertHub.jid);
                sbDetailUrl.Replace("{MESSAGE}", _ObjAlertHub.content);
                sbDetailUrl.Replace("{MESSAGETYPE}", _ObjAlertHub.messagetype);
                sbDetailUrl.Replace("{SCANNO}", _ObjAlertHub.ScanNo);
                sbDetailUrl.Replace("{QUOTEID}", _ObjAlertHub.ConversationID);
                sbDetailUrl.Replace("{QUOTEMSG}", _ObjAlertHub.QuoteMsg);
                sbDetailUrl.Replace("{REPLYJID}", _ObjAlertHub.ReplyJID);
                StringBuilder respex = new StringBuilder("");
                var resp = await AppWebRequest.O.CallUsingWebClient_GETAsync(sbDetailUrl.ToString()).ConfigureAwait(false);
                var wrr = new ApiRequestResponseLog()
                {
                    RequestUrl = sbDetailUrl.ToString(),
                    Response = resp == "" ? respex.ToString() : resp,
                    RequestName = "SendSessionMessage",
                    Remark = "WhatsappAPIML"
                };
                //Save Log
                using (var unitofwork = _unitOfWorkFactory.Create())
                {
                    unitofwork.Repository().Add(wrr);
                    int i = await unitofwork.SaveChangesAsync();
                }
                //Save Log End
                if (!string.IsNullOrEmpty(resp))
                {
                    var failres = JsonConvert.DeserializeObject<WhatsappAPIReqRes>(resp);
                    if (failres.status == "SUCCESS" || failres.status == "PENDING")
                    {
                        reserr.statuscode = 1;
                        reserr.result = "success";
                        reserr.info = "success";
                        reserr.conversationId = failres.conversationId;
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return reserr;
        }
        #endregion



    }
}
