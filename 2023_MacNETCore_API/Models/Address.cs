using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;

namespace _2023_MacNETCore_API.Models
{
	public class Address
	{
		public Address()
		{
		}

        [Key]
        public int Address_id { get; set; }

        public string? Address_line1 { get; set; }

        public string? Address_line2 { get; set; }

        public string? City { get; set; }

        public char? State { get; set; }

        public string? Country { get; set; }

        public int Zipcode { get; set; }
    }
}

