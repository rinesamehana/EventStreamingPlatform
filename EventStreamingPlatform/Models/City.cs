﻿namespace EventStreamingPlatform.Models
{
    public class City
    {
        public int CityId { get; set; }

        public string Name { get; set; }

        public int? CountryId { get; set; }

        public Country Country { get; set; }

        public ICollection<Actor> Actors { get; set; }

        public ICollection<Company> Companies { get; set; }
    }
}
