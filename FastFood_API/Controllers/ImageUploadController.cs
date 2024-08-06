using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace FastFood_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageUploadController : ControllerBase
    {
        [HttpPost("upload-product-image")]
        public IActionResult Upload()
        {
            var file = Request.Form.Files[0];
            var folderName = Path.Combine("images", "products");
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", folderName);

            if (file.Length > 0)
            {
                var fileName = ContentDispositionHeaderValue.Parse
                                (file.ContentDisposition).FileName!.Trim('"');
                var fullPath = Path.Combine(pathToSave, fileName);
                var dbPath = Path.Combine(folderName, fileName);

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                return Ok(dbPath);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
