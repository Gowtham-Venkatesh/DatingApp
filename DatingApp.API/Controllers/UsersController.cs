using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using DatingApp.API.Data;
using DatingApp.API.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IDatingRepository _repo;
        private readonly IMapper _mapper;
        public UsersController(IDatingRepository repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
        }

        [HttpGet]

        public async Task<IActionResult> GetUsers()
        {
            var users = await _repo.GetUsers();

            var userstoreturn = _mapper.Map<IEnumerable<UserForListDto>>(users);

            return Ok(userstoreturn);
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _repo.GetUser(id);

            var usertoreturn = _mapper.Map<UserForDetailedDto>(user);

            return Ok(usertoreturn);
        }

        [HttpPut("{id}")]

        public async Task<IActionResult> GetUser(int id, UserForUpdatedDto userForUpdatedDto)
        {
            if (id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var userfromrepo = await _repo.GetUser(id);

            _mapper.Map(userForUpdatedDto, userfromrepo);

            if(await _repo.SaveAll())
                return NoContent();

            throw new Exception($"Updating User {id} failed on save");
        }
    }
}