using IRPr.Models;
using IRPr.Repositories.Interfaces;
using IRPr.Services.Interfaces;

namespace IRPr.Services
{
    public class ImageService : IImageService
    {
        private IRepositoryWrapper _repositoryWrapper;
        private IWebHostEnvironment _webHostEnvironment;

        public ImageService(IRepositoryWrapper repositoryWrapper, IWebHostEnvironment webHostEnvironment)
        {
            _repositoryWrapper = repositoryWrapper;
            _webHostEnvironment = webHostEnvironment;
        }

        public ICollection<Image> AddImage(ICollection<IFormFile> imageFiles)
        {
            List<Image> images = new List<Image>();
            if (imageFiles.Count != 0)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                foreach (IFormFile imageFile in imageFiles)
                {
                    Image image = new Image();
                    string fileName = Path.GetFileNameWithoutExtension(imageFile.FileName);
                    string extension = Path.GetExtension(imageFile.FileName);
                    image.name = fileName = fileName + DateTime.Now.ToString("_yyMMddHHmmss") + extension;
                    string path = Path.Combine(wwwRootPath + "/images", fileName);
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        imageFile.CopyTo(fileStream);
                    }
                    _repositoryWrapper.ImageRepository.Create(image);
                    images.Add(image);
                }
            }
            return images;
        }

        public ICollection<Image> GetAllImages()
        {
            var images = _repositoryWrapper.ImageRepository.FindAll().ToList();
            return images;
        }

    }
}
