using System;
//Using System.Net.Http directive which will enable HttpClient.
using System.Net.Http;
//use newtonsoft to convert json to c# objects.
using Newtonsoft.Json.Linq;

namespace PokeApi
{
    class Program
    {
        static void Main(string[] args)
        {
            GetPokemon();
        }

        public static async void GetPokemon()
        {
            //Define baseURL
            string baseUrl = "http://pokeapi.co/api/v2/pokemon/";
            //Have using statements within a try/catch block that will catch any exceptions.
            try
            {
                //Define HttpClient with first using statement which will use a IDisposable.
                using (HttpClient client = new HttpClient())
                {
                    //The next using statement initiates the Get Request, use the await keyword so it will execute the using statement in order.
                    //The HttpResponseMessage which contains status code, and data from response.
                    using (HttpResponseMessage res = await client.GetAsync(baseUrl))
                    {
                        //Then get the data or content from the response in the next using statement, then within it you will get the data, and convert it to a c# object.
                        using (HttpContent content = res.Content)
                        {
                            //Now assign content to the data variable, by converting into a string using the await keyword.
                            var data = await content.ReadAsStringAsync();
                            //If the data isn't null return log convert the data using newtonsoft JObject Parse class method on the data.
                            if (data != null)
                            {
                                //Now log your data in the console
                                Console.WriteLine("data------------{0}", JObject.Parse(data)["results"]);
                            }
                            else
                            {
                                Console.WriteLine("NO Data----------");
                            }
                        }
                    }
                } 
            }catch(Exception exception)
            {
                Console.WriteLine("Exception Hit------------");
                Console.WriteLine(exception);
            }
        }
    }
}