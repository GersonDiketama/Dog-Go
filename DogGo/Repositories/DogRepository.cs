using System;
using DogGo.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;


namespace DogGo.Repositories
{
    public class DogRepository : IDogRepository
    {
        private readonly IConfiguration _config;

        // The constructor accepts an IConfiguration object as a parameter. This class comes from the ASP.NET framework and is useful for retrieving things out of the appsettings.json file like connection strings.
        public DogRepository(IConfiguration config)
        {
            _config = config;
        }

        public SqlConnection Connection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            }
        }

        public List<Dog> GetAllDogs()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT Id, Name, OwnerId, Breed, Notes, ImageUrl
                        FROM Dog
                       
                    ";

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        List<Dog> dogs = new List<Dog>();
                        while (reader.Read())
                        {
                            Dog dog = new Dog
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Name = reader.GetString(reader.GetOrdinal("Name")),
                                OwnerId = reader.GetInt32(reader.GetOrdinal("OwnerId")),
                                Owner = new Owner
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                    Name = reader.GetString(reader.GetOrdinal("Name"))
                                },
                                Breed = reader.GetString(reader.GetOrdinal("Breed")),
                            
                                Notes = reader.IsDBNull(reader.GetOrdinal("Notes")) ? null : reader.GetString(reader.GetOrdinal("Notes")),

                                ImageUrl = reader.IsDBNull(reader.GetOrdinal("ImageUrl")) ? null : reader.GetString(reader.GetOrdinal("ImageUrl"))


                            };

                            dogs.Add(dog);
                        }

                        return dogs;
                    }
                }
            }
        }

        public Dog GetDogsById(int id)
        {

            Dog dog = null;
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"select o.*, d.Id DogID, d.Name DogName, d.Breed Breed, d.Notes, d.ImageUrl, o.Id as dogId, o.Name as dogName, o.NeighborhoodId, n.Name as neighborhoodName, Address, Email, Phone
                                        From dog o
                                        left join Dog d on d.DogId = o.Id
                                        join Neighborhood n on n.Id = o.NeighborhoodId
                                        where o.Id =  @id";



                    cmd.Parameters.AddWithValue("@id", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (dog == null)
                            {
                                dog = new Dog
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("dogId")),
                                    Name = reader.GetString(reader.GetOrdinal("dogName")),
                                    OwnerId = reader.GetInt32(reader.GetOrdinal("NeighborhoodId")),
                                    Owner = new Owner
                                    {
                                        Id = reader.GetInt32(reader.GetOrdinal("neighborhoodName")),
                                        Name = reader.GetString(reader.GetOrdinal("neighborhoodName"))
                                    },
                                    Breed = reader.GetString(reader.GetOrdinal("Address")),
                                    Notes = reader.IsDBNull(reader.GetOrdinal("Notes")) ? null : reader.GetString(reader.GetOrdinal("Notes")),

                                    ImageUrl = reader.IsDBNull(reader.GetOrdinal("ImageUrl")) ? null : reader.GetString(reader.GetOrdinal("ImageUrl")),

                                    owner = new List<Owner>()

                               
                                    
                                };


                            }
                            if (!reader.IsDBNull(reader.GetOrdinal("DogId")))
                            {
                                dog.owner.Add(new Owner
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("DogId")),
                                    Name = reader.GetString(reader.GetOrdinal("DogName")),
                                    
                                });
                            }
                        }
                        return dog;
                    }

                }
            }
        }


        public Dog GetDogByEmail(string email)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT Id, [Name], Email, Address, Phone, NeighborhoodId
                        FROM Dog
                        WHERE Email = @email";

                    cmd.Parameters.AddWithValue("@email", email);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Dog dog = new Dog()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Name = reader.GetString(reader.GetOrdinal("Name")),
                                OwnerId = reader.GetInt32(reader.GetOrdinal("Email")),
                                Owner = new Owner
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("neighborhoodName")),
                                    Name = reader.GetString(reader.GetOrdinal("neighborhoodName"))
                                },
                                Breed = reader.GetString(reader.GetOrdinal("Phone")),
                                Notes = reader.GetString(reader.GetOrdinal("NeighborhoodId")),
                                ImageUrl = reader.GetString(reader.GetOrdinal("neighborhoodName"))
                            };

                            return dog;
                        }

                        return null;
                    }
                }
            }
        }

        public void AddDog(Dog dog)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    INSERT INTO Dog ([Name], Email, Phone, Address, NeighborhoodId)
                    OUTPUT INSERTED.ID
                    VALUES (@name, @email, @phoneNumber, @address, @neighborhoodId);
                ";

                    cmd.Parameters.AddWithValue("@name", dog.Name);
                    cmd.Parameters.AddWithValue("@email", dog.OwnerId);
                    cmd.Parameters.AddWithValue("@phoneNumber", dog.Breed);
                    cmd.Parameters.AddWithValue("@address", dog.Notes);
                    cmd.Parameters.AddWithValue("@neighborhoodId", dog.ImageUrl);

                    int id = (int)cmd.ExecuteScalar();

                    dog.Id = id;
                }
            }
        }

        public void UpdateDog(Dog dog)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                            UPDATE Dog
                            SET 
                                [Name] = @name, 
                                Email = @email, 
                                Address = @address, 
                                Phone = @phone, 
                                NeighborhoodId = @neighborhoodId
                            WHERE Id = @id";

                    cmd.Parameters.AddWithValue("@name", dog.Name);
                    cmd.Parameters.AddWithValue("@email", dog.OwnerId);
                    cmd.Parameters.AddWithValue("@address", dog.Breed);
                    cmd.Parameters.AddWithValue("@phone", dog.Notes);
                    cmd.Parameters.AddWithValue("@neighborhoodId", dog.ImageUrl);
                    cmd.Parameters.AddWithValue("@id", dog.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteDog(int dogId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                            DELETE FROM Dog
                            WHERE Id = @id
                        ";

                    cmd.Parameters.AddWithValue("@id", dogId);

                    cmd.ExecuteNonQuery();
                }
            }
        }




        public List<Dog> GetDogsByOwnerId(int ownerId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                SELECT Id, Name, Breed, Notes, ImageUrl, OwnerId 
                FROM Dog
                WHERE OwnerId = @ownerId
            ";

                    cmd.Parameters.AddWithValue("@ownerId", ownerId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {

                        List<Dog> dogs = new List<Dog>();

                        while (reader.Read())
                        {
                            Dog dog = new Dog()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Name = reader.GetString(reader.GetOrdinal("Name")),
                                Breed = reader.GetString(reader.GetOrdinal("Breed")),
                                OwnerId = reader.GetInt32(reader.GetOrdinal("OwnerId"))
                            };

                            // Check if optional columns are null
                            if (reader.IsDBNull(reader.GetOrdinal("Notes")) == false)
                            {
                                dog.Notes = reader.GetString(reader.GetOrdinal("Notes"));
                            }
                            if (reader.IsDBNull(reader.GetOrdinal("ImageUrl")) == false)
                            {
                                dog.ImageUrl = reader.GetString(reader.GetOrdinal("ImageUrl"));
                            }

                            dogs.Add(dog);
                        }

                        return dogs;
                    }
                }
            }
        }




    }
}