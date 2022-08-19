using Hangfire;
using Security.Job.Interfaces;
using Microsoft.AspNetCore.Builder;
using Security.Transversal.Common.Helpers;
namespace Security.Job.Configuration
{
    public static class HangfireTimeIntervalSettings
    {
        public static void UseHangfireMethods(this IApplicationBuilder app)
        {
            RecurringJob.AddOrUpdate<IJobDemoAppService>("IdDemo", 
                x => x.DemoUpdateState(),
                    "* * * * *",
                    InternationalTimeHelper.GetTimeZoneInfoBySpecificCountry("PE"));
        }


    }
}
