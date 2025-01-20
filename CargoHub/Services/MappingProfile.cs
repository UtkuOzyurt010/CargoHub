using AutoMapper;
using CargoHub.Models;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Source > mapTo > new Object
        //Shipment Maps
        CreateMap<Shipment, ReadShipmentDto>();
        
        //Order Maps
        CreateMap<Order, ReadOrderDto>();

        //Transfer Maps
        CreateMap<Transfer, ReadTransferDto>();
    }
}