using System.Collections.Generic;
using System.Threading.Tasks;
using Blocks.Core.Navigation.Models;
using Blocks.Framework.Security;
using Blocks.Framework.Security.Authorization.User;

namespace Blocks.Core.Navigation.Services
{
    public interface IUserNavigationManager
    {
        /// Gets a menu specialized for given user.
        /// </summary>
        /// <param name="menuName">Unique name of the menu</param>
        /// <param name="user">The user, or null for anonymous users</param>
        Task<UserNavigation> GetMenuAsync(string menuName, IUserIdentifier user);

        /// <summary>
        /// Gets all menus specialized for given user.
        /// </summary>
        /// <param name="user">User id or null for anonymous users</param>
        Task<IReadOnlyList<UserNavigation>> GetMenusAsync(IUserIdentifier user);
    }
}