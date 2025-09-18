using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APILoanProduct.Models
{
    public class InterestRate
    {
        [Key] 
        public string InterestId {  get; set; }

        public string Interesttype { get; set; }

        public decimal interestRate { get; set; }

        public double ProcessingFee { get; set; }

        public double Penaltyrate { get; set; }

        public string ProductId { get; set; }
        [ForeignKey("ProductId")]
        public LoanProduct LoanProduct { get; set; }

    }
}
