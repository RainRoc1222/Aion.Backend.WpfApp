using System;

namespace Aion.Backend.WpfApp
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public int Age { get; set; }
        public int ParentId { get; set; }
        public DateTime Birthday { get; set; }
        public string Style { get;set; }
        public string Relation { get; set; }
    }
}