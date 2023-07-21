using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace _2023_MacNETCore_API.Models
{
	public class Jobs
	{
		public Jobs()
		{
		}

        [Key]
        public int? Job_id { get; set; }

        public string? Job_Title { get; set; }

        public int? Dept_id { get; set; }

        public int? Job_Rank { get; set; }
    }
}

