using System.Collections.Generic;
using System.Linq;
using ApartmentRentingSystem.Controllers;
using ApartmentRentingSystem.Models.Home;
using ApartmentRentingSystemTests.Mocks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using MyTested.AspNetCore.Mvc;
using Xunit;

namespace ApartmentRentingSystemTests.Controllers
{
    public class HomeControllerTest
    {


        //here is used MyTestedASPNETCoreMVC framework by Ivaylo Kenov
        [Fact]
        public void IndexShouldReturnViewWithCorrectModelAndData()
            => MyController<HomeController>
                .Instance(controller => controller.WithData(GetApartments()))
                .Calling(a => a.Index())
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<IndexViewModel>()
                    .Passing(m => m.Apartments.Should().HaveCount(3)));
        

        [Fact]
        public void ErrorShouldReturnView()
        {
            //Arrange
            var homeController = new HomeController(StatisticsServiceMock.InstanceService);

            //Act
            var result = homeController.Error();
            

            //Assert
            Assert.NotNull(result);
           Assert.IsType<ViewResult>(result);


        }

        [Fact]
        public void IndexShouldReturnViewWithCorectStatistics()
        {
            //Arrange
         var homeController = new HomeController(StatisticsServiceMock.InstanceService);
           

            //Act
            var result = homeController.Index();
          

          //  //Assert
          //  Assert.NotNull(result);
          // var viewResult = Assert.IsType<ViewResult>(result);

          // var model = viewResult.Model;

          //var indexViewModel = Assert.IsType<IndexViewModel>(model);
          
          //Assert.Equal(20,indexViewModel.AllApartments);
          //Assert.Equal(15,indexViewModel.AllRents);
          //Assert.Equal(7,indexViewModel.AllUsers);

          //or 

          //Assert with FluentAssertion

          result
              .Should()
              .NotBeNull()
              .And
              .BeAssignableTo<ViewResult>()
              .Which
              .Model
              .As<IndexViewModel>()
              .Invoking(model =>
              {
                  model.AllApartments.Should().Be(20);
                  model.AllRents.Should().Be(15);
                  model.AllUsers.Should().Be(7);
              })
              .Invoke();
        }

        private static IEnumerable<Apartment> GetApartments()
        {
            return Enumerable.Range(0, 10).Select(i => new Apartment());
        }
    }
}