using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ScalperPlus.Common.Sqlite
{
    public class Table<T> where T : IModel , new()
    {
        private readonly SQLiteAsyncConnection _db;
        public Table(SQLiteAsyncConnection dbContext) {
            this._db = dbContext;
            initialize();
        }
        public void initialize()
        {
            try
            {
                _db.CreateTableAsync<T>().GetAwaiter().GetResult();
            }
            catch { }
        }

        #region quering
        public Task<T> Find(int Id)
        {
            return _db.FindAsync<T>(item => item.Id == Id);
        }
        public Task<T> Find(Expression<Func<T,bool>> filter)
        {
            return _db.FindAsync<T>(filter);
        }
        public Task<T> First()
        {
            return _db.Table<T>().FirstOrDefaultAsync();
        }
        public async Task<T?> Last()
        {
            return (await _db.Table<T>().ToListAsync()).OrderByDescending(a => a.Id).FirstOrDefault();
        }
        public Task<List<T>> GetAll()
        {
            return _db.Table<T>().ToListAsync();
        }
        public Task<T> Get(Expression<Func<T, bool>> filter)
        {
            return _db.Table<T>().Where(filter).FirstOrDefaultAsync();
        }
        public async Task<List<T>> GetRangeReversed(int? start, int limit = 100)
        {
            return (await _db.Table<T>().Where(a => start == null || a.Id < start).ToListAsync()).OrderByDescending(a => a.Id).Take(limit).ToList();
        }
        public Task<List<T>> GetFilters(Expression<Func<T, bool>> filter)
        {
            return _db.Table<T>().Where(filter).ToListAsync();
        }

        public Task<int> Add(T instance)
        {
            return _db.InsertAsync(instance);
        }
        public Task<int> Add(List<T> items)
        {
            return _db.InsertAllAsync(items);
        }
        public Task<int> Update(T item)
        {
            return _db.UpdateAsync(item);
        }
        public Task<int> Update(List<T> items)
        {
            return _db.UpdateAllAsync(items);
        }

        public Task<int> Delete(T item)
        {
            return _db.DeleteAsync(item);
        }
        public void DeleteLast()
        {
            T? last = (_db.Table<T>().ToListAsync()).GetAwaiter().GetResult().OrderByDescending(a => a.Id).FirstOrDefault();
            if (last != null) _db.DeleteAsync(last).GetAwaiter().GetResult();
        }
        public Task<int> Clear(List<T> items)
        {

            return _db.DeleteAllAsync<T>();
        }
        #endregion
    }
}
