using UploadFilesApi.Models;
using UploadFilesApi.Services.InteFaces;

namespace UploadFilesApi.Services
{
    public class UploadFile : IUploadFile
    {
        private readonly FilestoreContext _storeContext;

        public UploadFile(FilestoreContext storeContext)
        {
            _storeContext = storeContext;
        }

        public Task<object> DownLoad(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<object> UpLoad(IFormFile formFile)
        {
            try
            {
                if (formFile != null || formFile.Length != 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await formFile.CopyToAsync(memoryStream);

                        var file = new FileUpload
                        {
                            FileData = memoryStream.ToArray(),
                            FilName = formFile.Name,
                            ContentType = formFile.ContentType
                        };

                        await _storeContext.Files.AddAsync(file);
                        await _storeContext.SaveChangesAsync();

                        return new { message = "Sikeres tárolás."};
                    }
                }

                return new { message = "Sikertelen tárolás." };
            }
            catch (Exception ex)
            {
                return new { message = ex.Message };
            }
        }
    }
}
