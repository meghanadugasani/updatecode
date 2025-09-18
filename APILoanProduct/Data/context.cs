using APILoanProduct.Models;
using APILoanProduct.Models.BranchModule;
using APILoanProduct.Models.LoanApplication;
using APILoanProduct.Models.Roles;
using Microsoft.EntityFrameworkCore;

namespace APILoanProduct.Data
{
    public class context : DbContext
    {
        public context() { }

        public context(DbContextOptions options) : base(options)
        {
        }

        //Users for roles 

        public DbSet<RoleMaster> roleMasters { get; set; }

        public DbSet<UserMaster> userMasters { get; set; }

        //User Module on Application process

        public DbSet<LoanApplication> loanApplications { get; set; }

        public DbSet<LoanApplicantDetails> LoanApplicantDetails { get; set; }

        public DbSet<LoanApplicationDocuments> LoanApplicationDocuments { get; set; }

        public DbSet<LoanApplicationReview> LoanApplicationReviews { get; set; }

        public DbSet<LoanDisbursement> loanDisbursements { get; set; }


        //For branch Module 

        public DbSet<Branch> Branches { get; set; }

        public DbSet<BranchLoanProduct> BranchLoans { get; set; }

        //For all the Admin module stuff
        public DbSet<LoanProduct> LoanProducts { get; set; }

        public DbSet<InterestRate> interestRates { get; set; }

        public DbSet<LoanAmountDetails> LoanAmountDetails { get; set; }

        public DbSet<LoanDocuments> LoanDocuments { get; set; }

        public DbSet<RepaymentDetails> RepaymentDetails { get; set; }

        public DbSet<ProductAvailability> ProductAvailability { get; set; }
        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlServer("data source = YASH_MALLADI; database = lp; integrated security = true; trustservercertificate = true;");


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<BranchLoanProduct>()
                .HasKey(p => new {p.ProductId, p.BranchId});

            modelBuilder.Entity<BranchLoanProduct>()
                .HasOne(e => e.LoanProduct)
                .WithMany(e => e.BranchLoanProducts)
                .HasForeignKey(e => e.ProductId)
                .OnDelete(DeleteBehavior.Restrict);          // Prevent cascade delete (safe for managers)


            modelBuilder.Entity<BranchLoanProduct>()
                .HasOne(e => e.Branch)
                .WithMany(e => e.BranchLoanProducts)
                .HasForeignKey(e => e.BranchId)
                .OnDelete(DeleteBehavior.Restrict);          // Prevent cascade delete (safe for managers)


            modelBuilder.Entity<Branch>()
                .HasOne(b => b.BranchManager)                // Branch → UserMaster
                .WithMany(u => u.ManagedBranches)            // UserMaster → Branch (reverse collection)
                .HasForeignKey(b => b.ManagerUserId)         // FK in Branch
                .OnDelete(DeleteBehavior.Restrict);          // Prevent cascade delete (safe for managers)


            //Loan application stuff


            modelBuilder.Entity<LoanApplication>()
                .HasOne(la => la.User)
                .WithMany() // user can have many loan applications
                .HasForeignKey(la => la.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<LoanApplication>()
                .HasOne(la => la.Branch)
                .WithMany() // branch can have many loan applications
                .HasForeignKey(la => la.BranchId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<LoanApplication>()
                .HasOne(la => la.LoanProduct)
                .WithMany() // product can be applied for many times
                .HasForeignKey(la => la.ProductId)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<LoanApplicantDetails>()
                .HasOne(ad => ad.LoanApplication)
                .WithOne() // if you add navigation LoanApplication.ApplicantDetails, replace WithOne(a => a.ApplicantDetails)
                .HasForeignKey<LoanApplicantDetails>(ad => ad.ApplicationId)
                .OnDelete(DeleteBehavior.Cascade);

            // ------------------------
            // LoanApplicationDocuments → LoanApplication (1-to-many)
            // ------------------------
            modelBuilder.Entity<LoanApplicationDocuments>()
                .HasOne(d => d.LoanApplication)
                .WithMany() // if LoanApplication.Documents exists → .WithMany(a => a.Documents)
                .HasForeignKey(d => d.ApplicationId)
                .OnDelete(DeleteBehavior.Cascade);

            // ------------------------
            // LoanApplicationReview → LoanApplication (1-to-many)
            // ------------------------
            modelBuilder.Entity<LoanApplicationReview>()
                .HasOne(r => r.LoanApplication)
                .WithMany() // if LoanApplication.Reviews exists → .WithMany(a => a.Reviews)
                .HasForeignKey(r => r.ApplicationId)
                .OnDelete(DeleteBehavior.Cascade);

            // LoanApplicationReview → UserMaster (BranchManager)
            modelBuilder.Entity<LoanApplicationReview>()
                .HasOne(r => r.BranchManager)
                .WithMany() // no navigation on UserMaster
                .HasForeignKey(r => r.ManagerUserId)
                .OnDelete(DeleteBehavior.Restrict);

            // ------------------------
            // LoanDisbursement → LoanApplication (1-to-1)
            // ------------------------
            modelBuilder.Entity<LoanDisbursement>()
                .HasOne(d => d.LoanApplication)
                .WithOne() // if LoanApplication.Disbursement exists → .WithOne(a => a.Disbursement)
                .HasForeignKey<LoanDisbursement>(d => d.ApplicationId)
                .OnDelete(DeleteBehavior.Cascade);
        



        // LoanAmountDetails one-to-one
        modelBuilder.Entity<LoanProduct>()
                .HasOne(lp => lp.LoanAmountDetails)
                .WithOne(l => l.LoanProduct)
                .HasForeignKey<LoanAmountDetails>(l => l.ProductId)
                .OnDelete(DeleteBehavior.Restrict);          // Prevent cascade delete (safe for managers)


            // InterestRate one-to-one
            modelBuilder.Entity<LoanProduct>()
                .HasOne(lp => lp.InterestRate)
                .WithOne(i => i.LoanProduct)
                .HasForeignKey<InterestRate>(i => i.ProductId)
                .OnDelete(DeleteBehavior.Restrict);          // Prevent cascade delete (safe for managers)


            // RepaymentDetails one-to-one
            modelBuilder.Entity<LoanProduct>()
                .HasOne(lp => lp.RepaymentDetails)
                .WithOne(r => r.LoanProduct)
                .HasForeignKey<RepaymentDetails>(r => r.ProductId)
                .OnDelete(DeleteBehavior.Restrict);          // Prevent cascade delete (safe for managers)


            // LoanDocuments one-to-one
            modelBuilder.Entity<LoanProduct>()
                .HasOne(lp => lp.Documents)
                .WithOne(d => d.LoanProduct)
                .HasForeignKey<LoanDocuments>(d => d.ProductId)
                .OnDelete(DeleteBehavior.Restrict);          // Prevent cascade delete (safe for managers)


            // ProdAvailability one-to-one
            modelBuilder.Entity<LoanProduct>()
                .HasOne(lp => lp.ProductAvailability)
                .WithOne(p => p.LoanProduct)
                .HasForeignKey<ProductAvailability>(p => p.ProductId)
                .OnDelete(DeleteBehavior.Restrict);          // Prevent cascade delete (safe for managers)



        }
    }
}
