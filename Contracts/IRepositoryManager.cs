namespace PMS.Contracts;
public interface IRepositoryManager
{
    IProductRepository Product { get; }
    Task SaveAsync();
}