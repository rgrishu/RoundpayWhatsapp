using Microsoft.AspNetCore.Http;

namespace Whatsapp.Models.ViewModel
{
    public class Whatsapp
    {
    }
    public class WhatsappConversation
    {
        public int Id { get; set; }

        public string conversationId { get; set; }
        public string Msg { get; set; }
        public string APICODE { get; set; }
        public int LoginTypeID { get; set; }
        public string ContactId { get; set; }
        public string Type { get; set; }
        public string StatusString { get; set; }
        public string SenderName { get; set; }
        public string SenderNo { get; set; }
        public string Text { get; set; }
        public string SaveText { get; set; }
        public string EntryDate { get; set; }
        public string Cdate { get; set; }
        public string MessageDate { get; set; }
        public string MessageTime { get; set; }
        public int CCID { get; set; }
        public string CCName { get; set; }
        public string Data { get; set; }
        public int Statuscode { get; set; }
        public int MessageID { get; set; }
        public string FileName { get; set; }
        public int UnreadMessages { get; set; }
        public string absoluteurl { get; set; }
        public string APIURL { get; set; }
        public bool IsSeen { get; set; }

        public string ForwardString { get; set; }
        public string WAMobileNo { get; set; }
        public string QuoteMobileno { get; set; }

        public string QuoteMsg { get; set; }
        public string QuoteMsgID { get; set; }
        public string ReplyJID { get; set; }

        public string GroupID { get; set; }
        public string Screenshot { get; set; }
        public string RemChatTime { get; set; }
        public IFormFile File { get; set; }
    }

}
