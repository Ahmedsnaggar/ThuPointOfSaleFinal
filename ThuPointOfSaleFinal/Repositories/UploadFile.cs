using ThuPointOfSaleFinal.App.Interfaces;

namespace ThuPointOfSaleFinal.App.Repositories
{
    public class UploadFile : IUploadFile
    {
        private IWebHostEnvironment _environment;

        public UploadFile(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public async Task<string> UploadFileAsync(string filePath, IFormFile file)
        {
            string UploadFolder = _environment.WebRootPath + filePath;
            if (!Directory.Exists(UploadFolder))
            {
                Directory.CreateDirectory(UploadFolder);
            }
            string UniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
            string FullPath = Path.Combine(UploadFolder, UniqueFileName);
            using (var stream = new FileStream(FullPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
                stream.Dispose();
            }
            return Path.Combine(filePath,  UniqueFileName);
        }
    }
}
