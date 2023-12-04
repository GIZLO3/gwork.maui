﻿using gwork.maui.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gwork.maui.Data
{
    public class UserDatabase
    {
        SQLiteAsyncConnection Database;

        async Task Init()
        {
            if (Database is not null)
                return;

            Database = new SQLiteAsyncConnection(DatabaseConstants.DatabasePath, DatabaseConstants.Flags);
            var result = await Database.CreateTableAsync<User>();
        }

        public async Task<int> InsertUserAsync(User user)
        {
            await Init();
            return await Database.InsertAsync(user);
        }

        public async Task<User> GetUserAsync(string email)
        {
            await Init();
            return await Database.Table<User>().Where(user => user.Email == email).FirstOrDefaultAsync();
        }

        public async Task<User> GetUserAsync(int id)
        {
            await Init();
            return await Database.Table<User>().Where(user => user.Id == id).FirstOrDefaultAsync();
        }
    }
}
