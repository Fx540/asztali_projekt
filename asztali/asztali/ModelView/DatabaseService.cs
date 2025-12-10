using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using asztali.Model;

namespace asztali.ModelView
{
    public class DatabaseService
    {
        private SQLiteAsyncConnection _database;

        async Task Init()
        {
            if (_database is not null)
                return;

            var databasePath = Path.Combine(FileSystem.AppDataDirectory, "MyContacts.db3");
            _database = new SQLiteAsyncConnection(databasePath);
            await _database.CreateTableAsync<ContactItem>();
        }

        public async Task<List<ContactItem>> GetContactsAsync()
        {
            await Init();
            return await _database.Table<ContactItem>().ToListAsync();
        }

        public async Task<ContactItem> GetContactAsync(int id)
        {
            await Init();
            return await _database.Table<ContactItem>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public async Task<int> SaveContactAsync(ContactItem contact)
        {
            await Init();
            if (contact.Id != 0)
                return await _database.UpdateAsync(contact);
            else
                return await _database.InsertAsync(contact);
        }

        public async Task<int> DeleteContactAsync(ContactItem contact)
        {
            await Init();
            return await _database.DeleteAsync(contact);
        }
    }
}
