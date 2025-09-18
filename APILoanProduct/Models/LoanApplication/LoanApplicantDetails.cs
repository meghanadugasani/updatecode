using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APILoanProduct.Models.LoanApplication
{
    public class LoanApplicantDetails
    {
        [Key]
        public int ApplicantDetailsId { get; set; }

        // FK to LoanApplication
        public int ApplicationId { get; set; }
        [ForeignKey("ApplicationId")]
        public LoanApplication LoanApplication { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string EmploymentStatus { get; set; } // Salaried, Self-employed, Student

        [Required]
        public decimal AnnualIncome { get; set; }

    }

}
