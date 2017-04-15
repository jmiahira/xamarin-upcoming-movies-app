using Newtonsoft.Json;
using System;
//using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;


namespace TheMovies.Services
{
    class APICall
    {
        public static readonly string TMDbToken = "1f54bd990f1cdfb230adb312546d765d";
        public static readonly string ApiUrl = "https://api.themoviedb.org/3/{0}/";
        //public static readonly string ApiUrl = "https://api.themoviedb.org/3/movie/";
        public static readonly string ApiUrlForImage = "http://image.tmdb.org/t/p/";

        public APICall()
        {

        }

        // ToDo: We can change the 2nd parameter from string to dictionary
        public async Task<T> GetResponse<T>(string target, string method, string param) where T : class
        {
            var client = new System.Net.Http.HttpClient();

            string v_api_key = string.Concat("api_key=", TMDbToken);
            // we are considering that "param" are ready to use only missing the first "&" at the head of string.
            string v_params = string.IsNullOrEmpty(param) ? "" : string.Concat("&", param);
            // Concatenates all parameters before append them to main URL.
            string v_all_parameters = string.Concat("?", v_api_key, v_params);

            // Forcing JSON result for avoiding HTML or XML as results.
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //Preparing URL by joining method and param to ApiUrl string.
            //Also, in order to avoid freezing of device, let's use an asynchronous method and the keyword AWAIT.
            //This way allows main thread be executed and the process resume when the download is finished.
            try
            {
                var response = await client.GetAsync(string.Concat(string.Format(ApiUrl, target), method, v_all_parameters));
                //var response = await client.GetAsync(string.Concat(ApiUrl, method, v_all_parameters));

                //Lê a string retornada
                var JsonResult = response.Content.ReadAsStringAsync().Result;

                if (typeof(T) == typeof(string))
                    return null;

                // Converting the JSON result to a class by using Newtonsoft.Json lib
                var rootobject = JsonConvert.DeserializeObject<T>(JsonResult);

                //fake object to test
                //TheMovies.Model.MovieRootObject teste = new TheMovies.Model.MovieRootObject();
                //return teste as T;

                return rootobject;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                Debug.WriteLine(e.Source);
                Debug.WriteLine(e.StackTrace);
            }

            return null;
        }
    }
}
