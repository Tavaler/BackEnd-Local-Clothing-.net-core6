using Local.Interfaces;
using Local.Settings;

namespace Web_Project.Services
{
    public class UploadFileService : IUploadFileService
    {
        //private readonly IUploadFileService _uploadFileService;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IConfiguration configuration;

        public UploadFileService(IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.configuration = configuration;
                
        }


        public bool IsUpload(IFormFileCollection formFiles)
        {
            return formFiles != null || formFiles?.Count() > 0;

            
        }

        public async Task<List<string>> UploadImages(IFormFileCollection formFiles)
        {
            var listFileName = new List<string>();
            var uploadPath = $"{webHostEnvironment.WebRootPath}/images/";


            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            foreach (var formFile in formFiles)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(formFile.FileName);
                string fullName = uploadPath + fileName;

                using (var stream = File.Create(fullName))
                {
                    await formFile.CopyToAsync(stream);
                }
                listFileName.Add(fileName);
            }
            return listFileName;

        }

        public string Validation(IFormFileCollection formFiles)
        {
            foreach (var file in formFiles)
            {
                if (!ValidationExtension(file.FileName)) return "Invalid file  " + String.Join(", ", Constants.TypeImageForUploads); ;

                if (!ValidationSize(file.Length)) return "The file is too large " + (double)Constants.FileSizeLimit / 1024 / 1024 + "M";
            }
            return null!;
        }


        public Task DeleteImages(string fileName)
        {
            if (!string.IsNullOrEmpty(fileName))
            {
                var uploadPath = $"{webHostEnvironment.WebRootPath}/images/";
                string fullName = uploadPath + fileName;

                if (File.Exists(fullName)) File.Delete(fullName);

            }
            return Task.CompletedTask;
        }

        public bool ValidationExtension(string filename)
        {
            string[] permittedExtensions = Constants.TypeImageForUploads;
            string extension = Path.GetExtension(filename).ToLowerInvariant();

            if (string.IsNullOrEmpty(extension) || !permittedExtensions.Contains(extension)) return false;

            return true;

        }

        public bool ValidationSize(long fileSize) => 
            configuration.GetValue<long>("FileSizeLimit") > fileSize;

        

    }

}

