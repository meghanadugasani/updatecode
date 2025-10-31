using System.ComponentModel.DataAnnotations;

namespace APILoanProduct.DTO.LoanApplications
{
    public class FileUploadDto
    {
        [Required]
        public IFormFile File { get; set; } = default!;
        
        [Required, MaxLength(200)]
        public string DocumentType { get; set; } = default!;
        
        public Guid ApplicationId { get; set; }
    }

    public class FileUploadResponseDto
    {
        public string FilePath { get; set; } = default!;
        public string FileName { get; set; } = default!;
        public long FileSize { get; set; }
        public string DocumentType { get; set; } = default!;
    }
}