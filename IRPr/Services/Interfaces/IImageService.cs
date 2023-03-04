using IRPr.Models;

namespace IRPr.Services.Interfaces
{
    public interface IImageService
    {
        ICollection<Image> AddImage(ICollection<IFormFile> imageFiles);
        ICollection<Image> GetAllImages();
    }
}
