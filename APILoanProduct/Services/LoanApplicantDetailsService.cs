using APILoanProduct.DTO.LoanApplications;
using APILoanProduct.Interfaces.Repositories;
using APILoanProduct.Interfaces.Services;
using APILoanProduct.Models.LoanApplications;

namespace APILoanProduct.Services
{
    public class LoanApplicantDetailsService : GenericService<LoanApplicantDetails, Guid>, ILoanApplicantDetailsService
    {
        public LoanApplicantDetailsService(IGenericRepository<LoanApplicantDetails, Guid> repository) : base(repository)
        {
        }

        public override async Task<LoanApplicantDetails> CreateAsync<TDto>(TDto dto)
        {
            var createDto = dto as LoanApplicantDetailsCreateDto;
            var entity = new LoanApplicantDetails
            {
                ApplicationId = Guid.NewGuid(),
                FullName = createDto!.FullName,
                DateOfBirth = createDto.DateOfBirth,
                Address = createDto.Address,
                EmploymentStatus = (APILoanProduct.Models.Roles.Employmentstatus?)createDto.EmploymentStatus,
                AnnualIncome = createDto.AnnualIncome
            };
            return await _repository.AddAsync(entity);
        }

        public override async Task<LoanApplicantDetails> UpdateAsync<TDto>(Guid id, TDto dto)
        {
            var updateDto = dto as LoanApplicantDetailsCreateDto;
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) throw new KeyNotFoundException();

            entity.FullName = updateDto!.FullName;
            entity.DateOfBirth = updateDto.DateOfBirth;
            entity.Address = updateDto.Address;
            entity.EmploymentStatus = (APILoanProduct.Models.Roles.Employmentstatus?)updateDto.EmploymentStatus;
            entity.AnnualIncome = updateDto.AnnualIncome;

            return await _repository.UpdateAsync(id, entity);
        }
    }
}