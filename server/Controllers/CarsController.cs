using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc;
using Server.Records;
namespace server.Controllers;

[ApiController]
[Route("[controller]")]
public class CarsController : ControllerBase
{

    private readonly ILogger<CarsController> _logger;

    public CarsController(ILogger<CarsController> logger)
    {
        _logger = logger;
    }

    [HttpGet("Index")]
    public ActionResult Index([FromHeader(Name = "SimpleAuth")] string auth)
    {
        if (auth != adminKey) { return Unauthorized(); }

        return Ok(CarList);
    }

    [HttpGet]
    public ActionResult Get([FromHeader(Name = "SimpleAuth")] string auth, int id)
    {
        if (auth != adminKey) { return Unauthorized(); }

        return Ok(CarList[id]);
    }

    [HttpPost]
    public ActionResult Post([FromHeader(Name = "SimpleAuth")] string auth, [FromBody] Car car)
    {
        if (auth != adminKey) { return Unauthorized(); }

        CarList.Add(car);
        return CreatedAtAction(nameof(Get), CarList.Count - 1, CarList.Last());
    }

    private List<Car> CarList { get; } = new()
    {
        new("Toyota","Camry","Black","2011"),
        new("Jeep","Wrangler","Yellow","2018"),
    };

    private readonly string adminKey="759EAA4FFD5A45E63D2E9D3C4578AA4EF5F5F88A6A0AE70F264A19003962974E";
}
