using APILoanProduct.DTO.LoanApplications;
using APILoanProduct.Models.LoanApplications;
using APILoanProduct.Interfaces.Services;

namespace APILoanProduct.Interfaces.Services
{
    public interface ILoanApplicantDetailsService : IGenericService<LoanApplicantDetails, Guid>
    {
    }
}