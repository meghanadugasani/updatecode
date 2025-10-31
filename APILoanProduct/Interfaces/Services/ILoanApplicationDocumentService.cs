using APILoanProduct.DTO.LoanApplications;
using APILoanProduct.Models.LoanApplications;

namespace APILoanProduct.Interfaces.Services
{
    public interface ILoanApplicationDocumentService : IGenericService<LoanApplicationDocuments, Guid>
    {
        Task<IEnumerable<LoanApplicationDocuments>> GetDocumentsByUserIdAsync(Guid userId);
    }
}