using Dapper;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;

namespace Aion.Backend.WpfApp
{
    public static class SQLiteMananergment
    {
        private static readonly string myConnectionString = $"Data Source={Environment.CurrentDirectory}\\AionDB.db";
        public static TeacherService TeacherService { get; set; }
        public static ParentService ParentService { get; set; }
        public static UserService UserService { get; set; }
        public static ReportService ReportService { get; set; }
        static SQLiteMananergment()
        {
            TeacherService = new TeacherService(myConnectionString);
            ParentService = new ParentService(myConnectionString);
            UserService = new UserService(myConnectionString);
            ReportService = new ReportService(myConnectionString);
        }
        public static IEnumerable<T> GetAllData<T>(T table) where T : class
        {
            using (var connection = new SqliteConnection(myConnectionString))
            {
                string sqlstr = $"SELECT * FROM {table.GetType().Name}";
                return connection.Query<T>(sqlstr);
            }
        }

        public static void DeleteAllData<T>(T table) where T : class
        {
            using (var connection = new SqliteConnection(myConnectionString))
            {
                var datas = GetAllData(table);
                var sqlstr = $"DELETE FROM {table.GetType().Name}";
                connection.Execute(sqlstr, datas);
            }
        }
    }
}
