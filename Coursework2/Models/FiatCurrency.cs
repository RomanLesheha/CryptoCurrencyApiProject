namespace Coursework2.Models
{
    public class FiatCurrency
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Sign { get; set; }
        public string Symbol { get; set; }
    }

    public class FiatMapResponse
    {
        public List<FiatCurrency> Data { get; set; }
        public ResponseStatus Status { get; set; }
    }
}
