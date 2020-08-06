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

        
    }
}
