using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheMovies.Services;
using TheMovies.ViewModel;


namespace TheMovies.Model
{
    public class MovieModel
    {
        public string poster_path { get; set; }
        public bool adult { get; set; }
        public string overview { get; set; }
        public DateTime release_date { get; set; }
        public List<int> genre_ids { get; set; }
        public int id { get; set; }
        public string original_title { get; set; }
        public string original_language { get; set; }
        public string title { get; set; }
        public string backdrop_path { get; set; }
        public decimal popularity { get; set; }
        public int vote_count { get; set; }
        public bool video { get; set; }
        public decimal vote_average { get; set; }
        public string poster_path_full => string.Format("{0}{1}{2}", APICall.ApiUrlForImage, "w500",poster_path);
        public string release_date_formmated => String.Format("{0:yyyy/MM/dd}", release_date);
        public string genres_description => LookupGenre();

        public string LookupGenre()
        {
            string strGenreDescription = "";
            foreach (int item in genre_ids)
            {
                if (GenreViewModel.genresLookup.ContainsKey(item))
                {
                    strGenreDescription = strGenreDescription + GenreViewModel.genresLookup[item] + ", ";
                }
            }

            if (strGenreDescription.Length > 0)
            {
                strGenreDescription = strGenreDescription.Substring(0, strGenreDescription.Length - 2);
            }

            return strGenreDescription;
        }
    }
}
