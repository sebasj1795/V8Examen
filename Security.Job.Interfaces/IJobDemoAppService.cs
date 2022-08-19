using System.Threading.Tasks;

namespace Security.Job.Interfaces
{
    public interface IJobDemoAppService
    {
        /// <summary>
        /// Cambia los estados de las reuniones presenciales o virtuales
        /// </summary>
        /// <returns></returns>
        public Task DemoUpdateState();
    }
}
