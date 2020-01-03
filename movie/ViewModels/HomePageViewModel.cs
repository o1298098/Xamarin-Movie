using System;
using System.Threading.Tasks;
using movie.Models;
using movie.Utils;

namespace movie.ViewModels
{
    public class HomePageViewModel:BaseViewModel
    {
        VideoListModel _nowPlayingMovies;
        public HomePageViewModel()
        {
           GetNowPlayMovie();
        }

        private async void GetNowPlayMovie()
        {
            nowPlayingMovies=await ApiHelper.GetNowPlayingMovieAsync();
        }

        public VideoListModel nowPlayingMovies {
            get=>_nowPlayingMovies;
            set => SetProperty<VideoListModel>(ref _nowPlayingMovies,value, "nowPlayingMovies");
        }
    }
}
