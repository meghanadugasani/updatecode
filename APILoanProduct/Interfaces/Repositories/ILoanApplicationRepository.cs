using APILoanProduct.Models.LoanApplications;

namespace APILoanProduct.Interfaces.Repositories
{
    public interface ILoanApplicationRepository : IGenericRepository<LoanApplication, Guid>
    {
        Task<IEnumerable<LoanApplication>> GetByBranchNameAsync(string branchName);
        Task<IEnumerable<LoanApplication>> GetUserApplicationsAsync(Guid userId);
    }
}