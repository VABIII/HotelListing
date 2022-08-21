using AutoMapper;
using HotelListing.DTOs.CountryDTO;
using HotelListing.Data;

namespace HotelListing.Configs
{
    public class MapperConfig : Profile  // Make sure the 'Mapper' class is inheriting from the 'Profile' brought in from 'AutoMapper'
    {
        // Create the constructor that allows us to create maps between our data types
        public MapperConfig()
        {
            CreateMap<Country, CreateCountryDTO>().ReverseMap(); // This will create a two  way connection between the data types
            /*   CreateMap<Country, CreateCountryDTO>(); // Creates a one way connection from 'Country' to 'CreateCountryDTO'  */
        }

    }
}









































