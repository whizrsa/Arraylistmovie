using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arraylistmovie
{
    class Movie
    {
        public string Title { get; set; }
        public string Director { get; set; }
        public string ReleaseYear { get; set; }
        public string Genre { get; set; }

        public Movie(string title,string director,string releaseyear,string genre)
        {
            Title = title;
            Director = director;
            ReleaseYear = releaseyear;
            Genre = genre;
        }

        public override string ToString()
        {
            return $"{Title} ({ReleaseYear}), directed by {Director}, Genre: {Genre}";
        }

    }
}
