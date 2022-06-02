namespace Whatsapp.Models.UtilityModel
{
    public interface IResponse
    {
        int StatusCode { get; set; }
        string ResponseText { get; set; }
    }
    public interface IResponse<T> : IResponse
    {
        T Data { get; set; }
    }
    public class Response : IResponse
    {
        public int StatusCode { get; set; }
        public string ResponseText { get; set; }
        public string ResponseAmt { get; set; }
    }
    public class Response<T> : Response, IResponse<T>
    {
        public T Data { get; set; }
    }
    public enum ResponseStatus
    {
        Failed = -1,
        Success = 1,
        Pending = 2,
        info = 3,
        warning = 4,
        Unauthorized = -2
    }

}
