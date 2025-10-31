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
            var createDto = dto as LoanApplicationDocumentsCreateDto;
            var entity = new LoanApplicationDocuments
            {
                ApplicationId = Guid.NewGuid(),
                DocumentType = createDto!.DocumentType,
                FilePath = createDto.FilePath
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
    }
}