namespace Whatsapp.Models.ViewModel
{
    public class Sms
    {




    }
    public class SMSSetting
    {
     
        public string Msg { get; set; }
        public int FormatID { get; set; }
        public string Template { get; set; }
        public string Subject { get; set; }
        public bool IsEnableSMS { get; set; }
        public int APIType { get; set; }
        public string URL { get; set; }
        public string APIMethod { get; set; }
        public int APIID { get; set; }
        public int ResType { get; set; }
        public int SMSID { get; set; }
        public string SenderID { get; set; }
        public string MobileNos { get; set; }
        public string SMS { get; set; }
        public int WID { get; set; }
        public bool IsLapu { get; set; }
    }

    public class SMSResponse
    {
        public int SMSID { get; set; }
        public int WID { get; set; }
        public string MobileNo { get; set; }
        public string SMS { get; set; }
        public int Status { get; set; }
        public string TransactionID { get; set; }
        public string Response { get; set; }
        public string ResponseID { get; set; }
        public string ReqURL { get; set; }
        public int SocialAlertType { get; set; }
    }
    public class SendSMSRequest
    {
        public int SMSID { get; set; }
        public int APIID { get; set; }
        public string SMS { get; set; }
        public string SmsURL { get; set; }
        public string APIMethod { get; set; }
        public string TransactionID { get; set; }
        public string MobileNo { get; set; }
        public bool IsLapu { get; set; }
        public bool IsSend { get; set; }
        public string ApiResp { get; set; }
    }

    public class SMSResponseTYPE
    {
        public static int SEND = 1;
        public static int UNSENT = 2;
        public static int DELIVERED = 3;
        public static int UNDELIVERED = 4;
        public static int FAILED = -1;
        public static int RESEND = 5;
    }
}
