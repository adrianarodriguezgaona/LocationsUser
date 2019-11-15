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
        bool DatastoreExists { get; }

        Task<IQueryable<LocationUser>> GetAll();

        Task<LocationUser> GetLocationUser(int id);

        Task<LocationUser> UpdateLocationUser(LocationUser location);

        Task<LocationUser> AddLocationUser(LocationUser location);

        Task<LocationUser> DeleteLocationUser(int id);
    }
}
