using System.Linq;
using ApartmentRentingSystem.Controllers;
using ApartmentRentingSystem.Data.Models;
using ApartmentRentingSystem.Models.Broker;
using ApartmentRentingSystem.Utilities;
using MyTested.AspNetCore.Mvc;
using Xunit;

namespace ApartmentRentingSystemTests.Controllers
{
    public class BrokerControllerTest
    {
       
        [Fact]
        public void GetCreateRouteTest()
            => MyRouting
                .Configuration()
                .ShouldMap("/Broker/Create")
                .To<BrokerController>(b => b.Create());

        [Fact]
        public void PostCreateRouteTest()
            => MyRouting
                .Configuration()
                .ShouldMap(request => request.WithPath("/Broker/Create").WithMethod(HttpMethod.Post))
                .To<BrokerController>(b => b.Create(With.Any<BecomeBrokerFormModel>()));

        [Fact]
        public void CreateActionShouldBeForAuthorizedUsersAndReturnView()
            => MyController<BrokerController>
                .Instance()
                .Calling(b => b.Create())
                .ShouldHave()
                .ActionAttributes(attributes => attributes.RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .View();

        [Theory]
        [InlineData("Broker","+358451777777")]
        public void PostCreateShouldBeForAuthorizedUsersAndReturnRedirectWithCorrectModelAndSavesItInDatabase(string brokerName, string brokerPhone)
            => MyController<BrokerController>
                .Instance(controller => controller.WithUser())
                .Calling(a => a.Create(new BecomeBrokerFormModel
                {
                    Name = brokerName,
                    PhoneNumber = brokerPhone

                }))
                .ShouldHave()
                .ActionAttributes(attributes => attributes.RestrictingForHttpMethod(HttpMethod.Post).RestrictingForAuthorizedRequests())
                .ValidModelState()
                .Data(data=> 
                    data.WithSet<Broker>(brokers => brokers.Any(b =>
                        b.Name == brokerName &&
                        b.PhoneNumber == brokerPhone &&
                        b.UserId == TestUser.Identifier)))
                .TempData(tempData => tempData
                    .ContainingEntryWithKey(Constants.WebConstants.GlobalMessageKey))
                .AndAlso()
                .ShouldReturn()
                .RedirectToAction("All", "Apartment");

        [Fact]
        public void CreateActionShouldBeForAuthorizedUsersAndReturnViewAlsoTestsTheRouting()
            => MyMvc
                .Pipeline()
                .ShouldMap(request => request.WithPath("/Broker/Create").WithUser())
                .To<BrokerController>(b => b.Create())
                .Which()
                .ShouldHave()
                .ActionAttributes(attributes => attributes.RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .View();
    }
}