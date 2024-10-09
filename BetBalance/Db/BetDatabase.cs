using SQLite;

namespace BetBalance.Db
{
    public class BetDatabase
    {
        SQLiteAsyncConnection Database;
        public BetDatabase()
        {
        }

        public async Task Init()
        {
            if (Database is not null)
                return;

            Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
            var result = await Database.CreateTableAsync<Bet>();
        }

        public async Task<List<Bet>> GetItemsAsync()
        {
            await Init();
            return await Database.Table<Bet>().ToListAsync();
        }

        public async Task<List<Bet>> GetLastWeekItemseAsync()
        {
            await Init();
            DateTime startOfLastWeek = DateTime.Today.AddDays(-7);
            return await Database.Table<Bet>().Where(t => t.Date >= startOfLastWeek).ToListAsync();

            // SQL queries are also possible
            //return await Database.QueryAsync<TodoItem>("SELECT * FROM [TodoItem] WHERE [Done] = 0");
        }

        public async Task<Bet> GetItemAsync(int id)
        {
            await Init();
            return await Database.Table<Bet>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public async Task<int> SaveItemAsync(Bet item)
        {
            await Init();
            if (item.Id != 0)
                return await Database.UpdateAsync(item);
            else
                return await Database.InsertAsync(item);
        }

        public async Task<int> DeleteItemAsync(Bet item)
        {
            await Init();
            return await Database.DeleteAsync(item);
        }
    }
}

