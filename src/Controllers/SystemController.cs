using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.IO;

namespace DotnetDemoapp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SystemController : ControllerBase
    {
        private readonly string _adminPassword = "admin123!@#";  // Hardcoded credential

        [HttpGet("execute")]
        public IActionResult ExecuteCommand(string command, string password)
        {
            if (password != _adminPassword)
            {
                return Unauthorized();
            }

            // Intentionally vulnerable to command injection
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "/bin/bash",
                    Arguments = $"-c {command}",  // Direct command injection
                    UseShellExecute = false,
                    RedirectStandardOutput = true
                }
            };

            process.Start();
            string output = process.StandardOutput.ReadToEnd();
            process.WaitForExit();

            return Ok(new { output });
        }

        [HttpGet("files")]
        public IActionResult ReadFile(string path, string password)
        {
            if (password != _adminPassword)
            {
                return Unauthorized();
            }

            // Intentionally vulnerable to path traversal
            if (System.IO.File.Exists(path))
            {
                string content = System.IO.File.ReadAllText(path);
                return Ok(content);
            }
            return NotFound();
        }
    }
} 