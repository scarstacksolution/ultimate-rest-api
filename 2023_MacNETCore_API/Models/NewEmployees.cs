using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _2023_MacNETCore_API.Models
{
	public class NewEmployees
	{
		public NewEmployees()
		{
		}

        [Key]
        public int id { get; set; }

        public string? firstname { get; set; }

        public string? lastname { get; set; }

        public int age { get; set; }

        public string? gender { get; set; }

        public DateTime? hire_date { get; set; }

        public Decimal salary { get; set; }

        public string? job_title { get; set; }

        public string? manager_name { get; set; }

        public string? department { get; set; }

        public string? phone { get; set; }

        public string? email { get; set; }

        public int ssn { get; set; }

        public string? address { get; set; }

        public string? address_optional { get; set; }

        public string? city { get; set; }

        public string? state { get; set; }

        public string? country { get; set; }

        public int zipcode { get; set; }

        public string? worklocation_state { get; set; }
    }
}

