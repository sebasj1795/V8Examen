using AutoMapper;
using Security.Domain.Interfaces.IUnitOfWork;
using Security.Transversal.Auth.Entity;
using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;

namespace Security.Application.MainModule.Base
{
    public class BaseAppService
    {
        protected readonly IServiceProvider ServiceProvider;
        protected readonly IUnitOfWork UnitOfWork;
        protected readonly IMapper Mapper;
        protected readonly IConfiguration Configuration;
        public readonly User CurrentUser;
        public BaseAppService(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
            UnitOfWork = serviceProvider.GetService<IUnitOfWork>();
            Mapper = serviceProvider.GetService<IMapper>();
            Configuration = serviceProvider.GetService<IConfiguration>();
            var httpContextAccessor = serviceProvider.GetService<IHttpContextAccessor>();

            CurrentUser = new User(httpContextAccessor?.HttpContext?.User);
        }
    }
}
