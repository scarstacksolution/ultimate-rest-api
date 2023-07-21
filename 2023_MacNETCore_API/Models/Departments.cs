using System;
using System.ComponentModel.DataAnnotations;

namespace _2023_MacNETCore_API.Models
{
	public class Departments
	{
		public Departments()
		{
		}

        [Key]
        public int? Dept_id { get; set; }

        public string? Dept_name { get; set; }
    }
}

