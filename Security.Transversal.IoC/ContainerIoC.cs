using Microsoft.Extensions.DependencyInjection;
using Security.Application.Interfaces;
using Security.Application.MainModule;
using Security.Domain.Interfaces.IRepository;
using Security.Domain.Interfaces.IUnitOfWork;
using Security.Infrastucture.Repository;
using Security.Infrastucture.Repository.UnitOfWork;
using Security.Transversal.Auth.Jwt;

namespace Security.Transversal.IoC
{
    public static class ContainerIoC
    {
        public static void AddInjectionsServices(this IServiceCollection services)
        {
            services.AddTransient<ILoginAppService, LoginAppService>();
            services.AddTransient<IJwtFactory, JwtFactory>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IUserAppService, UserAppService>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<IAppAppService, AppAppService>();
            services.AddScoped<IAppRepository, AppRepository>();

            services.AddScoped<IRoleAppService, RoleAppService>();
            services.AddScoped<IRoleRepository, RoleRepository>();

            services.AddScoped<IMenuAppService, MenuAppService>();
            services.AddScoped<IMenuRepository, MenuRepository>();

            services.AddScoped<IActionAppService, ActionAppService>();
            services.AddScoped<IActionRepository, ActionRepository>();

            services.AddScoped<IAppAppService, AppAppService>();
            services.AddScoped<IAppRepository, AppRepository>();

            services.AddScoped<IModuleAppService, ModuleAppService>();
            services.AddScoped<IModuleRepository, ModuleRepository>();

            services.AddScoped<IMasterAppService, MasterAppService>();
            services.AddScoped<IMasterRepository, MasterRepository>();

            services.AddScoped<IMasterDetAppService, MasterDetAppService>();
            services.AddScoped<IMasterDetRepository, MasterDetRepository>();

            services.AddScoped<IPermissionAppService, PermissionAppService>();

            services.AddScoped<IEmployeeAppService, EmployeeAppService>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();

        }
    }
}
