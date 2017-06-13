using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesDatabase.ModelParsers
{
    public interface IParser
    {
        ICollection<MovieParser> ParseMovies(string path);
        ICollection<PersonParser> ParsePersons(string path);


    }
}
