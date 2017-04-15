using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using TheMovies.Model;
using TheMovies.ViewModel;

namespace TheMovies.View
{
    public partial class MovieDetailsPage : ContentPage
    {
        public MovieDetailsPage(MovieModel movie)
        {
            InitializeComponent();
            BindingContext = new MovieDetailsViewModel(movie);
        }
    }
}
