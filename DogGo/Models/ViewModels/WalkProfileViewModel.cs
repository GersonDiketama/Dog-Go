using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DogGo.Models.ViewModels
{
    public class WalkProfileViewModel
    {
        public Walker Walker { get; set; }
        public Walk Walk { get; set; }
        public List<Dog> dogs { get; set; }

    }
}
