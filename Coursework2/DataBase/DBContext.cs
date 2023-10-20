using Coursework2.Models;
using Microsoft.EntityFrameworkCore;

namespace Coursework2.DataBase
{
    public class DBContext :DbContext
    {
        public DBContext(DbContextOptions options):base(options) {}

        public DbSet<CryproCurrencyInDB> CryproCurrency { get; set; }
    }
}
