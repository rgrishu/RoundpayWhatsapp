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

        public async Task<SendSessionMessageResponse> WhatsappAlertHub(WhatsappConversation wc)
        {
            //Whatsapp AlertHub Api Text and MEdia Send  In a Api
            var aw = new ApiWhatsappService(_unitOfWorkFactory);
            var objAlertHub = new WhatsappAPIAlertHub
            {
                jid = wc.ContactId,
                messagetype = wc.Type == "" ? "" : wc.Type.ToUpper(),
                content = wc.Text,
                APIURL = "http://api.alerthub.in/api/send?apiusername=roundp_99y767&apipassword=P-Ji@r]y@ydnRjF!&requestid={RequestID}&jid={COUNTRY}{TO}&content={MESSAGE}&messagetype=TEXT&from={SCANNO}&quotemsgid={QUOTEID}&quotemsg={QUOTEMSG}&quotedmsgfrom={REPLYJID}",
                ScanNo = "918312345678",
                ConversationID =String.Empty,
                QuoteMsg = String.Empty,
                ReplyJID = String.Empty
            };
            return await aw.AlertHub_SendSessionMessage(objAlertHub);
        }


    }
}
