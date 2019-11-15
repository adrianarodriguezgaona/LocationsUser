using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using B4.PE3.RodriguezA.Domain.Models;
using Newtonsoft.Json;
using Xamarin.Essentials;

namespace B4.PE3.RodriguezA.Domain.Services
{
    public class JsonLocationRepository : ILocationUserRepository
    {
        private readonly string _filePath;
        public JsonLocationRepository()
        {
            _filePath = Path.Combine(FileSystem.AppDataDirectory, "locationsUser.json");
        }
        public bool DatastoreExists => File.Exists(_filePath);

        public async Task<LocationUser> AddLocationUser(LocationUser location)
        {
            var locationsUser = (await GetAll()).ToList();
            locationsUser.Add(location);
            SaveLocationsUserToFile(locationsUser);
            return await GetLocationUser(location.Id);
        }

        private void SaveLocationsUserToFile(IEnumerable<LocationUser> locationsUser)
        {
            string sitesJson = JsonConvert.SerializeObject(locationsUser);
            File.WriteAllText(_filePath, sitesJson);
        }

        public async Task<LocationUser> DeleteLocationUser(int id)
        {
            var locationsUser = (await GetAll()).ToList();
            var locationToRemove = locationsUser.FirstOrDefault(l => l.Id == id);
            locationsUser.Remove(locationToRemove);
            SaveLocationsUserToFile(locationsUser);
            return locationToRemove;
        }

        public async Task<IQueryable<LocationUser>>GetAll()
        {
            try
            {
                string locationsUserJson = File.ReadAllText(_filePath);
                var locationsUser = JsonConvert.DeserializeObject<IEnumerable<LocationUser>>(locationsUserJson);
                var locationsUserQuery = locationsUser.AsQueryable();
                return await Task.FromResult(locationsUserQuery);
            }
            catch
            {
                return (new List<LocationUser>()).AsQueryable();
            }
        }

        public async Task<LocationUser> GetLocationUser(int id)
        {
            var locationsUser = await GetAll();
            return locationsUser.FirstOrDefault(l => l.Id == id);
        }

        public async Task<LocationUser> UpdateLocationUser(LocationUser location)
        {
            await DeleteLocationUser(location.Id);
            return await AddLocationUser(location);
        }


    }
}
