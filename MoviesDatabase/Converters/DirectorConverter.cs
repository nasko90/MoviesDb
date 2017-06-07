using MoviesDatabase.Context;
using MoviesDatabase.ModelParsers;
using MoviesDatabase.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesDatabase.Converters
{
    public class DirectorConverter
    {
        private readonly MovieDatabaseContext movieDb;

        public DirectorConverter(MovieDatabaseContext movieDb)
        {
            this.movieDb = movieDb;
        }

        public void UpdateDirectorInfo(PersonParser parsedPerson)
        {
            var director = this.movieDb.Directors.FirstOrDefault(x => x.Name.Equals(parsedPerson.Name));
            if (!(director == null))
            {

                director.Country = AddOrUpdateCountry(GetCountryName(parsedPerson.PlaceOfBirth));
                director.Gender = parsedPerson.Gender;
                director.DateOfBirth = parsedPerson.DateOfBirth;
            }

            this.movieDb.SaveChanges();

        }

        private Country AddOrUpdateCountry(string countryName)
        {
            var country = this.movieDb.Countries.FirstOrDefault(x => x.Name.Equals(countryName));
            if (country == null)
            {
                country = new Country { Name = countryName };
            }

            return country;
        }

        private string GetCountryName(string stringToConvert)
        {
            var countryName = stringToConvert.Split(new char[] { ',', '-' }).Select(a => a.Trim()).ToList(); ;
            return countryName[countryName.Count - 1];
        }
    }
}
