using APILoanProduct.DTO.LoanApplications;
using APILoanProduct.Models.LoanApplications;

namespace APILoanProduct.Interfaces.Services
{
    public interface ILoanApplicationService : IGenericService<LoanApplication, Guid>
    {
        Task<IEnumerable<LoanApplicationReadDto>> GetByBranchNameAsync(string branchName);
        Task<LoanApplicationReadDto> ApplyForLoanAsync(LoanApplicationCreateDto dto);
        Task<IEnumerable<LoanApplicationReadDto>> GetUserApplicationsAsync(Guid userId);
        Task<LoanApplicationReadDto> UpdateStatusAsync(Guid id, StatusUpdateDto dto);
        Task<IEnumerable<LoanApplicationFullDto>> GetAllFullApplicationsAsync();
    }
}