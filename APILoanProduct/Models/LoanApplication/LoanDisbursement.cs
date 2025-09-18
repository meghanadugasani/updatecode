using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APILoanProduct.Models.LoanApplication
{
    public class LoanDisbursement
    {
        [Key]
        public int DisbursementId { get; set; }

        public int ApplicationId { get; set; }
        [ForeignKey("ApplicationId")]
        public LoanApplication LoanApplication { get; set; }

        public DateTime DisbursementDate { get; set; }
        public decimal ApprovedAmount { get; set; }
        public string DisbursementAccount { get; set; }
    }

}
