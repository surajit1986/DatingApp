using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api.Data
{
    public class SeedData
    {
        public static async Task SeedUsers(DataContext context){

            if(await context.Users.AnyAsync()) return;

             var userData = System.IO.File.ReadAllText("Data/UserSeedData.Json");
             var users = JsonSerializer.Deserialize<List<AppUser>>(userData);

             foreach(var user in users){

                using var hmac = new HMACSHA512();
                user.UserName = user.UserName.ToLower();
                user.passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("password"));
                user.passwordSalt = hmac.Key;
                context.Users.Add(user);
            }

            await context.SaveChangesAsync();
            
        }
    }
}