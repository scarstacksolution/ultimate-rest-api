using System;
using System.ComponentModel.DataAnnotations;

namespace _2023_MacNETCore_API.Models
{
	public class LoginModel
	{
		public LoginModel()
		{
		}

        [Key]
        public int User_id { get; set; }

        public string EmailAddress { get; set; } = string.Empty;

        public string Firstname { get; set; } = string.Empty;

        public string Lastname { get; set; } = string.Empty;

        public string Username { get; set; } = string.Empty;

        public string Userpassword { get; set; } = string.Empty;
    }
}

