using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Xamarin.Forms;

using TheMovies.Services;
using TheMovies.Model;
using TheMovies.ViewModel;

using static TheMovies.Helpers.Helpers;


namespace TheMovies.View
{
    public partial class MoviesPage : ContentPage
    {
        ObservableCollection<MovieModel> movieManagedList = new ObservableCollection<MovieModel>();
        int intPageDownloaded = 0;

        private string url_groupCategory = "";
        private string v_params = "";

        public MoviesPage(GroupCategory groupCategory)
        {
            InitializeComponent();
            this.lbl_movie_category.Text = "";
            
            // Invoke API to get the genres
            LoadGenres();

            switch (groupCategory)
            {
                case GroupCategory.LATEST:
                    url_groupCategory = "latest";
                    v_params = "language=en-US";
                    this.lbl_movie_category.Text = "The latest movies";
                    break;
                case GroupCategory.NOW_PLAYING:
                    url_groupCategory = "now_playing";
                    v_params = "language=en-US&page=1";
                    this.lbl_movie_category.Text = "Now playing movies";
                    break;
                case GroupCategory.POPULAR:
                    url_groupCategory = "popular";
                    v_params = "language=en-US&page=1";
                    this.lbl_movie_category.Text = "The popular movies";
                    break;
                case GroupCategory.UPCOMING:
                    url_groupCategory = "upcoming";
                    v_params = "language=en-US&page=1";
                    this.lbl_movie_category.Text = "Upcoming movies";
                    break;
            }

            InvokeAPI("movie", url_groupCategory, v_params);

        }

        public void LoadGenres()
        {
            APICall apiCall = new APICall();

            apiCall.GetResponse<GenreRootObject>("genre", "movie/list", "language=en-US").ContinueWith(t =>
            {

                //Check possible problems during request
                if (t.IsFaulted)
                {
                    Debug.WriteLine(t.Exception.Message);
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        DisplayAlert("Failed", "Problems happened during API request", "Ok");
                    });
                }
                //Check whether request has been cancelled for unknown reason
                else if (t.IsCanceled)
                {
                    Debug.WriteLine("Request cancelled");

                    Device.BeginInvokeOnMainThread(() =>
                    {
                        DisplayAlert("Aborted", "Request aborted", "Ok");
                    });
                }
                //No problem? Continue...
                else
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        GenreRootObject rootObject = t.Result;
                        foreach (GenreModel genre in rootObject.genres)
                        {
                            GenreViewModel.genresLookup.Add(genre.id, genre.name);
                        }
                    });
                }
            });
        }

        public void InvokeAPI(string target, string method, string param)
        {
            APICall apiCall = new APICall();

            apiCall.GetResponse<MovieRootObject>(target, method, param).ContinueWith(t =>
            {

                //Check possible problems during request
                if (t.IsFaulted)
                {
                    Debug.WriteLine(t.Exception.Message);
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        DisplayAlert("Failed", "Problems happened during API request", "Ok");
                    });
                }
                //Check whether request has been cancelled for unknown reason
                else if (t.IsCanceled)
                {
                    Debug.WriteLine("Request cancelled");

                    Device.BeginInvokeOnMainThread(() =>
                    {
                        DisplayAlert("Aborted", "Request aborted", "Ok");
                    });
                }
                //No problem? Continue...
                else
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        MovieRootObject rootObject = t.Result;

                        foreach (MovieModel item in rootObject.results)
                        {
                            movieManagedList.Add(item);
                        }

                        ListMovies.ItemsSource = movieManagedList;
                        intPageDownloaded = rootObject.page;
                    });
                }
            });
        }

        private void ListMovies_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            MovieModel movie = e.Item as MovieModel;
            Navigation.PushAsync(new MovieDetailsPage(movie));
        }

        private void ListMovies_ItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
            MovieModel itemVisible = e.Item as MovieModel;

            int intCount = movieManagedList.Count;

            if (itemVisible.id == movieManagedList[intCount-1].id)
            {
                v_params = "language=en-US&page={0}";
                InvokeAPI("movie", url_groupCategory, string.Format(v_params, intPageDownloaded+1));
            }            
        }
    }
}