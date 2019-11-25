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
                .Returns(Task.FromResult(testLocation)); //repo returns the "fake deleted" site successfully

            var jsonRepo = new JsonLocationRepository();

            //act
            var deletedLocation = jsonRepo.DeleteLocationUserList(testLocation.Id);

            //assert
            Assert.NotNull(deletedLocation);
            Assert.Equal(testLocation.Id.ToString(), deletedLocation.Id.ToString());
        }

        //[Fact]
        //public void GetAll_Returns_SortedResults()
        //{
        //    //arrange
        //    var mockSiteRepo = new Mock<ISiteRepository>();
        //    mockSiteRepo.Setup(repo => repo.GetSites())
        //        .Returns(testSites); //repo returns unordered list to test

        //    var expectedResults = testSites
        //        .OrderByDescending(s => s.TimesVisited)
        //        .ThenBy(s => s.Name);

        //    var siteService = new SiteService(mockSiteRepo.Object);

        //    //act
        //    var actualResults = siteService.GetAll();


        //    //assert
        //    var expectedSites = expectedResults.ToArray();  //helps iterating over collection
        //    var actualSites = actualResults.ToArray();      //helps iterating over collection

        //    // -> resulting sites should be ordered by number of visits first
        //    for (int i = 0; i < expectedSites.Length; i++)
        //    {
        //        Assert.Equal(expectedSites[i].Id, actualSites[i].Id);
        //    }
        //}


        //[Fact]
        //public void Save_PrefixesScheme_When_UrlHasNoScheme()
        //{
        //    //arrange
        //    var testSite = TestData.InvalidPrefixedSites[0];
        //    var mockSiteRepo = new Mock<ISiteRepository>();

        //    //configure fake AddSite method
        //    mockSiteRepo
        //        .Setup(e => e.AddSite(It.IsAny<Site>()))
        //        .Returns<Site>(inputSite => inputSite); //returns same site as input (It)
        //    //configure fake UpdateSite method
        //    mockSiteRepo
        //        .Setup(e => e.UpdateSite(It.IsAny<Site>()))
        //        .Returns<Site>(inputSite => inputSite); //returns same site as input (It)

        //    var siteService = new SiteService(mockSiteRepo.Object);

        //    //act
        //    var result = siteService.Save(testSite);

        //    //assert
        //    Assert.StartsWith("https://", result.Url);
        //}

        //[Fact]
        //public void Save_Updates_When_SiteIdExists()
        //{
        //    //arrange
        //    var mockSiteRepo = new Mock<ISiteRepository>();
        //    mockSiteRepo
        //        .Setup(repo => repo.GetSite(It.IsAny<Guid>()))
        //        .Returns<Guid>(id => testSites.FirstOrDefault(e => e.Id == id)); //repo returns unordered list to test
        //    mockSiteRepo
        //        .Setup(e => e.AddSite(It.IsAny<Site>()))
        //        .Returns<Site>(inputSite => inputSite); //returns same site as input (It)
        //    mockSiteRepo
        //        .Setup(e => e.UpdateSite(It.IsAny<Site>()))
        //        .Returns<Site>(inputSite => inputSite); //returns same site as input (It)

        //    var siteService = new SiteService(mockSiteRepo.Object);

        //    var siteToUpdate = new Site
        //    {
        //        Id = Guid.Parse("00000000-0000-0000-0000-000000000003"), //existing Id
        //        Name = "Updated Site",
        //        Rating = 5,
        //        TimesVisited = 0,
        //        Url = "https://www.github.com"
        //    };

        //    //act
        //    var updatedSite = siteService.Save(siteToUpdate);

        //    //assert
        //    mockSiteRepo.Verify(m => m.UpdateSite(siteToUpdate), Times.AtLeastOnce());
        //}

        //[Fact]
        //public void Save_Inserts_When_SiteIdDoesntExist()
        //{
        //    //arrange
        //    var mockSiteRepo = new Mock<ISiteRepository>();
        //    mockSiteRepo
        //        .Setup(repo => repo.GetSite(It.IsAny<Guid>()))
        //        .Returns<Guid>(id => testSites.FirstOrDefault(e => e.Id == id)); //repo returns unordered list to test
        //    mockSiteRepo
        //        .Setup(e => e.AddSite(It.IsAny<Site>()))
        //        .Returns<Site>(inputSite => inputSite); //returns same site as input (It)
        //    mockSiteRepo
        //        .Setup(e => e.UpdateSite(It.IsAny<Site>()))
        //        .Returns<Site>(inputSite => inputSite); //returns same site as input (It)

        //    var siteService = new SiteService(mockSiteRepo.Object);

        //    var siteToInsert = new Site
        //    {
        //        Id = Guid.Parse("00000000-0000-0000-0000-000000000004"), //non-existing Id
        //        Name = "Inserted Site",
        //        Rating = 5,
        //        TimesVisited = 0,
        //        Url = "https://www.github.com"
        //    };

        //    //act
        //    var insertedSite = siteService.Save(siteToInsert);

        //    //assert
        //    mockSiteRepo.Verify(m => m.AddSite(siteToInsert), Times.AtLeastOnce());
        //}


        //[Fact]
        //public void Save_Throws_ValidationException_When_NameNotEmpty()
        //{
        //    //arrange
        //    var siteService = new SiteService(null);
        //    var invalidSite = new Site
        //    {
        //        Id = Guid.NewGuid(),
        //        Name = null,
        //        Rating = 2,
        //        TimesVisited = 0,
        //        Url = "https://www.github.com"
        //    };

        //    //act
        //    var saveAction = new Action(() => { siteService.Save(invalidSite); });

        //    //assert
        //    Assert.Throws<ValidationException>(saveAction);
        //}

    }
}
