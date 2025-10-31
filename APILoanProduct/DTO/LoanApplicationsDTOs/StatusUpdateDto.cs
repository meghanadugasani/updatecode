using System.ComponentModel.DataAnnotations;

namespace APILoanProduct.DTO.LoanApplications
{
    public class StatusUpdateDto
    {
        [Required]
        public LoanapplicationstatusDto Status { get; set; }
        public string? Remarks { get; set; }
    }
}