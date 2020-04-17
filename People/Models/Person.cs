using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace People.Models
{
    public class Person
    {
        public Person()
        {
            Children = new List<Children>();
            Siblings = new List<Sibling>();
        }

        public int PersonId { get; set; }

        [Display(Name="First Name")]
        public string FirstName { get; set; }

        [Display(Name= "Last Name")]
        public string LastName { get; set; }

        public int Age { get; set; } = 0;
        public string Gender { get; set; }
        public bool IsAlive { get; set; } = true;
        public int SpouseId { get; set; }
        public List<Children> Children { get; set; }
        public List<Sibling> Siblings { get; set; }

    }
}
