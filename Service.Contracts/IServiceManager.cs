namespace UMS.Service.Contracts
{
    public interface IServiceManager
    {
        IRoleService RoleService { get; }
        IUserService UserService { get; }

    }
}
