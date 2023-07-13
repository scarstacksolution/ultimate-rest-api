using System;
using _2023_MacNETCore_API.Models;

namespace _2023_MacNETCore_API.Interfaces
{
	public interface IReadPattern_Repository
    {
        Employees GetEmployeeById(int id);
        IEnumerable<Employees> GetAllEmployees();
        IEnumerable<Managers> GetAllManagers();
        Managers GetManagerbyId(int id);
        IEnumerable<Jobs> GetAllJobs();
        IEnumerable<Departments> GetAllDepartments();
        bool employeeExist(NewEmployees employee);
        JwtClients AuthenticateJwtClient(JwtClients user);
    }
}

