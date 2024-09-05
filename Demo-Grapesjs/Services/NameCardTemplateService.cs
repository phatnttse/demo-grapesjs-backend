using AutoMapper;
using Demo_Grapesjs.Dtos;
using Demo_Grapesjs.Entities;
using Demo_Grapesjs.Repositories;
using Demo_Grapesjs.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using System.Globalization;
using System.Text;

namespace Demo_Grapesjs.Services
{
    public class NameCardTemplateService : INameCardTemplateService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public NameCardTemplateService(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment hostingEnvironment)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _hostingEnvironment = hostingEnvironment;

        }
        public async Task<NameCardTemplate> CreateNameCardTemplate(NameCardTemplateDto nameCardTemplateDto)
        {
            if (nameCardTemplateDto == null) throw new ArgumentNullException(nameof(nameCardTemplateDto));

            var nameCardTemplateDirectory = Path.Combine(_hostingEnvironment.WebRootPath, "files");
            if (!Directory.Exists(nameCardTemplateDirectory))
            {
                Directory.CreateDirectory(nameCardTemplateDirectory);
            }

            // Normalize the name by converting to lowercase, removing spaces, and adding a random 8-digit number
            var normalizedName = NormalizeName(nameCardTemplateDto.Name);
            var randomSuffix = new Random().Next(10000000, 99999999); // Generate an 8-digit random number
            var uniqueName = $"{normalizedName}-{randomSuffix}";

            // Create the HTML file name with the unique name
            var fileName = $"{uniqueName}.html";
            var filePath = Path.Combine(nameCardTemplateDirectory, fileName);

            // Save HTML content to file
            await File.WriteAllTextAsync(filePath, nameCardTemplateDto.Content);

            NameCardTemplate nameCardTemplate = _mapper.Map<NameCardTemplate>(nameCardTemplateDto);
            nameCardTemplate.Url = $"/files/{fileName}"; // Save relative path

            NameCardTemplate newTemplate = await _unitOfWork.GetRepository<NameCardTemplate>().CreateAsync(nameCardTemplate);
            await _unitOfWork.SaveChangesAsync();
            return newTemplate;
        }

        // Helper method to normalize the name by removing accents, converting to lowercase, and replacing spaces with hyphens
        private string NormalizeName(string name)
        {
            string normalizedString = name.ToLowerInvariant().Trim();

            // Replace spaces with hyphens
            normalizedString = normalizedString.Replace(" ", "-");

            // Optionally, you can remove accents or any special characters
            normalizedString = RemoveDiacritics(normalizedString);

            return normalizedString;
        }

        // Helper method to remove diacritics (accents) from characters
        private string RemoveDiacritics(string text)
        {
            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }


        public async Task<IEnumerable<NameCardTemplate>> GetNameCardTemplates()
        {
            return await _unitOfWork.GetRepository<NameCardTemplate>().GetAllAsync();
        }

        public async Task<NameCardTemplate> GetNameCardTemplateById(Guid id)
        {
            NameCardTemplate existingTemplate = await _unitOfWork.GetRepository<NameCardTemplate>().GetByIdAsync(id) ?? throw new Exception("Template not found");
            return existingTemplate;
        }

        public Task DeleteNameCardTemplate(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<NameCardTemplate> UpdateNameCardTemplate(UpdateNameCardTemplateDto updateNameCardTemplateDto)
        {
            // Lấy trang hiện tại từ cơ sở dữ liệu
            NameCardTemplate existingTemplate = await _unitOfWork.GetRepository<NameCardTemplate>().GetByIdAsync(updateNameCardTemplateDto.Id)
                ?? throw new Exception("Template not found");

            // Lấy đường dẫn file từ trường Content 
            string filePath = Path.Combine(_hostingEnvironment.WebRootPath, existingTemplate.Url.TrimStart('/'));

            // Kiểm tra xem file có tồn tại hay không
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"File not found at path: {filePath}");
            }

            // Ghi đè nội dung mới vào file
            await File.WriteAllTextAsync(filePath, updateNameCardTemplateDto.Content);

            existingTemplate.Name = updateNameCardTemplateDto.Name;
            existingTemplate.Thumbnail = updateNameCardTemplateDto.Thumbnail;

            // Lưu thay đổi vào cơ sở dữ liệu
            await _unitOfWork.SaveChangesAsync();

            return existingTemplate;
        }

    }
}
