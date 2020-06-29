namespace CarMarket.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using AutoMapper;

    using CarMarket.Data.Models;
    using CarMarket.Services.Data.Exceptions;
    using CarMarket.Services.Data.Interfaces;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    public class UsersService : IUsersService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMapper mapper;

        public UsersService(UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            this.userManager = userManager;
            this.mapper = mapper;
        }

        public async Task<int> GetTotalCountAsync()
        {
            var count = await this.userManager.Users.AsNoTracking().CountAsync();
            return count;
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>()
        {
            var usersQuery = this.userManager.Users
                .AsNoTracking()
                .Include(u => u.Listings);

            var users = await this.mapper.ProjectTo<T>(usersQuery).ToListAsync();
            return users;
        }

        public async Task<T> GetUserInfoByIdAsync<T>(string userId)
        {
            var user = await this.userManager.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
            {
                throw new NotFoundException();
            }

            return this.mapper.Map<T>(user);
        }
    }
}
