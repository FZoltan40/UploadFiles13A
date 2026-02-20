using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UploadFilesApi.Services.InteFaces;

namespace UploadFilesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadFileController : ControllerBase
    {
        private readonly IUploadFile uploadFile;

        public UploadFileController(IUploadFile uploadFile)
        {
            this.uploadFile = uploadFile;
        }

        [HttpPost]
        public async Task<ActionResult> PostFiles(IFormFile formFile)
        {
            var up = await uploadFile.UpLoad(formFile);

            if (up != null) 
            {
                return Ok(up);
            }

            return BadRequest(up);
        }
    }
}
