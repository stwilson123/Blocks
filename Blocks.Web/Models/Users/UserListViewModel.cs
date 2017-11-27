using System.Collections.Generic;
using Blocks.Roles.Dto;
using Blocks.Users.Dto;

namespace Blocks.Web.Models.Users
{
    public class UserListViewModel
    {
        public IReadOnlyList<UserDto> Users { get; set; }

        public IReadOnlyList<RoleDto> Roles { get; set; }
    }
}