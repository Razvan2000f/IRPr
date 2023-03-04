namespace IRPr.Repositories.Interfaces
{
    public interface IRepositoryWrapper
    {
        IProductRepository productRepository { get; }
        IImageRepository ImageRepository { get; }
        void Save();
    }
}
