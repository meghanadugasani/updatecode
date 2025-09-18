using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APILoanProduct.Models
{
    public class LoanAmountDetails
    {
        [Key]
        public string LoanAmountId { get; set; }

        public decimal minAmount { get; set; }
        public decimal maxAmount { get; set; }

        public decimal DefaultAmount { get; set; }

        public int Tranches { get; set; }


        public string ProductId { get; set; }
        [ForeignKey("ProductId")]
        public LoanProduct LoanProduct { get; set; }

    }
}
