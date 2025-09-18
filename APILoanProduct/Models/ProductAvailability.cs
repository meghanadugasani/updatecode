using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APILoanProduct.Models
{
    public class ProductAvailability
    {
        [Key]
        public string AvailabilityId { get; set; }
        
        public string BranchId { get; set; }
        public string ProductAvailabilityTo { get; set; }

        public string ProductId { get; set; }
        [ForeignKey("ProductId")]
        public LoanProduct LoanProduct { get; set; }
    }
}
