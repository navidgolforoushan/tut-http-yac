using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Server.Records;
namespace server.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{

    private readonly ILogger<AuthController> _logger;

    public AuthController(ILogger<AuthController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public ActionResult Index()
    {
        return Ok("Welcome to dummy server auth controller!");
    }

    [HttpPost]
    public ActionResult Login([FromBody] Credential credentials)
    {
        StringBuilder sb = new StringBuilder();
        foreach (byte b in GetHash(credentials.Username + credentials.Password))
        {
            sb.Append(b.ToString("X2"));
        }

        return Ok(new{Token=sb.ToString(),TimeStamp=DateTime.UtcNow});
    }

    public static byte[] GetHash(string inputString)
    {
        using HashAlgorithm algorithm = SHA256.Create();
        return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
    }
}
  