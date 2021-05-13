using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Data;
using Api.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly DataContext _dbContext;
        public UsersController(DataContext dbContext)
        {
            _dbContext = dbContext;

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUserAsync(){

            return  await _dbContext.Users.ToListAsync();

        }

         [HttpGet("{id}")]
        public async Task<ActionResult<AppUser>> GetUserByIdAsync(int id){

            return  await _dbContext.Users.FindAsync(id);

        }

    }
}