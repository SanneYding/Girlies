using SQLite;
using SQLiteNetExtensions.Extensions;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices.JavaScript;
using System.Threading.Tasks;
using Vaerksted.Models;

namespace Vaerksted.Services
{
    public class Database
    {
        private readonly SQLiteAsyncConnection _connection;

        // Lavet til en singleton så alle page/views kan få adgang til den samme database instance
        private static Database _instance;

        public static Database Instance => _instance ??= new Database();

        private Database()
        {
            var dataDir = FileSystem.AppDataDirectory;
            var databasePath = Path.Combine(dataDir, "Vaerksted_3.db");

            string _dbEncryptionKey = SecureStorage.GetAsync("dbKey").Result;

            if (string.IsNullOrEmpty(_dbEncryptionKey))
            {
                Guid g = Guid.NewGuid();
                _dbEncryptionKey = g.ToString();
                SecureStorage.SetAsync("dbKey", _dbEncryptionKey);
            }

            var dbOptions = new SQLiteConnectionString(databasePath, true, key: _dbEncryptionKey);

            _connection = new SQLiteAsyncConnection(dbOptions);
            _ = Intialize();
        }

        private async Task Intialize()
        {
            await _connection.CreateTableAsync<Opgave>();
            await _connection.CreateTableAsync<Faktura>();
            await _connection.CreateTableAsync<Materialer>();
        }


        //OPGAVE
        public async Task<List<Opgave>> GetOpgaveAsync() => await _connection.Table<Opgave>().ToListAsync();

        public async Task<Opgave> GetOpgaveAsync(int id)
        {
            var query = _connection.Table<Opgave>().Where(t => t.ID == id);
            return await query.FirstOrDefaultAsync();
        }

        public async Task<List<Opgave>> GetOpgaveByDateAsync(DateTime date)
        {
            var selected = date;
            var tomorrow = date.AddDays(1);

            return await _connection.Table<Opgave>()
                .Where(t => t.Date > date && t.Date < tomorrow).ToListAsync();

            //return await query.ToListAsync();
        }

        public async Task<int> AddOpgaveAsync(Opgave opgave)
        {
            return await _connection.InsertAsync(opgave);
        }

        public async Task<int> DeleteOpgave(Opgave opgave)
        {
            return await _connection.DeleteAsync(opgave);
        }

        public async Task<int> UpdateOpgave(Opgave opgave)
        {
            return await _connection.UpdateAsync(opgave);
        }

        //FAKTURA
        public async Task<List<Faktura>> GetFakturaAsync() => await _connection.Table<Faktura>().ToListAsync();

        public async Task<Faktura> GetFakturaAsync(int id)
        {
            var query = _connection.Table<Faktura>().Where(t => t.ID == id);
            return await query.FirstOrDefaultAsync();
        }

        public async Task<int> AddFakturaAsync(Faktura faktura)
        {
            return await _connection.InsertAsync(faktura);
        }

        public async Task<int> DeleteFaktura(Faktura faktura)
        {
            return await _connection.DeleteAsync(faktura);
        }

        public async Task<int> UpdateFaktura(Faktura faktura)
        {
            return await _connection.UpdateAsync(faktura);
        }

        //MATERIALER
        public async Task<List<Materialer>> GetMaterialerAsync() => await _connection.Table<Materialer>().ToListAsync();

        public async Task<Materialer> GetMaterialerAsync(int id)
        {
            var query = _connection.Table<Materialer>().Where(t => t.ID == id);
            return await query.FirstOrDefaultAsync();
        }

        public async Task<int> AddMaterialerAsync(Materialer materialer)
        {
            return await _connection.InsertAsync(materialer);
        }

        public async Task<int> DeleteMaterialer(Materialer materialer)
        {
            return await _connection.DeleteAsync(materialer);
        }

        public async Task<int> UpdateMaterialer(Materialer materialer)
        {
            return await _connection.UpdateAsync(materialer);
        }
    }
}