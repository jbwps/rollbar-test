using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassLibrary1;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly ITestService _testService;

        public TestController(ITestService testService)
        {
            _testService = testService;
        }

        // GET api/test
        [HttpGet]
        public ActionResult<string> Get()
        {
            return "Hello World";
        }

        // Example 1
        // Actual: A Rollbar item with title "Rollbar.NetCore.AspNet.RollbarMiddlewareException: The included internal exception processed by the Rollbar middleware"
        // Expected: A Rollbar item with title "System.Exception: Test Exception"

        // POST api/test
        [HttpPost]
        public async Task<IActionResult> Post()
        {
            var message = $"Test Exception";

            throw new Exception(message);
        }

        // Example 2
        // Actual: Two Rollbar items with titles "RollbarMiddleware processed uncaught exception." and "The included internal exception processed by the Rollbar middleware"
        // Expected: A Single Rollbar item with title "System.Exception: Test Exception"
        // PUT api/test
        [HttpPut]
        public async Task<IActionResult> Put()
        {
            _testService.ThrowError();

            return Ok();
        }
    }
}
