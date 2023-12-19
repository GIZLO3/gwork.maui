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

        public async Task<ObservableCollection<Offer>> GetOffersAsyncBySearchText(string searchText)
        {
            await Init();
            return new ObservableCollection<Offer>(await Database.Table<Offer>().Where(o => o.PositionName.ToLower().Contains(searchText) || o.Description.ToLower().Contains(searchText) || o.FirmLocation.ToLower().Contains(searchText) || o.FirmName.ToLower().Contains(searchText)).ToListAsync());
        }

        public async Task<int> InsertOfferAsync(Offer offer)
        {
            await Init();
            return await Database.InsertAsync(offer);
        }

        public async Task<int> SaveOfferAsync(Offer offer)
        {
            await Init();
            if (offer.Id != 0)
            {
                return await Database.UpdateAsync(offer);
            }
            else
            {
                return await Database.InsertAsync(offer);
            }
        }

        public async Task<int> DeleteOfferAsync(Offer offer)
        {
            await Init();
            return await Database.DeleteAsync(offer);
        }
    }
}
