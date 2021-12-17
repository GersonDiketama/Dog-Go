using DogGo.Models;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;

namespace DogGo.Repositories
{
    public interface IWalkerRepository
    {
        void AddWalk(Walk walks);
        List<Walker> GetAllWalkers();
        Walker GetWalkerById(int id);

        List<Walker> GetWalkersInNeighborhood(int neighborhoodId);

        
    }
}