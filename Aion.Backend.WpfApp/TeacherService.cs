using Dapper;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using System.Linq;

namespace Aion.Backend.WpfApp
{
    public class TeacherService
    {
        private readonly string myConnectionString;
        public TeacherService(string connectionString)
        {
            myConnectionString = connectionString;
        }

        public IEnumerable<Teacher> GetAll()
        {
            return SQLiteMananergment.GetAllData(new Teacher());
        }

        public void Edit(Teacher teacher)
        {
            if (teacher.Id != 0)
            {
                using (var connection = new SqliteConnection(myConnectionString))
                {
                    string sqlstr = $"UPDATE Teacher SET Name='{teacher.Name}',Email='{teacher.Email}',Phone='{teacher.Phone}',Style='{teacher.Style}',Level='{teacher.Level}',Position='{teacher.Position}' WHERE Id='{teacher.Id}'";
                    connection.Execute(sqlstr, teacher);
                }
            }
        }

        public void Create(Teacher teacher)
        {
            using (var connection = new SqliteConnection(myConnectionString))
            {
                string sqlstr = $"INSERT INTO Teacher(Name,Email,Phone,Style,Level,Position) VALUES('{teacher.Name}','{teacher.Email}','{teacher.Phone}','{teacher.Style}','{teacher.Level}','{teacher.Position}')";
                connection.Execute(sqlstr, teacher);
            }
        }


        public void Delete(Teacher teacher)
        {
            using (var connection = new SqliteConnection(myConnectionString))
            {
                string sqlstr = $"DELETE FROM Teacher WHERE Id={teacher.Id}";
                connection.Execute(sqlstr, teacher);
            }
        }

        public List<TeacherViewModel> GetTeacherInfo(int id)
        {
            try
            {
                var result = new List<TeacherViewModel>();
                var users = SQLiteMananergment.GetAllData(new User());
                var teachers = SQLiteMananergment.GetAllData(new Teacher());
                var reports = SQLiteMananergment.GetAllData(new TeacherReport());
                var datas = reports.Join(teachers, r => r.TeacherId, t => t.Id, (r, t) => new {r.Lesson, r.Date, r.LessonName, r.UserId, t.Level, t.Position, t.Id }).Where(x => x.Id == id)
                    .Join(users, d => d.UserId, u => u.Id, (d, u) => new { d.Date, d.LessonName, u.FirstName, u.LastName, d.Level, d.Position, u.Id,d.Lesson});
                var teacherName = SQLiteMananergment.GetAllData(new Teacher()).First(x => x.Id == id).Name;

                foreach (var data in datas)
                {
                    result.Add(new TeacherViewModel()
                    {
                        Date = data.Date,
                        LessonName = data.LessonName,
                        Lesson = data.Lesson,
                        UserFirstName = data.FirstName,
                        UserLastName = data.LastName,
                        UserId = data.Id,
                        TeacherName = teacherName,
                        Level = data.Level,
                        Position = data.Position,
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