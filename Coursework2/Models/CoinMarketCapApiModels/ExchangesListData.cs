using Coursework2.Models;

namespace Coursework2.Models
{
    public class ExchangesListData
    {
        public List<ExchangeData> Data { get; set; }
        public ResponseStatus Status { get; set; }
    }
    public class ExchangeData
    {
        public int id { get; set; }
        public string name { get; set; }
        public string symbol { get; set; }
        public string slug { get; set; }
        public int іs_Active { get; set; }
        public DateTime first_historical_data { get; set; }
        public DateTime ast_historical_data { get; set; }
    }
}
