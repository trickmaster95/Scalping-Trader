using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScalperPlus.Common.Sqlite
{
    public interface IDbContext
    {
        public Table<T> GetTable<T>() where T:IModel , new();
    }
}
