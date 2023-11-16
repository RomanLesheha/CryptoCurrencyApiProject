namespace Coursework2.Models
{
    public class ResponseStatus
    {
        public DateTime Timestamp { get; set; }
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public int Elapsed { get; set; }
        public int CreditCount { get; set; }
    }
}
