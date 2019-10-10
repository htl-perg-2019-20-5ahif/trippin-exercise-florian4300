using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace TrippinExercise
{
    public static partial class TrippinClass
    {
        private static HttpClient HttpClient
            = new HttpClient() { BaseAddress = new Uri("https://services.odata.org/TripPinRESTierService/(S(pjbjptgenpy1ibxrlejqtlxh))/") };

        public static async Task Get()
        {
            string peopleRaw = await System.IO.File.ReadAllTextAsync("users.json");
            var people = JsonSerializer.Deserialize<Person[]>(peopleRaw);
            foreach(Person p in people)
            {
                var personResponse = await HttpClient.GetAsync("People('"+p.UserName+"')");
                if (!personResponse.IsSuccessStatusCode)
                {
                    Console.WriteLine("Adding: "+p.UserName);
                    PersonApi person = new PersonApi();
                    person.UserName = p.UserName;
                    person.FirstName = p.FirstName;
                    person.LastName = p.LastName;
                    person.Emails = new string [] { p.Email };
                    AddressInfo ai = new AddressInfo();
                    ai.Address = p.Address;
                    ai.City = new City();
                    ai.City.CountryRegion = p.Country;
                    ai.City.Name = p.CityName;
                    ai.City.Region = "ID";

                    person.AddressInfo = new AddressInfo[] { ai };
                    var personSerialized = new StringContent(JsonSerializer.Serialize(person), Encoding.UTF8, "application/json");
                    var personPost = await HttpClient.PostAsync("People", personSerialized);
                    personPost.EnsureSuccessStatusCode();
                }
                else
                {
                    Console.WriteLine("User " + p.UserName + " found, skipping");
                }
                
            }
        }
    }
}
