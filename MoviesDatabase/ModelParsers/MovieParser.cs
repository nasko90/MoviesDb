using MoviesDatabase.Models.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MoviesDatabase
{
    public class MovieParser 
    {       
        public string Actors { get; set; }     

        public string BoxOffice { get; set; }

        [JsonProperty("Country")]
        public string Countries { get; set; }

        public string Director { get; set; }

        [JsonProperty("Runtime")]
        public string Duration { get; set; }

        [JsonProperty("Genre")]
        public string Genres { get; set; }

        [JsonProperty("imdbRating")]
        public double ImdbRating { get; set; }


        public string Plot { get; set; }

        [JsonProperty("Released")]
        public DateTime ReleaseDate { get; set; }

        public string Title { get; set; }

    }
}
