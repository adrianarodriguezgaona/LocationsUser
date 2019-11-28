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
using FluentValidation;
using B4.PE3.RodriguezA.Validators;

namespace XUnitTestProject1
{
    public class LocationUserTest
    {
        LocationUser[] locationsUsertest;
        LocationItem[] locationItems;
        IValidator validator;

        public LocationUserTest()
        {
            locationsUsertest = TestData.TestLocationsUser;
            locationItems = TestData.TestLocationItems;
            validator = new LocationUserValidator();
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


        [Fact]
        public void LocationUserValidator_Throws_ValidationException_When_NameIsEmpty()
        {
            //Arrange
            var invalidLocation = locationsUsertest[1];

            //Act
            var results = validator.Validate(invalidLocation);
            var errors = results.Errors;

            //assert

            Assert.NotEmpty(errors);

        }

        [Fact]
        public void LocationUserValidator_Throws_ValidationException_When_NameOutOfRange()
        {
            //Arrange
            var invalidLocation = locationsUsertest[2];

            //Act
            var results = validator.Validate(invalidLocation);
            var errors = results.Errors;

            //assert

            Assert.NotEmpty(errors);

        }



        //[Fact]

        //public void LocationUserViewModel_SaveLocationUserState()
        //{
        //    //Arrange

        //    var newLocationUserTest = locationsUsertest[0];
        //    var mockService = new Mock<ILocationUserRepository>();
           
        //    //Act
        //    var viewModel = new LocationUserViewModel(mockService.Object);

        //    viewModel.SaveLocationUserCommand.Execute(null);
        //    mockService.Setup(repo => repo.AddLocationUserList(newLocationUserTest));
        //      //.Returns(Task.FromResult(newLocationUserTest));

        //    Assert.NotNull(viewModel.Name);
        //    Assert.Equal(viewModel.Name, newLocationUserTest.Name);
        //}





    }
}