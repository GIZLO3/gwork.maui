using gwork.maui.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<ObservableCollection<UserOfferApply>> GetOffersAsync()
        {
            await Init();
            return new ObservableCollection<UserOfferApply>(await Database.Table<UserOfferApply>().ToListAsync());
        }
        public async Task<UserOfferApply> GetOfferAsync(int userId, int offerId)
        {
            await Init();
            return await Database.Table<UserOfferApply>().Where(u => u.UserId == userId && u.OfferId == offerId).FirstOrDefaultAsync();
        }
        public async Task<int> SaveOfferAsync(UserOfferApply userOfferApply)
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

        public async Task<int> DeleteOfferAsync(UserOfferApply userOfferApply)
        {
            await Init();
            return await Database.DeleteAsync(userOfferApply);
        }
    }
}
