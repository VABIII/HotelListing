using System.ComponentModel.DataAnnotations; // Brings in the [Required] annotation




namespace HotelListing.DTOs.CountryDTO
{
    public class CreateCountryDTO
    {

        [Required] // This annotation will make the 'Name' field required for any 
        public string Name { get; set; }
        public string ShortName { get; set; }
    }
}
