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


        //MyTestedASPNETCoreMVC tests
        [Fact]
        public void IndexRouteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/")
                .To<HomeController>(i => i.Index());

        [Fact]
        public void IndexShouldReturnViewWithCorrectModelAndDataTestsAlsoTheRoute()
            => MyMvc
                .Pipeline()
                .ShouldMap("/")
                .To<HomeController>(x => x.Index())
                .Which(controller => controller.WithData(GetApartments()))
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<IndexViewModel>()
                    .Passing(m => m.Apartments.Should().HaveCount(3)));



        //regular XUnit test + FluentAssertion
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

        [Fact]
        public void IndexErrorRouteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Home/Error")
                .To<HomeController>(e => e.Error());

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

        private static IEnumerable<Apartment> GetApartments()
        {
            return Enumerable.Range(0, 10).Select(i => new Apartment
            {
                IsPublic = true
            });
        }
    }
}