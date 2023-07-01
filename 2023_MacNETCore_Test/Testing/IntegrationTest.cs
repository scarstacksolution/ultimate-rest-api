using System.Net;
using Moq;
using Moq.EntityFrameworkCore;
using Moq.Contrib.HttpClient;
using System.Net.Http.Json;
using _2023_MacNETCore_API.Controllers;
using _2023_MacNETCore_API.Repositories;
using _2023_MacNETCore_API.Interfaces;
using _2023_MacNETCore_API.Models;
using _2023_MacNETCore_Test.Constants;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Microsoft.EntityFrameworkCore.Internal;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Microsoft.Extensions.Configuration;
using Azure;
using System.Linq;
using NUnit.Framework;

namespace _2023_MacNETCoreAPITest.Testing;


[TestFixture]
public class IntegrationTest
{

    private HttpClient? httpClient;
    private IConfiguration? config;
    private IConfigurationBuilder? builder;
    private Mock<IReadPattern_Repository> readRepository;
    private Mock<ILogger<DiscoverYourLocalController>> logger;
    private Mock<IWritePattern_Repository> writeRepository;
    private Mock<IJwtAuthenticator> jwtAuthenticator;
    private Mock<IMemoryCaching> memoryCache;
    private Mock<DbContextOptions<ApplicationDBContext>> dbContext;
    List<Departments> _departments = new List<Departments>();



[SetUp]
    public void Setup()
    {
        readRepository = new Mock<IReadPattern_Repository>();
        logger = new Mock<ILogger<DiscoverYourLocalController>>();
        writeRepository = new Mock<IWritePattern_Repository>();
        jwtAuthenticator = new Mock<IJwtAuthenticator>();
        memoryCache = new Mock<IMemoryCaching>();
        dbContext = new Mock<DbContextOptions<ApplicationDBContext>>();
        var webAppFactory = new WebApplicationFactory<Program>();
        httpClient = webAppFactory.CreateDefaultClient();
        httpClient!.BaseAddress = new Uri(Const.baseUri);
        builder = new ConfigurationBuilder().AddJsonFile($"testsettings.json", optional: false);
        config = builder.Build();
    }


    //  ------ || Controller Integration Test || -------


    /// <summary>
    /// Integration Testing on API Controller
    /// </summary>
    [Test, Order(1), Description("Asserts that sample data exists in Data returnd from API Call")]
    public void IntegrationTest_ControllerAgainstTheDatabase()
    {
        //Arrange
        var _department = new Departments() { Dept_id = 1, Dept_name = "Paralegal" };
        List<Departments>? departments = new List<Departments>();

        //Act
        var response = httpClient!.GetAsync("api/DiscoverYourLocal/GetAllDepartments").Result;
        if (response.IsSuccessStatusCode)
        {
            departments = response.Content.ReadFromJsonAsync<List<Departments>>().Result;
        }

        Assert.That(_department.Dept_id!, Is.EqualTo(departments!.First().Dept_id));
        Assert.That(_department.Dept_name!, Is.EqualTo(departments!.First().Dept_name));
    }


    /// <summary>
    /// Integration Testing on API Controllers
    /// </summary>
    /// <param name="id"></param>
    /// <param name="fname"></param>
    /// <param name="lname"></param>
    /// <param name="mgr_emp_no"></param>
    /// <param name="mgr_job_id"></param>
    /// <param name="mgr_dept_id"></param>
    [TestCase(1, "Cornard", "Murray", 7, 17, 2), Order(2), Description("Asserts each testcase exists in Data returnd from API Call")]
    [TestCase(3, "Andrea", "Meldow", 4, 15, 7)]
    [TestCase(5, "Courtney", "Wright", 6, 10, 6)]
    public void IntegrationTest_ControllerAgainstDatabaseById(int id, string fname, string lname, int mgr_emp_no, int mgr_job_id, int mgr_dept_id)

