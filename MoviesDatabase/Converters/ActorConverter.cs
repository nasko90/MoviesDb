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
    public class ActorConverter 
    {
        private readonly MovieDatabaseContext movieDb;

        public ActorConverter(MovieDatabaseContext movieDb)
        {
            this.movieDb = movieDb;
        }

        public void UpdateActorInfo(PersonParser parsedPerson)
        {
            var actor = this.movieDb.Actors.FirstOrDefault(x => x.Name.Equals(parsedPerson.Name));
            if (!(actor == null))
            {

                actor.Country = AddOrUpdateCountry(GetCountryName(parsedPerson.PlaceOfBirth));
                actor.Gender = parsedPerson.Gender;
                actor.DateOfBirth = parsedPerson.DateOfBirth;
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
            return countryName[countryName.Count -1];
        }

    }
}
