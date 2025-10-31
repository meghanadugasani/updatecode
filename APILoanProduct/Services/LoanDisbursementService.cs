using APILoanProduct.DTO.LoanApplications;
using APILoanProduct.Interfaces.Repositories;
using APILoanProduct.Interfaces.Services;
using APILoanProduct.Models.LoanApplications;

namespace APILoanProduct.Services
{
    public class LoanDisbursementService : GenericService<LoanDisbursement, Guid>, ILoanDisbursementService
    {
        public LoanDisbursementService(IGenericRepository<LoanDisbursement, Guid> repository) : base(repository)
        {
        }

        public override async Task<LoanDisbursement> CreateAsync<TDto>(TDto dto)
        {
            var createDto = dto as LoanDisbursementCreateDto;
            var entity = new LoanDisbursement
            {
                ApplicationId = createDto!.ApplicationId,
                ApprovedAmount = createDto.ApprovedAmount,
                DisbursementAccount = createDto.DisbursementAccount
            };
            return await _repository.AddAsync(entity);
        }

        public override async Task<LoanDisbursement> UpdateAsync<TDto>(Guid id, TDto dto)
        {
            var updateDto = dto as LoanDisbursementCreateDto;
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) throw new KeyNotFoundException();

            entity.ApprovedAmount = updateDto!.ApprovedAmount;
            entity.DisbursementAccount = updateDto.DisbursementAccount;

            return await _repository.UpdateAsync(id, entity);
        }

        public async Task<IEnumerable<LoanDisbursement>> GetDisbursementsByDateAsync(DateTime disbursementDate)
        {
            var allDisbursements = await _repository.GetAllAsync();
            return allDisbursements.Where(d => d.DisbursementDate.Date == disbursementDate.Date);
        }

        public async Task<IEnumerable<LoanDisbursement>> GetDisbursementsByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            var allDisbursements = await _repository.GetAllAsync();
            return allDisbursements.Where(d => d.DisbursementDate.Date >= startDate.Date && d.DisbursementDate.Date <= endDate.Date);
        }

        public async Task<IEnumerable<LoanDisbursement>> GetDisbursementsByMaxAmountAsync(decimal maxAmount)
        {
            var allDisbursements = await _repository.GetAllAsync();
            return allDisbursements.Where(d => d.ApprovedAmount <= maxAmount);
        }

        public async Task<IEnumerable<LoanDisbursement>> GetDisbursementsByAmountRangeAsync(decimal minAmount, decimal maxAmount)
        {
            var allDisbursements = await _repository.GetAllAsync();
            return allDisbursements.Where(d => d.ApprovedAmount >= minAmount && d.ApprovedAmount <= maxAmount);
        }
    }
}