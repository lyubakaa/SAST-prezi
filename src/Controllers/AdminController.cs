using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DotnetDemoapp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly string _adminPassword = "admin123!@#"; // Hardcoded credential

        [HttpGet("execute")]
        public IActionResult ExecuteCommand(string cmd, string password)
        {
            if (password != _adminPassword)
            {
                return Unauthorized();
            }

            // Intentionally vulnerable to command injection
            var process = Process.Start(new ProcessStartInfo
            {
                FileName = "bash",
                Arguments = $"-c \"{cmd}\"",
                RedirectStandardOutput = true,
                UseShellExecute = false
            });

            string output = process.StandardOutput.ReadToEnd();
            process.WaitForExit();

            return Ok(new { output = output });
        }
    }
} 