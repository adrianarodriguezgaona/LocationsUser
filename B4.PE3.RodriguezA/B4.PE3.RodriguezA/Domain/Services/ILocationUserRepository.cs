using B4.PE3.RodriguezA.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B4.PE3.RodriguezA.Domain.Services
{
    public interface ILocationUserRepository
    {
      
        Task<LocationUser> GetLocationUserList(Guid id);


        /// <summary>
        /// Updates an existing Bucket instance in the data store
        /// </summary>
        /// <param name="location">The Bucket instance to persist in the data store</param>
        /// <returns>The updated Bucket instance on success</returns>
        Task<LocationUser> UpdateLocationUserList(LocationUser location);

        /// <summary>
        /// Adds a Bucket instance to the data store
        /// </summary>
        /// <param name="location">The Bucket instance to persist in the data store</param>
        /// <returns>The added Bucket instance on success</returns>
        Task<LocationUser> AddLocationUserList(LocationUser location);

        Task<IQueryable<LocationUser>> GetMyLocationLists();

        /// <summary>
        /// Deletes an existing Bucket instance from the data store
        /// </summary>
        /// <param name="id">The id of a Bucket instance</param>
        /// <returns>The deleted Bucket instance</returns>
        Task<LocationUser> DeleteLocationUserList(Guid id);
    }
}


