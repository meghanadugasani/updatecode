using APILoanProduct.DTO.LoanApplications;
using APILoanProduct.Models.LoanApplications;

namespace APILoanProduct.Interfaces.Services
{
    public interface ILoanDisbursementService : IGenericService<LoanDisbursement, Guid>
    {
        Task<IEnumerable<LoanDisbursement>> GetDisbursementsByDateAsync(DateTime disbursementDate);
        Task<IEnumerable<LoanDisbursement>> GetDisbursementsByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<IEnumerable<LoanDisbursement>> GetDisbursementsByMaxAmountAsync(decimal maxAmount);
        Task<IEnumerable<LoanDisbursement>> GetDisbursementsByAmountRangeAsync(decimal minAmount, decimal maxAmount);
    }
}