using Dapper;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Aion.Backend.WpfApp
{
    public class UserService
    {
        private readonly string myConnectionString;
        public UserService(string connectionString)
        {
            myConnectionString = connectionString;
        }

        public void Create(User user)
        {
            using (var connection = new SqliteConnection(myConnectionString))
            {
                string sqlstr = $"INSERT INTO User(FirstName,LastName,Address,Age,ParentId,Birthday,Style,Relation) VALUES('{user.FirstName}','{user.LastName}','{user.Address}',{user.Age},{user.ParentId},'{user.Birthday}','{user.Style}','{user.Relation}')";
                connection.Execute(sqlstr, user);
            }
        }
    }
}
