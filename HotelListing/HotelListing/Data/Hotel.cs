using System.ComponentModel.DataAnnotations.Schema;

namespace HotelListing.Data
{
    public class Hotel
    {
        // Auto incrementing primary key / 
        public int Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public double Rating { get; set; }


        // Identifies db columns that will house foreign keys pointing towards other tables in the database - 
        // Using nameof() will help keep your FK strictly typed or you can pass it a 'Magic String' which will do the same thing
        // though this method is more error prone
        //[ForeignKey("CountryId")]
        [ForeignKey(nameof(CountryId))]

        // Even though, in the Country class, it's id is declared as 'Id', the entity framework will automatically connect it to
        // 'CountryId' since it contains the class name of the id
        public int CountryId { get; set; }

        // This is declaring the table that will be associated with the foreign key
        public Country Country { get; set; }


    }

    // From here move to the database context and let it know about the 'Hotel' and 'Country' classes
}