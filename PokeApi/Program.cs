using System;
using System.Diagnostics;
//Using System.Net.Http directive which will enable HttpClient.
using System.Net.Http;
//use newtonsoft to convert json to c# objects.
using Newtonsoft.Json.Linq;
using PokemonApi;

namespace PokeApi
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Poke ID: ");
            //GetPokemon();
            //Console.ReadLine();
            string id = Console.ReadLine();
            //int id = Convert.ToInt16(Console.ReadLine());
            GetOnePokemon(id);
            Console.ReadKey();
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
        
        //Define your static method which will make the method become part of the class
        //Have it void since your are logging the result into the console.
        //Which would take a integer as a argument.
        public static async void GetOnePokemon(string pokeId)
        {
            //Define your base url
            string baseURL = $"http://pokeapi.co/api/v2/pokemon/{pokeId}";
            //Have your api call in try/catch block.
            try { 
                //Now we will have our using directives which would have a HttpClient 
                using (HttpClient client = new HttpClient())
                {
                    //Now get your response from the client from get request to baseurl.
                    //Use the await keyword since the get request is asynchronous, and want it run before next asychronous operation.
                    using (HttpResponseMessage res = await client.GetAsync(baseURL))
                    {
                        //Now retrieve content from the response, which would be HttpContent, retrieve from the response Content property.
                        using (HttpContent content = res.Content)
                        {
                            //Retrieve the data from the content of the response, have the await keyword since it is asynchronous.
                            string data = await content.ReadAsStringAsync();
                            //If the data is not null, parse the data to a C# object, then create a new instance of PokeItem.
                            if (data != null)
                            {
                                //Parse your data into a object.
                                var result = JObject.Parse(data)["abilities"];
                                Debug.WriteLine(result);

                                PokeItem pokeItem = new PokeItem(name: $"{result[0]["ability"]["name"]}");

                                Console.WriteLine("Pokemon Name: {0}", pokeItem.Name);
                            }
                            else
                            {
                                //If data is null log it into console.
                                Console.WriteLine("Data is null!");
                            }
                        }
                    }
                }
                //Catch any exceptions and log it into the console.
            } catch(Exception exception) {
                Console.WriteLine(exception);
            }
        }
        
    }
}