using ApartmentRentingSystem.Models.Apartments;
using ApartmentRentingSystem.Models.Home;
using AutoMapper;

namespace ApartmentRentingSystem.Infrastructure
{
    public class MappingProfile : Profile
    {

        public MappingProfile()
        {
            this.CreateMap<ApartmentDetailsModel, ApartmentFormModel>();

            this.CreateMap<ApartmentFormModel, Apartment>();

            this.CreateMap<Apartment, ApartmentIndexViewModel>();

            this.CreateMap<Apartment, ApartmentListingViewModel>()
                .ForMember(a => a.CategoryName,
                    config => config.MapFrom(a => a.Category.Name));
                ;




            //Sets the Automapper to map specific property from one object to another
            this.CreateMap<Apartment, ApartmentDetailsModel>()
                .ForMember(a => a.UserId,
                    config => config.MapFrom(a => a.Broker.UserId))
                .ForMember(c => c.CategoryName,
                    config => config.MapFrom(c => c.Category.Name));

        }
    }
}