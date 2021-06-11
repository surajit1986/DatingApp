using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Data;
using Api.DTOs;
using Api.Entities;
using Api.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{
    [Authorize]
    public class UsersController : BaseApiController
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UsersController(IUserRepository userRepository, IMapper mapper)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetUserAsync()
        {
            var users = await _userRepository.GetMembersAsync();
           
            return Ok(users);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MemberDto>> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            var userToReturn = _mapper.Map<MemberDto>(user);
            return userToReturn;

        }

        [HttpGet("{name}")]
        public async Task<ActionResult<MemberDto>> GetUserByNameAsync(string name)
        {
            var user = await _userRepository.GetMemberByNameAsync(name);
            return user;

        }

    }
}