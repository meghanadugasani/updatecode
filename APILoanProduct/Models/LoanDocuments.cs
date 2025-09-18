using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APILoanProduct.Models
{
    public class LoanDocuments
    {
        [Key]
        public string DocumentId { get; set; }
        [Required]
        public string AddressProof { get; set; }
        [Required]
        public string IncomeProoof { get; set; }
        [Required]
        public string IdentityProof { get; set; }
        
        public string AdditionalDocuments { get; set; }


        public string ProductId { get; set; }
        [ForeignKey("ProductId")]
        public LoanProduct LoanProduct { get; set; }

    }
}
