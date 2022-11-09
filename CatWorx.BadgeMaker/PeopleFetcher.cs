using System;
using System.Collections.Generic;
using System.Net.Http;      // use this to download information from API endpoints.
using Newtonsoft.Json.Linq; // JObject class will allow us to parse JSON files. 

namespace CatWorx.BadgeMaker
{
    class PeopleFetcher
    {

        async public static Task<List<Employee>> GetFromAPI()
        {
            List<Employee> employees = new List<Employee>();

            using (HttpClient client = new HttpClient())
            {
                // GetStringAsync() --> returns all the info from the URL as a JSON string. 
                string response = await client.GetStringAsync("https://randomuser.me/api/?results=10&nat=us&inc=name,id,picture");

                // Parse the JSON object.
                // This will convert the JSON string we downloaded from the API into a JObject. 
                JObject json = JObject.Parse(response);

                // Here, we do a foreach loop to iterate throught the JSON String.
                // Each token represents pieces of the JSON data after being parsed. 
                foreach (JToken token in json.SelectToken("results")!)
                {
                    Employee emp = new Employee(
                        token.SelectToken("name.first")!.ToString(),
                        token.SelectToken("name.last")!.ToString(),
                        Int32.Parse(token.SelectToken("id.value")!.ToString().Replace("-", "")),
                        token.SelectToken("picture.large").ToString()
                    );
                    employees.Add(emp);
                }
            }

            return employees;
        }
    }
}