using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Coursework2.Models.CoinMarketCapApiModels
{
    public class CryproCurrencyInDB
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int CurrencyId { get; set; }
        public string LogoUrl { get; set; }
    }
}
