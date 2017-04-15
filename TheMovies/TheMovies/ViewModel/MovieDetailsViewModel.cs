using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheMovies.Services;
using TheMovies.Model;

namespace TheMovies.ViewModel
{
    public class MovieDetailsViewModel
    {

        public string poster_path_full { get; set; }
        public string original_title { get; set; }
        public string genres_description { get; set; }
        public string release_date_formmated { get; set; }
        public string overview { get; set; }

        public MovieDetailsViewModel(MovieModel movie)
        {
            this.poster_path_full = movie.poster_path_full;
            this.original_title = movie.original_title;
            this.genres_description = movie.genres_description;
            this.release_date_formmated = movie.release_date_formmated;
            this.overview = movie.overview;
        }
    }
}