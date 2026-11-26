using AutoMapper;
using ClientManagementAPI.DTO.ClientDTO;
using ClientManagementAPI.Model;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ClientManagementAPI.Profiles
{
    public class ClientsProfile: Profile
    {
        public ClientsProfile()
        {
            CreateMap<Client, ClientCreateDto>();
            CreateMap<ClientCreateDto, Client>();

            CreateMap<Client, ClientUpdateDto>();

            CreateMap<Client, ClientResponseDto>();
        }
    }
}
