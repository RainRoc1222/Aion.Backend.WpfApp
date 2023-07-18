using Dapper;
using Microsoft.Data.Sqlite;

namespace Aion.Backend.WpfApp
{
    public class ReportService
    {
        private readonly string myConnectionString;
        public ReportService(string connectionString)
        {
            myConnectionString = connectionString;
        }

        public void Create(TeacherReport report)
        {
            using (var connection = new SqliteConnection(myConnectionString))
            {
                string sqlstr = $"INSERT INTO TeacherReport(TeacherId,LessonName,Date,UserId,Lesson) VALUES({report.TeacherId},'{report.LessonName}','{report.Date}',{report.UserId})";
                connection.Execute(sqlstr, report);
            }
        }
        public void Edit(TeacherReport report)
        {
            if (report.Id != 0)
            {
                using (var connection = new SqliteConnection(myConnectionString))
                {
                    string sqlstr = $"UPDATE TeacherReport SET LessonName={report.LessonName},UserId={report.UserId},Date={report.Date} WHERE Id={report.Id}";
                    connection.Execute(sqlstr, report);
                }
            }
        }
        public void Delete(TeacherReport report)
        {
            using (var connection = new SqliteConnection(myConnectionString))
            {
                string sqlstr = $"DELETE FROM Teacher WHERE Id={report.Id}";
                connection.Execute(sqlstr, report);
            }
        }
    }
}
