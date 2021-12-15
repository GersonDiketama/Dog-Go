using DogGo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DogGo.Repositories
{
  public interface IDogRepository
    {
        void AddDog(Dog dog);
        void DeleteDog(int dogId);
        List<Dog> GetAllDogs();
        Dog GetDogByEmail(string email);
        Dog GetDogsById(int id);
        void UpdateDog(Dog dog);

        List<Dog> GetDogsByOwnerId(int ownerId);
    }
}
