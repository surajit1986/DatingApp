using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Data;
using Api.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{
   
    public class UsersController : BaseApiController
    {
        private readonly DataContext _dbContext;
        public UsersController(DataContext dbContext)
        {
            _dbContext = dbContext;

        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUserAsync(){

            return  await _dbContext.Users.ToListAsync();

        }

         [HttpGet("{id}")]
         [Authorize]
        public async Task<ActionResult<AppUser>> GetUserByIdAsync(int id){

            return  await _dbContext.Users.FindAsync(id);

        }

    }
}