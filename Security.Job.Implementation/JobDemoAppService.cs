using Security.Domain.Interfaces.IRepository;
using Security.Job.Implementation.Base;
using Security.Job.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Security.Job.Implementation
{
    public class JobDemoAppService : BaseAppService, IJobDemoAppService
    {
        private readonly IActionRepository _actionRepository;
        public JobDemoAppService(IServiceProvider serviceProvider,
            IActionRepository actionRepository) : base(serviceProvider)
        {
            _actionRepository = actionRepository;
        }

        public async Task DemoUpdateState()
        {
            var ListActionEntitys = _actionRepository.Find(x => x.State == 1).ToList();
            ListActionEntitys.ForEach(item => { item.State = 0; });
            await UnitOfWork.SaveChangesAsync();
        }

    }
}
