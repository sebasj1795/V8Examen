using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Security.Transversal.Mapper.Profiler;

namespace Security.Transversal.Mapper
{
    public static class Mapping
    {
        public static IServiceCollection CustomAutomapper(this IServiceCollection services)
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new LoginProfile());
                mc.AddProfile(new AppProfile());
                mc.AddProfile(new RoleProfile());
                mc.AddProfile(new UserProfile());
                mc.AddProfile(new MenuProfile());
                mc.AddProfile(new MenuActionProfile());
                mc.AddProfile(new PermisoProfile());
                mc.AddProfile(new ActionProfile());
                mc.AddProfile(new MasterProfile());
                mc.AddProfile(new MasterDetProfile());
                mc.AddProfile(new EmployeeProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
            return services;
        }
    }
}
