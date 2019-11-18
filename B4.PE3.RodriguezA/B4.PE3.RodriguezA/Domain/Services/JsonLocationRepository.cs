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
        private readonly JsonSerializerSettings _serializerSettings;
        public JsonLocationRepository()
        {
            _filePath = Path.Combine(FileSystem.AppDataDirectory, "locationUserLists.json");
            _serializerSettings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
        }
        public async Task<LocationUser> AddLocationUserList(LocationUser location)
        {
            var locationsUserlists = await GetAllLocationUserlists();
            locationsUserlists.Add(location);
            SaveLocationsUserToJsonFile(locationsUserlists);
            return await GetLocationUserList(location.Id);
        }

        public async Task<LocationUser> DeleteLocationUserList(Guid id)
        {
            var locationUserlists = await GetAllLocationUserlists();
            var locationUserlistToRemove = locationUserlists.FirstOrDefault(e => e.Id == id);
            locationUserlists.Remove(locationUserlistToRemove);
            SaveLocationsUserToJsonFile(locationUserlists);
            return locationUserlistToRemove;
        }

        public async Task<LocationUser> GetLocationUserList(Guid id)
        {
            var locationUserLists = await GetAllLocationUserlists();
            return locationUserLists.FirstOrDefault(e => e.Id == id);
        }


        public async Task<LocationUser> UpdateLocationUserList(LocationUser location)
        {
            await DeleteLocationUserList(location.Id);
            return await AddLocationUserList(location);
        }

        public async Task<IQueryable<LocationUser>> GetMyLocationLists()
        {
            var LocationUserLists = await GetAllLocationUserlists();
            return LocationUserLists.AsQueryable();
        }
        protected async Task<IList<LocationUser>> GetAllLocationUserlists()
        {
            try
            {
                string locationUserListsJson = File.ReadAllText(_filePath);
                var locationUserLists = JsonConvert.DeserializeObject<IList<LocationUser>>(locationUserListsJson);
                return await Task.FromResult(locationUserLists);  //using Task.FromResult to have atleast one await in this async method
            }
            catch  //return empty collection on file not found, deserialization error, ...
            {
                return (new List<LocationUser>());
            }
        }

        protected void SaveLocationsUserToJsonFile(IEnumerable<LocationUser> locationsUser)
        {
            string locationsUserJson = JsonConvert.SerializeObject(locationsUser, Formatting.Indented, _serializerSettings);
            File.WriteAllText(_filePath, locationsUserJson);
        }

    }
}
