using SQLite;
using vaerkstedGenafl.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace vaerkstedGenafl.Data
{
    public static class Database
    {
        private static SQLiteAsyncConnection? _connection = null;

        public static async Task Initialize()
        {
            if (_connection != null) return; // Connection is already initialized.

            try
            {
                var dataDir = FileSystem.AppDataDirectory; // Gets app data directory
                var databasePath = Path.Combine(dataDir, "Vaerksted.db");

                // Retrieve or generate encryption key
                string _dbEncryptionKey = SecureStorage.GetAsync("dbKey").Result;

                if (string.IsNullOrEmpty(_dbEncryptionKey))
                {
                    Guid g = Guid.NewGuid();
                    _dbEncryptionKey = g.ToString();
                    await SecureStorage.SetAsync("dbKey", _dbEncryptionKey);
                    Debug.WriteLine("Encryption key generated and stored.");
                }
                else
                {
                    Debug.WriteLine("Encryption key retrieved from SecureStorage.");
                }

                // Set up database connection with encryption key
                var dbOptions = new SQLiteConnectionString(databasePath, true, key: _dbEncryptionKey);
                _connection = new SQLiteAsyncConnection(dbOptions);

                // Create tables if they don't already exist
                await _connection.CreateTableAsync<Opgave>();
                await _connection.CreateTableAsync<Faktura>();
                await _connection.CreateTableAsync<Materialer>();

                Debug.WriteLine("Database initialized successfully.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Database initialization failed: {ex.Message}");
                throw new Exception("Database initialization failed", ex);  // Rethrow to propagate failure
            }
        }

        // Helper method to ensure database is initialized before performing any operations
        private static async Task EnsureInitialized()
        {
            if (_connection == null)
            {
                await Initialize(); // Ensure initialization happens before operation
            }
        }

        // OPGAVER
        public static async Task<List<Opgave>> GetOpgaver()
        {
            await EnsureInitialized(); // Ensure database is initialized before use
            return await _connection.Table<Opgave>().ToListAsync();
        }

        public static async Task<Opgave> GetOpgave(int id)
        {
            await EnsureInitialized(); // Ensure database is initialized before use
            return await _connection.Table<Opgave>().Where(x => x.ID == id).FirstOrDefaultAsync();
        }

        public static async Task<int> SaveOpgave(Opgave opgave)
        {
            await EnsureInitialized(); // Ensure database is initialized before use
            if (opgave.ID != 0)
            {
                return await _connection.UpdateAsync(opgave);
            }
            else
            {
                return await _connection.InsertAsync(opgave);
            }
        }

        public static async Task<int> DeleteOpgave(Opgave opgave)
        {
            await EnsureInitialized(); // Ensure database is initialized before use
            return await _connection.DeleteAsync(opgave);
        }

        // FAKTURAER
        public static async Task<List<Faktura>> GetFakturaer()
        {
            await EnsureInitialized(); // Ensure database is initialized before use
            return await _connection.Table<Faktura>().ToListAsync();
        }

        public static async Task<Faktura> GetFaktura(Opgave opgave)
        {
            await EnsureInitialized(); // Ensure database is initialized before use
            return await _connection.Table<Faktura>().Where(x => x.OpgaveID == opgave.ID).FirstOrDefaultAsync();

            //return await _connection.Table<Faktura>().Where(x => x.ID == id).FirstOrDefaultAsync();
        }

        public static async Task<int> SaveFaktura(Faktura faktura)
        {
            await EnsureInitialized(); // Ensure database is initialized before use
            if (faktura.ID != 0)
            {
                return await _connection.UpdateAsync(faktura);
            }
            else
            {
                return await _connection.InsertAsync(faktura);
            }
        }

        public static async Task<int> DeleteFaktura(Faktura faktura)
        {
            await EnsureInitialized(); // Ensure database is initialized before use
            return await _connection.DeleteAsync(faktura);
        }

        // MATERIALER
        public static async Task<List<Materialer>> GetMaterialer()
        {
            await EnsureInitialized(); // Ensure database is initialized before use
            return await _connection.Table<Materialer>().ToListAsync();
        }

        public static async Task<Materialer> GetMaterial(int id)
        {
            await EnsureInitialized(); // Ensure database is initialized before use
            return await _connection.Table<Materialer>().Where(x => x.ID == id).FirstOrDefaultAsync();
        }

        public static async Task<int> SaveMaterial(Materialer material)
        {
            await EnsureInitialized(); // Ensure database is initialized before use
            if (material.ID != 0)
            {
                return await _connection.UpdateAsync(material);
            }
            else
            {
                return await _connection.InsertAsync(material);
            }
        }

        public static async Task<int> DeleteMaterial(Materialer material)
        {
            await EnsureInitialized(); // Ensure database is initialized before use
            return await _connection.DeleteAsync(material);
        }

        public static async Task<List<Materialer>> GetMaterialerByFakturaId(int fakturaId)
        {
            await EnsureInitialized(); // Ensure database is initialized before use
            return await _connection.Table<Materialer>().Where(x => x.FakturaID == fakturaId).ToListAsync();
        }

        public static async Task<int> DeleteMaterialerByFakturaId(int fakturaId)
        {
            await EnsureInitialized(); // Ensure database is initialized before use
            var materialer = await GetMaterialerByFakturaId(fakturaId);
            foreach (var material in materialer)
            {
                await _connection.DeleteAsync(material);
            }
            return materialer.Count;
        }

        public static async Task<int> DeleteMaterialerByOpgaveId(int opgaveId)
        {
            await EnsureInitialized(); // Ensure database is initialized before use
            var fakturaer = await GetFakturaer();
            int deletedCount = 0;
            foreach (var faktura in fakturaer)
            {
                if (faktura.OpgaveID == opgaveId)
                {
                    deletedCount += await DeleteMaterialerByFakturaId(faktura.ID);
                }
            }
            return deletedCount;
        }

        public static async Task<int> DeleteFakturaerByOpgaveId(int opgaveId)
        {
            await EnsureInitialized(); // Ensure database is initialized before use
            var fakturaer = await GetFakturaer();
            int deletedCount = 0;
            foreach (var faktura in fakturaer)
            {
                if (faktura.OpgaveID == opgaveId)
                {
                    deletedCount += await DeleteFaktura(faktura);
                }
            }
            return deletedCount;
        }

        public static async Task<int> DeleteOpgaveWithFakturaerAndMaterialer(Opgave opgave)
        {
            await EnsureInitialized(); // Ensure database is initialized before use
            var fakturaer = await GetFakturaer();
            foreach (var faktura in fakturaer)
            {
                if (faktura.OpgaveID == opgave.ID)
                {
                    await DeleteMaterialerByFakturaId(faktura.ID);
                    await DeleteFaktura(faktura);
                }
            }
            return await DeleteOpgave(opgave);
        }
    }
}
