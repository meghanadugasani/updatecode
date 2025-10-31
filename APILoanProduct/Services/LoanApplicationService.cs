using APILoanProduct.DTO.LoanApplications;
using APILoanProduct.Interfaces.Repositories;
using APILoanProduct.Interfaces.Services;
using APILoanProduct.Models.LoanApplications;
using APILoanProduct.Repositories;

namespace APILoanProduct.Services
{
    public class LoanApplicationService : GenericService<LoanApplication, Guid>, ILoanApplicationService
    {
        private readonly ILoanApplicationRepository _loanApplicationRepository;
        private readonly IGenericRepository<LoanApplicationReview, Guid> _reviewRepository;
        private readonly IGenericRepository<APILoanProduct.Models.Roles.UserMaster, Guid> _userRepository;
        private readonly IGenericRepository<LoanApplicationDocuments, Guid> _documentRepository;
        private readonly IGenericRepository<LoanDisbursement, Guid> _disbursementRepository;

        public LoanApplicationService(ILoanApplicationRepository repository, 
            IGenericRepository<LoanApplicationReview, Guid> reviewRepository, 
            IGenericRepository<APILoanProduct.Models.Roles.UserMaster, Guid> userRepository,
            IGenericRepository<LoanApplicationDocuments, Guid> documentRepository,
            IGenericRepository<LoanDisbursement, Guid> disbursementRepository) 
            : base(repository)
        {
            _loanApplicationRepository = repository;
            _reviewRepository = reviewRepository;
            _userRepository = userRepository;
            _documentRepository = documentRepository;
            _disbursementRepository = disbursementRepository;
        }

        public override async Task<LoanApplication> CreateAsync<TDto>(TDto dto)
        {
            var createDto = dto as LoanApplicationCreateDto;
            var entity = new LoanApplication
            {
                UserId = createDto!.UserId,
                BranchId = createDto.BranchId,
                RequestedAmount = createDto.RequestedAmount,
                TenureYears = createDto.TenureYears,
                Purpose = createDto.Purpose
            };
            return await _repository.AddAsync(entity);
        }

        public override async Task<LoanApplication> UpdateAsync<TDto>(Guid id, TDto dto)
        {
            var updateDto = dto as LoanApplicationUpdateDto;
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) throw new KeyNotFoundException();

            if (updateDto!.RequestedAmount.HasValue)
                entity.RequestedAmount = updateDto.RequestedAmount.Value;
            if (updateDto.TenureYears.HasValue)
                entity.TenureYears = updateDto.TenureYears.Value;
            if (!string.IsNullOrEmpty(updateDto.Purpose))
                entity.Purpose = updateDto.Purpose;

            return await _repository.UpdateAsync(id, entity);
        }

        public async Task<IEnumerable<LoanApplicationReadDto>> GetByBranchNameAsync(string branchName)
        {
            var applications = await _loanApplicationRepository.GetByBranchNameAsync(branchName);
            return applications.Select(a => new LoanApplicationReadDto
            {
                ApplicationId = a.ApplicationId,
                UserId = a.UserId,
                BranchId = a.BranchId,
                RequestedAmount = a.RequestedAmount,
                TenureYears = a.TenureYears,
                Purpose = a.Purpose,
                AppliedDate = a.AppliedDate
            });
        }

        public async Task<LoanApplicationReadDto> ApplyForLoanAsync(LoanApplicationCreateDto dto)
        {
            var application = await CreateAsync(dto);
            return new LoanApplicationReadDto
            {
                ApplicationId = application.ApplicationId,
                UserId = application.UserId,
                BranchId = application.BranchId,
                RequestedAmount = application.RequestedAmount,
                TenureYears = application.TenureYears,
                Purpose = application.Purpose,
                AppliedDate = application.AppliedDate
            };
        }

        public async Task<IEnumerable<LoanApplicationReadDto>> GetUserApplicationsAsync(Guid userId)
        {
            var allApplications = await _repository.GetAllAsync();
            var userApplications = allApplications.Where(a => a.UserId == userId);
            return userApplications.Select(a => new LoanApplicationReadDto
            {
                ApplicationId = a.ApplicationId,
                UserId = a.UserId,
                BranchId = a.BranchId,
                RequestedAmount = a.RequestedAmount,
                TenureYears = a.TenureYears,
                Purpose = a.Purpose,
                AppliedDate = a.AppliedDate
            });
        }

        public async Task<LoanApplicationReadDto> UpdateStatusAsync(Guid id, StatusUpdateDto dto)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) throw new KeyNotFoundException();

            // Get any user from the database
            var users = await _userRepository.GetAllAsync();
            var user = users.FirstOrDefault();
            
            if (user == null)
                throw new InvalidOperationException("No user found in the system");

            var review = new LoanApplicationReview
            {
                ApplicationId = id,
                ManagerUserId = user.UserId,
                Status = (APILoanProduct.Models.Roles.Loanapplicationstatus)dto.Status,
                Remarks = dto.Remarks
            };

            await _reviewRepository.AddAsync(review);
            return new LoanApplicationReadDto
            {
                ApplicationId = entity.ApplicationId,
                UserId = entity.UserId,
                BranchId = entity.BranchId,
                RequestedAmount = entity.RequestedAmount,
                TenureYears = entity.TenureYears,
                Purpose = entity.Purpose,
                AppliedDate = entity.AppliedDate
            };
        }

        public async Task<IEnumerable<LoanApplicationFullDto>> GetAllFullApplicationsAsync()
        {
            var applications = await _repository.GetAllAsync();
            var reviews = await _reviewRepository.GetAllAsync();
            var documents = await _documentRepository.GetAllAsync();
            var disbursements = await _disbursementRepository.GetAllAsync();

            return applications.Select(app =>
            {
                var review = reviews.FirstOrDefault(r => r.ApplicationId == app.ApplicationId);
                var appDocuments = documents.Where(d => d.ApplicationId == app.ApplicationId).ToList();
                var disbursement = disbursements.FirstOrDefault(d => d.ApplicationId == app.ApplicationId);

                return new LoanApplicationFullDto
                {
                    ApplicationId = app.ApplicationId,
                    UserId = app.UserId,
                    BranchId = app.BranchId,
                    RequestedAmount = app.RequestedAmount,
                    TenureYears = app.TenureYears,
                    Purpose = app.Purpose,
                    AppliedDate = app.AppliedDate,
                    Status = review?.Status != null ? (LoanapplicationstatusDto)review.Status : LoanapplicationstatusDto.Pending,
                    ReviewRemarks = review?.Remarks,
                    ReviewDate = review?.ReviewDate,
                    Documents = appDocuments.Select(d => new LoanApplicationDocumentDto
                    {
                        DocumentId = d.DocumentId,
                        DocumentType = d.DocumentType,
                        DocumentPath = d.FilePath,
                        UploadedDate = DateTime.Now
                    }).ToList(),
                    DisbursedAmount = disbursement?.ApprovedAmount ?? 0,
                    DisbursementDate = disbursement?.DisbursementDate
                };
            });
        }
    }
}