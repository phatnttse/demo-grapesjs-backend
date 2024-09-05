using Demo_Grapesjs.Entities;

namespace Demo_Grapesjs.Services.Interfaces
{
    public interface IVideoService
    {
        Task<Video> CreateVideo(Video video);
        Task<IEnumerable<Video>> GetVideos();
    }
}
