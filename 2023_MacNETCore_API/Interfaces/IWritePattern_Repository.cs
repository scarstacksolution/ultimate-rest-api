using System;
using _2023_MacNETCore_API.Models;

namespace _2023_MacNETCore_API.Interfaces
{
	public interface IWritePattern_Repository
	{
       void PostNewEmployeeInformation(NewEmployees employee);
    }
}

