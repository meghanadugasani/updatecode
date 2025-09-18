using APILoanProduct.Models.Roles;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APILoanProduct.Models.LoanApplication
{
    public class LoanApplicationReview
    {
        [Key]
        public int ReviewId { get; set; }

        public int ApplicationId { get; set; }
        [ForeignKey("ApplicationId")]
        public LoanApplication LoanApplication { get; set; }

        // Manager reviewing
        public int ManagerUserId { get; set; }
        [ForeignKey("ManagerUserId")]
        public UserMaster BranchManager { get; set; }

        public DateTime ReviewDate { get; set; } = DateTime.Now;

        public string ReviewStatus { get; set; } // Approved, Rejected, NeedsMoreInfo
        public string Remarks { get; set; }
    }



}
