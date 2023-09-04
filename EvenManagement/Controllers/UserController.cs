using AutoMapper;
using EvenManagement.Entities;
using EvenManagement.Requests.EventRequests;
using EvenManagement.Requests.UserRequests;
using EvenManagement.Responses.UserResponse;
using EvenManagement.Services;
using EvenManagement.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EvenManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUserServices _userSevices;
        private readonly IConfiguration _config;
        public UserController(IUserServices service, IMapper mapper, IConfiguration config)
        {
            _mapper = mapper;
            _userSevices = service;
            _config = config;
        }

        //Create User

        [HttpPost]
        public async Task<ActionResult<UserSucess>> AddUser(AddUser newUser)
        {
            var user = _mapper.Map<User>(newUser);
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
           // user.Role = "Admin";
            var res = await _userSevices.AddUserAsync(user);

            return CreatedAtAction(nameof(AddUser), new UserSucess(201, res));

        }
        //Login
        [HttpPost("login")]

        public async Task<ActionResult<string>> LoginUser(LoginUser logUser)
        {
            //check if user with that email exists

            var existingUser = await _userSevices.GetUserbyEmailasync(logUser.UserEmail);
            if (existingUser == null)
            {
                return NotFound("Invalid Credential");
            }
            // users exists

            var isPasswordValid = BCrypt.Net.BCrypt.Verify(logUser.Password, existingUser.Password);
            if (!isPasswordValid)
            {
                return NotFound("Invalid Credential");
            }

            //i provided the right credentials

            //create Token
            var token = CreateToken(existingUser);

            return Ok(token);
        }

        //Event Registration

        [HttpPut("RegisterEvent")]
        public async Task<ActionResult<UserSucess>> RegisterEvent(EventRegister eventRegister) 
        {
            var events = _mapper.Map<User>(eventRegister);
            var res = await _userSevices.RegisterEventAsync(eventRegister);
            return CreatedAtAction(nameof(RegisterEvent), new UserSucess(201, res));

        }

        //Geting all Users

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserResponse>>> GetAllUsersAsync()
        {
            var response = await _userSevices.GetAllUsersAsync();
            var users = _mapper.Map<IEnumerable<UserResponse>>(response);
            return Ok(users);
        }

        //Get a single user based on Id

        [HttpGet("{id}")]
        public async Task<ActionResult<UserResponse>> GetUser(Guid id)
        {
            var response = await _userSevices.GetUserAsync(id);
            var user = _mapper.Map<UserResponse>(response);
            return Ok(user);
        }

        //Update User

        [HttpPut("{id}")]
        public async Task<ActionResult<UserSucess>> UpdateUser(Guid id, AddUser UpdatedUser)
        {
            var response = await _userSevices.GetUserAsync(id);
            if (response == null)
            {
                return NotFound(new UserSucess(404, "User Does Not Exist"));
            }
            //update
            var updated = _mapper.Map(UpdatedUser, response);
            var res = await _userSevices.UpdateUserAsync(updated);
            return Ok(new UserSucess(204, res));
        }

        //User Deletion

        [HttpDelete("{id}")]
        public async Task<ActionResult<UserSucess>> DeleteUser(Guid id)
        {
            var response = await _userSevices.GetUserAsync(id);
            if (response == null)
            {
                return NotFound(new UserSucess(404, "User Does Not Exist"));
            }
            //delete

            var res = await _userSevices.DeleteUserAsync(response);
            return Ok(new UserSucess(204, res));
        }
        private string CreateToken(User user)
        {
            //key
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetValue<string>("TokenSecurity:SecretKey")));
            Console.WriteLine(key);
            //Signing Credentials
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            //payload-data

            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim("Names", user.Name));
            claims.Add(new Claim("Sub", user.UserId.ToString()));
            claims.Add(new Claim("Role", user.Role));

            //create Token 
            var tokenGenerated = new JwtSecurityToken(
                _config["TokenSecurity:Issuer"],
                _config["TokenSecurity:Audience"],
                signingCredentials: cred,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1)
                );

            var token = new JwtSecurityTokenHandler().WriteToken(tokenGenerated);
            return token;
        }

    }
}
