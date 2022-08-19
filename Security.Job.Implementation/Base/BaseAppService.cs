using AutoMapper;
using Security.Domain.Interfaces.IUnitOfWork;
using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
namespace Security.Job.Implementation.Base
{
    public class BaseAppService
    {
        protected readonly IServiceProvider ServiceProvider;
        protected readonly IUnitOfWork UnitOfWork;
        protected readonly IMapper Mapper;
        protected readonly IConfiguration Configuration;

        public BaseAppService(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
            UnitOfWork = serviceProvider.GetService<IUnitOfWork>();
            Mapper = serviceProvider.GetService<IMapper>();
            Configuration = serviceProvider.GetService<IConfiguration>();
        }
    }
}
