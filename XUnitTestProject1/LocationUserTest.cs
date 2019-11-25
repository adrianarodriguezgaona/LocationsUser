using B4.PE3.RodriguezA.Domain.Models;
using B4.PE3.RodriguezA.Domain.Services;
using B4.PE3.RodriguezA.ViewModels;
using Moq;
using FreshMvvm;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace XUnitTestProject1
{
    public class LocationUserTest
    {
        LocationUser[] locationsUsertest;
        LocationItem[] locationItems;

        public LocationUserTest()
        {
            locationsUsertest = TestData.TestLocationsUser;
            locationItems = TestData.TestLocationItems;
        }

        [Fact]
        public void LocationUserViewModel_ReceivesLocationUser_OnInit()
        {
            //Arrange
            var locationUser = locationsUsertest[0];

            var mockService = new Mock<ILocationUserRepository>();
            mockService.Setup(repo => repo.GetLocationUserList(locationUser.Id))
                .Returns(Task.FromResult(locationUser));

            //Act
            var viewmodel = new LocationUserViewModel(mockService.Object);
          
            if (viewmodel.GetType().GetMethod("Init") != null)
                viewmodel.Init(locationUser);

            //Assert
            Assert.NotNull(viewmodel.Name);
            Assert.Equal(viewmodel.Name, locationsUsertest[0].Name);
        }


        //[Fact]
        //public void LocationUserViewModel_GetALocationItem_OnReverseInit()
        //{
        //    //Arrange
        //    var locationItem = locationItems[0];
        //    locationItem.ParentLocation.Items.Add(locationItem);
        //    var mockService = new Mock<ILocationUserRepository>();
        //    //mockService.Setup(repo => repo.GetLocationUserList(locationItem.Id))
        //    //    .Returns(Task.FromResult(locationItem));

        //    //Act
        //    var viewmodel = new LocationUserViewModel(mockService.Object);

        //    if (viewmodel.GetType().GetMethod("ReverseInit") != null)
        //        viewmodel.ReverseInit(locationItem);

        //    //Assert
        //    //Assert.NotNull(viewmodel.LocationItems);
        //    Assert.Equal(viewmodel.LocationItems[0].ItemName, locationItem.ItemName);
        //}

        //[Fact]

        //public void LocationUserViewModel_SaveLocationUserState()
        //{
        //    //Arrange

        //    var newLocationUserTest = locationsUsertest[0];
        //    var mockService = new Mock<ILocationUserRepository>();
        //    mockService.Setup(repo => repo.AddLocationUserList(newLocationUserTest))
        //       .Returns(Task.FromResult(newLocationUserTest));
        //    //Act
        //    var viewModel = new LocationUserViewModel(mockService.Object);

        //    viewModel.SaveLocationUserCommand.Execute(null);

        //    Assert.NotNull(viewModel.Name);
        //    Assert.Equal(viewModel.Name, newLocationUserTest.Name);
        //}





    }
}