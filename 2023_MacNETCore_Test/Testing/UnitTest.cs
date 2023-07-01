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

namespace _2023_MacNETCoreAPITest.Testing;


[TestFixture]
public class UnitTest
{

    private HttpClient? httpClient;
    private Mock<IReadPattern_Repository> readRepository;
    private Mock<ILogger<DiscoverYourLocalController>> logger;
    private Mock<IWritePattern_Repository> writeRepository;
    private Mock<IJwtAuthenticator> jwtAuthenticator;
    private Mock<IMemoryCaching> memoryCache;
    private Mock<DbContextOptions<ApplicationDBContext>> dbContext;


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
    }


    //  ------ || Endpoints Unit Test || -------

    /// <summary>
    /// Unit Testing on URIEndpoint Controller 
    /// </summary>
    /// <param name="endpoint"></param>
    /// <param name="completion"></param>
    [TestCase("api/DiscoverYourLocal/GetAllDepartments", true), Order(1), Description("Asserts each URI Call returns Success status")]
    [TestCase("api/DiscoverYourLocal/GetManagerbyId/4", true)]
    [TestCase("api/DiscoverYourLocal/GetAllJobs", true)]
    public void TestApiHttpClientEndpoints(string endpoint, bool completion)
    {
        //Act
        var response = httpClient!.GetAsync(endpoint).Result;
        var content = response.Content.ReadAsStringAsync();

        //Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        Assert.That(response.IsSuccessStatusCode, Is.EqualTo(completion));

    }


    //  ------ || Repository Unit Test || -------

    /// <summary>
    /// Unit Testing on IEnumerable Repository
    /// </summary>
    [Test, Order(2), Description("Asserts that our List Collection matches the Mocked Repository call")]
    public void TestIEumerableRepositoryCall()
    {
        //Arrange
        var departments = new List<Departments>
        {
        { new Departments() { Dept_id = 1, Dept_name = "Paralegal"}},
        { new Departments() { Dept_id = 2, Dept_name = "Business"}},
        { new Departments() { Dept_id = 3, Dept_name = "Public Relations"}},
        { new Departments() { Dept_id = 4, Dept_name = "Customer Support"}}
        };

        //Act
        readRepository.Setup(r => r.GetAllDepartments()).Returns(departments);
        var discoverYourLocal = MockInstantiateDiscoverYourLocal();
        var result = discoverYourLocal.GetAllDepartments();

        //Assert
        Assert.IsNotNull(result);
        Assert.That(departments, Is.EqualTo(result));
    }


    /// <summary>
    /// Unit Testing on Single row Repository
    /// </summary>
    /// <param name="id"></param>
    /// <param name="fname"></param>
    /// <param name="lname"></param>
    /// <param name="mgr_emp_no"></param>
    /// <param name="mgr_job_id"></param>
    /// <param name="mgr_dept_id"></param>
    [TestCase(1, "Cornard", "Murray", 7, 17, 2), Order(3), Description("Asserts each Testcase matches the Mocked Repository call")]
    [TestCase(3, "Andrea", "Meldow", 4, 15, 7)]
    [TestCase(5, "Courtney","Wright", 6, 10, 6)]
    public void TestFlatEmptyClassRepository(int id, string fname, string lname, int mgr_emp_no, int mgr_job_id, int mgr_dept_id)
    {
        //Arrange
        var managers = new Managers()
        {
            Manager_id = id,
            Manager_fname = fname,
            Manager_lname = lname,
            Manager_emp_no = mgr_emp_no,
            Manager_Job_id = mgr_job_id,
            Manager_dept_id = mgr_dept_id
        };

        //Act
        readRepository.Setup(r => r.GetManagerbyId(id)).Returns(managers);
        var discoverYourLocal = MockInstantiateDiscoverYourLocal();
        var result = discoverYourLocal.GetManagerbyId(id);

        //Assert
        Assert.IsNotNull(result);
        Assert.That(managers, Is.EqualTo(result));
    }


    //  ------ || Non-Tested Methods || -------


    /// <summary>
    /// This doesn't run as Main Tests due to Ignore Attributes
    /// </summary>
    [Ignore("This method will be ignored as a unit test")]
    public DiscoverYourLocalController MockInstantiateDiscoverYourLocal()
    {
        var discoverYourLocal = new DiscoverYourLocalController(logger!.Object, readRepository!.Object, writeRepository!.Object,
        jwtAuthenticator!.Object, memoryCache!.Object);
        return discoverYourLocal;
    }


    /// <summary>
    /// Identifies method to be run after each test is executed
    /// </summary>
    [TearDown()]
    public void RunAfterEachTest_MainTearDownMethod()
    {
        httpClient!.Dispose();
    }


}
