using System;
using System.Linq;
using System.Net;
using _2023_MacNETCore_API.Interfaces;
using _2023_MacNETCore_API.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Sane.Http.HttpResponseExceptions;

namespace _2023_MacNETCore_API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DiscoverYourLocalController : ControllerBase
{
    // Field Properties
    private readonly ILogger<DiscoverYourLocalController> _logger;
    private readonly IReadPattern_Repository _readRepository;
    private readonly IWritePattern_Repository _writeRepository;
    private readonly IJwtAuthenticator _jwtAuthenticator;

    // Constructor
    public DiscoverYourLocalController(ILogger<DiscoverYourLocalController> logger, IReadPattern_Repository readRepository,
        IWritePattern_Repository writeRepository, IJwtAuthenticator jwtAuthenticator)
    {
        _logger = logger;
        _readRepository = readRepository;
        _writeRepository = writeRepository;
       _jwtAuthenticator = jwtAuthenticator;
}


    /// <summary>
    /// Creates a JWT Token once login is verified
    /// </summary>
    /// <returns></returns>
    [AllowAnonymous]
    [Route("JwtTokenGenerator")]    
    [HttpPost]
    public ActionResult JwtLogin()
    {
        var re = Request;
        var headers = re.Headers;

        string? userName = null;
        string? password = null;

        if (headers.ContainsKey("username"))
        {
            userName = headers["username"].ToString();
        }

        if (headers.ContainsKey("password"))
        {
            password = headers["password"].ToString();
        }

        var user = new JwtUserLogin()
        {
            Username = userName,
            Password = password
        }; //Authenticate(userLogin);

        if (user != null)
        {
            var token = _jwtAuthenticator.GenerateToken(user);
            return Ok(token);
        }

        return NotFound("user not found");
    }


