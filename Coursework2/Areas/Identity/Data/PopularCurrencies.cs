using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coursework2.Areas.Identity.Data
{
    public class PopularCurrencies
    {
        public int ID { get; set; }
        public int CurrencyID {  get; set; }

        public int NumberOfVisits { get; set; }
    }
}
