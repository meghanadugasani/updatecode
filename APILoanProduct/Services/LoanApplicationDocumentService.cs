using APILoanProduct.DTO.LoanApplications;
using APILoanProduct.Interfaces.Repositories;
using APILoanProduct.Interfaces.Services;
using APILoanProduct.Models.LoanApplications;

namespace APILoanProduct.Services
{
    public class LoanApplicationDocumentService : GenericService<LoanApplicationDocuments, Guid>, ILoanApplicationDocumentService
    {
        public LoanApplicationDocumentService(IGenericRepository<LoanApplicationDocuments, Guid> repository) : base(repository)
        {
        }

        public override async Task<LoanApplicationDocuments> CreateAsync<TDto>(TDto dto)
        {
            if (dto is LoanApplicationDocumentsCreateWithAppDto createWithAppDto)
            {
                var entity = new LoanApplicationDocuments
                {
                    ApplicationId = createWithAppDto.ApplicationId,
                    DocumentType = createWithAppDto.DocumentType,
                    FilePath = createWithAppDto.FilePath
                };
                return await _repository.AddAsync(entity);
            }
            else if (dto is LoanApplicationDocumentsCreateDto createDto)
            {
                var entity = new LoanApplicationDocuments
                {
                    ApplicationId = Guid.NewGuid(), // Default behavior for backward compatibility
                    DocumentType = createDto.DocumentType,
                    FilePath = createDto.FilePath
                };
                return await _repository.AddAsync(entity);
            }
            throw new ArgumentException("Invalid DTO type");
        }

        public async Task<LoanApplicationDocuments> CreateWithApplicationAsync(Guid applicationId, LoanApplicationDocumentsCreateDto dto)
        {
            var entity = new LoanApplicationDocuments
            {
                ApplicationId = applicationId,
                DocumentType = dto.DocumentType,
                FilePath = dto.FilePath
            };
            return await _repository.AddAsync(entity);
        }

        public override async Task<LoanApplicationDocuments> UpdateAsync<TDto>(Guid id, TDto dto)
        {
            var updateDto = dto as LoanApplicationDocumentsCreateDto;
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) throw new KeyNotFoundException();

            entity.DocumentType = updateDto!.DocumentType;
            entity.FilePath = updateDto.FilePath;

            return await _repository.UpdateAsync(id, entity);
        }

        public async Task<IEnumerable<LoanApplicationDocuments>> GetDocumentsByUserIdAsync(Guid userId)
        {
            var allDocuments = await _repository.GetAllAsync();
            return allDocuments.Where(d => d.ApplicationId == userId);
        }

        public async Task<IEnumerable<LoanApplicationDocuments>> GetDocumentsByRoleAsync(Guid userId, string role)
        {
            var allDocuments = await _repository.GetAllAsync();
            
            return role.ToLower() switch
            {
                "admin" => allDocuments, // Admin sees all documents
                "branch_manager" => allDocuments.Where(d => d.LoanApplication != null && 
                    d.LoanApplication.Branch != null && 
                    d.LoanApplication.Branch.ManagerUserId == userId),
                "user" => allDocuments.Where(d => d.LoanApplication != null && 
                    d.LoanApplication.UserId == userId),
                _ => Enumerable.Empty<LoanApplicationDocuments>()
            };
        }
    }
}