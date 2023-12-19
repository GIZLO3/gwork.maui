using gwork.maui.Models;
using SQLite;
using System.Collections.ObjectModel;

namespace gwork.maui.Data
{
    public class UserOfferApplyDatabase
    {
        SQLiteAsyncConnection Database;

        async Task Init()
        {
            if (Database is not null)
                return;

            Database = new SQLiteAsyncConnection(DatabaseConstants.DatabasePath, DatabaseConstants.Flags);
            var result = await Database.CreateTableAsync<UserOfferApply>();
        }

        public async Task<ObservableCollection<UserOfferApply>> GetUserOffersAppliesAsync()
        {
            await Init();
            return new ObservableCollection<UserOfferApply>(await Database.Table<UserOfferApply>().ToListAsync());
        }
        public async Task<UserOfferApply> GetUserOfferApplyAsync(int userId, int offerId)
        {
            await Init();
            return await Database.Table<UserOfferApply>().Where(u => u.UserId == userId && u.OfferId == offerId).FirstOrDefaultAsync();
        }

        public async Task<List<UserOfferApply>> GetUserOffersAppliedAsync(int userId)
        {
            await Init();
            return await Database.Table<UserOfferApply>().Where(u => u.UserId == userId).ToListAsync();
        }

        public async Task<int> SaveUserOfferApplyAsync(UserOfferApply userOfferApply)
        {
            await Init();
            if (userOfferApply.Id != 0)
            {
                return await Database.UpdateAsync(userOfferApply);
            }
            else
            {
                return await Database.InsertAsync(userOfferApply);
            }
        }

        public async Task<int> DeleteUserOfferApplyAsync(UserOfferApply userOfferApply)
        {
            await Init();
            return await Database.DeleteAsync(userOfferApply);
        }
    }
}