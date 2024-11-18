using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DotnetDemoapp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SystemController : ControllerBase
    {
        [HttpGet("ping")]
        public IActionResult Ping(string host)
        {
            // Intentionally vulnerable to command injection
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "/bin/bash",
                    Arguments = $"-c \"ping -c 4 {host}\"",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                }
            };
            
            process.Start();
            string result = process.StandardOutput.ReadToEnd();
            process.WaitForExit();
            
            return Ok(result);
        }

        [HttpGet("files")]
        public IActionResult ListFiles(string path)
        {
            // Another vulnerability - path traversal
            if (System.IO.Directory.Exists(path))
            {
                var files = System.IO.Directory.GetFiles(path);
                return Ok(files);
            }
            return NotFound();
        }
    }
} 