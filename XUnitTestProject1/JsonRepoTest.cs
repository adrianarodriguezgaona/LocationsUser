using B4.PE3.RodriguezA.Domain.Models;
using B4.PE3.RodriguezA.Domain.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Newtonsoft.Json;
using Xamarin.Essentials;


namespace XUnitTestProject1
{
    public class JsonRepoTest
    {
        LocationUser[] testLocations;

        public JsonRepoTest()
        {
            testLocations = TestData.TestLocationsUser;
        }


        [Fact]
        public void Delete_Returns_DeletedLocationUserList()
        {
            //arrange
            var testLocation = testLocations[0];

            var mockSiteRepo = new Mock<ILocationUserRepository>();
            mockSiteRepo.Setup(repo => repo.DeleteLocationUserList(testLocation.Id))
                .Returns(Task.FromResult(testLocation)); 

            var jsonRepo = new JsonLocationRepository();

            //act
            var deletedLocation = jsonRepo.DeleteLocationUserList(testLocation.Id);

            //assert
            Assert.NotNull(deletedLocation);
            Assert.Equal(testLocation.Id.ToString(), deletedLocation.Id.ToString());
        }

       

    }
}
