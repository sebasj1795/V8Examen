using Security.Application.Dto.Master;
using Security.Application.Interfaces;
using Security.Application.MainModule.Base;
using Security.Domain.Entities;
using Security.Domain.Interfaces.IRepository;
using Security.Domain.MainModule.Validations;
using Security.Transversal.Common;
using Security.Transversal.Common.Enum;
using Security.Transversal.Common.Helpers;
using System;
using System.Threading.Tasks;

namespace Security.Application.MainModule
{
    public class MasterAppService : BaseAppService, IMasterAppService
    {
        private readonly IMasterRepository _masterRepository;
        private readonly IMasterDetRepository _masterdetRepository;
        public MasterAppService(IServiceProvider serviceProvider,
            IMasterRepository masterRepository,
            IMasterDetRepository masterDetRepository) : base(serviceProvider)
        {
            _masterRepository = masterRepository;
            _masterdetRepository = masterDetRepository;
        }

        public async Task<Response<MasterCreateRequestDto>> CreateDemoAsync(MasterCreateRequestDto request)
        {
            
            var response = TypeMessageHelper.MessageSuccess<MasterCreateRequestDto>();
            var master = Mapper.Map<Master>(request);
            master.IdCompany = CurrentUser.CompanyId;
            master.State = (int)StateEnum.Active;
            master.UserCrea = CurrentUser.Id;
            master.DateCrea = DateTime.Now;

            try
            {
                await UnitOfWork.BeginTransactionAsync();
                var resultMaster = await _masterRepository.AddAsync(master);

                var masterdetEntity = Mapper.Map<MasterDet>(request.masterDet);
                masterdetEntity.Master = resultMaster;

                var resultMasterdet = await _masterdetRepository.AddAsync(masterdetEntity);
                resultMasterdet.UserCrea = master.UserCrea;
                resultMasterdet.DateCrea = DateTime.Now;
                await UnitOfWork.CommitTransactionAsync();
            }
            catch (Exception ex )
            {

                throw;
            }
           
            response.Data = Mapper.Map<MasterCreateRequestDto>(master);
            return response;

        }

    }
}
