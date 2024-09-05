using Demo_Grapesjs.Dtos;
using Demo_Grapesjs.Models;
using Demo_Grapesjs.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Demo_Grapesjs.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Microsoft.AspNetCore.Cors.EnableCors("CorsPolicy")]
    public class NameCardTemplateController : ControllerBase
    {
        private readonly INameCardTemplateService _nameCardTemplateService;
        public NameCardTemplateController(INameCardTemplateService nameCardTemplateService) { _nameCardTemplateService = nameCardTemplateService; }


        [HttpGet("")]
        [AllowAnonymous]
        public async Task<IActionResult> GetNameCardTemplates()
        {
            try
            {
                var nameCardTemplates = await _nameCardTemplateService.GetNameCardTemplates();
                var response = new ApiResponse<object>(nameCardTemplates, "OK", "Successfully");
                return Ok(response);
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<object>(null, "ERROR", ex.Message);
                return BadRequest(response);

            }
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetNameCardTemplateById([FromRoute] Guid id)
        {
            try
            {
                var nameCardTemplate = await _nameCardTemplateService.GetNameCardTemplateById(id);
                var response = new ApiResponse<object>(nameCardTemplate, "OK", "Successfully");
                return Ok(response);

            }
            catch (Exception ex)
            {
                var response = new ApiResponse<object>(null, "ERROR", ex.Message);
                return BadRequest(response);
            }
        }


        [HttpPost("")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateNameCardTemplate(NameCardTemplateDto nameCardTemplateDto)
        {
            try
            {
                var nameCardTemplate = await _nameCardTemplateService.CreateNameCardTemplate(nameCardTemplateDto);
                var response = new ApiResponse<object>(nameCardTemplate, "OK", "Successfully");
                return Ok(response);

            }
            catch (Exception ex)
            {
                var response = new ApiResponse<object>(null, "ERROR", ex.Message);
                return BadRequest(response);

            }
        }

        [HttpPatch("")]
        [AllowAnonymous]
        public async Task<IActionResult> UpdateNameCardTemplate(UpdateNameCardTemplateDto updateNameCardTemplateDto)
        {
            try
            {
                var nameCardTemplate = await _nameCardTemplateService.UpdateNameCardTemplate(updateNameCardTemplateDto);
                var response = new ApiResponse<object>(nameCardTemplate, "OK", "Successfully");
                return Ok(response);
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<object>(null, "ERROR", ex.Message);
                return BadRequest(response);
            }
        }
    }
}
