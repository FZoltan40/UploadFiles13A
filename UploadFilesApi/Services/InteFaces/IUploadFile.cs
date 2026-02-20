namespace UploadFilesApi.Services.InteFaces
{
    public interface IUploadFile
    {
        Task<object> UpLoad(IFormFile formFile);
        Task<object> DownLoad(int id);
    }
}
