using APILoanProduct.DTO.LoanApplications;
using APILoanProduct.Interfaces.Repositories;
using APILoanProduct.Interfaces.Services;
using APILoanProduct.Models.LoanApplications;

namespace APILoanProduct.Services
{
    public class LoanApplicationReviewService : GenericService<LoanApplicationReview, Guid>, ILoanApplicationReviewService
    {
        public LoanApplicationReviewService(IGenericRepository<LoanApplicationReview, Guid> repository) : base(repository)
        {
        }

        public override async Task<LoanApplicationReview> CreateAsync<TDto>(TDto dto)
        {
            var createDto = dto as LoanApplicationReviewCreateDto;
            var entity = new LoanApplicationReview
            {
                ApplicationId = createDto!.ApplicationId,
                ManagerUserId = new Guid(createDto.ManagerUserId.ToString().PadLeft(32, '0')),
                Status = (APILoanProduct.Models.Roles.Loanapplicationstatus)createDto.Status,
                Remarks = createDto.Remarks
            };
            return await _repository.AddAsync(entity);
        }

        public override async Task<LoanApplicationReview> UpdateAsync<TDto>(Guid id, TDto dto)
        {
            var updateDto = dto as LoanApplicationReviewCreateDto;
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) throw new KeyNotFoundException();

            entity.Status = (APILoanProduct.Models.Roles.Loanapplicationstatus)updateDto!.Status;
            entity.Remarks = updateDto.Remarks;

            return await _repository.UpdateAsync(id, entity);
        }

        public async Task<IEnumerable<LoanApplicationReview>> GetReviewsByDateAsync(DateTime reviewDate)
        {
            var allReviews = await _repository.GetAllAsync();
            return allReviews.Where(r => r.ReviewDate.Date == reviewDate.Date);
        }
    }
}