using Demo_Grapesjs.Dtos;
using Demo_Grapesjs.Entities;
using System.Collections;
using System.Collections.Generic;

namespace Demo_Grapesjs.Services.Interfaces
{
    public interface IImageService
    {
        Task<IEnumerable<Image>> GetImages();
        Task<Image> CreateImage(ImageDto image);
    }
}
