using SQLite;
using SQLiteNetExtensions.Extensions;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System;
using Vaerksted.Models;

namespace Vaerksted.Services
{
    public class Database
    {
        private readonly SQLiteAsyncConnection _connection;

        public Database()
        {
            var dataDir = FileSystem.AppDataDirectory;
            var databasePath = Path.Combine(dataDir, "Vaerksted_3.db");

            // ⚠️ Debug: Slet eksisterende database (brug kun midlertidigt!)
            // if (File.Exists(databasePath))
            //     File.Delete(databasePath);

            _connection = new SQLiteAsyncConnection(databasePath);
        }

        public async Task InitializeAsync()
        {
            try
            {
                await _connection.CreateTableAsync<Opgave>();
                await _connection.CreateTableAsync<Faktura>();
                await _connection.CreateTableAsync<Materialer>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Database initialization failed: {ex.Message}");
                throw;
            }
        }

        // -------------------- OPGAVE --------------------

        public async Task<List<Opgave>> GetOpgaveAsync() =>
            await _connection.Table<Opgave>().ToListAsync();

        public async Task<Opgave> GetOpgaveAsync(int id) =>
            await _connection.Table<Opgave>().Where(t => t.ID == id).FirstOrDefaultAsync();

        public async Task<List<Opgave>> GetOpgaveByDateAsync(DateTime date)
        {
            var tomorrow = date.AddDays(1);
            return await _connection.Table<Opgave>()
                .Where(t => t.Date >= date && t.Date < tomorrow)
                .ToListAsync();
        }

        public async Task<int> AddOpgaveAsync(Opgave opgave)
        {
            try
            {
                return await _connection.InsertAsync(opgave);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to insert Opgave: {ex.Message}");
                throw;
            }
        }

        public async Task<int> DeleteOpgave(Opgave opgave) =>
            await _connection.DeleteAsync(opgave);

        public async Task<int> UpdateOpgave(Opgave opgave) =>
            await _connection.UpdateAsync(opgave);

        // -------------------- FAKTURA --------------------

        public async Task<List<Faktura>> GetFakturaAsync() =>
            await _connection.Table<Faktura>().ToListAsync();

        public async Task<Faktura> GetFakturaAsync(int id) =>
            await _connection.Table<Faktura>().Where(t => t.ID == id).FirstOrDefaultAsync();

        public async Task<int> AddFakturaAsync(Faktura faktura) =>
            await _connection.InsertAsync(faktura);

        public async Task<int> DeleteFaktura(Faktura faktura) =>
            await _connection.DeleteAsync(faktura);

        public async Task<int> UpdateFaktura(Faktura faktura) =>
            await _connection.UpdateAsync(faktura);

        // -------------------- MATERIALER --------------------

        public async Task<List<Materialer>> GetMaterialerAsync() =>
            await _connection.Table<Materialer>().ToListAsync();

        public async Task<Materialer> GetMaterialerAsync(int id) =>
            await _connection.Table<Materialer>().Where(t => t.ID == id).FirstOrDefaultAsync();

        public async Task<int> AddMaterialerAsync(Materialer materialer) =>
            await _connection.InsertAsync(materialer);

        public async Task<int> DeleteMaterialer(Materialer materialer) =>
            await _connection.DeleteAsync(materialer);

        public async Task<int> UpdateMaterialer(Materialer materialer) =>
            await _connection.UpdateAsync(materialer);
    }
}
