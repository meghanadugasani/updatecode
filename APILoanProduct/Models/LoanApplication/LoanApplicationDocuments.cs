using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APILoanProduct.Models.LoanApplication
{
    public class LoanApplicationDocuments
    {
        [Key]
        public int DocumentId { get; set; }

        public int ApplicationId { get; set; }
        [ForeignKey("ApplicationId")]
        public LoanApplication LoanApplication { get; set; }

        [Required]
        public string DocumentType { get; set; } // ID Proof, Address Proof, Salary Slip

        [Required]
        public string FilePath { get; set; } // location of uploaded file
    }

}
