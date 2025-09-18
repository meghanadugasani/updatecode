using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APILoanProduct.Models
{
    public class RepaymentDetails
    {
        [Key]
        public string RepaymentId { get; set; }

        public string Frequency { get; set; }
        public int MinimumTenure { get; set; }
        public int MaximumTenure { get; set; }
        public int GracePeriodDays { get; set; }

        public string ProductId { get; set; }
        [ForeignKey("ProductId")]
        public LoanProduct LoanProduct { get; set; }

    }
}
