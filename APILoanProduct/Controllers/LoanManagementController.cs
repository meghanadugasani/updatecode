using Microsoft.AspNetCore.Mvc;
using APILoanProduct.DTO.LoanApplications;
using APILoanProduct.Interfaces.Services;

namespace APILoanProduct.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanManagementController : ControllerBase
    {

        private readonly ILoanApplicationService _loanApplicationService;
        private readonly ILoanApplicantDetailsService _applicantDetailsService;
        private readonly ILoanApplicationDocumentService _documentService;
        private readonly ILoanApplicationReviewService _reviewService;
        private readonly ILoanDisbursementService _disbursementService;
        public LoanManagementController(
            ILoanApplicationService loanApplicationService,
            ILoanApplicantDetailsService applicantDetailsService,
            ILoanApplicationDocumentService documentService,
            ILoanApplicationReviewService reviewService,
            ILoanDisbursementService disbursementService)
        {
            _loanApplicationService = loanApplicationService;
            _applicantDetailsService = applicantDetailsService;
            _documentService = documentService;
            _reviewService = reviewService;
            _disbursementService = disbursementService;
        }

        [HttpGet("applications")]
        public async Task<IActionResult> GetAllApplications()
        {
            var entities = await _loanApplicationService.GetAllFullApplicationsAsync();
            return Ok(entities);
        }

        [HttpGet("applications/{id}")]
        public async Task<IActionResult> GetApplicationById(Guid id)
        {
            var entity = await _loanApplicationService.GetByIdAsync(id);
            if (entity == null) return NotFound();
            return Ok(entity);
        }

        [HttpPost("applications")]
        public async Task<IActionResult> CreateApplication([FromBody] LoanApplicationCreateDto dto)
        {
            var entity = await _loanApplicationService.CreateAsync(dto);
            return Ok(entity);
        }

        [HttpPut("applications/{id}")]
        public async Task<IActionResult> UpdateApplication(Guid id, [FromBody] LoanApplicationUpdateDto dto)
        {
            try
            {
                var entity = await _loanApplicationService.UpdateAsync(id, dto);
                return Ok(entity);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpGet("applications/branch/{branchName}")]
        public async Task<IActionResult> GetApplicationsByBranch(string branchName)
        {
            var result = await _loanApplicationService.GetByBranchNameAsync(branchName);
            return Ok(result);
        }

        [HttpGet("applications/user/{userId}")]
        public async Task<IActionResult> GetUserApplications(Guid userId)
        {
            var result = await _loanApplicationService.GetUserApplicationsAsync(userId);
            return Ok(result);
        }

        [HttpPut("applications/{id}/status")]
        public async Task<IActionResult> UpdateApplicationStatus(Guid id, [FromBody] StatusUpdateDto dto)
        {
            try
            {
                var result = await _loanApplicationService.UpdateStatusAsync(id, dto);
                return Ok(result);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpGet("documents")]
        public async Task<IActionResult> GetAllDocuments([FromQuery] Guid? userId = null, [FromQuery] string? role = null)
        {
            if (userId.HasValue && !string.IsNullOrEmpty(role))
            {
                var entities = await _documentService.GetDocumentsByRoleAsync(userId.Value, role);
                return Ok(entities);
            }
            var allEntities = await _documentService.GetAllAsync();
            return Ok(allEntities);
        }

        [HttpGet("documents/user/{userId}")]
        public async Task<IActionResult> GetDocumentsByUserId(Guid userId)
        {
            var entities = await _documentService.GetDocumentsByUserIdAsync(userId);
            return Ok(entities);
        }

        [HttpGet("documents/my-documents/{userId}/{role}")]
        public async Task<IActionResult> GetMyDocuments(Guid userId, string role)
        {
            var entities = await _documentService.GetDocumentsByRoleAsync(userId, role);
            return Ok(entities);
        }

        //[HttpPost("documents")]
        //public async Task<IActionResult> CreateDocument([FromBody] LoanApplicationDocumentsCreateDto dto)
        //{
        //    var entity = await _documentService.CreateAsync(dto);
        //    return Ok(entity);
        //}

        //[HttpPost("documents/upload")]
        //public async Task<IActionResult> UploadDocument([FromForm] IFormFile file, [FromForm] string documentType)
        //{
        //    try
        //    {
        //        if (file == null || file.Length == 0)
        //            return BadRequest("No file uploaded");

        //        // Validate file type
        //        var allowedExtensions = new[] { ".pdf", ".jpg", ".jpeg", ".png", ".doc", ".docx" };
        //        var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();
        //        if (!allowedExtensions.Contains(fileExtension))
        //            return BadRequest("Invalid file type. Allowed: PDF, JPG, JPEG, PNG, DOC, DOCX");

        //        // Create unique filename
        //        var fileName = $"{Guid.NewGuid()}{fileExtension}";
        //        var uploadsPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
                
        //        // Ensure directory exists
        //        if (!Directory.Exists(uploadsPath))
        //            Directory.CreateDirectory(uploadsPath);

        //        var filePath = Path.Combine(uploadsPath, fileName);
        //        var relativePath = $"/uploads/{fileName}";

        //        // Save file
        //        using (var stream = new FileStream(filePath, FileMode.Create))
        //        {
        //            await file.CopyToAsync(stream);
        //        }

        //        var response = new FileUploadResponseDto
        //        {
        //            FilePath = relativePath,
        //            FileName = file.FileName,
        //            FileSize = file.Length,
        //            DocumentType = documentType
        //        };

        //        return Ok(response);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Error uploading file: {ex.Message}");
        //    }
        //}

        [HttpPost("documents/upload-with-application")]
        public async Task<IActionResult> UploadDocumentWithApplication([FromForm] FileUploadDto dto)
        {
            try
            {
                if (dto.File == null || dto.File.Length == 0)
                    return BadRequest("No file uploaded");

                // Validate application exists
                var application = await _loanApplicationService.GetByIdAsync(dto.ApplicationId);
                if (application == null)
                    return NotFound("Application not found");

                // Validate file type
                var allowedExtensions = new[] { ".pdf", ".jpg", ".jpeg", ".png", ".doc", ".docx" };
                var fileExtension = Path.GetExtension(dto.File.FileName).ToLowerInvariant();
                if (!allowedExtensions.Contains(fileExtension))
                    return BadRequest("Invalid file type. Allowed: PDF, JPG, JPEG, PNG, DOC, DOCX");

                // Create unique filename
                var fileName = $"{dto.ApplicationId}_{dto.DocumentType}_{Guid.NewGuid()}{fileExtension}";
                var uploadsPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
                
                // Ensure directory exists
                if (!Directory.Exists(uploadsPath))
                    Directory.CreateDirectory(uploadsPath);

                var filePath = Path.Combine(uploadsPath, fileName);
                var relativePath = $"/uploads/{fileName}";

                // Save file
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await dto.File.CopyToAsync(stream);
                }

                // Create document record with application link
                var documentDto = new LoanApplicationDocumentsCreateDto
                {
                    DocumentType = dto.DocumentType,
                    FilePath = relativePath
                };

                var document = await _documentService.CreateWithApplicationAsync(dto.ApplicationId, documentDto);

                var response = new FileUploadResponseDto
                {
                    FilePath = relativePath,
                    FileName = dto.File.FileName,
                    FileSize = dto.File.Length,
                    DocumentType = dto.DocumentType
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error uploading file: {ex.Message}");
            }
        }



        [HttpGet("reviews")]
        public async Task<IActionResult> GetAllReviews()
        {
            var entities = await _reviewService.GetAllAsync();
            return Ok(entities);
        }

        [HttpGet("reviews/date/{reviewDate}")]
        public async Task<IActionResult> GetReviewsByDate(DateTime reviewDate)
        {
            var entities = await _reviewService.GetReviewsByDateAsync(reviewDate);
            return Ok(entities);
        }

        [HttpPost("reviews")]
        public async Task<IActionResult> CreateReview([FromBody] LoanApplicationReviewCreateDto dto)
        {
            var entity = await _reviewService.CreateAsync(dto);
            return Ok(entity);
        }

        [HttpGet("disbursements")]
        public async Task<IActionResult> GetAllDisbursements()
        {
            var entities = await _disbursementService.GetAllAsync();
            return Ok(entities);
        }

        [HttpGet("disbursements/date/{disbursementDate}")]
        public async Task<IActionResult> GetDisbursementsByDate(DateTime disbursementDate)
        {
            var entities = await _disbursementService.GetDisbursementsByDateAsync(disbursementDate);
            return Ok(entities);
        }

        [HttpGet("disbursements/date-range")]
        public async Task<IActionResult> GetDisbursementsByDateRange([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var entities = await _disbursementService.GetDisbursementsByDateRangeAsync(startDate, endDate);
            return Ok(entities);
        }

        [HttpGet("disbursements/amount/{maxAmount}")]
        public async Task<IActionResult> GetDisbursementsByMaxAmount(decimal maxAmount)
        {
            var entities = await _disbursementService.GetDisbursementsByMaxAmountAsync(maxAmount);
            return Ok(entities);
        }

        [HttpGet("disbursements/amount-range")]
        public async Task<IActionResult> GetDisbursementsByAmountRange([FromQuery] decimal minAmount, [FromQuery] decimal maxAmount)
        {
            var entities = await _disbursementService.GetDisbursementsByAmountRangeAsync(minAmount, maxAmount);
            return Ok(entities);
        }

        [HttpPost("disbursements")]
        public async Task<IActionResult> CreateDisbursement([FromBody] LoanDisbursementCreateDto dto)
        {
            var entity = await _disbursementService.CreateAsync(dto);
            return Ok(entity);
        }

        [HttpPost("applications/{applicationId}/disburse")]
        public async Task<IActionResult> DisburseApplication(Guid applicationId, [FromBody] LoanDisbursementCreateDto dto)
        {
            dto.ApplicationId = applicationId;
            var entity = await _disbursementService.CreateAsync(dto);
            return Ok(entity);
        }
    }
}