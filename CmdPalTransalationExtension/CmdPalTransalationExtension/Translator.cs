using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CmdPalTransalationExtension
{
    public class Translator
    {
        private static readonly string endpoint = "https://api.cognitive.microsofttranslator.com";
        private static readonly string location = "westus2";

        public static async Task<string> Translate(string fromLang, string toLang, string text)
        {
            var fromLanguage = fromLang;
            var toLanguage = toLang;
            // Input and output languages are defined as parameters.
            string route = $"/translate?api-version=3.0&from={fromLanguage}&to={toLanguage}";
            object[] body = new object[] { new { Text = text } };
            var requestBody = JsonConvert.SerializeObject(body);

            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage())
            {
                // Build the request.
                request.Method = HttpMethod.Post;
                request.RequestUri = new Uri(endpoint + route);
                request.Content = new StringContent(requestBody, Encoding.UTF8, "application/json");
                request.Headers.Add("Ocp-Apim-Subscription-Key", key);
                // location required if you're using a multi-service or regional (not global) resource.
                request.Headers.Add("Ocp-Apim-Subscription-Region", location);

                // Send the request and get response.
                HttpResponseMessage response = await client.SendAsync(request).ConfigureAwait(false);
                // Read response as a string.
                string result = await response.Content.ReadAsStringAsync();
                Console.WriteLine(result);
                
                // Parse the result and extract the translated text
                var translations = JsonConvert.DeserializeObject<List<dynamic>>(result);
                string translatedText = translations[0].translations[0].text.ToString();
                // Return the translated text
                return translatedText;
            }
        }

    }


}
