using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using App.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class Seed
{
    public static async Task SeedUser(DataContext context){

        if (await context.Users.AnyAsync()) return;

        var userData = await File.ReadAllTextAsync("Data/UserSeedData.json");
        
        var option = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        var users = JsonSerializer.Deserialize<List<AppUser>>(userData, option);
        if (users == null) return;
        foreach (var user in users)
        {
            using var hmac = new HMACSHA512();
            user.Username = user.Username.ToLower();
            user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("12345"));
            user.PasswordSalt = hmac.Key;

            context.Users.Add(user);
        }

        await context.SaveChangesAsync();
    }
}