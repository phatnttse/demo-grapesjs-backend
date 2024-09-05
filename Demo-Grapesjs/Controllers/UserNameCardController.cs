using Microsoft.AspNetCore.Mvc;


namespace Demo_Grapesjs.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserNameCardController : ControllerBase
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public UserNameCardController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public async Task<IActionResult> GetNameCard(string fullName)
        {
            try
            {
                // Xây dựng đường dẫn đến file HTML
                var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "files", "ng-tran-tan-phat-92249246.html");

                // Kiểm tra xem file có tồn tại không
                if (!System.IO.File.Exists(filePath))
                {
                    return NotFound("File not found.");
                }

                // Đọc nội dung file
                var htmlContent = await System.IO.File.ReadAllTextAsync(filePath);

                // Thay thế biến {{fullName}} bằng giá trị thực
                htmlContent = htmlContent.Replace("{{fullName}}", fullName);

                // Trả nội dung HTML đã được thay thế
                return Content(htmlContent, "text/html");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
