namespace IRPr.Repositories.Interfaces
{
    public interface IRepositoryWrapper
    {
        IProductRepository productRepository { get; }
        IImageRepository ImageRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        void Save();
    }
}
