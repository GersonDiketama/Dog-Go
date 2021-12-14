SELECT w.Id, w.Name, N.[Name] as 'Neighborhood Name', w.ImageUrl, w.NeighborhoodId
                        FROM Walker w
                        LEFT JOIN Neighborhood N on w.NeighborhoodId = N.Id