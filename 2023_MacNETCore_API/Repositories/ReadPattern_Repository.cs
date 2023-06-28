using System;
using _2023_MacNETCore_API.Interfaces;
using _2023_MacNETCore_API.Models;
using Microsoft.EntityFrameworkCore;

namespace _2023_MacNETCore_API.Repositories
{
    public class ReadPattern_Repository : IReadPattern_Repository
    {
        // Field Properties
        private readonly ApplicationDBContext _context;


        // Constuctor
        public ReadPattern_Repository(ApplicationDBContext context)
        {
            _context = context;
        }

        // Methods

        /// <summary>
        /// Gets All Managers from Managers Table
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Managers> GetAllManagers()
        {
            var _managers = new List<Managers>();

            IEnumerable<Managers> managers = (IQueryable<Managers>)_context.Managers
                .OrderBy(m => m.Manager_id)
                 .AsNoTracking();

            foreach (var manager in managers)
            {
                _managers.Add(manager);
            }

            return _managers;
        }


        /// <summary>
        /// Gets Manager by Id from Managers Table
        /// </summary>
        /// <returns></returns>
        public Managers GetManagerbyId(int id)
        {

            // STYLE 1
            //return _context.Managers.First(m => m.Manager_id == id);


            // STYLE 2
            /*Managers manager = _context.Managers.First(m => m.Manager_id == id && m.Manager_lname == "");
            return manager;*/


            // STYLE 3
            IEnumerable<Managers> manager = (IQueryable<Managers>)_context.Managers
            .Where(m => m.Manager_id == id)
            .AsNoTracking();

            Managers _manager = new Managers
            {
                Manager_id = manager.First().Manager_id,
                Manager_fname = manager.First().Manager_fname,
                Manager_lname = manager.First().Manager_lname,
                Manager_emp_no = manager.First().Manager_emp_no,
                Manager_Job_id = manager.First().Manager_Job_id,
                Manager_dept_id = manager.First().Manager_dept_id
            };

            return _manager;
        }


        /// <summary>
        /// Gets all Jobs from the Jobs Table
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Jobs> GetAllJobs()
        {
            var _jobs = new List<Jobs>();

            IEnumerable<Jobs> jobs = (IQueryable<Jobs>)_context.Jobs
                .OrderBy(m => m.Job_id)
                 .AsNoTracking();

            foreach (var job in jobs)
            {
                _jobs.Add(job);
            }

            return _jobs;
        }


        // <summary>
        /// Gets all Departments from the Departments Table
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Departments> GetAllDepartments()
        {
            // STYLE 1
            /*List<Departments> departments = _context.Departments
                .OrderBy(m => m.Dept_id)
                 .AsNoTracking()
                 .ToList();*/

            //STYLE 2
            var _departments = new List<Departments>();

            IEnumerable<Departments> departments = (IQueryable<Departments>)_context.Departments
                .OrderBy(m => m.Dept_id)
                 .AsNoTracking();

            foreach (var department in departments)
            {
                _departments.Add(department);
            }

            return _departments;
        }


        /// <summary>
        /// Checks if an employee exists in NewEmployee Table
        /// </summary>
        /// <returns></returns>
        public bool employeeExist(NewEmployees employee)
        {
            NewEmployees _employee = _context.NewEmployees.First(e => e.ssn == employee.ssn);

            if (_employee != null)
            {
                return true;
            }
            return false;
        }


        /// <summary>
        /// returns a Jwt User from Database
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public JwtClients AuthenticateJwtClient(JwtClients user)
        {
            var _user = _context.JwtClients.First(j => j.username == user.username && j.password == user.password);
            return _user!;
        }



    }
}

