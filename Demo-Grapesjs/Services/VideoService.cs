using AutoMapper;
using Demo_Grapesjs.Entities;
using Demo_Grapesjs.Repositories;
using Demo_Grapesjs.Services.Interfaces;

namespace Demo_Grapesjs.Services
{
    public class VideoService : IVideoService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public VideoService(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment hostingEnvironment)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Video> CreateVideo(Video video)
        {
            await _unitOfWork.GetRepository<Video>().CreateAsync(video);
            await _unitOfWork.SaveChangesAsync();
            return video;
        }

        public Task<IEnumerable<Video>> GetVideos()
        {
            var response = _unitOfWork.GetRepository<Video>().GetAllAsync();
            return response;
        }
    }
}
