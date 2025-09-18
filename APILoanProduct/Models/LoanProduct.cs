using APILoanProduct.Models.BranchModule;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Contracts;

namespace APILoanProduct.Models
{
    public class LoanProduct
    {
        [Key]
        public string ProductId { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public bool status { get; set; }
        [Required]
        public DateTime CreatedDate {  get; set; }
        [Required]
        public string ProductDescription { get; set; }

        // foreign keys navigations

        public InterestRate AccountSettings { get; set; }
        public LoanAmountDetails LoanAmountDetails { get; set; }
        public InterestRate InterestRate { get; set; }
        public RepaymentDetails RepaymentDetails { get; set; }
        public LoanDocuments Documents { get; set; }

        public ProductAvailability ProductAvailability { get; set; }

        public ICollection<BranchLoanProduct> BranchLoanProducts { get; set; }



    }
}
