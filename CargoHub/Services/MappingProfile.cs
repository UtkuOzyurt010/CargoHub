using AutoMapper;
using CargoHub.Models;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Source > mapTo > new Object
        CreateMap<Shipment, ReadShipmentDto>();
    }
}