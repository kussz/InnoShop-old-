namespace UMS.Contracts;
public interface IRepositoryManager
{
    IRoleRepository Role { get; }
    IUserRepository User { get; }
    Task SaveAsync();
}