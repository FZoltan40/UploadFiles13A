using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UploadFilesApi.Models;
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

        [HttpPost("upload")]
        public async Task<ActionResult> PostFiles(IFormFile formFile)
        {
            var up = await uploadFile.UpLoad(formFile);

            if (up != null) 
            {
                return Ok(up);
            }

            return BadRequest(up);
        }

        [HttpGet("download")]
        public async Task<ActionResult> GetFile(int id)
        {
            var file = await uploadFile.DownLoad(id) as FileUpload;

            if (file != null)
            {
                return File(file.FileData,file.ContentType,file.FilName);
            }

            return BadRequest(file);
        }
    }
}
