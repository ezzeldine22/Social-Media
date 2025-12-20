using API.Domain.Entites;
using Application.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity
{
    public class UserManagerService : IUserManagerService
    {
        private readonly UserManager<User> _userManager;

        public UserManagerService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        public Task<IdentityResult> addClaimsAsync(User user, IEnumerable<Claim> claims)
            => _userManager.AddClaimsAsync(user, claims);
      
        public Task<bool> checkPasswordAsync(User user, string password)
            => _userManager.CheckPasswordAsync(user, password);

        public Task<IdentityResult> createAsync(User user, string password)
            => _userManager.CreateAsync(user, password);
       
        public Task<User> findByEmailAsync(string email)
            => _userManager.FindByEmailAsync(email);

        public Task<User> findByIdAsync(string userId)
            =>_userManager.FindByIdAsync(userId);
      
        public Task<IdentityResult> updateAsync(User user)
            => _userManager.UpdateAsync(user);
       
    }
}
