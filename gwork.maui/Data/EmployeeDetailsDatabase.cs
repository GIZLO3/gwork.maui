﻿using gwork.maui.Models;
using SQLite;

namespace gwork.maui.Data
{
    public class EmployeeDetailsDatabase
    {
        SQLiteAsyncConnection Database;

        async Task Init()
        {
            if (Database is not null)
                return;

            Database = new SQLiteAsyncConnection(DatabaseConstants.DatabasePath, DatabaseConstants.Flags);
            var result = await Database.CreateTableAsync<EmployeeDetails>();
        }

        public async Task<EmployeeDetails> GetEmpleyeeDetailsAsync(int Id)
        {
            await Init();

            return await Database.FindWithQueryAsync<EmployeeDetails>($"SELECT * FROM [EmployeeDetails] WHERE [Id] = {Id}");
        }
        public async Task<int> SaveEmpleyeeDetailsAsync(EmployeeDetails employeeDetails)
        {
            await Init();
            if (employeeDetails.Id != 0)
            {
                await Database.UpdateAsync(employeeDetails);
                return employeeDetails.Id;
            }     
            else
            {
                await Database.InsertAsync(employeeDetails);
                var lastAdded = await Database.Table<EmployeeDetails>().OrderByDescending(x => x.Id).FirstOrDefaultAsync();
                return lastAdded.Id;
            }     
        }
    }
}