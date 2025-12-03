using AutoMapper;
using WorkshopMaster.Application.Bookings;
using WorkshopMaster.Application.Customers;
using WorkshopMaster.Application.ServiceTypes;
using WorkshopMaster.Application.Vehicles;
using WorkshopMaster.Domain.Entities;

namespace WorkshopMaster.Application.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Customer, CustomerDto>();
            CreateMap<CreateCustomerDto, Customer>();
            CreateMap<UpdateCustomerDto, Customer>();

            // Vehicles
            CreateMap<Vehicle, VehicleDto>()
                .ForMember(d => d.CustomerName, opt => opt.MapFrom(s => s.Customer.FullName));

            CreateMap<CreateVehicleDto, Vehicle>();
            CreateMap<UpdateVehicleDto, Vehicle>();

            // ServiceTypes
            CreateMap<ServiceType, ServiceTypeDto>();
            CreateMap<CreateServiceTypeDto, ServiceType>();
            CreateMap<UpdateServiceTypeDto, ServiceType>();

            // Bookings
            CreateMap<Booking, BookingDto>()
                .ForMember(d => d.VehicleRegistrationNumber, opt => opt.MapFrom(s => s.Vehicle.RegistrationNumber))
                .ForMember(d => d.VehicleBrand, opt => opt.MapFrom(s => s.Vehicle.Brand))
                .ForMember(d => d.CustomerName, opt => opt.MapFrom(s => s.Vehicle.Customer.FullName))
                .ForMember(d => d.ServiceTypes, opt => opt.MapFrom(
                    s => s.BookingServiceTypes.Select(x => x.ServiceType.Name)));
        }
    }
}