    {
        //Arrange
        Managers? manager = new Managers();


        //Act
        var response = httpClient!.GetAsync("api/DiscoverYourLocal/GetManagerbyId/" + id).Result;
        if (response.IsSuccessStatusCode)
        {
            manager = response.Content.ReadFromJsonAsync<Managers>().Result;
        }

        //Assert
        Assert.That(id, Is.EqualTo(manager!.Manager_id));
        Assert.That(fname, Is.EqualTo(manager!.Manager_fname));
        Assert.That(lname, Is.EqualTo(manager!.Manager_lname));
        Assert.That(mgr_emp_no, Is.EqualTo(manager!.Manager_emp_no));
        Assert.That(mgr_job_id, Is.EqualTo(manager!.Manager_Job_id));
        Assert.That(mgr_dept_id, Is.EqualTo(manager!.Manager_dept_id));
    }



    //  ------ || Repository Integration Test || -------


    /// <summary>
    /// Integration Test On DB From Repository
    /// </summary>
    /// <param name="id"></param>
    /// <param name="name"></param>
    [TestCase(1, "Paralegal"), Order(3), Description("Asserts Each TestCase exists in Data returned from DB")]
    [TestCase(2, "Business")]
    [TestCase(3, "Public Relations")]
    [TestCase(4, "Customer Support")]
    [TestCase(5, "Human Resources")]
    [TestCase(6, "Marketing")]
    [TestCase(7, "Sales")]
    [TestCase(8, "Technology")]
    public void IntegrationTestAgainstDatabaseFromRepository_1(int id, string name)
    {
        //Act
        if(_departments.Count() == 0)
        {
            _departments = GetAllManagersUsingDBContextDB();
        }

        //Assert
        Assert.That(_departments.Count(d => d.Dept_id == id && d.Dept_name == name), Is.EqualTo(1));
    }


    /// <summary>
    /// Integration Test On DB From Repository
    /// </summary>
    [Test, Order(4), Description("Asserts Count from our List matches Count returned from DB")]
    public void IntegrationTestAgainstDatabaseFromRepository_2()
    {
        //Arrange
        var deptData = new List<Departments>
        {
        { new Departments() { Dept_id = 1, Dept_name = "Paralegal"}},
        { new Departments() { Dept_id = 2, Dept_name = "Business"}},
        { new Departments() { Dept_id = 3, Dept_name = "Public Relations"}},
        { new Departments() { Dept_id = 4, Dept_name = "Customer Support"}},
        { new Departments() { Dept_id = 5, Dept_name = "Human Resources"}},
        { new Departments() { Dept_id = 6, Dept_name = "Marketing"}},
        { new Departments() { Dept_id = 7, Dept_name = "Sales"}},
        { new Departments() { Dept_id = 8, Dept_name = "Technology"}},
        };

        if (_departments.Count() == 0)
        {
            _departments = GetAllManagersUsingDBContextDB();
        }

        //Assert
        Assert.That(_departments.Count(), Is.EqualTo(deptData.Count()));
    }


    //  ------ || Non-Tested Methods || -------


    /// <summary>
    /// This doesn't run as Main Tests due to Ignore Attributes
    /// </summary>
    [Ignore("This will be ignored as an actual unit test")]
    public List<Departments> GetAllManagersUsingDBContextDB()
    {
        var _departments = new List<Departments>();

        var contextOptions = new DbContextOptionsBuilder<ApplicationDBContext>()
        .UseSqlServer(config![Const.conn])
        .Options;

        using (var context = new ApplicationDBContext(contextOptions))
        {
            var departments = (IQueryable<Departments>)context.Departments
              .OrderBy(d => d.Dept_id)
               .AsNoTracking();

            //Populate
            foreach (var department in departments)
            {
                _departments.Add(department);
            }
        }
        return _departments;
    }


    /// <summary>
    /// Identifies method to be run after each test is executed
    /// </summary>
    [TearDown]
    public void RunAfterEachTest()
    {
        httpClient!.Dispose();
    }


}
