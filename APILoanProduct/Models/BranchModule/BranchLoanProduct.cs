using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APILoanProduct.Models.BranchModule
{
    public class BranchLoanProduct
    {
        [Key]
        public int Id { get; set; }

        public int BranchId { get; set; }
        public Branch Branch { get; set; }

        public string ProductId { get; set; }
        public LoanProduct LoanProduct { get; set; }

        public DateTime AssignedDate { get; set; }
        public bool IsActive { get; set; }
    }


}
