using Identity.Application.Contracts.Repositories;
using Identity.Domain.Entities;
using Identity.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Identity.Infrastructure.Repository
{
    internal class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly ContextPostgreSQL _contextPostgreSQL;

        public RefreshTokenRepository(ContextPostgreSQL contextPostgreSQL)
        {
            _contextPostgreSQL = contextPostgreSQL;
        }

        public async Task<RefreshToken> CreateAsync(string userId, string ipAddress)
        {
            var token = new RefreshToken
            {
                Token = Guid.NewGuid().ToString(),
                UserId = userId,
                ExpiryDate = DateTime.UtcNow.AddDays(7),
                IsRevoked = false,
                IsUsed = false,
                CreatedAt = DateTime.UtcNow,
                CreatedByIp = ipAddress
            };
            var result = await _contextPostgreSQL.RefreshTokens.AddAsync(token);
            if(result!=null)
            {
                await _contextPostgreSQL.SaveChangesAsync();
                return token;
            }
            return null;
        }

        public async Task<RefreshToken> GetByTokenAsync(string token)
        {
            return await _contextPostgreSQL.RefreshTokens.FirstOrDefaultAsync(rt => rt.Token == token);
        }

        public async Task<bool> RevokeAllTokensByUserIdAsync(string userId)
        {
            var tokens = await _contextPostgreSQL.RefreshTokens.Where(rt => rt.UserId == userId).ToListAsync();
            foreach (var token in tokens)
            {
                token.IsRevoked = true;
            }
            _contextPostgreSQL.RefreshTokens.UpdateRange(tokens);
            await _contextPostgreSQL.SaveChangesAsync();
            return true;
        }

        public async Task UpdateAsync(RefreshToken refreshToken)
        {
            _contextPostgreSQL.RefreshTokens.Update(refreshToken);
            await _contextPostgreSQL.SaveChangesAsync();
        }
    }
}
