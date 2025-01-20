using Identity.Domain.Entities;

namespace Identity.Application.Contracts.Repositories
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshToken> GetByTokenAsync(string token);
        Task<RefreshToken> CreateAsync(string userId, string ipAddress);
        Task UpdateAsync(RefreshToken refreshToken);
        Task<bool> RevokeAllTokensByUserIdAsync(string userId);
    }
}
