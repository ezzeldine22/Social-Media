using API.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IJWTService
    {
        public Task<string> generateTokenAsync(User user, IEnumerable<Claim> claims);
        public Task<IEnumerable<Claim>> generateClaimsAsync(User user);
    }
}