    /// <summary>
    /// Gets All Managers
    /// PostMan: https://localhost:7038/api/DiscoverYourLocal/GetAllManagers
    /// To return: return Ok(manager), use IActionResult in method signature
    /// </summary>
    /// <returns></returns>
    [Route("GetAllManagers")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IEnumerable<Managers> GetAllManagers()
    {
        if (!ModelState.IsValid)
        {
            return (IEnumerable<Managers>)BadRequest(ModelState);
        }

        try
        {
            _logger.LogInformation("Processing request for GetAllManagers uri", DateTime.UtcNow.Date);

            var managers = _readRepository.GetAllManagers();

            if (managers.Count() == 0)
            {
                _logger.LogWarning("No information found for GetAllManagers uri", DateTime.UtcNow.Date);
                return (IEnumerable<Managers>)NotFound();
            }

            _logger.LogInformation("Returning results found for GetAllManagers uri", DateTime.UtcNow.Date);

            return managers;
        }

        catch (Exception ex)
        {
            _logger.LogError("Exception errror was caught for GetAllManagers uri", DateTime.UtcNow.Date + "");
            Console.WriteLine("{0} First exception caught.", ex.Message);
            throw ex.InnerException!;
            throw new ApplicationException("Exception thrown");

        }
    }


    /// <summary>
    /// Gets Managers by Id
    /// PostMan: https://localhost:7038/api/DiscoverYourLocal/GetManagerbyId/4
    /// To return: "return Ok(manager)", use IActionResult in method signature
    /// </summary>
    /// <returns></returns>
    [Route("GetManagerbyId/{id}")]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public Managers GetManagerbyId(int id)
    {
        if (!ModelState.IsValid)
        {
            return (Managers)(IEnumerable<Managers>)BadRequest(ModelState);
        }

        try
        {
            _logger.LogInformation("Processing request for: Get Manager by id: {0}, at: {DT}", id, DateTime.UtcNow.Date);

            var manager = _readRepository.GetManagerbyId(id);

            if (manager == null)
            {
                _logger.LogWarning("No information found for: Get Manager by id: {0}, at: {DT}", id, DateTime.UtcNow.Date);
                var message = string.Format("No information found for: Get Manager by id: {0}, at: {DT}", id, DateTime.UtcNow.Date);
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            _logger.LogInformation("Returning results found for: Get Manager by id: {0}, at: {DT}", DateTime.UtcNow.Date);

            return manager;
        }

        catch (Exception ex)
        {
            _logger.LogError("At {DT}, Exception errror for: Get Manager by id: {0}, was caught: {ex.InnerException}",
                DateTime.UtcNow.Date, id, ex.InnerException);
            throw ex.InnerException!;
            throw new ApplicationException("Exception thrown");
        }
    }



    /// <summary>
    /// Gets All Managers
    /// PostMan: https://localhost:7038/api/DiscoverYourLocal/GetAllJobs
    /// To return: return Ok(jobs), use IActionResult in method signature
    /// </summary>
    /// <returns></returns>
    [Route("GetAllJobs")]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IEnumerable<Jobs> GetAllJobs()
    {
        if (!ModelState.IsValid)
        {
            return (IEnumerable<Jobs>)BadRequest(ModelState);
        }

        try
        {
            _logger.LogInformation("Processing request for {Jobs}", DateTime.Now.ToLongTimeString());

            var jobs = _readRepository.GetAllJobs();

            if (jobs.Count() == 0)
            {
                _logger.LogWarning("No information found for {Jobs}", DateTime.Now.ToLongTimeString());
                return (IEnumerable<Jobs>)NotFound();
            }

            _logger.LogInformation("Returning results found for {Jobs}", DateTime.Now.ToLongTimeString());

            return jobs;
        }

        catch (Exception ex)
        {
            _logger.LogError("Exception errror was caught: {ex.InnerException}", DateTime.Now.ToLongTimeString());
            Console.WriteLine("{0} First exception caught.", ex.Message);
            throw ex.InnerException!;
            throw new ApplicationException("Exception thrown");
        }
    }


    /// <summary>
    /// Gets All Departments
    /// PostMan: https://localhost:7038/api/DiscoverYourLocal/GetAllDepartments
    /// To return: return Ok(departments), use IActionResult in method signature
    /// </summary>
    /// <returns></returns>
    [Route("GetAllDepartments")]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IEnumerable<Departments> GetAllDepartments()
    {
        if (!ModelState.IsValid)
        {
            return (IEnumerable<Departments>)BadRequest(ModelState);
        }

        try
        {
            _logger.LogInformation("Processing request for - GET ALL DEPARTMENTS - endpoint, at: {DT}", DateTime.Now.ToLongTimeString());

            var departments = _readRepository.GetAllDepartments();

            if (departments.Count() == 0)
            {
                _logger.LogWarning("At: {DT}, no information found for: - GET ALL DEPARTMENTS - endpoint", DateTime.Now.ToLongTimeString());
                return (IEnumerable<Departments>)NotFound();
            }

            _logger.LogInformation("Successfully returned results at: {DT}, found for: - GET ALL DEPARTMENTS - endpoint", DateTime.Now.ToLongTimeString());

            return departments;
        }

        catch (Exception ex)
        {
            _logger.LogError("At: {DT}, Exception errror for: - GET ALL DEPARTMENTS - endpoint, was caught: {ex.InnerException}",
                DateTime.Now.ToLongTimeString(), ex.InnerException);
            throw ex.InnerException!;
            throw new ApplicationException("Exception thrown");
        }
    }


    /// <summary>
    /// Saves New Employee information to Database
    /// </summary>
    /// <param name="employee"></param>
    /// <returns></returns>
    /// <exception cref="ApplicationException"></exception>
    [Route("PostEmployeeDetail", Name = "NewEmployeeDetail")]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public Object PostNewEmployeeDetail([FromBody] NewEmployees employee)
    {
        if (!ModelState.IsValid)
        {
            return HttpStatusCode.BadRequest;
        }

        try
        {
            _logger.LogInformation("Processing {NewEmployees}, PostEmployeeDetail request at {DT}",
                employee, DateTime.Now.ToLongTimeString());
            _writeRepository.PostNewEmployeeInformation(employee);
        }

        catch (DbUpdateException ex)
        {
            if (_readRepository.employeeExist(employee))
            {
                _logger.LogError("At: {DT}, DbUpdateException errror for {NewEmployees}, was caught: {ex.InnerException}",
                    DateTime.Now.ToLongTimeString(), employee, ex.InnerException);
                return Conflict();
            }
            else
            {
                throw;
            }
        }

        catch (Exception ex)
        {
            _logger.LogError("At: { DT}, Exception errror for {NewEmployees}, was caught: {ex.InnerException}",
                DateTime.Now.ToLongTimeString(), employee, ex.InnerException);
            throw ex.InnerException!;
            throw new ApplicationException("Exception thrown");
        }

        _logger.LogInformation("New Employee Detail {NewEmployees}, Successfully saved to Database at {DT}",
            employee, DateTime.Now.ToLongTimeString());

        return CreatedAtRoute("NewEmployeeDetail", new { id = employee.id, employee });
    }


}

