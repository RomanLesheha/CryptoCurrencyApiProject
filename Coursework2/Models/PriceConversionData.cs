using Microsoft.VisualBasic;

namespace Coursework2.Models
{
    public class PriceConversionData
    {
        public DataConversion data { get; set; }
        public ResponseStatus status { get; set; }
    }
    public class DataConversion
    {
        public string symbol { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public decimal amount { get; set; }
        public DateTime last_updated { get; set; }
        public Dictionary<string, Conversion> quote { get; set; }
    }
    public class Conversion
    {
        public decimal price { get; set; }
        public DateTime last_updated { get; set; }
    }
}
