using System;
using System.ComponentModel.DataAnnotations;

namespace _2023_MacNETCore_API.Models
{
	public class JwtClients
	{
		public JwtClients()
		{
		}

		[Key]
		public int? id { get; set; }

		public string? username { get; set; }

		public string? password { get; set; }

		public string? clientname { get; set; }

		public string? role { get; set; }
	}
}

