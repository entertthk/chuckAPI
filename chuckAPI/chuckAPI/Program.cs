using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;  // vaja api jaoks
using System.Net;
using Newtonsoft.Json; // vaja api jaoks
using Nancy.Json;

namespace chuckAPI
{
    class Program
    {
        static void Main(string[] args)
        {
            showCategories();
            //showRandomJoke();
            catJoke();

            Console.ReadLine();
        }

        private static void catJoke()
        {
            Console.WriteLine("Choose category:");
            string userInput = Console.ReadLine();

            string categoryJokeUrl = ($"https://api.chucknorris.io/jokes/random?category={userInput}");
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(categoryJokeUrl);  //teeb päringu url-ile
            request.Method = "GET";

            var webResponse = request.GetResponse();  //salvestame api vastuse siia sisse
            var webStream = webResponse.GetResponseStream();

            using (var responseReader = new StreamReader(webStream))
            {
                var response = responseReader.ReadToEnd();
                Joke randomJoke = JsonConvert.DeserializeObject<Joke>(response);
                Console.WriteLine(randomJoke.Value);

            }
        }

        public static void showCategories()
        {
            string categoryUrl = "https://api.chucknorris.io/jokes/categories";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(categoryUrl);  //teeb päringu url-ile
            request.Method = "GET";

            var webResponse = request.GetResponse();  //salvestame api vastuse siia sisse
            var webStream = webResponse.GetResponseStream();

            using (var responseReader = new StreamReader(webStream))
            {
                var response = responseReader.ReadToEnd();
                //Console.WriteLine(response);
                JavaScriptSerializer ser = new JavaScriptSerializer();  //teeb pikast sõnest massiivi
                var categories = ser.Deserialize<List<string>>(response);

                foreach (string category in categories)
                {
                    Console.WriteLine(category);
                }

            }

        }

        public static void showRandomJoke()
        {
            string categoryJokeUrl = "https://api.chucknorris.io/jokes/random";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(categoryJokeUrl);  //teeb päringu url-ile
            request.Method = "GET";

            var webResponse = request.GetResponse();  //salvestame api vastuse siia sisse
            var webStream = webResponse.GetResponseStream();

            using (var responseReader = new StreamReader(webStream))
            {
                var response = responseReader.ReadToEnd();
                Joke randomJoke = JsonConvert.DeserializeObject<Joke>(response);
                Console.WriteLine(randomJoke.Value);

            }
        }
    }
}
