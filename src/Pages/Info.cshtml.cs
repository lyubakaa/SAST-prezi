using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DotnetDemoapp.Pages
{
    public class SystemInfoModel : PageModel
    {
        private readonly ILogger<SystemInfoModel> _logger;
        private readonly IConfiguration _config;

        public bool isInContainer { get; private set; } = false;
        public bool isInKubernetes { get; private set; } = false;
        public string hostname { get; private set; } = "";
        public string osDesc { get; private set; } = "";
        public string osArch { get; private set; } = "";
        public string osVersion { get; private set; } = "";
        public string framework { get; private set; } = "";
        public string processorCount { get; private set; } = "";
        public string workingSet { get; private set; } = "";
        public string physicalMem { get; private set; } = "";
        public Dictionary<string, string> envVars { get; private set; } = new Dictionary<string, string>();

        public SystemInfoModel(ILogger<SystemInfoModel> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }

        public void OnGet()
        {
            hostname = System.Environment.MachineName;
            osDesc = System.Runtime.InteropServices.RuntimeInformation.OSDescription;
            osArch = System.Runtime.InteropServices.RuntimeInformation.OSArchitecture.ToString();
            framework = System.Runtime.InteropServices.RuntimeInformation.FrameworkDescription;
            processorCount = System.Environment.ProcessorCount.ToString();
            workingSet = (System.Environment.WorkingSet / 1024 / 1024).ToString();
            physicalMem = (System.GC.GetGCMemoryInfo().TotalAvailableMemoryBytes / 1024 / 1024).ToString();
            
            foreach (System.Collections.DictionaryEntry de in System.Environment.GetEnvironmentVariables())
            {
                envVars.Add(de.Key.ToString(), de.Value.ToString());
            }
        }
    }
}
