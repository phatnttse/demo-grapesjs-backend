using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Demo_Grapesjs.Dtos;
using Demo_Grapesjs.Models;
using Demo_Grapesjs.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Demo_Grapesjs.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Microsoft.AspNetCore.Cors.EnableCors("CorsPolicy")]
    public class GrapesjsController : ControllerBase
    {
        private readonly Cloudinary _cloudinary;
        private readonly IImageService _imageService;
        private readonly IVideoService _videoService;
        private readonly IWebHostEnvironment _webHostEnvironment;


        public GrapesjsController(IOptions<CloudinarySettings> config, IImageService imageService, IVideoService videoService, IWebHostEnvironment webHostEnvironment)
        {
            var account = new Account(
               config.Value.CloudName,
               config.Value.ApiKey,
               config.Value.ApiSecret
               );
            _cloudinary = new Cloudinary(account);
            _imageService = imageService;
            _videoService = videoService;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet("assets")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAssets()
        {
            try
            {
                var assets = await _imageService.GetImages();
                return Ok(assets);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }
        [HttpGet("videos")]
        [AllowAnonymous]
        public async Task<IActionResult> GetVideos()
        {
            try
            {
                // Fetch videos from the database
                var videos = await _videoService.GetVideos();

                // Format the videos to the desired structure
                var videoList = videos.Select(video => new
                {
                    id = video.Id,
                    poster = video.Poster,
                    source = video.Source,
                    height = video.Height,
                    width = video.Width,
                    snippet = new
                    {
                        publishedAt = video.CreatedAt, 
                        title = video.Title, 
                        description = video.Description, 
                        thumbnails = new
                        {
                            @default = new
                            {
                                url = video.Poster, 
                                width = 120, 
                                height = 90
                            }
                        }
                    }
                }).ToList();

                var response = new
                {
                    page = 1, 
                    items = videoList,
                    pageInfo = new
                    {
                        totalResults = videoList.Count,
                        resultsPerPage = videoList.Count
                    }
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost("upload-cloudinary")]
        [AllowAnonymous]
        public async Task<IActionResult> UploadFileCloudinary([FromForm] IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                {
                    return BadRequest("No file uploaded.");
                }

                await using var stream = file.OpenReadStream();
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, stream),
                    Transformation = new Transformation().Height(250).Width(350).Crop("fill")
                };

                var uploadResult = await _cloudinary.UploadAsync(uploadParams);
                if (uploadResult.Error != null)
                {
                    throw new Exception(uploadResult.Error.Message);
                }

                ImageDto imageDto = new ImageDto { Src = uploadResult.SecureUrl.ToString(), Type = "image", Width = "350", Height = "250" };
                var response = await _imageService.CreateImage(imageDto);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("upload")]
        [AllowAnonymous]
        public async Task<IActionResult> UploadFile([FromForm] IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            try
            {
                // Tạo đường dẫn lưu trữ tệp
                var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", file.FileName);

                // Đảm bảo thư mục tồn tại
                var directory = Path.GetDirectoryName(filePath);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory!);
                }

                // Lưu tệp vào thư mục wwwroot/uploads
                await using var stream = new FileStream(filePath, FileMode.Create);
                await file.CopyToAsync(stream);

                var request = HttpContext.Request;
                var baseUrl = $"{request.Scheme}://{request.Host}";
                var iamgePath = $"{baseUrl}/images/{file.FileName}";

                ImageDto imageDto = new ImageDto { Src = iamgePath, Type = "image", Width = "350", Height = "250" };
                var response = await _imageService.CreateImage(imageDto);

                return Ok(response);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("upload-video")]
        [AllowAnonymous]
        public async Task<IActionResult> UploadVideo(
               [FromForm] IFormFile videoFile,
               [FromForm] string title,
               [FromForm] string description,
               [FromForm] string poster)
        {
            try
            {
                if (videoFile == null || videoFile.Length == 0)
                {
                    return BadRequest("No file uploaded.");
                }

                // Tạo đường dẫn tới thư mục wwwroot/videos
                var uploadsFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "videos");

                // Tạo thư mục nếu chưa tồn tại
                if (!Directory.Exists(uploadsFolderPath))
                {
                    Directory.CreateDirectory(uploadsFolderPath);
                }

                // Tạo đường dẫn đầy đủ tới file
                var filePath = Path.Combine(uploadsFolderPath, videoFile.FileName);

                // Lưu file vào thư mục
                await using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await videoFile.CopyToAsync(fileStream);
                }

                var request = HttpContext.Request;
                var baseUrl = $"{request.Scheme}://{request.Host}";
                var videoPath = $"{baseUrl}/videos/{videoFile.FileName}";

                Entities.Video video = new Entities.Video
                {
                    Source = videoPath,
                    Poster = poster, 
                    Width = 400, 
                    Height = 700,
                    Title = title,
                    Description = description
                };


                var newVideo = await _videoService.CreateVideo(video);

                return Ok(newVideo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
