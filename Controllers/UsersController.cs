using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebApiTravel.Models;
using WebApiTravel.Models.Dto;
using WebApiTravel.Repository.IRepository;

namespace WebApiTravel.Controllers
{
    [Route("api/TravelAPI")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly IUserRepository _userRepository;
        protected APIResponse _apiResponse;
        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            this._apiResponse = new();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO model)
        {
            var loginReponse = await _userRepository.Login(model);
            if (loginReponse.User == null || string.IsNullOrEmpty(loginReponse.Token))
            {
                _apiResponse.StatusCode = HttpStatusCode.BadRequest;
                _apiResponse.IsSuccess = false;
                _apiResponse.ErrorMessages.Add("Username or password is incorrect");

                return BadRequest(_apiResponse);
            }
            _apiResponse.StatusCode = HttpStatusCode.OK;
            _apiResponse.IsSuccess = true;
            _apiResponse.Result = loginReponse;

            return Ok(_apiResponse);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDTO model)
        {
            bool ifUserNameUnique = _userRepository.IsUniqueUser(model.UserName);
            if (!ifUserNameUnique) 
            {
                _apiResponse.StatusCode = HttpStatusCode.BadRequest;
                _apiResponse.IsSuccess = false;
                _apiResponse.ErrorMessages.Add("Username already exist!");

                return BadRequest(_apiResponse);
            }

            var user = await _userRepository.Register(model);
            if(user == null)
            {
                _apiResponse.StatusCode = HttpStatusCode.BadRequest;
                _apiResponse.IsSuccess = false;
                _apiResponse.ErrorMessages.Add("Error while registering!");

                return BadRequest(_apiResponse);
            }
            _apiResponse.StatusCode = HttpStatusCode.OK;
            _apiResponse.IsSuccess = true;
            return Ok(_apiResponse);
        }

    }
}
