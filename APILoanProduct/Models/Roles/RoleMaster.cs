using System.ComponentModel.DataAnnotations;

namespace APILoanProduct.Models.Roles
{
    public class RoleMaster
    {
        [Key]
        public int RoleId { get; set; }

        [Required]
        public string RoleName { get; set; } // "Admin", "BranchManager", "User"

        public ICollection<UserMaster> Users { get; set; }
    }

}
