using APILoanProduct.Models.BranchModule;
using APILoanProduct.Models.Roles;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APILoanProduct.Models.LoanApplication
{
    public class LoanApplication
    {
        [Key]
        public int ApplicationId { get; set; }

        // FK to User (Applicant)
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public UserMaster User { get; set; }

        // FK to Branch
        public int BranchId { get; set; }
        [ForeignKey("BranchId")]
        public Branch Branch { get; set; }

        // FK to Loan Product
        public string ProductId { get; set; }
        [ForeignKey("ProductId")]
        public LoanProduct LoanProduct { get; set; }

        [Required]
        public decimal RequestedAmount { get; set; }

        [Required]
        public int TenureMonths { get; set; }

        [Required]
        public string Purpose { get; set; }  // e.g., Education, Home, Car

        // Workflow
        public string Status { get; set; } = "Pending"; // Pending, UnderReview, Approved, Rejected
        public DateTime AppliedDate { get; set; } = DateTime.Now;
    }

}
