using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DogGo.Controllers;
using DogGo.Repositories;
using DogGo.Models;

namespace DogGo.Repositories
{
   public interface IwalkRepository
    {
        public List<Walk> GetWalksByWalkerId(int id);
    }
}
