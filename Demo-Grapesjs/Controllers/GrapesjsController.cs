using API.Core;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Demo_Grapesjs.Controllers
{
    [Route("api/v1/form/")]
    [ApiController]
    [Microsoft.AspNetCore.Cors.EnableCors("CorsPolicy")]
    public class GrapesjsController : ControllerBase
    {
        private readonly Cloudinary _cloudinary;

        public GrapesjsController(IOptions<CloudinarySettings> config)
        {
            var account = new Account(
               config.Value.CloudName,
               config.Value.ApiKey,
               config.Value.ApiSecret
               );
            _cloudinary = new Cloudinary(account);
        }

        [HttpPost("upload")]
        [AllowAnonymous]
        public async Task<IActionResult> UploadFile([FromForm] IFormFile file)
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

                var response = new { src = uploadResult.SecureUrl.ToString(), type = "image", height = 250, width = 350 };
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
