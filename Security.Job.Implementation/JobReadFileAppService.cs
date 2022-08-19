using Microsoft.Extensions.Configuration;
using Security.Job.Implementation.Base;
using Security.Job.Interfaces;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.Job.Implementation
{
    public class JobReadFileAppService : BaseAppService, IJobReadFileAppService
    {
        private readonly string[] _folders = { "Enviados", "Log", "Procesados" };
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;
        public JobReadFileAppService(IServiceProvider serviceProvider,
            IConfiguration configuration) : base(serviceProvider)
        {

        }

        public async Task ReadFile()
        {
            var path = _configuration["path"];
            var originFolder = Path.Combine(path, _folders[0]);

            _logger.Information($"Read directory: {originFolder}");
            if (Directory.Exists(originFolder))
            {
                var files = Directory.GetFiles(originFolder, "*.csv");
                foreach (var file in files)
                {
                    _logger.Information($"File: {file}");
                    var lines = await File.ReadAllLinesAsync(file);
                    foreach (var line in lines)
                    {
                        if (line.Length > 0)
                        {
                            try
                            {

                            }
                            catch (Exception ex)
                            {
                                _logger.Error(ex, ex.Message);
                            }
                        }
                    }
                }
            }
        }
    }
}
