using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WAEFCore22.AppCode.BusinessLogic.ApiService;
using WAEFCore22.AppCode.Interface;
using WAEFCore22.AppCode.Interface.Repos;
using Whatsapp.Models;
using Whatsapp.Models.Data;
using Whatsapp.Models.UtilityModel;
using Whatsapp.Models.ViewModel;

namespace WAEFCore22.AppCode.BusinessLogic
{
    public class WhatsappService
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        public WhatsappService(IUnitOfWorkFactory unitOfWorkFactory)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        public async Task<List<WhatsappUser>> GetAllUsersById(int id)
        {
            try
            {
                using (var unitofwork = _unitOfWorkFactory.Create())
                {
                    var data = await unitofwork.Repository().FindAsync<WhatsappUser>(x => x.Id == id);
                    return data.ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public async Task<IEnumerable<WhatsappUser>> GetAllUsers()
        {
            try
            {
                using (var unitofwork = _unitOfWorkFactory.Create())
                {
                    var data = await unitofwork.Repository().FindAllRecords<WhatsappUser>();
                    return data.ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<SendSessionMessageResponse> WhatsappAlertHub(AlertReplacementModel param, string template)
        {
            //Whatsapp AlertHub Api Text and MEdia Send  In a Api
            var aw = new ApiWhatsappService(_unitOfWorkFactory);
            FormatedMessages fm = new FormatedMessages();
            string msg = fm.GetFormatedMessage(template, param);
            using (var unitofwork = _unitOfWorkFactory.Create())
            {
                var data = await unitofwork.Repository().SingleOrDefaultAsync<MasterApi>(x => x.IsDefault && x.IsActive);
                var wc = new WhatsappConversation()
                {
                    ContactId = param.UserMobileNo.Length == 10 ? "91" + param.UserMobileNo : param.UserMobileNo,
                    SenderName = param.UserName,
                    Text = msg,
                    Type = "Text"
                };
                var objAlertHub = new WhatsappAPIAlertHub
                {
                    jid = wc.ContactId,
                    messagetype = wc.Type == "" ? "" : wc.Type.ToUpper(),
                    content = wc.Text,
                    APIURL = data.BaseUrl,
                    ScanNo = "918312345678",
                    ConversationID = String.Empty,
                    QuoteMsg = String.Empty,
                    ReplyJID = String.Empty
                };
                return await aw.AlertHub_SendSessionMessage(objAlertHub);
            }
        }
    }
}
