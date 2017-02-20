using System;
using System.Collections.Generic;
using SpaData.Models;

namespace SpaData.DataSeed
{
    public static class PersonSeed
    {
        public static List<Person> Persons = new List<Person>()
        {
            new Person() { FirstName = "Jamie", MiddleName = "K",LastName = "Lannister",Dob = new DateTime(),Email = "kingslayer@gmail.com"},
            new Person() { FirstName = "Tyrion", MiddleName = "H",LastName = "Lannister",Dob = new DateTime(),Email = "halfman@gmail.com"},
            new Person() { FirstName = "Arya", MiddleName = "N",LastName = "Stark",Dob = new DateTime(),Email = "noone@gmail.com"},
            new Person() { FirstName = "Sansa", MiddleName = "B",LastName = "Stark",Dob = new DateTime(),Email = "sansa@gmail.com"},
            new Person() { FirstName = "Jon", MiddleName = "",LastName = "Snow",Dob = new DateTime(),Email = "zombie@gmail.com"},
            new Person() { FirstName = "Rajesh", MiddleName = "Ramayan",LastName = "Koothrapalli",Dob = new DateTime(),Email = "raj@gmail.com"},
            new Person() { FirstName = "Amy", MiddleName = "Farah",LastName = "Fowler",Dob = new DateTime(),Email = "amy@gmail.com"}
        };
    }
}
