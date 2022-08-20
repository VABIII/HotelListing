namespace HotelListing.Data
{
    public class Country
    {
        // Step 1: Create auto incrementing id
        public int Id { get; set; }

        // Step 2: Create whichever additional columns you may require
        public string Name { get; set; }
        public string ShortName { get; set; }

        // Since the country will have a one-to-many relationship with the hotels, meaning one country will have many hotels,
        // this will create a list that contains all the hotels associated to a paticular hotel
        public virtual ICollection<Hotel> Hotels { get; set; }


    }
}