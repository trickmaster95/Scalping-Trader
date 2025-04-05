using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScalperPlus.Common.Sqlite
{
    public class DbContext:IDbContext
    {
        private readonly SQLiteAsyncConnection _database;
        public SQLiteAsyncConnection Db { get => _database; }
        public DbContext()
        {
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "scalpingPlusDb.db");
            _database = new SQLiteAsyncConnection(dbPath);
        }

        public Table<T> GetTable<T>() where T : IModel, new() => new Table<T>(_database);
    }
}
