using System;
using System.ComponentModel.DataAnnotations;

namespace _2023_MacNETCore_API.Models
{
	public class Worklocation
	{
		public Worklocation()
		{
		}

        [Key]
        public int Loc_id { get; set; }

        public string? Address { get; set; }

        public string? Address_line2 { get; set; }

        public string? City { get; set; }

        public char? State { get; set; }

        public string? Country { get; set; }

        public int Zipcode { get; set; }
    }
}

