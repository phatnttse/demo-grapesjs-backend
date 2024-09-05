using Demo_Grapesjs.Entities;

namespace Demo_Grapesjs.Services.Interfaces
{
    public interface IUserService
    {
        Task<User> CreateUser();
    }
}
