using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aion.Backend.WpfApp
{
    public class TeacherReport
    {
        public int Id { get; set; }
        public int TeacherId { get; set; }
        public string LessonName { get; set; }
        public string Date{ get; set; }
        public int UserId { get; set; }
        public string Lesson { get; set; }
        public bool IsMissClass { get; set; }
    }
}
