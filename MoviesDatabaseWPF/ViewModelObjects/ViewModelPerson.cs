using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoviesDatabase.Models.Models;
using MoviesDatabase.Models.Models.Enums;

namespace MoviesDatabaseWPF.ViewModelObjects
{
    public class ViewModelPerson : IViewable
    {
        public ViewModelPerson(Person person)
        {
            this.Name = person.Name;
            this.DateOfBirth = person.DateOfBirth.ToString();
            this.Nationality = person.Country.Name;
            this.Gender = person.Gender.ToString();
        }

        public ViewModelPerson()
        {
            
        }
        public string Name { get; set; }

        public string DateOfBirth { get; set; }

        public string Nationality { get; set; }

        public string Gender { get; set; }
    }
}
