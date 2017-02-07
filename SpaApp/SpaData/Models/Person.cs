using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaData.Models
{
    public class Person
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Email { get; set; }
        public DateTime Dob { get; set; }
    }
}
