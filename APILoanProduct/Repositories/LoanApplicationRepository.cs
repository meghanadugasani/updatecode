using APILoanProduct.Data;
using APILoanProduct.Interfaces.Repositories;
using APILoanProduct.Models.LoanApplications;
using Microsoft.EntityFrameworkCore;

namespace APILoanProduct.Repositories
{
    public class LoanApplicationRepository : GenericRepository<LoanApplication, Guid>, ILoanApplicationRepository
    {
        public LoanApplicationRepository(Context context) : base(context)
        {
        }

        public async Task<IEnumerable<LoanApplication>> GetByBranchNameAsync(string branchName)
        {
            return await GetAsync(la => la.Branch!.BranchName == branchName, "Branch,User");
        }

        public async Task<IEnumerable<LoanApplication>> GetUserApplicationsAsync(Guid userId)
        {
            return await GetAsync(la => la.UserId == userId, "Branch,User");
        }
    }
}