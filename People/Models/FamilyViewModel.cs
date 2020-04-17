using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace People.Models
{
    public class FamilyViewModel
    {


        public List<Children> GetChildrens { get; set; }
        public List<Sibling> GetSiblings { get; set; }

        //public string FirstName { get; set; }
        //public string LastName { get; set; }
        //public int Age { get; set; }
        //public string Gender { get; set; }
        //public bool IsAlive { get; set; }

    }
}
