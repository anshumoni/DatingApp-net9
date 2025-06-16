using API.Data;
using API.Interfaces;
using App.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Entities;

public class UserRepository(DataContext context) : IUserRepository
{
    public async Task<AppUser?> GetUserByIdAsync(int id)
    {
        return await context.Users.
        FindAsync(id);
    }

    public async  Task<AppUser?> GetUserByUsernameAsync(string username)
    {
        return await context.Users.
        Include((x)=>x.Photos).
        SingleOrDefaultAsync(x => x.Username == username);
    }

    public async Task<IEnumerable<AppUser>> GetUsersAsync()
    {
        return await context.Users.
        Include((x)=>x.Photos).
        ToListAsync();
    }

    public async Task<bool> SaveAllAsync()
    {
        return await context.SaveChangesAsync() > 0;
    }

    
    public void Update(AppUser user)
    {
        context.Entry(user).State = EntityState.Modified;
    }
}