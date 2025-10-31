using APILoanProduct.DTO.LoanApplications;
using APILoanProduct.Models.LoanApplications;

namespace APILoanProduct.Interfaces.Services
{
    public interface ILoanApplicationDocumentService : IGenericService<LoanApplicationDocuments, Guid>
    {
        Task<IEnumerable<LoanApplicationDocuments>> GetDocumentsByUserIdAsync(Guid userId);
        Task<LoanApplicationDocuments> CreateWithApplicationAsync(Guid applicationId, LoanApplicationDocumentsCreateDto dto);
        Task<IEnumerable<LoanApplicationDocuments>> GetDocumentsByRoleAsync(Guid userId, string role);
    }
}