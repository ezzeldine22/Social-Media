using API.Domain.Entites;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUserManagerService
    {
        public Task<User> findByEmailAsync(string email);
        public Task<User> findByIdAsync(string userId);
        public Task<IdentityResult> createAsync(User user , string password);
        public Task<bool> checkPasswordAsync(User user , string password);
        public Task<IdentityResult> addClaimsAsync(User user , IEnumerable<Claim> claims);
        public Task<IdentityResult> updateAsync(User user);
    }
}
