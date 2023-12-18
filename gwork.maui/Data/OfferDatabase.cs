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
    public class OfferDatabase
    {
        SQLiteAsyncConnection Database;

        async Task Init()
        {
            if (Database is not null)
                return;

            Database = new SQLiteAsyncConnection(DatabaseConstants.DatabasePath, DatabaseConstants.Flags);
            var result = await Database.CreateTableAsync<Offer>();
        }

        public async Task<ObservableCollection<Offer>> GetOffersAsync()
        {
            await Init();
            return new ObservableCollection<Offer>(await Database.Table<Offer>().ToListAsync());
        }

        public async Task<Offer> GetOfferAsync(int Id)
        {
            await Init();
            return await Database.Table<Offer>().Where(o => o.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<int> InsertOfferAsync(Offer offer)
        {
            await Init();
            return await Database.InsertAsync(offer);
        }
    }
}
