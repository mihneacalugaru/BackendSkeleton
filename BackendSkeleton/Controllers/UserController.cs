using BackendSkeleton.Controllers.User;
using BackendSkeleton.DataLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace BackendSkeleton.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationContext _db;

        public UserController(ApplicationContext db)
        {
            _db = db;
        }

        // TODO
        [HttpGet("DownloadProfilePicture")]
        public FileContentResult DownloadProfilePicture(int id)
        {
            return null;
        }

        // TODO
        [HttpPost("UploadProfilePicture/{id:int}")]
        public ActionResult UploadProfilePicture(int id)
        {
            try
            {
                // ...

                var picture = Request.Form.Files[0];

                // ...

                return Ok();
            }
            catch
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        // TODO
        [HttpGet("GetUserProfile/{id:int}")] // https://localhost:5000/api/user/GetUserProfile/45
        public ActionResult<UserDTO> GetUserProfile(int id)
        {
            return null;
        }

        // TODO
        [HttpPut("UpdateUser/{id:int}")]
        public ActionResult<UserDTO> UpdateUser(int id, [FromBody] object payload)
        {
            return null;
        }

        // TODO
        [HttpDelete("DeleteUser/{id:int}")]
        public ActionResult<bool> DeleteUser(int id)
        {
            return null;
        }

        // TODO
        [HttpGet("AllUsers")] // https://localhost:5000/api/user/AllUsers?pageSize=20&pageNumber=3&sortType=1
        public ActionResult<GetAllUsersResponse> GetAllUsers(int pageSize, int pageNumber, Enums.SortType sortType)
        {
            var allUsersQuery = _db.Users.AsQueryable();

            switch (sortType)
            {
                case Enums.SortType.FirstNameAscending:
                    allUsersQuery = allUsersQuery.OrderBy(x => x.FirstName);
                    break;
                case Enums.SortType.FirstNameDescending:
                    allUsersQuery = allUsersQuery.OrderByDescending(x => x.FirstName);
                    break;
                case Enums.SortType.LastNameAscending:
                    allUsersQuery = allUsersQuery.OrderBy(x => x.LastName);
                    break;
                case Enums.SortType.LastNameDescending:
                    allUsersQuery = allUsersQuery.OrderByDescending(x => x.LastName);
                    break;
                default: throw new ApplicationException("Unknown sort type");
            }

            var allUsers = allUsersQuery
                .Select(u => new UserDTO
                {
                    FullName = u.FirstName + ' ' + u.LastName,
                    Email = u.Email,
                })
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToList();

            return Ok(new GetAllUsersResponse
            {
                Users = allUsers,
            });
        }

        private string GetCurrentUserEmail()
        {
            return HttpContext.User.Claims
                .FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Email)?
                .Value;
        }
    }
}
