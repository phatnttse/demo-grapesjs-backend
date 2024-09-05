using Demo_Grapesjs.Dtos;
using Demo_Grapesjs.Entities;

namespace Demo_Grapesjs.Services.Interfaces
{
    public interface INameCardTemplateService
    {
        Task<NameCardTemplate> CreateNameCardTemplate(NameCardTemplateDto nameCardTemplateDto);
        Task<IEnumerable<NameCardTemplate>> GetNameCardTemplates();
        Task<NameCardTemplate> GetNameCardTemplateById(Guid id);
        Task DeleteNameCardTemplate (Guid id);
        Task<NameCardTemplate> UpdateNameCardTemplate(UpdateNameCardTemplateDto updateNameCardTemplateDto);
    }
}
