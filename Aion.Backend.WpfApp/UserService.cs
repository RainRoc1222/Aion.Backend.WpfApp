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
                string sqlstr = $"INSERT INTO User(FirstName,LastName,Address,Age,ParentId,Birthday,Style,Relation) VALUES('{user.FirstName}','{user.LastName}','{user.Address}',{user.Age},'{user.ParentId}','{user.Birthday}','{user.Style}','{user.Relation}')";
                connection.Execute(sqlstr, user);
            }
        }
        public List<TeacherViewModel> GetUserInfo(int id)
        {
            try
            {
                var result = new List<TeacherViewModel>();
                var teachers = SQLiteMananergment.GetAllData(new Teacher());
                var reports = SQLiteMananergment.GetAllData(new TeacherReport());
                var datas = reports.Join(teachers, r => r.TeacherId, t => t.Id, (r, t) => new { r.Id, r.Lesson, r.Date, r.LessonName, r.UserId,r.IsMissClass, t.Level, t.Position,  t.Name }).Where(x => x.UserId == id);

                foreach (var data in datas)
                {
                    result.Add(new TeacherViewModel()
                    {
                        ReportId = data.Id,
                        Date = data.Date,
                        LessonName = data.LessonName,
                        Lesson = data.Lesson,
                        TeacherName = data.Name,
                        IsMissClass = data.IsMissClass,
                    });
                }
                return result;
            }
            catch (System.Exception ex)
            {
                return new List<TeacherViewModel>();
            }
        }
    }
}
