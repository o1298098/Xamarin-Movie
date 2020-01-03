using System;
using System.Globalization;
using System.Threading.Tasks;
using movie.Models;
using Newtonsoft.Json;
using Xamarin.Essentials;

namespace movie.Utils
{
    public class ApiHelper
    {
        static string _language =CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;
        static string _apikey = "d7ff494718186ed94ee75cf73c1a3214";
        static HttpRequest _http = new HttpRequest("https://api.themoviedb.org/3");
        public static async Task<VideoListModel> GetNowPlayingMovieAsync(int page = 1)
        {
            VideoListModel model=null;
                string param =
                    $"/movie/now_playing?api_key={_apikey}&page={page}&language={_language}";
                string r = await _http.Request(param);
                if (r != null)
                model =JsonConvert.DeserializeObject<VideoListModel>(r);
                return model;
        }
    }
}
