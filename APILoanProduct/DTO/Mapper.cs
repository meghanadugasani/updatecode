using APILoanProduct.DTO.LoanApplications;
using APILoanProduct.Models.LoanApplications;
using AutoMapper;

public class DtoMappingProfile : Profile
{
    public DtoMappingProfile()
    {
        // Loan applications only
        CreateMap<LoanApplication, LoanApplicationReadDto>();
        CreateMap<LoanApplicationCreateDto, LoanApplication>();
        CreateMap<LoanApplicationUpdateDto, LoanApplication>();

        CreateMap<LoanApplicationDocuments, LoanApplicationDocumentsReadDto>();
        CreateMap<LoanApplicationDocumentsCreateDto, LoanApplicationDocuments>();

        CreateMap<LoanApplicantDetails, LoanApplicantDetailsReadDto>();
        CreateMap<LoanApplicantDetailsCreateDto, LoanApplicantDetails>();

        CreateMap<LoanApplicationReview, LoanApplicationReviewReadDto>();
        CreateMap<LoanApplicationReviewCreateDto, LoanApplicationReview>();

        CreateMap<LoanDisbursement, LoanDisbursementReadDto>();
        CreateMap<LoanDisbursementCreateDto, LoanDisbursement>();
    }
}