using APILoanProduct.Models.Roles;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APILoanProduct.Models.BranchModule
{
    public class Branch
    {
        [Key]
        public int BranchId { get; set; }

        public string BranchName { get; set; }
        public string Location { get; set; }

        // Manager is a User with Role = BranchManager
        public int ManagerUserId { get; set; }
        [ForeignKey("ManagerUserId")]
        public UserMaster BranchManager { get; set; }

        public ICollection<BranchLoanProduct> BranchLoanProducts { get; set; }
    }


}
