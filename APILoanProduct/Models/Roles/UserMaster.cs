using APILoanProduct.Models.BranchModule;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APILoanProduct.Models.Roles
{
    public class UserMaster
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string PasswordHash { get; set; } // store hashed passwords

        [Required]
        public string Email { get; set; }

        public string Phone { get; set; }

        // FK to Role
        public int RoleId { get; set; }
        [ForeignKey("RoleId")]
        public RoleMaster Role { get; set; }
        public ICollection<Branch> ManagedBranches { get; set; }
    }

}
