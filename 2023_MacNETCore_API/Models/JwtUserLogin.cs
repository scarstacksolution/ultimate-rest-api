using System;
namespace _2023_MacNETCore_API.Models
{
	public class JwtUserLogin
	{
		public JwtUserLogin()
		{
		}

        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Role { get; set; }
    }
}

