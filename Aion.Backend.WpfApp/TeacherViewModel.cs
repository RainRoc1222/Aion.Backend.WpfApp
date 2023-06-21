using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aion.Backend.WpfApp
{
    public class TeacherViewModel
    {
        public DateTime Date { get; set; }  
        public string TeacherName { get; set; } 
        public string UserName { get; set; }
        public string Lesson { get; set; }
        public string LessonName { get; set; }
        public int Level { get; set; }
        public string Position { get; set; }
    }
}
