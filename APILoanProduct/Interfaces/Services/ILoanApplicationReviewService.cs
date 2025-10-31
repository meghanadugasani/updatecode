using APILoanProduct.DTO.LoanApplications;
using APILoanProduct.Models.LoanApplications;

namespace APILoanProduct.Interfaces.Services
{
    public interface ILoanApplicationReviewService : IGenericService<LoanApplicationReview, Guid>
    {
        Task<IEnumerable<LoanApplicationReview>> GetReviewsByDateAsync(DateTime reviewDate);
    }
}