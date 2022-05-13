using System;

namespace Whatsapp.Models.ViewModel
{
    public class WhatsappAPIReqRes
    {
        public string status { get; set; }
        public string status_code { get; set; }
        public string description { get; set; }
        public string conversationId { get; set; }
        public string datetime { get; set; }
    }
    public class SendSessionMessageResponse
    {
        public object result { get; set; }
        // public string result { get; set; }
        public int statuscode { get; set; }

        // public string message { get; set; }
        public message message { get; set; }
        //  public string message { get; set; }
        public string ticketStatus { get; set; }
        public string Data { get; set; }
        public string conversationId { get; set; }
        public string msg { get; set; }
        public string info { get; set; }
        //  public Message message { get; set; }
    }
    public class WhatsappAPIAlertHub
    {
        public string requestid { get; set; }  //Random UniqID
        public string jid { get; set; }  //Mobile No Of User
        public string content { get; set; }   //Message Text
        public string messagetype { get; set; } //Message Type
        public string APIURL { get; set; }
        public string ScanNo { get; set; }
        public string ConversationID { get; set; }
        public string QuoteMsg { get; set; }
        public string ReplyJID { get; set; }
    }
    public class Message
    {
        public string from { get; set; }
        public string id { get; set; }
        public text text { get; set; }
        public image image { get; set; }
        public string timestamp { get; set; }
        public string type { get; set; }
    }
    public class message
    {
        public string whatsappMessageId { get; set; }
        public string localMessageId { get; set; }
        public string text { get; set; }
        public Media media { get; set; }
        public object messageContact { get; set; }
        public object location { get; set; }
        public string type { get; set; }
        public string time { get; set; }
        public int status { get; set; }
        public object statusString { get; set; }
        public bool isOwner { get; set; }
        public bool isUnread { get; set; }
        public string ticketId { get; set; }
        public object avatarUrl { get; set; }
        public object assignedId { get; set; }
        public object operatorName { get; set; }
        public object replyContextId { get; set; }
        public int sourceType { get; set; }
        public object failedDetail { get; set; }
        public object messageReferral { get; set; }
        public string id { get; set; }
        public DateTime created { get; set; }
        public string conversationId { get; set; }

    }

    public class messages
    {
        public string id { get; set; }
    }


    public class text
    {
        public string body { get; set; }
    }

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class Profile
    {
        public string name { get; set; }
    }

    public class Contact
    {
        public Profile profile { get; set; }
        public string wa_id { get; set; }


    }
    public class Media
    {
        public string id { get; set; }
        public string mimeType { get; set; }
        public string caption { get; set; }
    }

    public class image
    {
        public string id { get; set; }
        public string mime_type { get; set; }
        public string sha256 { get; set; }
        public string link { get; set; }
    }
}
