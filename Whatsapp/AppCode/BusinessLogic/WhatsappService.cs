using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WAEFCore22.AppCode.BusinessLogic.ApiService;
using WAEFCore22.AppCode.Interface;
using WAEFCore22.AppCode.Interface.Repos;
using Whatsapp.Models.Data;
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

        public async Task<IEnumerable<WhatsappUser>> GetAllUsers ()
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

        //public async Task<SendSessionMessageResponse> WhatsappAlertHub(WhatsappConversation wc, WhatsappConversation res)
        //{
        //    //Whatsapp AlertHub Api Text and MEdia Send  In a Api
        //    var aw = new ApiWhatsappService(_unitOfWorkFactory);
           
        //    var objAlertHub = new WhatsappAPIAlertHub
        //    {
        //        jid = wc.ContactId,
        //        messagetype = res.Type == "" ? "" : wc.Type.ToUpper(),
        //        content = wc.Text,
        //        APIURL = res.APIURL,
        //        ScanNo = res.SenderNo,
        //        ConversationID = wc.conversationId,
        //        QuoteMsg = wc.QuoteMsg,
        //        ReplyJID = wc.ReplyJID
        //    };
        //    return await aw.AlertHub_SendSessionMessage(objAlertHub);
        //}


    }
}
