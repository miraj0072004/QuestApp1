using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuestionRevisedApi.Data;
using QuestionRevisedApi.Dtos;
using QuestionRevisedApi.Helpers;
using QuestionRevisedApi.Models;

namespace QuestionRevisedApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly IUsersRepository _repo;
        private readonly IMapper _mapper;

        public UsersController(IUsersRepository repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;


        }
        [HttpGet]
        public async Task<IActionResult> GetUsers([FromQuery] UserParams userParams)
        {
            var users = await _repo.GetUsers(userParams);
            var usersToRetun = _mapper.Map<IEnumerable<UserForClientDto>>(users);
            return Ok(usersToRetun);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var userFromRepo = await _repo.GetUser(id);

            if (userFromRepo == null)
            {
                return BadRequest("User not found");
            }

            var userToReturn = _mapper.Map<UserForClientDto>(userFromRepo);
            return Ok(userToReturn);
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UserToUpdateDto userUpdateDto)
        {
            var userFromRepo = await _repo.GetUser(id);

            if(userFromRepo == null)
            {
                return BadRequest("The user doesn't exist");
            }

            
            //questionUpdateDto.Id = id;
            var user = _mapper.Map<User>(userUpdateDto);
            var updatedUser =await _repo.UpdateUser(user);
            var userToReturn = _mapper.Map<UserForClientDto>(updatedUser);

            return Ok(userToReturn);
        }

        [HttpPut("stats/{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UserStatsToUpdateDto userStatsToUpdate)
        {
            var userFromRepo = await _repo.GetUser(id);

            if(userFromRepo == null)
            {
                return BadRequest("The user doesn't exist");
            }


            //questionUpdateDto.Id = id;

            var userToUpdate = _mapper.Map<User>(userStatsToUpdate);
            userToUpdate.Id = id;
            userToUpdate.TotalGamesCount += 1;
            var updatedUser = await _repo.UpdateStats(userToUpdate);
            var userToReturn = _mapper.Map<UserForClientDto>(updatedUser);

            return Ok(userToReturn);

        }


    }
}