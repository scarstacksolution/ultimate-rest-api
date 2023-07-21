using System;
using _2023_MacNETCore_API.Interfaces;
using _2023_MacNETCore_API.Models;
using Microsoft.EntityFrameworkCore;

namespace _2023_MacNETCore_API.Repositories
{
	public class WritePattern_Repository : IWritePattern_Repository
    {
        // Field Properties

        private readonly ApplicationDBContext _context;


        // Constuctor

        public WritePattern_Repository(ApplicationDBContext context)
        {
            _context = context;
        }


        // Methods

        /// <summary>
        /// Posts New employee detail to the NewEmployee DB Table
        /// </summary>
        /// <returns></returns>
        public void PostNewEmployeeInformation(NewEmployees employee)
        {
                _context.NewEmployees.Add(employee);
                _context.SaveChanges();
        }


        /// <summary>
        /// Posts New User to the LoginModel Database Table
        /// </summary>
        /// <param name="user"></param>
        public void PostNewUserLoginDetail(LoginModel user)
        {
            _context.LoginModel.Add(user);
            _context.SaveChanges();
        }


    }
}

