using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace TrippinExercise
{
    public class Person
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string CityName { get; set; }

        public string Country { get; set; }

    }

    public class People
    {
        public List<Person> Results { get; set; }
    }

    public class PersonApi
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string[] Emails { get; set; }

        public AddressInfo[] AddressInfo { get; set; }
    }

    public class AddressInfo
    {
        public string Address { get; set; }
        public City City { get; set; }
    }

    public class City
    {
        public string Name { get; set; }
        public string CountryRegion { get; set; }
        public string Region { get; set; }

    }
}
