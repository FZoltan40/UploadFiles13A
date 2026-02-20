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

        public async Task<object> DownLoad(int id)
        {
            try
            {
                var file = await _storeContext.Files.FindAsync(id);

                if (file != null) 
                { 
                    return file;
                }

                return new { message = "Sikertelen lekérés." };
            }
            catch (Exception ex)
            {
                return new { message = ex.Message };
            }
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
                            FilName = formFile.FileName,
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
