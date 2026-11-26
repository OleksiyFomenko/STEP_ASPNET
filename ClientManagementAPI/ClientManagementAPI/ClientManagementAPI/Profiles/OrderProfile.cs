using AutoMapper;
using ClientManagementAPI.DTO.OrderDTO;
using ClientManagementAPI.Model;

namespace ClientManagementAPI.Profiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile() 
        {
            CreateMap<Order, OrderResponseDto>()
    .ForMember(
        dest => dest.ClientName,
        opt => opt.MapFrom(src => src.Client.FullName)
    );
        }
        

    }
}
