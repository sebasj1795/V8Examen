using Microsoft.Extensions.DependencyInjection;
using Security.Job.Implementation;
using Security.Job.Interfaces;

namespace Security.Job.Configuration
{
    public static class ContainerIoC
    {
        public static void AddInjectionsJobs(this IServiceCollection services)
        {
            services.AddScoped<IJobDemoAppService, JobDemoAppService>();
        }
    }
}
