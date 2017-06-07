using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesDatabase.ModelParsers
{
    public class Parser : IParser
    {
        public ICollection<MovieParser> ParseMovies(string path)
        {
            
           return JsonConvert.DeserializeObject<List<MovieParser>>(File.ReadAllText(path));
        }

        public ICollection<PersonParser> ParsePersons(string path)
        {
            return JsonConvert.DeserializeObject<List<PersonParser>>(File.ReadAllText(path));
        }
    }
}
