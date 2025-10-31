using APILoanProduct.DTO.LoanApplications;

namespace APILoanProduct.DTO.LoanApplications
{
    public class LoanApplicationFullDto
    {
        public Guid ApplicationId { get; set; }
        public Guid UserId { get; set; }
        public int BranchId { get; set; }
        public decimal RequestedAmount { get; set; }
        public int TenureYears { get; set; }
        public string? Purpose { get; set; }
        public DateTime AppliedDate { get; set; }
        
        // Review data (default pending)
        public LoanapplicationstatusDto Status { get; set; } = LoanapplicationstatusDto.Pending;
        public string? ReviewRemarks { get; set; }
        public DateTime? ReviewDate { get; set; }
        
        // Document data
        public List<LoanApplicationDocumentDto> Documents { get; set; } = new List<LoanApplicationDocumentDto>();
        
        // Disbursement data (default 0)
        public decimal DisbursedAmount { get; set; } = 0;
        public DateTime? DisbursementDate { get; set; }
    }

    public class LoanApplicationDocumentDto
    {
        public Guid DocumentId { get; set; }
        public string? DocumentType { get; set; }
        public string? DocumentPath { get; set; }
        public DateTime UploadedDate { get; set; }
    }
}