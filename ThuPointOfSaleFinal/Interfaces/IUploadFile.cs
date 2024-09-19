namespace ThuPointOfSaleFinal.App.Interfaces
{
    public interface IUploadFile
    {
        Task<string> UploadFileAsync(string filePath, IFormFile file);
    }
}
