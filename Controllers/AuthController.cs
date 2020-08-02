using Microsoft.AspNetCore.Mvc;
using WebAPI_RPG.Data;
using WebAPI_RPG.Models;
using AutoMapper;
using System.Threading.Tasks;
using WebAPI_RPG.DTOs.User;

namespace WebAPI_RPG.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepo;
        private readonly IMapper _mapper;
        public AuthController(IAuthRepository authRepo, IMapper mapper)
        {
            _mapper = mapper;
            _authRepo = authRepo;
        }
        [HttpPost("Register")]
        public async Task<ActionResult> Register(UserRegDTO regUserDTO)
        {
            ServiceWrapper<int> wrapper = await _authRepo.Register(
                new User {Name = regUserDTO.Username}, regUserDTO.Password); 

            if (!wrapper.DidSend)
            {
                return Ok(wrapper); 
            }
            return BadRequest(wrapper); 
        }
       [HttpPost("Login")] 
       public async Task<ActionResult> Login(UserLoginDTO userLoginDTO)
        {
            ServiceWrapper<string> wrapper = await _authRepo.Login(userLoginDTO.Name, userLoginDTO.Password); 
            
            if (!wrapper.DidSend)
            {
                return Ok(wrapper); 
            }
            return BadRequest(wrapper);
        }
    }
}