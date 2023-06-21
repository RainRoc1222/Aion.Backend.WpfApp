using Dapper;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aion.Backend.WpfApp
{
    public static class SQLiteMananergment
    {
        private static readonly string myConnectionString = $"Data Source={Environment.CurrentDirectory}\\AionDB.db";
        public static TeacherService TeacherService { get; set; }
        public static ParentService ParentService { get; set; }
        static SQLiteMananergment()
        {
            TeacherService = new TeacherService(myConnectionString);
            ParentService = new ParentService(myConnectionString);  
        }
        public static IEnumerable<T> GetAllData<T>(T table) where T : class
        {
            using (var connection = new SqliteConnection(myConnectionString))
            {
                string sqlstr = $"SELECT * FROM {table.GetType().Name}";
                 return connection.Query<T>(sqlstr);
            }
        }
    }
}
