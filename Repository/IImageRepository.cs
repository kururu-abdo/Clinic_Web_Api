namespace clinic.Repository
{
    public interface IImageRepository
    {
        String? UploadImage(IFormFile file);
    }
}