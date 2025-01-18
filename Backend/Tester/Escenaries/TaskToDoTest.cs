using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Models;
using Models.Entities;
using Moq;
using NUnit.Framework;
using Services.Services;
using System.IO;
using System.Threading.Tasks;

namespace Tester.Escenaries
{
    public class TaskToDoTest
    {
        private ApplicationDbContext _context;
        private TaskToDoService _taskToDoService;
        private Mock<ILogger<TaskToDoService>> _loggerMock;
        private readonly int testId = 1;
        private IConfiguration _configuration;

        [SetUp]
        public void Setup()
        {
            // Manually specify the path to appsettings.json
            string appSettingsPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "appsettings.json");

            // Load configuration from appsettings.json
            _configuration = new ConfigurationBuilder()
                .AddJsonFile(appSettingsPath, optional: false, reloadOnChange: true)
                .Build();

            // Configure DbContext with options
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .Options;

            // Pass both IConfiguration and DbContextOptions to the ApplicationDbContext constructor
            _context = new ApplicationDbContext(options, _configuration);

            // Mock logger
            _loggerMock = new Mock<ILogger<TaskToDoService>>();

            // Initialize TaskToDoService
            _taskToDoService = new TaskToDoService(_context, _loggerMock.Object);
        }



        [TearDown]
        public void Cleanup()
        {
            _context.Dispose();
        }

        [Test]
        public async Task GetAllTasks()
        {
            var tasks = await _taskToDoService.GetAll();

            Assert.IsNotNull(tasks);
        }

        [Test]
        public async Task GetUniqueTask()
        {

            var retrievedTask = await _taskToDoService.GetBy(testId);

            Assert.IsNotNull(retrievedTask);

            Assert.AreEqual(testId, retrievedTask.Id);
        }

        [Test]
        public async Task CreateTask()
        {
            var newTask = new TaskToDo { Description = "Test Created" };

            var createdTask = await _taskToDoService.Create(newTask);

            Assert.IsNotNull(createdTask);

            Assert.AreEqual(newTask.Description, createdTask.Description);
        }

        [Test]
        public async Task UpdateTask()
        {

            var updatedTask = await _taskToDoService.Update(new TaskToDo { Id = testId, Description = "Changed Description" });

            Assert.IsNotNull(updatedTask);
        }

        [Test]
        public async Task DeleteTask()
        {
            var deletedTask = await _taskToDoService.Delete(testId);

            Assert.IsNotNull(deletedTask);
        }
    }
}
