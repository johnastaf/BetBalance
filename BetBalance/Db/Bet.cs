using SQLite;

namespace BetBalance.Db
{
    public class Bet
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int Amount { get; set; }

        [MaxLength(100)]
        public string Bookmaker { get; set; }
    }
}
