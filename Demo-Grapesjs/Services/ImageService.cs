using AutoMapper;
using Demo_Grapesjs.Dtos;
using Demo_Grapesjs.Entities;
using Demo_Grapesjs.Repositories;
using Demo_Grapesjs.Services.Interfaces;

namespace Demo_Grapesjs.Services
{
    public class ImageService : IImageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ImageService(IUnitOfWork unitOfWork, IMapper mapper) 
        {  
            _unitOfWork = unitOfWork; 
            _mapper = mapper; 
        }
        public async Task<Image> CreateImage(ImageDto imageDto)
        {
            if (imageDto == null) throw new ArgumentNullException(nameof(imageDto));
            
            Image image = _mapper.Map<Image>(imageDto);
            Image newImage = await _unitOfWork.GetRepository<Image>().CreateAsync(image);

            await _unitOfWork.SaveChangesAsync();

            return newImage;
        }

        public async Task<IEnumerable<Image>> GetImages()
        {
            return await _unitOfWork.GetRepository<Image>().GetAllAsync();
        }
    }
}
