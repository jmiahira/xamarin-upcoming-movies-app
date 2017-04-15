using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheMovies.Model
{
    public class MovieRootObject
    {
        public int page { get; set; }
        public List<MovieModel> results { get; set; }
        public RangeDateMovies dates { get; set; }
        public int total_pages { get; set; }
        public int total_results { get; set; }
    }
}