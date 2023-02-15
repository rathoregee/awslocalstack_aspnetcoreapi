using Microsoft.AspNetCore.Mvc;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Json;

namespace ApiGateway.Controllers;

[Route("api/[controller]")]
public class ValuesController : ControllerBase
{
    private IConfiguration config;
    public ValuesController(IConfiguration configuration)
    {
        this.config = configuration;
        Log.Logger = new LoggerConfiguration()

            // Add console (Sink) as logging target
            .WriteTo.Console()

            // Set default minimum log level
            .MinimumLevel.Debug()

            // Create the actual logger
            .CreateLogger();

    }
    // GET api/values
    [HttpGet]  
    public IEnumerable<string> Get()
    {
        var d = new DataContext(config);
        var c = d.Dump.ToList();

        Log.Debug("Hello, Serilog!");
        return new string[] { "value1", "value2" };
    }

    // GET api/values/5
    [HttpGet("{id}")]
    public string Get(int id)
    {
        return "value";
    }

    // POST api/values
    [HttpPost]
    public void Post([FromBody]string value)
    {
    }

    // PUT api/values/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody]string value)
    {
    }

    // DELETE api/values/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
}