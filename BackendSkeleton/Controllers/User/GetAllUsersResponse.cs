using System.Collections.Generic;

namespace BackendSkeleton.Controllers.User
{
    public class GetAllUsersResponse
    {
        public List<UserDTO> Users { get; set; }
    }

    public class UserDTO
    {
        public string FullName { get; set; }

        public string Email { get; set; }
    }
}