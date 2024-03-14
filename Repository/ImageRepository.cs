
using System.Net.Http.Headers;
using clinic.Utils;

namespace clinic.Repository
{
    public class ImageRepository : IImageRepository
    {
        public string? UploadImage(IFormFile file)
        {
            try
    {
        var folderName = Path.Combine("Resources", "Images");
        var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
        if (file.Length > 0)
        {
            var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"').AppendTimeStamp();
            var fullPath = Path.Combine(pathToSave, fileName);
            var dbPath = Path.Combine(folderName, fileName);
            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                file.CopyTo(stream);
            }
            return  dbPath ;
        }
        else
        {
            return null;
        }
    }
    catch (Exception ex)
    {
        return null;
    }
        }
    }
}